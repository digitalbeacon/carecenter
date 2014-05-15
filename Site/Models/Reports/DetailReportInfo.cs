// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using System.Linq;
using System.Collections.Generic;

namespace DigitalBeacon.CareCenter.Site.Models.Reports
{
	public class DetailReportInfo
	{
		private const int FirstPageSize = 45;
		private const int PageSize = 60;

		private RowInfo _totals = new RowInfo();
		private RowInfo _familyData = new RowInfo();
		private List<RowInfo> _rows = new List<RowInfo>();
		private Dictionary<int, int> _householdCounts = new Dictionary<int, int>();

		public string Title { get; set; }
		public DateTime MinDate { get; set; }
		public DateTime MaxDate { get; set; }
		public string UserDisplayName { get; set; }
		public string LocationName { get; set; }

		public RowInfo Totals { get { return _totals; } }
		public RowInfo FamilyData { get { return _familyData; } }
		public List<RowInfo> Rows { get { return _rows; } }
		public Dictionary<int, int> HouseholdCounts { get { return _householdCounts; } }
		public int MaxHouseholdCount { get { return _householdCounts.Count < 10 ? 10 : _householdCounts.Keys.Max(); } }
		
		public int Pages 
		{ 
			get 
			{
				if (_rows.Count <= FirstPageSize)
				{
					return 1;
				}
				return (_rows.Count - FirstPageSize) % PageSize == 0 ? ((_rows.Count - FirstPageSize) / PageSize + 1) : ((_rows.Count - FirstPageSize) / PageSize + 2); 
			} 
		}
		
		public IEnumerable<RowInfo> GetRowsForPageIndex(int pageIndex)
		{
			if (pageIndex == 0)
			{
				for (int i = 0; i < FirstPageSize && i < Rows.Count; i++)
				{
					yield return Rows[i];
				}
			}
			else
			{
				for (int i = 0; i < PageSize && ((pageIndex - 1) * PageSize + i + FirstPageSize < Rows.Count); i++)
				{
					yield return Rows[(pageIndex - 1) * PageSize + i + FirstPageSize];
				}
			}
		}

		public class RowInfo : CountInfo
		{
			public string RaceCode { get; set; }
			public int Member { get; set; }
			public int Repeat { get; set; }
			public int HouseholdCount { get; set; }
			public int Visits { get; set; }
			public int Bibles { get; set; }
			public int ClientChoice { get; set; }
			public DateTime? LastVisitDate { get; set; }
		}
	}
}