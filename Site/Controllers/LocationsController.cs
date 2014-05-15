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
using DigitalBeacon.Util;
using DigitalBeacon.CareCenter.Model;
using DigitalBeacon.CareCenter.Site.Models.Locations;

namespace DigitalBeacon.CareCenter.Site.Controllers
{
	[Authorization(Role.Administrator)]
	public class LocationsController : NamedEntityController<LocationEntity, EditModel>
	{
		static LocationsController()
		{
			Mapper.CreateMap<LocationEntity, ListItem>();
			Mapper.CreateMap<LocationEntity, EditModel>();
			Mapper.CreateMap<EditModel, LocationEntity>()
				.ForMember(t => t.Address, o => o.Ignore());
		}

		public LocationsController()
		{
			DefaultSort = null;
		}

		#region Overrides of NamedEntityController<LocationEntity,EditModel>

		protected override string GetSortMember(string member)
		{
			if (member == "Inactive")
			{
				return LocationEntity.DisplayOrderProperty;
			}
			return base.GetSortMember(member);
		}

		protected override EditModel PopulateSelectLists(EditModel model)
		{
			AddSelectList(model, GetPropertyName(LocationEntity.AddressProperty, model.Address.PropertyName(m => m.StateId)), LookupService.GetNameList<StateEntity>());
			return model;
		}

		protected override ListModelBase ConstructListModel()
		{
			return new ListModel<ListItem>();
		}

		protected override IEnumerable ConstructGridItems(IEnumerable<LocationEntity> source, ListModelBase model)
		{
			return source.Select(x => Mapper.Map<ListItem>(x));
		}

		protected override EditModel ConstructModel(LocationEntity entity)
		{
			var model = Mapper.Map<EditModel>(entity);
			model.Inactive = entity.DisplayOrder == 0;
			return model;
		}

		protected override LocationEntity ConstructEntity(EditModel model)
		{
			var entity = base.ConstructEntity(model);
			if (entity.Address == null)
			{
				entity.Address = new AddressEntity();
			}
			Mapper.Map(model, entity);
			Mapper.Map(model.Address, entity.Address);
			if (model.DisplayOrder == null)
			{
				entity.DisplayOrder = 1;
			}
			return entity;
		}

		#endregion
	}
}