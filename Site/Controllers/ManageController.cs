// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System.Web.Mvc;
using DigitalBeacon.SiteBase.Controllers;
using DigitalBeacon.SiteBase.Model;
using DigitalBeacon.SiteBase.Models;
using DigitalBeacon.SiteBase.Web;

namespace DigitalBeacon.CareCenter.Site.Controllers
{
	public class ManageController : SiteBaseController
    {
		[Authorization(Role.Administrator)]
		public ActionResult Index()
		{
			return View("PartialNav", AddTransientMessages(new PartialNavModel { Url = "~/manage" }));
		}
	}
}