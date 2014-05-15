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

namespace DigitalBeacon.CareCenter.Site.Models.Visits
{
	public class EditModel : EntityModel
	{
		public long ClientId { get; set; }

		[ReadOnly(true)]
		public bool CanDelete { get; set; }

		[Required]
		[LocalizedDisplayName("Visits.Date.Label")]
		public DateTime? Date { get; set; }

		[Required]
		[LocalizedDisplayName("Locations.Singular.Label")]
		public long? LocationId { get; set; }

		//[Required]
		[LocalizedDisplayName("Visits.Interviewer.Label")]
		public long? InterviewerId { get; set; }

		//public bool ClientMode { get; set; }
	}
}