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
	public class ZipCodeReportController : BaseReportController
	{
		protected override object GetReportData(BaseReportModel model)
		{
			model.Guard("model");

			var data = new ZipCodeReportInfo();

			data.MinDate = model.MinDate.Value;
			data.MaxDate = model.MaxDate.Value;
			var search = new ClientSearchInfo { VisitMinDate = model.MinDate, VisitMaxDate = model.MaxDate };
			if (model.LocationId.HasValue)
			{
				search.LocationId = model.LocationId.Value;
				var location = LookupService.Get<LocationEntity>(model.LocationId.Value);
				data.LocationName = location.Name;
			}
			search.AddSort(x => x.Address.City);
			var map = new Dictionary<string, CountInfo>();
			foreach (var c in ClientService.GetClients(search))
			{
				var key = c.Address.PostalCode ?? String.Empty;
				CountInfo rowInfo;
				if (map.ContainsKey(key))
				{
					rowInfo = map[key];
				}
				else
				{
					rowInfo = new CountInfo();
					if (key.HasText())
					{
						rowInfo.Description = LookupService.GetByCode<PostalCodeEntity>(key).IfNotNull(x => x.ToString("{City}, {StateCode} {Code}")) ?? key;
					}
					else
					{
						rowInfo.Description = GetLocalizedText("Reports.ZipCode.NoZipCode.Label");
					}
					map[key] = rowInfo;
				}
				rowInfo.Families++;
				rowInfo.Total += c.HouseholdCount ?? 1;
				data.Totals.Families++;
				data.Totals.Total += c.HouseholdCount ?? 1;
				if (!c.Repeat)
				{
					rowInfo.NewFamilies++;
					rowInfo.NewTotal += c.HouseholdCount ?? 1;
					data.Totals.NewFamilies++;
					data.Totals.NewTotal += c.HouseholdCount ?? 1;
				}
			}
			data.Rows = map.Values.OrderBy(x => x.Description).ToList();
			return data;
		}
	}
}