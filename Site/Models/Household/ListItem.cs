// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System.ComponentModel;
using DigitalBeacon.Util;
using DigitalBeacon.Web;
using DigitalBeacon.Web.Validation;
using System;

namespace DigitalBeacon.CareCenter.Site.Models.Household
{
	public class ListItem
	{
		[ReadOnly(true)]
		public long Id { get; set; }

		[ReadOnly(true)]
		[LocalizedDisplayName("Household.Name.Label")]
		public string Name 
		{
			get { return "{0}, {1}{2}".FormatWith(LastName, FirstName, MiddleName.HasText() ? " " + MiddleName : String.Empty); }
		}

		[ReadOnly(true)]
		[LocalizedDisplayName("Common.FirstName.Label")]
		public string FirstName { get; set; }

		[ReadOnly(true)]
		[LocalizedDisplayName("Common.LastName.Label")]
		public string LastName { get; set; }

		[ReadOnly(true)]
		[StringLength(1)]
		[LocalizedDisplayName("Common.MiddleInitial.Label")]
		public string MiddleName { get; set; }

		[ReadOnly(true)]
		[LocalizedDisplayName("Common.LastFourSsn.Label")]
		public string Ssn4 { get; set; }

		[ReadOnly(true)]
		public DateTime? DateOfBirth { get; set; }

		[ReadOnly(true)]
		public DateTime? AgeBasis { get; set; }

		[ReadOnly(true)]
		[LocalizedDisplayName("Common.Age.Label")]
		public int? Age 
		{
			get { return DateOfBirth.HasValue ? DateOfBirth.Value.Age() : (AgeBasis.HasValue ? AgeBasis.Value.Age() : (int?)null); }
		}
	}
}