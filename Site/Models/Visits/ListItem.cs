// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using System.ComponentModel;
using DigitalBeacon.Web;

namespace DigitalBeacon.CareCenter.Site.Models.Visits
{
	public class ListItem
	{
		[ReadOnly(true)]
		public long Id { get; set; }

		[ReadOnly(true)]
		public long ClientId { get; set; }

		[ReadOnly(true)]
		[LocalizedDisplayName("Visits.Date.Label")]
		public DateTime? Date { get; set; }

		[ReadOnly(true)]
		[LocalizedDisplayName("Clients.Singular.Label")]
		public string ClientName { get; set; }

		[ReadOnly(true)]
		[LocalizedDisplayName("Locations.Singular.Label")]
		public string LocationName { get; set; }

		[ReadOnly(true)]
		[LocalizedDisplayName("Visits.Interviewer.Label")]
		public string InterviewerDisplayName { get; set; }
	}
}