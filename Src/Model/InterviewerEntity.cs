// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using DigitalBeacon.Util;

namespace DigitalBeacon.CareCenter.Model
{
	[Serializable]
	public class InterviewerEntity : GeneratedInterviewerEntity
	{
		public override string DisplayName
		{
			get 
			{
				return "{0}, {1}{2}".FormatWith(LastName, FirstName, MiddleName.HasText() ? " " + MiddleName : String.Empty);
			}
		}
	}
}
