// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using System.Globalization;
using DigitalBeacon.Util;
using DigitalBeacon.Web;

namespace DigitalBeacon.CareCenter.Site.Models.Clients
{
	public class ListItem
	{
		public long Id { get; set; }
		
		[LocalizedDisplayName("Common.FirstName.Label")]
		public string FirstName { get; set; }

		[LocalizedDisplayName("Common.LastName.Label")]
		public string LastName { get; set; }

		[LocalizedDisplayName("Common.DateOfBirth.Label")]
		public DateTime? DateOfBirth { get; set; }

		[LocalizedDisplayName("Clients.LocationName.Label")]
		public string LocationName { get; set; }

		[LocalizedDisplayName("Clients.LastVisitDate.Label")]
		public DateTime? LastVisitDate { get; set; }

		public DateTime? AgeBasis { get; set; }

		[LocalizedDisplayName("Common.Age.Label")]
		public int? Age
		{
			get { return DateOfBirth.HasValue ? DateOfBirth.Value.Age() : (AgeBasis.HasValue ? AgeBasis.Value.Age() : (int?)null); }
		}

		[LocalizedDisplayName("Clients.HouseholdCount.Label.Short")]
		public int HouseholdCount { get; set; }

		[LocalizedDisplayName("Clients.Repeat.Label")]
		public bool Repeat { get; set; }

		[LocalizedDisplayName("Clients.Flagged.Label")]
		public bool Flagged { get; set; }

		[LocalizedDisplayName("Common.Inactive.Label")]
		public bool Inactive { get; set; }

		public bool Enabled
		{
			get { return !Inactive; }
			set { Inactive = !value; }
		}

		//[LocalizedDisplayName("Clients.Today.Label")]
		//public bool Today
		//{
		//    get
		//    {
		//        return LastVisitDate.HasValue ? LastVisitDate.Value.Date == DateTime.Today : false;
		//    }
		//}

		//[LocalizedDisplayName("Clients.ThisWeek.Label")]
		//public bool ThisWeek 
		//{
		//    get 
		//    { 
		//        var start = DateTime.Today.StartOfWeek(DayOfWeek.Monday);
		//        var end = start.AddDays(6);
		//        return LastVisitDate.HasValue ? LastVisitDate.Value >= start && LastVisitDate.Value <= end : false; 
		//    }
		//}

		//[LocalizedDisplayName("Clients.LastWeek.Label")]
		//public bool LastWeek { get; set; }

		public bool HasFlaggedComment { get; set; }
	}
}