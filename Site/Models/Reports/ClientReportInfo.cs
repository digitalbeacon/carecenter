// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using System.Collections.Generic;

namespace DigitalBeacon.CareCenter.Site.Models.Reports
{
	public class ClientReportInfo
	{
		private const int FirstPageSize = 40;
		private const int PageSize = 45;

		public DateTime? MinDate { get; set; }
		public DateTime? MaxDate { get; set; }
		public string UserDisplayName { get; set; }
		public string LocationName { get; set; }
		public bool FromListView { get; set; }
		public bool ShowDetails { get; set; }

		public IList<RowItem> Rows { get; set; }

		public int Pages
		{
			get
			{
				if (Rows == null || Rows.Count <= FirstPageSize || ShowDetails)
				{
					return 1;
				}
				return (Rows.Count - FirstPageSize) % PageSize == 0 ? ((Rows.Count - FirstPageSize) / PageSize + 1) : ((Rows.Count - FirstPageSize) / PageSize + 2);
			}
		}

		public IEnumerable<RowItem> GetRowsForPageIndex(int pageIndex)
		{
			if (Rows == null)
			{
				yield break;
			}
			if (pageIndex == 0)
			{
				if (ShowDetails)
				{
					for (int i = 0; i < Rows.Count; i++)
					{
						yield return Rows[i];
					}
				}
				else
				{
					for (int i = 0; i < FirstPageSize && i < Rows.Count; i++)
					{
						yield return Rows[i];
					}
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