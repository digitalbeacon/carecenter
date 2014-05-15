// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using DigitalBeacon.SiteBase.Business.Support;
using DigitalBeacon.SiteBase.Model;
using DigitalBeacon.Util;
using DigitalBeacon.Web;
using DigitalBeacon.CareCenter.Model;
using DigitalBeacon.CareCenter.Site.Models;
using DigitalBeacon.CareCenter.SqlUpdate;

namespace DigitalBeacon.CareCenter.Site
{
	public class Application : DigitalBeacon.SiteBase.Application
	{
		protected override void Application_Start()
		{
			base.Application_Start();
			UpgradeCustomDatabase();
			RegisterSharedObjectMappings();
			//RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
			RouteTable.Routes.Remove(RouteTable.Routes["Default"]);
			//RouteTable.Routes.MapRoute(
			//    "Main", // Route name
			//    "{controller}/{budgetId}/{fundId}/{departmentId}/{projectId}/{id}/{action}", // URL with parameters
			//    new { controller = "Main", action = "Edit" },
			//    new { budgetId = @"\d+", fundId = @"\d+", departmentId = @"\d+", projectId = @"\d+", id = @"\d+", httpVerb = new HttpVerbConstraint(HttpVerbs.Get) }
			//);
			RouteTable.Routes.MapRoute(
				"DefaultPost", // Route name
				"{controller}", // URL with parameters
				new { controller = "Clients", action = "Create" },
				new { httpVerb = new HttpVerbConstraint(HttpVerbs.Post) }
			);
			RouteTable.Routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Clients", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);
			
			//RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
		}

		protected override void RegisterLookupAdminTypes()
		{
			base.RegisterLookupAdminTypes();
			LookupAdminService.RegisterAcceptedType<CommentTypeEntity>();
			LookupAdminService.RegisterAcceptedType<ClientCommentEntity>();
			LookupAdminService.RegisterAcceptedType<InterviewerEntity>();
			LookupAdminService.RegisterAcceptedType<HouseholdMemberEntity>();
			LookupAdminService.RegisterAcceptedType<VisitEntity>();
		}

		protected static void UpgradeCustomDatabase()
		{
			var sqlUpdateConnection = ConfigurationManager.AppSettings[DigitalBeacon.SqlUpdate.SqlUpdater.ConnectionKey];
			if (sqlUpdateConnection.HasText())
			{
				var sqlUpdater = new SqlUpdater();
				sqlUpdater.Run();
			}
		}

		protected static void RegisterSharedObjectMappings()
		{
			Mapper.CreateMap<AddressEntity, AddressModel>()
				.ForMember(t => t.Id, o => o.Ignore());
			Mapper.CreateMap<AddressModel, AddressEntity>()
				.ForMember(t => t.Id, o => o.Ignore());
		}
	}
}