// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Routing;
using AutoMapper;
using DigitalBeacon.Business;
using DigitalBeacon.Model;
using DigitalBeacon.SiteBase.Controllers;
using DigitalBeacon.SiteBase.Model;
using DigitalBeacon.SiteBase.Web;
using DigitalBeacon.SiteBase.Web.Models;
using DigitalBeacon.Util;
using DigitalBeacon.Web.Formatters;
using DigitalBeacon.CareCenter.Business;
using DigitalBeacon.CareCenter.Model;
using DigitalBeacon.CareCenter.Site.Models.Household;

namespace DigitalBeacon.CareCenter.Site.Controllers
{
	[Authorization(Role.Administrator)]
	public class HouseholdController : EntityController<HouseholdMemberEntity, EditModel>
	{
		private const string DeletePath = "/household/delete";

		private static readonly SsnFormatter SsnFormatter = new SsnFormatter();

		private static readonly IClientService ClientService = ServiceFactory.Instance.GetService<IClientService>();

		static HouseholdController()
		{
			Mapper.CreateMap<HouseholdMemberEntity, ListItem>();
			Mapper.CreateMap<HouseholdMemberEntity, EditModel>()
				.ForMember(t => t.Ssn, o => o.Ignore());
			Mapper.CreateMap<EditModel, HouseholdMemberEntity>()
				.ForMember(t => t.Address, o => o.Ignore())
				.ForMember(t => t.Ssn, o => o.Ignore());
		}

		public HouseholdController()
		{
			RequireParentId = true;
			DefaultSort = new[] { new SortItem { Member = HouseholdMemberEntity.AgeBasisProperty, SortDirection = ListSortDirection.Descending } };
		}

		#region EntityController

		protected override string GetIndexUrl(EditModel model)
		{
			return Url.Action(EditActionName, "clients", new { id = model.ClientId });
			//if (ControllerContext.IsChildAction)
			//{
			//    return Url.Action(EditActionName, "clients", new { id = model.ClientId });
			//}
			//return Url.Action("comments", "clients", new { id = model.ClientId });
		}

		protected override ListModelBase ConstructListModel()
		{
			return new ListModel { CanDelete = PermissionService.HasPermissionToSitePath(CurrentUser, DeletePath) };
		}

		protected override EditModel PopulateSelectLists(EditModel model)
		{
			return model;
		}

		protected override string GetSortMember(string member)
		{
			if (member == "Name")
			{
				return HouseholdMemberEntity.LastNameProperty;
			}
			if (member == "Age")
			{
				return HouseholdMemberEntity.AgeBasisProperty;
			}
			return base.GetSortMember(member);
		}

		protected override ListSortDirection GetSortDirection(string member, ListSortDirection sortDirection)
		{
			if (member == "Age")
			{
				return sortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
			}
			return base.GetSortDirection(member, sortDirection);
		}

		protected override string GetDescription(EditModel model)
		{
			return "{0}, {1}{2}".FormatWith(model.LastName, model.FirstName, model.MiddleName.HasText() ? " " + model.MiddleName : String.Empty);
		}

		protected override SearchInfo<HouseholdMemberEntity> PrepareSearchInfo(SearchInfo<HouseholdMemberEntity> searchInfo, ListModelBase model)
		{
			searchInfo.AddFilter(x => x.ClientId, ParentId.Value);
			return base.PrepareSearchInfo(searchInfo, model);
		}

		protected override IEnumerable<HouseholdMemberEntity> GetEntities(SearchInfo<HouseholdMemberEntity> searchInfo, ListModelBase model)
		{
			return LookupService.GetEntityList(CurrentAssociationId, searchInfo);
		}

		protected override long GetEntityCount(SearchInfo<HouseholdMemberEntity> searchInfo, ListModelBase model)
		{
			return LookupService.GetEntityCount(CurrentAssociationId, searchInfo);
		}

		protected override HouseholdMemberEntity GetEntity(long id)
		{
			return LookupService.Get<HouseholdMemberEntity>(id);
		}

		protected override EditModel ConstructCreateModel()
		{
			return new EditModel { ClientId = ParentId.Value };
		}

		protected override RouteValueDictionary ConstructRouteValuesForBulkCreate()
		{
			ParentId = Entity.ClientId;
			return base.ConstructRouteValuesForBulkCreate();
		}

		protected override HouseholdMemberEntity SaveEntity(HouseholdMemberEntity entity, EditModel model)
		{
			bool isNew = entity.IsNew;
			if (isNew)
			{
				entity.Enabled = true;
			}
			var retVal = LookupService.SaveEntity(CurrentAssociationId, entity);
			if (isNew)
			{
				ClientService.UpdateClientHouseholdCount(entity.ClientId);
			}
			return retVal;
		}

		protected override void DeleteEntity(HouseholdMemberEntity entity, EditModel model)
		{
			base.DeleteEntity(entity, model);
			ClientService.UpdateClientHouseholdCount(entity.ClientId);
		}

		protected override void DeleteEntity(long id)
		{
			LookupService.DeleteEntity<HouseholdMemberEntity>(id);
		}

		protected override EditModel ConstructModel(HouseholdMemberEntity entity)
		{
			var model = Mapper.Map<EditModel>(entity);
			model.Age = entity.DateOfBirth.HasValue ? entity.DateOfBirth.Value.Age() : (entity.AgeBasis.HasValue ? entity.AgeBasis.Value.Age() : (int?)null);
			model.CanDelete = PermissionService.HasPermissionToSitePath(CurrentUser, DeletePath);
			return model;
		}

		protected override HouseholdMemberEntity ConstructEntity(EditModel model)
		{
			var entity = model.Id == 0 ? new HouseholdMemberEntity { ClientId = model.ClientId } : GetEntity(model.Id);
			Mapper.Map(model, entity);
			if (SsnFormatter.Parse(model.Ssn).HasText())
			{
				entity.Ssn = model.Ssn;
				entity.Ssn4 = entity.LastFourSsn;
			}
			if (model.DateOfBirth.HasValue)
			{
				entity.AgeBasis = model.DateOfBirth;
			}
			else if (model.Age.HasValue)
			{
				if (entity.AgeBasis == null || entity.AgeBasis.Value.Age() != model.Age.Value)
				{
					entity.AgeBasis = DateTime.Today.AddYears(-model.Age.Value).AddDays(-182);
				}
			}
			else
			{
				entity.AgeBasis = null;
			}
			return entity;
		}

		protected override IEnumerable ConstructGridItems(IEnumerable<HouseholdMemberEntity> source, ListModelBase model)
		{
			return source.Select(entity => Mapper.Map<ListItem>(entity));
		}

		#endregion
	}
}
