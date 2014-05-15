// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using DigitalBeacon.Model;

namespace DigitalBeacon.CareCenter.Model
{
	/// <summary>
	/// A struct used to aggregate search parameters
	/// </summary>
	public class ClientSearchInfo : SearchInfo<ClientEntity>
	{
		public long? ClientId { get; set; }
		public long? LocationId { get; set; }
		public bool? Inactive { get; set; }
		public DateTime? VisitMinDate { get; set; }
		public DateTime? VisitMaxDate { get; set; }
		public DateTime? LastVisitMinDate { get; set; }
		public DateTime? LastVisitMaxDate { get; set; }
		public long? CommentTypeId { get; set; }
		public bool? HasFlaggedComment { get; set; }
	}
}