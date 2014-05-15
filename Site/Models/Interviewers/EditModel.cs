// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DigitalBeacon.SiteBase.Web.Models;
using DigitalBeacon.Web;
using DigitalBeacon.CareCenter.Model;

namespace DigitalBeacon.CareCenter.Site.Models.Interviewers
{
	public class EditModel : EntityModel
	{
		public EditModel()
		{
			Address = new AddressModel();
		}

		public bool CreateForVisit { get; set; }

		public AddressModel Address { get; set; }

		[Required]
		[StringLength(InterviewerEntity.FirstNameMaxLength)]
		[LocalizedDisplayName("Common.FirstName.Label")]
		public string FirstName { get; set; }

		[Required]
		[StringLength(InterviewerEntity.LastNameMaxLength)]
		[LocalizedDisplayName("Common.LastName.Label")]
		public string LastName { get; set; }

		[LocalizedDisplayName("Common.Inactive.Label")]
		public bool? Inactive { get; set; }

		[ReadOnly(true)]
		public bool Enabled
		{
			get { return Inactive.HasValue ? !Inactive.Value : true; }
			set { Inactive = !value; }
		}
	}
}