// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using DigitalBeacon.SiteBase.Web.Models;
using DigitalBeacon.Web;

namespace DigitalBeacon.CareCenter.Site.Models.Clients
{
	public class ListModel : ListModel<ListItem>
	{
		public bool? Inactive { get; set; }
		public long? CommentTypeId { get; set; }
		public long? LocationId { get; set; }
		public bool CanDelete { get; set; }
	}
}