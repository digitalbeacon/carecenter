// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using System.Collections.Generic;
using System.Linq;
using DigitalBeacon.Util;

namespace DigitalBeacon.CareCenter.Site.Models.Reports
{
	public class FoodNetworkReportInfo
	{
		private CountInfo _countData = new CountInfo();
		private Dictionary<string, CountInfo> _countyData = new Dictionary<string, CountInfo>();

		public string Title { get; set; }
		public DateTime MinDate { get; set; }
		public DateTime MaxDate { get; set; }
		public string UserDisplayName { get; set; }
		public string AgencyName { get; set; }
		public string AgencyCode { get; set; }
		public string ContactName { get; set; }
		public string AlternateContactName { get; set; }
		public string AddressWorkPhoneText { get; set; }
		public string AddressFaxText { get; set; }
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public string AddressCity { get; set; }
		public string AddressState { get; set; }
		public string AddressPostalCode { get; set; }
		public CountInfo CountData { get { return _countData; } }
		public Dictionary<string, CountInfo> CountyData { get { return _countyData; } }
		
		public IEnumerable<string> CountyKeys
		{
			get { return _countyData.Keys.Where(x => x.HasText()).OrderBy(x => x); }
		}
	}
}