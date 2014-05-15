// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System.Collections.Generic;
using System.Linq;
using DigitalBeacon.Data.NHibernate;
using DigitalBeacon.Model;
using DigitalBeacon.Util;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using DigitalBeacon.CareCenter.Model;

namespace DigitalBeacon.CareCenter.Data.NHibernate
{
	public class ClientDao : BaseDao<ClientEntity>, IClientDao
	{
		#region BaseDao Overrides

		protected override ICriteria ApplyFilters(ICriteria c, SearchInfo<ClientEntity> searchInfo)
		{
			base.ApplyFilters(c, searchInfo);

			if (!(searchInfo is ClientSearchInfo))
			{
				return c;
			}

			var clientSearch = (ClientSearchInfo)searchInfo;
			if (clientSearch.Inactive.HasValue)
			{
				c.Add(Restrictions.Eq(ClientEntity.EnabledProperty, !clientSearch.Inactive.Value));
			}
			if (clientSearch.ClientId.HasValue)
			{
				c.Add(Restrictions.Eq(ClientEntity.IdProperty, clientSearch.ClientId.Value));
			}
			if (clientSearch.LocationId.HasValue)
			{
				c.Add(Restrictions.Eq(GetIdProperty(ClientEntity.LocationProperty), clientSearch.LocationId.Value));
			}
			if (clientSearch.CommentTypeId.HasValue)
			{
				c.Add(Subqueries.Exists(
					DetachedCriteria.For(typeof(ClientCommentEntity))
						.Add(Restrictions.EqProperty(ClientCommentEntity.ClientIdProperty, GetIdProperty(DefaultAlias)))
						.Add(Restrictions.Eq(GetIdProperty(ClientCommentEntity.CommentTypeProperty), clientSearch.CommentTypeId.Value))
						.SetProjection(Projections.Constant(true))));
			}
			if (clientSearch.HasFlaggedComment ?? false)
			{
				c.Add(Subqueries.Exists(
					DetachedCriteria.For(typeof(ClientCommentEntity))
						.CreateAlias(ClientCommentEntity.CommentTypeProperty, ClientCommentEntity.CommentTypeProperty)
						.Add(Restrictions.EqProperty(ClientCommentEntity.ClientIdProperty, GetIdProperty(DefaultAlias)))
						.Add(Restrictions.Eq(GetPropertyName(ClientCommentEntity.CommentTypeProperty, CommentTypeEntity.FlaggedProperty), true))
						.SetProjection(Projections.Constant(true))));
			}
			if (clientSearch.LastVisitMinDate.HasValue)
			{
				c.Add(Restrictions.Ge(ClientEntity.LastVisitDateProperty, clientSearch.LastVisitMinDate.Value));
			}
			if (clientSearch.LastVisitMaxDate.HasValue)
			{
				c.Add(Restrictions.Or(
					Restrictions.IsNull(ClientEntity.LastVisitDateProperty),
					Restrictions.Lt(ClientEntity.LastVisitDateProperty, clientSearch.LastVisitMaxDate.Value.AddDays(1))));
			}
			if (clientSearch.VisitMinDate.HasValue || clientSearch.VisitMaxDate.HasValue)
			{
				var dc = DetachedCriteria.For(typeof(VisitEntity))
							.Add(Restrictions.EqProperty(VisitEntity.ClientIdProperty, GetIdProperty(DefaultAlias)));
				if (clientSearch.VisitMinDate.HasValue)
				{
					dc.Add(Restrictions.Ge(VisitEntity.DateProperty, clientSearch.VisitMinDate.Value));
				}
				if (clientSearch.VisitMaxDate.HasValue)
				{
					dc.Add(Restrictions.Lt(VisitEntity.DateProperty, clientSearch.VisitMaxDate.Value.AddDays(1)));
				}
				c.Add(Subqueries.Exists(dc.SetProjection(Projections.Constant(true))));
			}
			if (clientSearch.SearchText.HasText())
			{
				var searchTextCriterion = Restrictions.Or(
					Restrictions.Like(ClientEntity.FirstNameProperty, clientSearch.SearchText, MatchMode.Anywhere),
					Restrictions.Like(ClientEntity.LastNameProperty, clientSearch.SearchText, MatchMode.Anywhere));
				if (clientSearch.SearchText.Length == 4)
				{
					searchTextCriterion = Restrictions.Or(searchTextCriterion,
						Restrictions.Eq(ClientEntity.Ssn4Property, clientSearch.SearchText));
				}
				searchTextCriterion = Restrictions.Or(searchTextCriterion,
					Restrictions.Like(ClientEntity.UniqueIdProperty, clientSearch.SearchText, MatchMode.Start));
				var client = new ClientEntity { Ssn = clientSearch.SearchText };
				if (client.EncryptedSsn.HasText())
				{
					searchTextCriterion = Restrictions.Or(searchTextCriterion,
						Restrictions.Eq(ClientEntity.EncryptedSsnProperty, client.EncryptedSsn));
				}
				if (clientSearch.VisitMinDate == null && clientSearch.VisitMaxDate == null && clientSearch.SearchText.IsDate())
				{
					searchTextCriterion = Restrictions.Or(
						searchTextCriterion,
						Subqueries.Exists(DetachedCriteria.For(typeof(VisitEntity))
						//.Add(Restrictions.EqProperty(GetIdProperty(VisitEntity.ClientProperty), GetIdProperty(DefaultAlias)))
							.Add(Restrictions.EqProperty(VisitEntity.ClientIdProperty, GetIdProperty(DefaultAlias)))
							.Add(Restrictions.Ge(VisitEntity.DateProperty, clientSearch.MinDateForSearchText.Value))
							.Add(Restrictions.Le(VisitEntity.DateProperty, clientSearch.MaxDateForSearchText.Value))
							.SetProjection(Projections.Constant(true))));
				}
				// search comments
				searchTextCriterion = Restrictions.Or(
					searchTextCriterion,
					Subqueries.Exists(DetachedCriteria.For(typeof(ClientCommentEntity))
						.Add(Restrictions.EqProperty(ClientCommentEntity.ClientIdProperty, GetIdProperty(DefaultAlias)))
						.Add(Restrictions.Like(ClientCommentEntity.TextProperty, clientSearch.SearchText, MatchMode.Anywhere))
						.SetProjection(Projections.Constant(true))));
				c.Add(searchTextCriterion);
			}

			return c;
		}

		#endregion

		#region IClientDao Members

		public IList<int> FetchVisitYears(long clientId)
		{
			return HibernateTemplate.Execute(session =>
			{
				var q = from x in session.Query<VisitEntity>()
						where x.ClientId == clientId
						orderby x.Date.Year
						select x.Date.Year;
				return q.Distinct().ToList();
			});
		}

		//public IList<ClientEntity> FetchList(DateTime minDate, DateTime maxDate)
		//{
		//}

		#endregion
	}
}