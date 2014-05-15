// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using DigitalBeacon.Model;
using DigitalBeacon.SiteBase.Model;
using DigitalBeacon.SiteBase.Web;
using DigitalBeacon.Util;
using DigitalBeacon.CareCenter.Model;
using DigitalBeacon.CareCenter.Site.Models.Reports;

namespace DigitalBeacon.CareCenter.Site.Controllers.Reports
{
	[Authorization(Role.Administrator, Exclude = "html")]
	public class ClientReportController : BaseReportController
	{
		static ClientReportController()
		{
			Mapper.CreateMap<ClientEntity, RowItem>();
		}

		//public ClientReportController()
		//{
		//    UseLandscapeOrientation = true;
		//}

		protected override object GetReportData(BaseReportModel model)
		{
			model.Guard("model");

			var data = new ClientReportInfo 
			{ 
				MinDate = model.MinDate,
				MaxDate = model.MaxDate,
				FromListView = model.FromListView,
				ShowDetails = model.ShowDetails
			};

			var search = new ClientSearchInfo { SearchText = model.SearchText, CommentTypeId = model.CommentTypeId, LocationId = model.LocationId };
			// semantics of model.Inactive value are non-standard
			if (model.Inactive == null)
			{
				search.Inactive = false;
			}
			else
			{
				search.Inactive = model.Inactive.Value ? true : (bool?)null;
			}

			if (model.Sort.HasText())
			{
				var parts = model.Sort.Split('-');
				if (parts.Length == 2)
				{
					search.AddSort(GetSortMember(parts[0]), GetSortDirection(parts[0], parts[1].EqualsIgnoreCase("desc") ? ListSortDirection.Descending : ListSortDirection.Ascending));
				}
			}
			else if (!model.FromListView)
			{
				search.AddSort(x => x.LastVisitDate, ListSortDirection.Descending);
			}

			//var lastWeekMinDate = DateTime.Today.AddDays(-7).StartOfWeek(DayOfWeek.Monday);
			//var lastWeekMaxDate = lastWeekMinDate.AddDays(6);

			data.Rows = ClientService.GetClients(search).Select(entity =>
			{
				var item = new RowItem(entity);
				Mapper.Map(entity, item);
				//item.LastWeek = ClientService.GetClientCount(new ClientSearchInfo { ClientId = entity.Id, MinDate = lastWeekMinDate, MaxDate = lastWeekMaxDate }) > 0;
				var flaggedCommentSearch = new SearchInfo<ClientCommentEntity>();
				flaggedCommentSearch.AddFilter(x => x.ClientId, entity.Id);
				flaggedCommentSearch.AddFilter(x => x.CommentType.Flagged, true);
				item.HasFlaggedComment = LookupService.GetEntityCount(flaggedCommentSearch) > 0;
				return item;
			}).ToList();

			return data;
		}
	}
}