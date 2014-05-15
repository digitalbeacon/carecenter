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
	public class LocationEntity : BaseEntity, INamedEntity
	{
		#region Private Members
		private string _name;
		private int _displayOrder;
		private string _county;
		private string _agencyName;
		private string _agencyCode;
		private string _contactName;
		private string _alternateContactName;
		private AddressEntity _address;
		#endregion
		
		#region Properties Names
			
		public const string CountyProperty = "County";
		public const string AgencyNameProperty = "AgencyName";
		public const string AgencyCodeProperty = "AgencyCode";
		public const string ContactNameProperty = "ContactName";
		public const string AlternateContactNameProperty = "AlternateContactName";
		public const string AddressProperty = "Address";
			
		#endregion
		
		#region String Length Constants
			
		public const int NameMaxLength = 50;
		public const int CountyMaxLength = 50;
		public const int AgencyNameMaxLength = 50;
		public const int AgencyCodeMaxLength = 50;
		public const int ContactNameMaxLength = 100;
		public const int AlternateContactNameMaxLength = 100;
			
		#endregion

		#region Constructor
		/// <summary>
		/// default constructor
		/// </summary>
		public LocationEntity()
		{
			_name = null;
			_displayOrder = 0;
			_county = null;
			_agencyName = null;
			_agencyCode = null;
			_contactName = null;
			_alternateContactName = null;
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
		/// DisplayOrder property
		/// </summary>		
		public virtual int DisplayOrder
		{
			get { return _displayOrder; }
			set { _displayOrder = value; }
		}
			
		/// <summary>
		/// County property
		/// </summary>		
		public virtual string County
		{
			get { return _county; }
			set	
			{
				if (value != null && value.Length > 50)
				{
					throw new ArgumentOutOfRangeException("Invalid value for County", value, value.ToString());
				}
				_county = value;
			}
		}
			
		/// <summary>
		/// AgencyName property
		/// </summary>		
		public virtual string AgencyName
		{
			get { return _agencyName; }
			set	
			{
				if (value != null && value.Length > 50)
				{
					throw new ArgumentOutOfRangeException("Invalid value for AgencyName", value, value.ToString());
				}
				_agencyName = value;
			}
		}
			
		/// <summary>
		/// AgencyCode property
		/// </summary>		
		public virtual string AgencyCode
		{
			get { return _agencyCode; }
			set	
			{
				if (value != null && value.Length > 50)
				{
					throw new ArgumentOutOfRangeException("Invalid value for AgencyCode", value, value.ToString());
				}
				_agencyCode = value;
			}
		}
			
		/// <summary>
		/// ContactName property
		/// </summary>		
		public virtual string ContactName
		{
			get { return _contactName; }
			set	
			{
				if (value != null && value.Length > 100)
				{
					throw new ArgumentOutOfRangeException("Invalid value for ContactName", value, value.ToString());
				}
				_contactName = value;
			}
		}
			
		/// <summary>
		/// AlternateContactName property
		/// </summary>		
		public virtual string AlternateContactName
		{
			get { return _alternateContactName; }
			set	
			{
				if (value != null && value.Length > 100)
				{
					throw new ArgumentOutOfRangeException("Invalid value for AlternateContactName", value, value.ToString());
				}
				_alternateContactName = value;
			}
		}
			
		/// <summary>
		/// Address property
		/// </summary>		
		public virtual AddressEntity Address
		{
			get { return _address; }
			set { _address = value; }
		}
			
		#endregion
	}
}
