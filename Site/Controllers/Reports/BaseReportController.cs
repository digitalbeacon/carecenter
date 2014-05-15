// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using System.ComponentModel;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using DigitalBeacon.Business;
using DigitalBeacon.SiteBase.Controllers;
using DigitalBeacon.SiteBase.Web;
using DigitalBeacon.Util;
using DigitalBeacon.Web;
using DigitalBeacon.CareCenter.Business;
using DigitalBeacon.CareCenter.Model;
using DigitalBeacon.CareCenter.Site.Models.Reports;

namespace DigitalBeacon.CareCenter.Site.Controllers.Reports
{
	public abstract class BaseReportController : SiteBaseController
	{
		private const string DownloadCookieKey = "DigitalBeacon.CareCenter.DownloadReport";

		protected static readonly IClientService ClientService = ServiceFactory.Instance.GetService<IClientService>();

		protected abstract object GetReportData(BaseReportModel model);

		protected bool UseLandscapeOrientation { get; set; }

		protected virtual RouteValueDictionary ConstructRouteValues(BaseReportModel model)
		{
			return new RouteValueDictionary(new
			{
				model.SearchText,
				model.LocationId,
				model.MinDate,
				model.MaxDate,
				model.CommentTypeId,
				model.Inactive,
				model.Sort,
				model.FromListView,
				model.ShowDetails
			});
		}

		public ActionResult Index(BaseReportModel model)
		{
			ModelState.Clear();
			
			var today = DateTime.Today;
			if (model.MinDate == null)
			{
				model.MinDate = new DateTime(today.Year, today.Month, 1); // first day of current month
				//model.MinDate = new DateTime(today.Year, today.Month - 1, 1); // first day of last month
				//model.MinDate = new DateTime(2011, 1, 1); //
			}
			if (model.MaxDate == null)
			{
				model.MaxDate = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month)); // last day of current month
				//model.MaxDate = new DateTime(today.Year, today.Month, 1).AddDays(-1); // last day of last month
				//model.MaxDate = today;
				//model.MaxDate = new DateTime(2011, 1, 31);
			}

			// semantics of model.Inactive value are non-standard
			model.Inactive = GetParamAsString(model.PropertyName(x => x.Inactive)).ToBoolean();
			model.ListItems[model.PropertyName(x => x.Inactive)] = new[] { 
				new SelectListItem { Value = String.Empty, Text = GetLocalizedText("Clients.Enabled.Active.Label") },
				new SelectListItem { Value = Boolean.TrueString, Text = GetLocalizedText("Clients.Enabled.Inactive.Label") },
				new SelectListItem { Value = Boolean.FalseString, Text = GetLocalizedText("Clients.Enabled.Both.Label") }
			};

			model.CommentTypeId = GetParamAsString(model.PropertyName(x => x.CommentTypeId)).ToInt64();
			AddSelectList(model, model.PropertyName(x => x.CommentTypeId),
				LookupService.GetNameList<CommentTypeEntity>());

			model.LocationId = GetParamAsString(model.PropertyName(x => x.LocationId)).ToInt64();
			AddSelectList(model, model.PropertyName(x => x.LocationId),
				LookupService.GetNameList<LocationEntity>());

			model.Download = GetCookieValue(DownloadCookieKey).ToBoolean() ?? false;
			return View(AddTransientMessages(model));
		}

		public ActionResult Create(BaseReportModel model)
		{
			if (!ModelState.IsValid)
			{
				return ErrorAction(WebConstants.DefaultErrorHeadingKey, "Error.DataInvalid");
			}

			SetCookieValue(DownloadCookieKey, model.Download ?? true);

			var options = GetLocalizedText("Reports.LeftFooter", CurrentUser.DisplayName, DateTime.Now);
			if (options.HasText())
			{
				options = "--footer-left \"" + options + "\"";
			}
			var htmlUrl = new Uri(new Uri(ConfigurationManager.AppSettings["ReportingBaseUrl"]), 
				Url.Action("html", ConstructRouteValues(model))).AbsoluteUri;
			return HtmlToPdfAction(model.Download ?? false ? "report.pdf" : null, htmlUrl, UseLandscapeOrientation, options) ?? DefaultErrorAction();
		}

		public ActionResult Html(BaseReportModel model)
		{
			model.Guard("model");

			if (!Request.IsLocal)
			{
				return new HttpForbiddenResult();
			}

			if (model.MinDate == null && model.MaxDate == null)
			{
				var today = DateTime.Today;
				model.MinDate = new DateTime(today.Year, today.Month, 1);
				model.MaxDate = new DateTime(today.Year, today.Month + 1, 1).AddDays(-1);
			}

			return View("Report", GetReportData(model));
		}

		protected virtual string GetSortMember(string member)
		{
			if (member == "LocationName")
			{
				return GetPropertyName(ClientEntity.LocationProperty, LocationEntity.NameProperty);
			}
			if (member == "Age")
			{
				return ClientEntity.AgeBasisProperty;
			}
			if (member == "Inactive")
			{
				return ClientEntity.EnabledProperty;
			}
			return member;
		}

		protected virtual ListSortDirection GetSortDirection(string member, ListSortDirection sortDirection)
		{
			if (member == "Age" || member == "Flagged")
			{
				return sortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
			}
			return sortDirection;
		}
	}
}