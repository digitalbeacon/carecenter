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

namespace DigitalBeacon.CareCenter.Model
{
	/// <summary>
	///	Generated with MyGeneration using the Entity template.
	/// </summary>
	[Serializable]
	public class VisitEntity : BaseEntity
	{
		#region Private Members
		private DateTime _date;
		private LocationEntity _location;
		private long _clientId;
		private InterviewerEntity _interviewer;
		#endregion
		
		#region Properties Names
			
		public const string DateProperty = "Date";
		public const string LocationProperty = "Location";
		public const string ClientIdProperty = "ClientId";
		public const string InterviewerProperty = "Interviewer";
			
		#endregion

		#region Constructor
		/// <summary>
		/// default constructor
		/// </summary>
		public VisitEntity()
		{
			_date = DateTime.MinValue;
			_clientId = 0;
			_interviewer = null;
		}
		#endregion
		
		#region Public Properties
			
		/// <summary>
		/// Date property
		/// </summary>		
		public virtual DateTime Date
		{
			get { return _date; }
			set { _date = value; }
		}
			
		/// <summary>
		/// Location property
		/// </summary>		
		public virtual LocationEntity Location
		{
			get { return _location; }
			set { _location = value; }
		}
			
		/// <summary>
		/// ClientId property
		/// </summary>		
		public virtual long ClientId
		{
			get { return _clientId; }
			set { _clientId = value; }
		}
			
		/// <summary>
		/// Interviewer property
		/// </summary>		
		public virtual InterviewerEntity Interviewer
		{
			get { return _interviewer; }
			set { _interviewer = value; }
		}
			
		#endregion
	}
}
