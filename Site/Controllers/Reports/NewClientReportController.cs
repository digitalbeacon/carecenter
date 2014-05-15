// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using System.Linq;
using AutoMapper;
using DigitalBeacon.SiteBase.Model;
using DigitalBeacon.SiteBase.Web;
using DigitalBeacon.Util;
using DigitalBeacon.CareCenter.Model;
using DigitalBeacon.CareCenter.Site.Models.Reports;
using System.Collections.Generic;

namespace DigitalBeacon.CareCenter.Site.Controllers.Reports
{
	[Authorization(Role.Administrator, Exclude = "html")]
	public class NewClientReportController : BaseReportController
	{
		protected override object GetReportData(BaseReportModel model)
		{
			model.Guard("model");

			var data = new NewClientReportInfo();

			data.MinDate = model.MinDate.Value;
			data.MaxDate = model.MaxDate.Value;
			var search = new ClientSearchInfo { VisitMinDate = model.MinDate, VisitMaxDate = model.MaxDate };
			if (model.LocationId.HasValue)
			{
				search.LocationId = model.LocationId.Value;
				var location = LookupService.Get<LocationEntity>(model.LocationId.Value);
				data.LocationName = location.Name;
			}
			search.AddFilter(x => x.Repeat, false);
			search.AddSort(x => x.Address.City);
			var list = new List<CountInfo>();
			foreach (var c in ClientService.GetClients(search))
			{
				var rowInfo = new CountInfo();
				rowInfo.Name = c.DisplayName;
				rowInfo.Description = "{0}, {1} {2}".FormatWith(c.Address.City, c.Address.State.HasValue ? LookupService.GetCode<StateEntity>((long)c.Address.State.Value) : String.Empty, c.Address.PostalCode);
				list.Add(rowInfo);
			}
			data.Rows = list.OrderBy(x => x.Description).ToList();
			return data;
		}
	}
}