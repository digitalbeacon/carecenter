// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using DigitalBeacon.Data;
using DigitalBeacon.CareCenter.Model;
using System.Collections.Generic;
using System;

namespace DigitalBeacon.CareCenter.Data
{
	public interface IClientDao : IDao<ClientEntity>
	{
		IList<int> FetchVisitYears(long clientId);
		//IList<ClientEntity> FetchList(DateTime minDate, DateTime maxDate);
	}
}
