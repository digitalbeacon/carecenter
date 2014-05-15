// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System.ComponentModel.DataAnnotations;
using DigitalBeacon.SiteBase.Web.Models;
using DigitalBeacon.Web;
using DigitalBeacon.CareCenter.Model;

namespace DigitalBeacon.CareCenter.Site.Models.Locations
{
	public class EditModel : NamedEntityModel
	{
		public EditModel()
		{
			Address = new AddressModel();
		}

		public AddressModel Address { get; set; }

		[LocalizedDisplayName("Common.DisplayOrder.Label")]
		public int? DisplayOrder { get; set; }

		[LocalizedDisplayName("Common.Inactive.Label")]
		public bool? Inactive { get; set; }

		[StringLength(LocationEntity.AgencyNameMaxLength)]
		[LocalizedDisplayName("Locations.AgencyName.Label")]
		public string AgencyName { get; set; }

		[StringLength(LocationEntity.AgencyCodeMaxLength)]
		[LocalizedDisplayName("Locations.AgencyCode.Label")]
		public string AgencyCode { get; set; }

		[StringLength(LocationEntity.ContactNameMaxLength)]
		[LocalizedDisplayName("Locations.ContactName.Label")]
		public string ContactName { get; set; }

		[StringLength(LocationEntity.AlternateContactNameMaxLength)]
		[LocalizedDisplayName("Locations.AlternateContactName.Label")]
		public string AlternateContactName { get; set; }
	}
}