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

namespace DigitalBeacon.CareCenter.Site.Controllers.Reports
{
	[Authorization(Role.Administrator, Exclude = "html")]
	public class FoodNetworkReportController : BaseReportController
	{
		public FoodNetworkReportController()
		{
			Mapper.CreateMap<LocationEntity, FoodNetworkReportInfo>();
		}

		protected override object GetReportData(BaseReportModel model)
		{
			model.Guard("model");

			var data = new FoodNetworkReportInfo();

			data.MinDate = model.MinDate.Value;
			data.MaxDate = model.MaxDate.Value;
			var location = LookupService.Get<LocationEntity>(model.LocationId.Value);
			Mapper.Map(location, data);
			if (location.Address.State.HasValue)
			{
				data.AddressState = LookupService.GetCode<StateEntity>((long)location.Address.State.Value);
			}
			foreach (var c in ClientService.GetClients(new ClientSearchInfo { LocationId = location.Id, VisitMinDate = model.MinDate, VisitMaxDate = model.MaxDate }))
			{
				CountInfo countyCounts;
				var key = c.Address.County.HasText() ? c.Address.County.ToUpperInvariant() : String.Empty;
				if (data.CountyData.ContainsKey(key))
				{
					countyCounts = data.CountyData[key];
				}
				else
				{
					countyCounts = new CountInfo();
					data.CountyData[key] = countyCounts;
				}
				var visits = c.Visits.Count(x => x.Date >= data.MinDate && x.Date <= data.MaxDate);
				data.CountData.Families += visits;
				countyCounts.Families += visits;
				if (c.AgeBasis.HasValue)
				{
					IncrementCounter(data.CountData, c.AgeBasis.Value.Age(model.MaxDate.Value), visits);
					IncrementCounter(countyCounts, c.AgeBasis.Value.Age(model.MaxDate.Value), visits);
					data.CountData.Total += visits;
					countyCounts.Total += visits;
				}
				else
				{
					data.CountData.AgeUnknown++;
					countyCounts.AgeUnknown++;
				}
				foreach (var h in c.HouseholdMembers)
				{
					if (h.AgeBasis.HasValue)
					{
						IncrementCounter(data.CountData, h.AgeBasis.Value.Age(model.MaxDate.Value), visits);
						IncrementCounter(countyCounts, h.AgeBasis.Value.Age(model.MaxDate.Value), visits);
						data.CountData.Total += visits;
						countyCounts.Total += visits;
					}
					else
					{
						data.CountData.AgeUnknown += visits;
						countyCounts.AgeUnknown += visits;
					}
				}
				if (c.HouseholdCount != c.HouseholdMembers.Count + 1)
				{
					data.CountData.HouseholdCountDifferent += visits;
					countyCounts.HouseholdCountDifferent += visits;
				}
			}
			return data;
		}

		private static void IncrementCounter(CountInfo data, int age, int visits)
		{
			if (age < 6)
			{
				data.Age0To5 += visits;
			}
			else if (age < 19)
			{
				data.Age6To18 += visits;
			}
			else if (age < 40)
			{
				data.Age19To40 += visits;
			}
			else if (age < 61)
			{
				data.Age41To60 += visits;
			}
			else
			{
				data.AgeOver60 += visits;
			}
		}
	}
}