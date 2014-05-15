// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using DigitalBeacon.SiteBase.Web.Models;

namespace DigitalBeacon.CareCenter.Site.Models.Visits
{
	public class ListModel : ListModel<ListItem>
	{
		public long? LocationId { get; set; }
		public long? InterviewerId { get; set; }
		public int? Year { get; set; }
		public bool CanDelete { get; set; }
	}
}