// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using System.ComponentModel;
using DigitalBeacon.SiteBase.Model;
using DigitalBeacon.SiteBase.Web.Models;
using DigitalBeacon.Web;
using DigitalBeacon.Web.Validation;
using DigitalBeacon.CareCenter.Model;

namespace DigitalBeacon.CareCenter.Site.Models.Clients
{
	public class EditModel : AddressModel
	{
		#region Address Overrides

		[Required]
		public override string Line1 { get; set; }

		[Required]
		public override string PostalCode { get; set; }

		[Required]
		public override string City { get; set; }

		[Required]
		public override long? StateId { get; set; }

		[Required]
		public override string County { get; set; }

		#endregion

		[ReadOnly(true)]
		public bool CanDelete { get; set; }

		[ReadOnly(true)]
		public long PhotoId { get; set; }

		[LocalizedDisplayName("Clients.Photo.Label")]
		public string Photo { get; set; }

		[LocalizedDisplayName("Common.Inactive.Label")]
		public bool? Inactive { get; set; }

		[StringLength(ClientEntity.UniqueIdMaxLength)]
		[LocalizedDisplayName("Clients.UniqueId.Label")]
		public virtual string UniqueId { get; set; }

		[Required]
		[StringLength(ClientEntity.FirstNameMaxLength)]
		[LocalizedDisplayName("Common.FirstName.Label")]
		public virtual string FirstName { get; set; }

		[Required]
		[StringLength(ClientEntity.LastNameMaxLength)]
		[LocalizedDisplayName("Common.LastName.Label")]
		public virtual string LastName { get; set; }

		[StringLength(ClientEntity.MiddleNameMaxLength)]
		[LocalizedDisplayName("Common.MiddleName.Label")]
		public virtual string MiddleName { get; set; }

		[Required]
		[LocalizedDisplayName("Races.Singular.Label")]
		public virtual string RaceId { get; set; }

		[Required]
		[LocalizedDisplayName("Common.Gender.Label")]
		public virtual long? GenderId { get; set; }

		[Required]
		[LocalizedDisplayName("Common.DateOfBirth.Label")]
		public virtual DateTime? DateOfBirth { get; set; }

		[StringLength(ClientEntity.SsnMaxLength)]
		[LocalizedDisplayName("Common.Ssn.Label")]
		public virtual string Ssn { get; set; }

		[ReadOnly(true)]
		[LocalizedDisplayName("Common.LastFourSsn.Label")]
		public virtual string LastFourSsn { get; set; }

		[LocalizedDisplayName("Clients.Income.Label")]
		public virtual decimal? Income { get; set; }

		[LocalizedDisplayName("Clients.Bibles.Label")]
		public virtual int? Bibles { get; set; }

		[ReadOnly(true)]
		[LocalizedDisplayName("Clients.HouseholdCount.Label")]
		public virtual int? HouseholdCount { get; set; }

		[LocalizedDisplayName("Clients.GiftCardAmount.Label")]
		public virtual decimal? GiftCardAmount { get; set; }

		[LocalizedDisplayName("Clients.Flagged.Label")]
		public virtual bool? Flagged { get; set; }

		[LocalizedDisplayName("Clients.Member.Label")]
		public virtual bool? Member { get; set; }

		[LocalizedDisplayName("Clients.ClientChoice.Label")]
		public virtual bool? ClientChoice { get; set; }

		[LocalizedDisplayName("Clients.FoodStamps.Label")]
		public virtual bool? FoodStamps { get; set; }

		[LocalizedDisplayName("Clients.Tanf.Label")]
		public virtual bool? Tanf { get; set; }

		[LocalizedDisplayName("Clients.Ssi.Label")]
		public virtual bool? Ssi { get; set; }

		[LocalizedDisplayName("Clients.Unemployed.Label")]
		public virtual bool? Unemployed { get; set; }

		[LocalizedDisplayName("Clients.Elderly.Label")]
		public virtual bool? Elderly { get; set; }

		[LocalizedDisplayName("Clients.SelfDeclared.Label")]
		public virtual bool? SelfDeclared { get; set; }

		[ReadOnly(true)]
		[LocalizedDisplayName("Clients.Repeat.Label")]
		public virtual bool? Repeat { get; set; }

		[Required]
		[Range(0, 120)]
		[LocalizedDisplayName("Common.Age.Label")]
		public virtual int? Age { get; set; }

		[ReadOnly(true)]
		public virtual Gender? Gender
		{
			get { return GenderId.HasValue ? (Gender)GenderId.Value : (Gender?)null; }
			set { GenderId = value.HasValue ? (long)value.Value : (long?)null; }
		}

		[ReadOnly(true)]
		public virtual bool Enabled
		{
			get { return Inactive.HasValue ? !Inactive.Value : true; }
			set { Inactive = !value; }
		}
	}
}