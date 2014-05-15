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
	public class GeneratedInterviewerEntity : PersonEntity
	{
		#region Private Members
		private bool _enabled;
		#endregion
		
		#region Properties Names
			
		public const string EnabledProperty = "Enabled";
			
		#endregion

		#region Constructor
		/// <summary>
		/// default constructor
		/// </summary>
		public GeneratedInterviewerEntity()
		{
			_enabled = false; 
		}
		#endregion
		
		#region Public Properties
			
		/// <summary>
		/// Enabled property
		/// </summary>		
		public virtual bool Enabled
		{
			get { return _enabled; }
			set { _enabled = value; }
		}
			
		#endregion
	}
}
