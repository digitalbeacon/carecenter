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
	public class CountInfo
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public int Families { get; set; }
		public int Total { get; set; }

		public int NewFamilies { get; set; }
		public int NewTotal { get; set; }

		public int Age0To5 { get; set; }
		public int Age6To18 { get; set; }
		public int Age19To40 { get; set; }
		public int Age41To60 { get; set; }
		public int AgeOver60 { get; set; }
		public int AgeUnknown { get; set; }

		public int RaceWhite { get; set; }
		public int RaceBlack { get; set; }
		public int RaceHispanic { get; set; }
		public int RaceAsian { get; set; }
		public int RaceIndian { get; set; }
		public int RaceOther { get; set; }
		public int RaceUnknown { get; set; }

		public int HouseholdCountDifferent { get; set; }
	}
}