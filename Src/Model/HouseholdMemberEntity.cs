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

namespace DigitalBeacon.CareCenter.Model
{
	/// <summary>
	///	Generated with MyGeneration using the Entity template.
	/// </summary>
	[Serializable]
	public class HouseholdMemberEntity : ContactEntity
	{
		#region Private Members
		private long _clientId;
		private DateTime? _ageBasis;
		#endregion
		
		#region Properties Names
			
		public const string ClientIdProperty = "ClientId";
		public const string AgeBasisProperty = "AgeBasis";
			
		#endregion

		#region Constructor
		/// <summary>
		/// default constructor
		/// </summary>
		public HouseholdMemberEntity()
		{
			_clientId = 0;
			_ageBasis = null;
		}
		#endregion
		
		#region Public Properties
			
		/// <summary>
		/// ClientId property
		/// </summary>		
		public virtual long ClientId
		{
			get { return _clientId; }
			set { _clientId = value; }
		}
			
		/// <summary>
		/// AgeBasis property
		/// </summary>		
		public virtual DateTime? AgeBasis
		{
			get { return _ageBasis; }
			set { _ageBasis = value; }
		}
			
		#endregion
	}
}
