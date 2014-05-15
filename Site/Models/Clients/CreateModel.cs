// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using System.ComponentModel;
using DigitalBeacon.SiteBase.Model;
using DigitalBeacon.SiteBase.Web.Models;
using DigitalBeacon.Util;
using DigitalBeacon.Web;
using DigitalBeacon.Web.Validation;
using DigitalBeacon.CareCenter.Model;

namespace DigitalBeacon.CareCenter.Site.Models.Clients
{
	public class CreateModel : EditModel
	{
		[StringLength(ClientEntity.UniqueIdMaxLength)]
		[LocalizedDisplayName("Clients.UniqueId.Label")]
		public string SearchUniqueId { get; set; }

		[StringLength(ClientEntity.FirstNameMaxLength)]
		[LocalizedDisplayName("Common.FirstName.Label")]
		public string SearchFirstName { get; set; }

		[StringLength(ClientEntity.LastNameMaxLength)]
		[LocalizedDisplayName("Common.LastName.Label")]
		public string SearchLastName { get; set; }

		[Required]
		[LocalizedDisplayName("Visits.Date.Label")]
		public DateTime? Date { get; set; }

		[Required]
		[LocalizedDisplayName("Locations.Singular.Label")]
		public long? LocationId { get; set; }

		//[Required]
		[LocalizedDisplayName("Visits.Interviewer.Label")]
		public long? InterviewerId { get; set; }

		[LocalizedDisplayName("CommentTypes.Singular.Label")]
		public string CommentType { get; set; }

		[LocalizedDisplayName("Comments.Plural.Label")]
		public string Comments { get; set; }

		[Required]
		public override long? GenderId { get; set; }

		[Required]
		public override int? Age { get; set; }

		//[Required]
		//public override string UniqueId { get; set; }
	}
}