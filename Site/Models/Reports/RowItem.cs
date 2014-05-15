// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System.Collections.Generic;
using System.Linq;
using DigitalBeacon.CareCenter.Model;
using DigitalBeacon.CareCenter.Site.Models.Clients;

namespace DigitalBeacon.CareCenter.Site.Models.Reports
{
	public class RowItem : ListItem
	{
		private ClientEntity _client { get; set; }

		public RowItem(ClientEntity client)
		{
			_client = client;
		}

		public IEnumerable<VisitEntity> Visits
		{
			get { return _client.Visits.OrderByDescending(x => x.Date); }
		}

		public IEnumerable<ClientCommentEntity> Comments
		{
			get { return _client.Comments.OrderByDescending(x => x.Date); }
		}
	}
}