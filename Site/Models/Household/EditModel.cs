// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DigitalBeacon.SiteBase.Web.Models;
using DigitalBeacon.Web;
using DigitalBeacon.CareCenter.Model;

namespace DigitalBeacon.CareCenter.Site.Models.Household
{
	public class EditModel : EntityModel
	{
		public long ClientId { get; set; }

		[ReadOnly(true)]
		public bool CanDelete { get; set; }

		#region Wrapped Properties
		
		[ReadOnly(true)]
		public string FirstName { get; set; }

		[ReadOnly(true)]
		public string LastName { get; set; }

		[ReadOnly(true)]
		public string MiddleName { get; set; }

		[ReadOnly(true)]
		public string LastFourSsn { get; set; }

		[ReadOnly(true)]
		public string Ssn { get; set; }

		[ReadOnly(true)]
		public DateTime? DateOfBirth { get; set; }

		[ReadOnly(true)]
		public int? Age { get; set; }

		#endregion

		[Required]
		[StringLength(HouseholdMemberEntity.FirstNameMaxLength)]
		[LocalizedDisplayName("Common.FirstName.Label")]
		public string HouseholdFirstName
		{
			get { return FirstName; }
			set { FirstName = value; }
		}

		[Required]
		[StringLength(HouseholdMemberEntity.LastNameMaxLength)]
		[LocalizedDisplayName("Common.LastName.Label")]
		public string HouseholdLastName
		{
			get { return LastName; }
			set { LastName = value; }
		}

		[StringLength(1)]
		[LocalizedDisplayName("Common.MiddleInitial.Label")]
		public string HouseholdMiddleName
		{
			get { return MiddleName; }
			set { MiddleName = value; }
		}

		[Required]
		[Range(0, 120)]
		[LocalizedDisplayName("Common.Age.Label")]
		public int? HouseholdAge
		{
			get { return Age; }
			set { Age = value; }
		}

		[LocalizedDisplayName("Common.DateOfBirth.Label")]
		public DateTime? HouseholdDateOfBirth
		{
			get { return DateOfBirth; }
			set { DateOfBirth = value; }
		}

		[StringLength(HouseholdMemberEntity.SsnMaxLength)]
		[LocalizedDisplayName("Common.Ssn.Label")]
		public string HouseholdSsn
		{
			get { return Ssn; }
			set { Ssn = value; }
		}

		[ReadOnly(true)]
		[LocalizedDisplayName("Common.LastFourSsn.Label")]
		public string HouseholdLastFourSsn
		{
			get { return LastFourSsn; }
			set { LastFourSsn = value; }
		}
	}
}