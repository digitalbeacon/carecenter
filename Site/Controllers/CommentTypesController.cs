// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DigitalBeacon.SiteBase.Controllers;
using DigitalBeacon.SiteBase.Model;
using DigitalBeacon.SiteBase.Web;
using DigitalBeacon.SiteBase.Web.Models;
using DigitalBeacon.CareCenter.Model;
using DigitalBeacon.CareCenter.Site.Models.CommentTypes;

namespace DigitalBeacon.CareCenter.Site.Controllers
{
	[Authorization(Role.Administrator)]
	public class CommentTypesController : NamedEntityController<CommentTypeEntity, EditModel>
	{
		static CommentTypesController()
		{
			Mapper.CreateMap<CommentTypeEntity, ListItem>();
			Mapper.CreateMap<CommentTypeEntity, EditModel>();
			Mapper.CreateMap<EditModel, CommentTypeEntity>();
		}

		#region Overrides of NamedEntityController<CommentTypeEntity,EditModel>

		protected override ListModelBase ConstructListModel()
		{
			return new ListModel<ListItem>();
		}

		protected override IEnumerable ConstructGridItems(IEnumerable<CommentTypeEntity> source, ListModelBase model)
		{
			return source.Select(x => Mapper.Map<ListItem>(x));
		}

		protected override EditModel ConstructModel(CommentTypeEntity entity)
		{
			var model = Mapper.Map<EditModel>(entity);
			model.Inactive = entity.DisplayOrder == 0;
			return model;
		}

		protected override CommentTypeEntity ConstructEntity(EditModel model)
		{
			var entity = base.ConstructEntity(model);
			Mapper.Map(model, entity);
			if (model.DisplayOrder == null)
			{
				entity.DisplayOrder = 1;
			}
			return entity;
		}

		#endregion
	}
}