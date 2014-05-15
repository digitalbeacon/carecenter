// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using DigitalBeacon.SiteBase.Web.Models;
using DigitalBeacon.Web;
using DigitalBeacon.Web.Validation;
using System.ComponentModel;

namespace DigitalBeacon.CareCenter.Site.Models.Reports
{
	public class BaseReportModel : BaseViewModel
	{
		[LocalizedDisplayName("Common.SearchText.Label")]
		public virtual string SearchText { get; set; }

		[LocalizedDisplayName("Common.Status.Label")]
		public virtual bool? Inactive { get; set; }

		[LocalizedDisplayName("CommentTypes.Singular.Label")]
		public virtual long? CommentTypeId { get; set; }

		[LocalizedDisplayName("Locations.Singular.Label")]
		public virtual long? LocationId { get; set; }

		[LocalizedDisplayName("Reports.MinDate.Label")]
		public virtual DateTime? MinDate { get; set; }

		[LocalizedDisplayName("Reports.MaxDate.Label")]
		public virtual DateTime? MaxDate { get; set; }

		[LocalizedDisplayName("Reports.Download.Label")]
		public virtual bool? Download { get; set; }

		public virtual string Sort { get; set; }
		public virtual bool FromListView { get; set; }
		public virtual bool ShowDetails { get; set; }
	}
}