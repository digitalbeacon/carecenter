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
	public class NewClientReportInfo
	{
		private const int FirstPageSize = 45;
		private const int PageSize = 60;

		private CountInfo _totals = new CountInfo();

		public string Title { get; set; }
		public DateTime MinDate { get; set; }
		public DateTime MaxDate { get; set; }
		public string UserDisplayName { get; set; }
		public string LocationName { get; set; }

		public CountInfo Totals { get { return _totals; } }
		public List<CountInfo> Rows { get; set; }

		public int Pages
		{
			get
			{
				if (Rows == null || Rows.Count <= FirstPageSize)
				{
					return 1;
				}
				return (Rows.Count - FirstPageSize) % PageSize == 0 ? ((Rows.Count - FirstPageSize) / PageSize + 1) : ((Rows.Count - FirstPageSize) / PageSize + 2);
			}
		}

		public IEnumerable<CountInfo> GetRowsForPageIndex(int pageIndex)
		{
			if (Rows == null)
			{
				//return Enumerable.Empty<CountInfo>();
				yield break;
			}
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
	}
}