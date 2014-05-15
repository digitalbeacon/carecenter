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
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using DigitalBeacon.Business;
using DigitalBeacon.Model;
using DigitalBeacon.SiteBase.Controllers;
using DigitalBeacon.SiteBase.Model;
using DigitalBeacon.SiteBase.Web;
using DigitalBeacon.SiteBase.Web.Models;
using DigitalBeacon.Util;
using DigitalBeacon.CareCenter.Business;
using DigitalBeacon.CareCenter.Model;
using DigitalBeacon.CareCenter.Site.Models.Visits;

namespace DigitalBeacon.CareCenter.Site.Controllers
{
	[Authorization(Role.Administrator)]
	public class VisitsController : EntityController<VisitEntity, EditModel>
	{
		private const string DeletePath = "/visits/delete";

		private static readonly IClientService ClientService = ServiceFactory.Instance.GetService<IClientService>();

		private string _description = String.Empty;

		static VisitsController()
		{
			Mapper.CreateMap<VisitEntity, ListItem>();
			Mapper.CreateMap<VisitEntity, EditModel>();
			Mapper.CreateMap<EditModel, VisitEntity>();
		}

		public VisitsController()
		{
			//RequireParentId = true;
			DefaultSort = new[] { new SortItem { Member = VisitEntity.DateProperty, SortDirection = ListSortDirection.Descending } };
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
			var model = new ListModel();

			if (ParentId.HasValue)
			{
				var years = ClientService.GetVisitYears(ParentId.Value);
				if (years.Count() > 1)
				{
					model.Year = GetParamAsString(model.PropertyName(x => x.Year)).ToInt32();
					model.ListItems[model.PropertyName(x => x.Year)] = new SelectList(years);
				}
			}
			else
			{
				model.LocationId = GetParamAsString(model.PropertyName(x => x.LocationId)).ToInt64();

				//var locationId = GetParamAsString(model.PropertyName(x => x.LocationId));
				//if (locationId == null)
				//{
				//    model.LocationId = GetCookieValue(SiteConstants.LocationIdCookieKey).ToInt64();
				//}
				//else
				//{
				//    model.LocationId = locationId.ToInt64();
				//};

				AddSelectList(model, model.PropertyName(x => x.LocationId),
					LookupService.GetNameList<LocationEntity>());

				model.InterviewerId = GetParamAsString(model.PropertyName(x => x.InterviewerId)).ToInt64();
				var interviewerSearch = new SearchInfo<InterviewerEntity>();
				interviewerSearch.AddSort(x => x.LastName);
				model.ListItems[model.PropertyName(x => x.InterviewerId)] = new SelectList(
					LookupService.GetEntityList(interviewerSearch),
					InterviewerEntity.IdProperty, InterviewerEntity.DisplayNameProperty);
			}

			model.CanDelete = PermissionService.HasPermissionToSitePath(CurrentUser, DeletePath);

			return model;
		}

		protected override EditModel PopulateSelectLists(EditModel model)
		{
			AddSelectList(model, model.PropertyName(x => x.LocationId),
				LookupService.GetNameList<LocationEntity>());
			var interviewerSearch = new SearchInfo<InterviewerEntity>();
			interviewerSearch.AddFilter(x => x.Enabled, true);
			interviewerSearch.AddSort(x => x.LastName);
			model.ListItems[model.PropertyName(x => x.InterviewerId)] = new SelectList(
				LookupService.GetEntityList(interviewerSearch),
				InterviewerEntity.IdProperty, InterviewerEntity.DisplayNameProperty);
			return model;
		}

		protected override string GetSortMember(string member)
		{
			if (member == "LocationName")
			{
				return GetPropertyName(VisitEntity.LocationProperty, LocationEntity.NameProperty);
			}
			if (member == "InterviewerDisplayName")
			{
				return GetPropertyName(VisitEntity.InterviewerProperty, InterviewerEntity.LastNameProperty);
			}
			return base.GetSortMember(member);
		}

		//protected override ListSortDirection GetSortDirection(string member, ListSortDirection sortDirection)
		//{
		//    if (member == "Age")
		//    {
		//        return sortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
		//    }
		//    return base.GetSortDirection(member, sortDirection);
		//}

		protected override string GetDescription(EditModel model)
		{
			if (_description.IsNullOrBlank() && model.ClientId > 0 && model.Date.HasValue)
			{
				_description = GetLocalizedText("Visits.Description.Format").FormatWith(
					ClientService.GetClient(model.ClientId).DisplayName, model.Date.Value.ToShortDateString());
			}
			return _description;
		}

		protected override SearchInfo<VisitEntity> PrepareSearchInfo(SearchInfo<VisitEntity> searchInfo, ListModelBase model)
		{
			if (ParentId.HasValue)
			{
				searchInfo.AddFilter(x => x.ClientId, ParentId.Value);
			}
			var listModel = (ListModel)model;
			if (listModel.LocationId.HasValue)
			{
				searchInfo.AddFilter(x => x.Location.Id, listModel.LocationId.Value);
			}
			if (listModel.InterviewerId.HasValue)
			{
				searchInfo.AddFilter(x => x.Interviewer.Id, listModel.InterviewerId.Value);
			}
			return base.PrepareSearchInfo(searchInfo, model);
		}

		protected override IEnumerable<VisitEntity> GetEntities(SearchInfo<VisitEntity> searchInfo, ListModelBase model)
		{
			return LookupService.GetEntityList(searchInfo);
		}

		protected override long GetEntityCount(SearchInfo<VisitEntity> searchInfo, ListModelBase model)
		{
			return LookupService.GetEntityCount(searchInfo);
		}

		protected override VisitEntity GetEntity(long id)
		{
			return LookupService.Get<VisitEntity>(id);
		}

		protected override EditModel ConstructCreateModel()
		{
			return new EditModel 
			{
				ClientId = ParentId.Value, 
				Date = DateTime.Today,
				LocationId = GetCookieValue(SiteConstants.LocationIdCookieKey).ToInt64(),
				InterviewerId = GetCookieValue(SiteConstants.InterviewerIdCookieKey).ToInt64() 
			};
		}

		//protected override EditModel ConstructUpdateModel(long id)
		//{
		//    var model = base.ConstructUpdateModel(id);
		//    model.ClientMode = GetParamAsString(model.PropertyName(x => x.ClientMode)).ToBoolean() ?? false;
		//    return model;
		//}

		protected override RouteValueDictionary ConstructRouteValuesForBulkCreate()
		{
			ParentId = Entity.ClientId;
			return base.ConstructRouteValuesForBulkCreate();
		}

		protected override VisitEntity SaveEntity(VisitEntity entity, EditModel model)
		{
			SetCookieValue(SiteConstants.LocationIdCookieKey, entity.Location.Id);
			if (entity.Interviewer != null)
			{
				SetCookieValue(SiteConstants.InterviewerIdCookieKey, entity.Interviewer.Id);
			}
			entity = LookupService.SaveEntity(entity);
			ClientService.UpdateClientLocation(entity.ClientId);
			return entity;
		}

		protected override void DeleteEntity(VisitEntity entity, EditModel model)
		{
			base.DeleteEntity(entity, model);
			ClientService.UpdateClientLocation(entity.ClientId);
		}

		protected override void DeleteEntity(long id)
		{
			LookupService.DeleteEntity<VisitEntity>(id);
		}

		protected override EditModel ConstructModel(VisitEntity entity)
		{
			var model = Mapper.Map<EditModel>(entity);
			model.CanDelete = PermissionService.HasPermissionToSitePath(CurrentUser, DeletePath);
			return model;
		}

		protected override VisitEntity ConstructEntity(EditModel model)
		{
			var entity = model.Id == 0 ? new VisitEntity { ClientId = model.ClientId } : GetEntity(model.Id);
			Mapper.Map(model, entity);
			entity.Location = LookupService.Get<LocationEntity>(model.LocationId.Value);
			if (model.InterviewerId.HasValue)
			{
				entity.Interviewer = LookupService.Get<InterviewerEntity>(model.InterviewerId.Value);
			}
			return entity;
		}

		protected override IEnumerable ConstructGridItems(IEnumerable<VisitEntity> source, ListModelBase model)
		{
			return source.Select(entity => 
			{
				var item = Mapper.Map<ListItem>(entity);
				if (model.ParentId == null)
				{
					item.ClientId = entity.ClientId;
					item.ClientName = ClientService.GetClient(entity.ClientId).DisplayName;
				}
				return item;
			});
		}

		#endregion
	}
}
