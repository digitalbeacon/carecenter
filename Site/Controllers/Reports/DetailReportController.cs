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
	public class DetailReportController : BaseReportController
	{
		//public DetailReportController()
		//{
		//    Mapper.CreateMap<LocationEntity, DetailReportInfo>();
		//}

		protected override object GetReportData(BaseReportModel model)
		{
			model.Guard("model");

			var data = new DetailReportInfo();

			data.MinDate = model.MinDate.Value;
			data.MaxDate = model.MaxDate.Value;
			var search = new ClientSearchInfo { VisitMinDate = model.MinDate, VisitMaxDate = model.MaxDate };
			if (model.LocationId.HasValue)
			{
				search.LocationId = model.LocationId.Value;
				var location = LookupService.Get<LocationEntity>(model.LocationId.Value);
				data.LocationName = location.Name;
			}
			search.AddSort(x => x.LastName);
			search.AddSort(x => x.FirstName);
			foreach (var c in ClientService.GetClients(search))
			{
				var rowInfo = new DetailReportInfo.RowInfo
				{
					Name = c.DisplayName,
					HouseholdCount = c.HouseholdCount ?? 1,
					Visits = c.Visits.Count(x => x.Date >= data.MinDate && x.Date <= data.MaxDate),
					Member = c.Member ? 1 : 0,
					Bibles = c.Bibles ?? 0,
					Repeat = c.Repeat ? 1 : 0,
					ClientChoice = c.ClientChoice ? 1 : 0,
					LastVisitDate = c.LastVisitDate
				};
				var key = c.HouseholdCount ?? 1;
				if (data.HouseholdCounts.ContainsKey(key))
				{
					data.HouseholdCounts[key] = data.HouseholdCounts[key] + 1;
				}
				else
				{
					data.HouseholdCounts[key] = 1;
				}
				if (c.AgeBasis.HasValue)
				{
					//IncrementCounter(data.Totals, c.AgeBasis.Value.Age(model.MaxDate.Value), c.RaceId ?? 0, c.HouseholdCount ?? 1);
					IncrementCounter(rowInfo, c.AgeBasis.Value.Age(model.MaxDate.Value), c.RaceId ?? 0, c.HouseholdCount ?? 1);
					data.Totals.Total++;
				}
				else
				{
					data.Totals.AgeUnknown++;
				}
				foreach (var h in c.HouseholdMembers)
				{
					if (h.AgeBasis.HasValue)
					{
						//IncrementCounter(data.Totals, h.AgeBasis.Value.Age(model.MaxDate.Value));
						IncrementCounter(rowInfo, h.AgeBasis.Value.Age(model.MaxDate.Value));
						data.Totals.Total++;
					}
					else
					{
						data.Totals.AgeUnknown++;
					}
				}
				//if (c.HouseholdCount != c.HouseholdMembers.Count + 1)
				//{
				//    data.Totals.HouseholdCountDifferent++;
				//}

				data.Totals.Families++;

				data.Totals.Age0To5 += rowInfo.Age0To5;
				data.Totals.Age6To18 += rowInfo.Age6To18;
				data.Totals.Age19To40 += rowInfo.Age19To40;
				data.Totals.Age41To60 += rowInfo.Age41To60;
				data.Totals.AgeOver60 += rowInfo.AgeOver60;

				data.Totals.RaceWhite += rowInfo.RaceWhite;
				data.Totals.RaceBlack += rowInfo.RaceBlack;
				data.Totals.RaceHispanic += rowInfo.RaceHispanic;
				data.Totals.RaceAsian += rowInfo.RaceAsian;
				data.Totals.RaceIndian += rowInfo.RaceIndian;
				data.Totals.RaceOther += rowInfo.RaceOther;

				data.Totals.Visits += rowInfo.Visits;
				data.Totals.Member += rowInfo.Member;
				data.Totals.Bibles += rowInfo.Bibles;
				data.Totals.Repeat += rowInfo.Repeat;
				data.Totals.ClientChoice += rowInfo.ClientChoice;

				//data.Totals.Total += c.HouseholdCount ?? 0;
				data.Rows.Add(rowInfo);
			}
			data.FamilyData.RaceWhite = data.Rows.Count(x => x.RaceWhite > 0);
			data.FamilyData.RaceBlack = data.Rows.Count(x => x.RaceBlack > 0);
			data.FamilyData.RaceHispanic = data.Rows.Count(x => x.RaceHispanic > 0);
			data.FamilyData.RaceAsian = data.Rows.Count(x => x.RaceAsian > 0);
			data.FamilyData.RaceIndian = data.Rows.Count(x => x.RaceIndian > 0);
			data.FamilyData.RaceOther = data.Rows.Count(x => x.RaceOther > 0);
			return data;
		}

		private void IncrementCounter(CountInfo data, int age, long raceId = 0, int householdCount = 0)
		{
			if (age < 6)
			{
				data.Age0To5++;
			}
			else if (age < 19)
			{
				data.Age6To18++;
			}
			else if (age < 40)
			{
				data.Age19To40++;
			}
			else if (age < 61)
			{
				data.Age41To60++;
			}
			else
			{
				data.AgeOver60++;
			}

			var rowInfo = data as DetailReportInfo.RowInfo;
			if (householdCount > 0 && raceId > 0)
			{
				var race = LookupService.Get<RaceEntity>(raceId);
				if (race != null)
				{
					if (rowInfo != null)
					{
						rowInfo.RaceCode = race.Code;
					}
					if (race.Code == "W")
					{
						data.RaceWhite += householdCount;
					}
					else if (race.Code == "B")
					{
						data.RaceBlack += householdCount;
					}
					else if (race.Code == "S")
					{
						data.RaceHispanic += householdCount;
					}
					else if (race.Code == "A")
					{
						data.RaceAsian += householdCount;
					}
					else if (race.Code == "I")
					{
						data.RaceIndian += householdCount;
					}
					else
					{
						data.RaceOther += householdCount;
					}
				}
			}
			else if (householdCount > 0)
			{
				data.RaceUnknown += householdCount;
				if (rowInfo != null)
				{
					rowInfo.RaceCode = "?";
				}
			}
		}
	}
}