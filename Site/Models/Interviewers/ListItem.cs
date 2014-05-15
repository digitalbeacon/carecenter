// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using DigitalBeacon.SiteBase.Web.Models;
using DigitalBeacon.Web;

namespace DigitalBeacon.CareCenter.Site.Models.Interviewers
{
	public class ListItem
	{
		public long Id { get; set; }

		[LocalizedDisplayName("Common.FirstName.Label")]
		public string FirstName { get; set; }

		[LocalizedDisplayName("Common.LastName.Label")]
		public string LastName { get; set; }

		[LocalizedDisplayName("Common.Inactive.Label")]
		public bool Inactive { get; set; }

		public bool Enabled
		{
			get { return !Inactive; }
			set { Inactive = !value; }
		}
	}
}