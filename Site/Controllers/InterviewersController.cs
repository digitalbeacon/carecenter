// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using DigitalBeacon.Model;
using DigitalBeacon.SiteBase.Controllers;
using DigitalBeacon.SiteBase.Model;
using DigitalBeacon.SiteBase.Web;
using DigitalBeacon.SiteBase.Web.Models;
using DigitalBeacon.Util;
using DigitalBeacon.CareCenter.Model;
using DigitalBeacon.CareCenter.Site.Models.Interviewers;

namespace DigitalBeacon.CareCenter.Site.Controllers
{
	[Authorization(Role.Administrator)]
	public class InterviewersController : EntityController<InterviewerEntity, EditModel>
	{
		static InterviewersController()
		{
			Mapper.CreateMap<InterviewerEntity, ListItem>();
			Mapper.CreateMap<InterviewerEntity, EditModel>();
			Mapper.CreateMap<EditModel, InterviewerEntity>()
				.ForMember(t => t.Address, o => o.Ignore());
		}

		public InterviewersController()
		{
			DefaultSort = new[] { new SortItem { Member = InterviewerEntity.LastNameProperty } };
		}

		protected override EditModel ConstructCreateModel()
		{
			var model = new EditModel();
			TryUpdateModel(model);
			ModelState.Clear();
			return model;
		}

		public override ActionResult Create(FormCollection form)
		{
			var retVal = base.Create(form);
			if ((GetParamAsString(Model.PropertyName(x => x.CreateForVisit)).ToBoolean() ?? false) && Entity != null)
			{
				return Json(new { Entity.Id, Entity.DisplayName }, JsonRequestBehavior.AllowGet);
			}
			return retVal;
		}

		protected override EditModel ConstructModel(InterviewerEntity entity)
		{
			return Mapper.Map<EditModel>(entity);
		}

		protected override ListModelBase ConstructListModel()
		{
			return new ListModel<ListItem>();
		}

		protected override IEnumerable ConstructGridItems(IEnumerable<InterviewerEntity> source, ListModelBase model)
		{
			return source.Select(x => Mapper.Map<ListItem>(x));
		}

		protected override void DeleteEntity(long id)
		{
			LookupService.DeleteEntity<InterviewerEntity>(id);
		}

		protected override string GetDescription(EditModel model)
		{
			return "{0} {1}".FormatWith(model.FirstName, model.LastName);
		}

		protected override string GetSortMember(string member)
		{
			if (member == "Inactive")
			{
				return InterviewerEntity.EnabledProperty;
			}
			return base.GetSortMember(member);
		}

		protected override IEnumerable<InterviewerEntity> GetEntities(SearchInfo<InterviewerEntity> searchInfo, ListModelBase model)
		{
			return LookupService.GetEntityList(searchInfo);
		}

		protected override InterviewerEntity GetEntity(long id)
		{
			return LookupService.Get<InterviewerEntity>(id);
		}

		protected override long GetEntityCount(SearchInfo<InterviewerEntity> searchInfo, ListModelBase model)
		{
			return LookupService.GetEntityCount(searchInfo);
		}

		protected override InterviewerEntity ConstructEntity(EditModel model)
		{
			var entity = base.ConstructEntity(model);
			if (entity.Address == null)
			{
				entity.Address = new AddressEntity();
			}
			Mapper.Map(model, entity);
			Mapper.Map(model.Address, entity.Address);
			return entity;
		}

		protected override InterviewerEntity SaveEntity(InterviewerEntity entity, EditModel model)
		{
			return LookupService.SaveEntity(entity);
		}
	}
}