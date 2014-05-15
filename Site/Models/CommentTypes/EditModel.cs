// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using DigitalBeacon.SiteBase.Web.Models;
using DigitalBeacon.Web;
using DigitalBeacon.Web.Validation;

namespace DigitalBeacon.CareCenter.Site.Models.CommentTypes
{
	public class EditModel : NamedEntityModel
	{
		[Range(0, Int32.MaxValue)]
		[LocalizedDisplayName("Common.DisplayOrder.Label")]
		public int? DisplayOrder { get; set; }

		[LocalizedDisplayName("Common.Flagged.Label")]
		public bool? Flagged { get; set; }

		[LocalizedDisplayName("Common.Inactive.Label")]
		public bool? Inactive { get; set; }
	}
}