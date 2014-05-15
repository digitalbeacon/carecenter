// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using DigitalBeacon.SiteBase.Web.Models;
using DigitalBeacon.Web;

namespace DigitalBeacon.CareCenter.Site.Models.CommentTypes
{
	public class ListItem : LookupInfo
	{
		[LocalizedDisplayName("Common.Flagged.Label")]
		public bool Flagged { get; set; }
	}
}