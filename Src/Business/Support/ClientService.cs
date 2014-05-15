// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using System.Collections.Generic;
using System.ComponentModel;
using DigitalBeacon.Business;
using DigitalBeacon.Model;
using DigitalBeacon.SiteBase.Business;
using DigitalBeacon.SiteBase.Business.Support;
using DigitalBeacon.Util;
using DigitalBeacon.CareCenter.Data;
using DigitalBeacon.CareCenter.Model;

namespace DigitalBeacon.CareCenter.Business.Support
{
	public class ClientService : BaseService, IClientService
	{
		#region Private Members

		private static readonly IClientDao ClientDao = ServiceFactory.Instance.GetService<IClientDao>();
		private static readonly IFileService FileService = ServiceFactory.Instance.GetService<IFileService>();
		private static readonly IIdentityService IdentityService = ServiceFactory.Instance.GetService<IIdentityService>();
		private static readonly IPreferenceService PreferenceService = ServiceFactory.Instance.GetService<IPreferenceService>();

		#endregion

		#region IClientService Members

		public ClientEntity GetClient(long id)
		{
			return ClientDao.Fetch(id);
		}

		public ClientEntity GetClientByEncryptedSsn(string encryptedSsn)
		{
			return DataAdapter.Fetch<ClientEntity>(ClientEntity.EncryptedSsnProperty, encryptedSsn);
		}

		public IList<ClientEntity> GetClients(ClientSearchInfo searchInfo)
		{
			return ClientDao.FetchList(PrepareSearch(searchInfo));
		}

		public long GetClientCount(ClientSearchInfo searchInfo)
		{
			return ClientDao.FetchCount(PrepareSearch(searchInfo));
		}

		public ClientEntity SaveClient(ClientEntity client)
		{
			ValidateClient(client);
			long? orphanedPhotoId = null;
			if (!client.IsNew)
			{
				ClientDao.Evict(client);
				var existing = ClientDao.Fetch(client.Id);
				if (existing.Photo != null && (client.Photo == null || client.Photo.DataChanged))
				{
					orphanedPhotoId = existing.Photo.Id;
				}
			}
			if (client.Photo != null && client.Photo.DataChanged)
			{
				client.Photo = FileService.SaveFile(client.Photo, false);
			}
			var retVal = SaveWithAudit(client);
			if (orphanedPhotoId.HasValue)
			{
				FileService.DeleteFile(orphanedPhotoId.Value, false);
			}
			return retVal;
		}

		public void DeleteClient(long clientId)
		{
			DeleteClientPhoto(clientId);
			DeleteWithAudit<ClientEntity>(clientId);
		}

		public void DeleteClientPhoto(long clientId)
		{
			var client = ClientDao.Fetch(clientId);
			if (client != null && client.Photo != null)
			{
				var photo = client.Photo;
				client.Photo = null;
				client.PhotoWidth = null;
				client.PhotoHeight = null;
				FileService.DeleteFile(photo.Id, false);
			}
		}

		public void UpdateClientLocation(long clientId)
		{
			var client = ClientDao.Fetch(clientId);
			var search = new SearchInfo<VisitEntity> { PageSize = 1, Page = 1 };
			search.AddFilter(x => x.ClientId, clientId);
			search.AddSort(x => x.Date, ListSortDirection.Descending);
			var result = DataAdapter.FetchList(search);
			if (result.Count == 1)
			{
				if (client.Location == null || client.Location.Id != result[0].Location.Id || client.LastVisitDate != result[0].Date)
				{
					client.Location = result[0].Location;
					client.LastVisitDate = result[0].Date;
				}
				client.Repeat = result.Count > 1;
			}
			else
			{
				client.LastVisitDate = null;
				client.Repeat = false;
			}
		}

		public void UpdateClientHouseholdCount(long clientId)
		{
			var client = ClientDao.Fetch(clientId);
			client.HouseholdCount = client.HouseholdMembers.Count + 1;
		}

		public long SetDormantClientsToInactive()
		{
			long count = 0;
			var pref = PreferenceService.GetPreference(IdentityService.GetCurrentAssociationId(), CareCenterConstants.DaysBeforeInactiveKey);
			if (pref != null && pref.Value.ToInt32().HasValue && pref.Value.ToInt32().Value > 0)
			{
				var search = new ClientSearchInfo { Inactive = false };
				search.AddFilter(x => x.LastVisitDate, ComparisonOperator.Null).Grouping = 1;
				search.AddFilter(x => x.LastVisitDate, ComparisonOperator.LessThan, DateTime.Today.AddDays(-pref.Value.ToInt32().Value)).Grouping = 2;
				foreach (var c in ClientDao.FetchList(search))
				{
					c.Enabled = false;
					count++;
				}
			}
			return count;
		}

		public IList<int> GetVisitYears(long clientId)
		{
			return ClientDao.FetchVisitYears(clientId);
		}

		#endregion

		#region Private Methods

		private static SearchInfo<ClientEntity> PrepareSearch(SearchInfo<ClientEntity> searchInfo)
		{
			if (searchInfo.ApplyDefaultFilters)
			{
				//if (searchInfo.SearchText.HasText())
				//{
				//    searchInfo.AddFilter(x => x.FirstName, ComparisonOperator.Contains, searchInfo.SearchText).Grouping = 1;
				//    searchInfo.AddFilter(x => x.LastName, ComparisonOperator.Contains, searchInfo.SearchText).Grouping = 2;
				//    searchInfo.AddFilter(x => x.LastFourSsn, ComparisonOperator.Equal, searchInfo.SearchText).Grouping = 3;
				//}
				searchInfo.ApplyDefaultFilters = false;
			}
			return searchInfo;
		}

		private void ValidateClient(ClientEntity client)
		{
			var messages = new List<string>();
			if (client.Ssn.HasText() && client.Ssn != CareCenterConstants.DefaultSsn)
			{
				var c = DataAdapter.Fetch<ClientEntity>(ClientEntity.EncryptedSsnProperty, client.EncryptedSsn);
				if (c != null && c.Id != client.Id)
				{
					messages.Add(CareCenterConstants.DuplicateSsn);
					throw new ServiceValidationException(messages);
				}
			}
		}

		#endregion
	}
}
