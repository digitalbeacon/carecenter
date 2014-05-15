// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using DigitalBeacon.SiteBase.Web.Models;

namespace DigitalBeacon.CareCenter.Site.Models.Household
{
	public class ListModel : ListModel<ListItem>
	{
		public bool CanDelete { get; set; }
	}
}