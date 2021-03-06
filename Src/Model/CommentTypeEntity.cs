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
	public class CommentTypeEntity : BaseEntity, INamedEntity
	{
		#region Private Members
		private string _name;
		private bool _flagged;
		private int _displayOrder;
		#endregion
		
		#region Properties Names
			
		public const string FlaggedProperty = "Flagged";
			
		#endregion
		
		#region String Length Constants
			
		public const int NameMaxLength = 50;
			
		#endregion

		#region Constructor
		/// <summary>
		/// default constructor
		/// </summary>
		public CommentTypeEntity()
		{
			_name = null;
			_flagged = false; 
			_displayOrder = 0;
		}
		#endregion
		
		#region Public Properties
			
		/// <summary>
		/// Name property
		/// </summary>		
		public virtual string Name
		{
			get { return _name; }
			set	
			{
				if (value != null && value.Length > 50)
				{
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
				}
				_name = value;
			}
		}
			
		/// <summary>
		/// Flagged property
		/// </summary>		
		public virtual bool Flagged
		{
			get { return _flagged; }
			set { _flagged = value; }
		}
			
		/// <summary>
		/// DisplayOrder property
		/// </summary>		
		public virtual int DisplayOrder
		{
			get { return _displayOrder; }
			set { _displayOrder = value; }
		}
			
		#endregion
	}
}
