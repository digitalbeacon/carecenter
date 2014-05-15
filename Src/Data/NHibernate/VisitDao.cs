// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using DigitalBeacon.Data.NHibernate;
using DigitalBeacon.Model;
using DigitalBeacon.Util;
using NHibernate;
using NHibernate.Criterion;
using DigitalBeacon.CareCenter.Model;

namespace DigitalBeacon.CareCenter.Data.NHibernate
{
	public class VisitDao : BaseDao<VisitEntity>
	{
		#region BaseDao Overrides

		protected override ICriteria ApplyFilters(ICriteria c, SearchInfo<VisitEntity> searchInfo)
		{
			base.ApplyFilters(c, searchInfo);
			if (searchInfo.SearchText.HasText())
			{
				var year = searchInfo.SearchText.ToInt32();
				if (year.HasValue && searchInfo.SearchText.Length == 4)
				{
					// search based on year
					c.Add(Restrictions.Ge(VisitEntity.DateProperty, new DateTime(year.Value, 1, 1)));
					c.Add(Restrictions.Le(VisitEntity.DateProperty, new DateTime(year.Value + 1, 1, 1).AddMilliseconds(-1)));
				}
				else if (searchInfo.SearchText.IsDate())
				{
					c.Add(Restrictions.Ge(VisitEntity.DateProperty, searchInfo.MinDateForSearchText.Value));
					c.Add(Restrictions.Le(VisitEntity.DateProperty, searchInfo.MaxDateForSearchText.Value));
				}
				else
				{
					c.Add(Subqueries.Exists(
						DetachedCriteria.For(typeof(ClientEntity))
							.Add(Restrictions.EqProperty(ClientEntity.IdProperty, GetPropertyName(DefaultAlias, VisitEntity.ClientIdProperty)))
							.Add(Restrictions.Or(
								Restrictions.Like(ClientEntity.FirstNameProperty, searchInfo.SearchText, MatchMode.Anywhere),
								Restrictions.Like(ClientEntity.LastNameProperty, searchInfo.SearchText, MatchMode.Anywhere)))
							.SetProjection(Projections.Constant(true))));
				}
			}

			return c;
		}

		#endregion
	}
}