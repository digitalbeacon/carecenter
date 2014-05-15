// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;
using DigitalBeacon.Model;
using DigitalBeacon.SiteBase.Model;
using DigitalBeacon.Util;

namespace DigitalBeacon.CareCenter.Model
{
	[Serializable]
	public class ClientEntity : GeneratedClientEntity
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
