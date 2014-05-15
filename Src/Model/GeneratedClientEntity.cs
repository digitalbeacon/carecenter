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
	public class GeneratedClientEntity : ContactEntity
	{
		#region Private Members
		private string _uniqueId;
		private bool _flagged;
		private LocationEntity _location;
		private DateTime? _lastVisitDate;
		private DateTime? _ageBasis;
		private int? _householdCount;
		private decimal? _income;
		private int? _bibles;
		private bool _member;
		private bool _foodStamps;
		private bool _tanf;
		private bool _ssi;
		private bool _unemployed;
		private bool _elderly;
		private bool _selfDeclared;
		private bool _repeat;
		private bool _clientChoice;
		private IList<HouseholdMemberEntity> _householdMembers;
		private IList<VisitEntity> _visits;
		private IList<ClientCommentEntity> _comments;
		#endregion
		
		#region Properties Names
			
		public const string UniqueIdProperty = "UniqueId";
		public const string FlaggedProperty = "Flagged";
		public const string LocationProperty = "Location";
		public const string LastVisitDateProperty = "LastVisitDate";
		public const string AgeBasisProperty = "AgeBasis";
		public const string HouseholdCountProperty = "HouseholdCount";
		public const string IncomeProperty = "Income";
		public const string BiblesProperty = "Bibles";
		public const string MemberProperty = "Member";
		public const string FoodStampsProperty = "FoodStamps";
		public const string TanfProperty = "Tanf";
		public const string SsiProperty = "Ssi";
		public const string UnemployedProperty = "Unemployed";
		public const string ElderlyProperty = "Elderly";
		public const string SelfDeclaredProperty = "SelfDeclared";
		public const string RepeatProperty = "Repeat";
		public const string ClientChoiceProperty = "ClientChoice";
		public const string HouseholdMembersProperty = "HouseholdMembers";
		public const string VisitsProperty = "Visits";
		public const string CommentsProperty = "Comments";
			
		#endregion
		
		#region String Length Constants
			
		public const int UniqueIdMaxLength = 100;
			
		#endregion

		#region Constructor
		/// <summary>
		/// default constructor
		/// </summary>
		public GeneratedClientEntity()
		{
			_uniqueId = null;
			_flagged = false; 
			_location = null;
			_lastVisitDate = null;
			_ageBasis = null;
			_householdCount = null;
			_income = null;
			_bibles = null;
			_member = false; 
			_foodStamps = false; 
			_tanf = false; 
			_ssi = false; 
			_unemployed = false; 
			_elderly = false; 
			_selfDeclared = false; 
			_repeat = false; 
			_clientChoice = false; 
		}
		#endregion
		
		#region Public Properties
			
		/// <summary>
		/// UniqueId property
		/// </summary>		
		public virtual string UniqueId
		{
			get { return _uniqueId; }
			set	
			{
				if (value != null && value.Length > 100)
				{
					throw new ArgumentOutOfRangeException("Invalid value for UniqueId", value, value.ToString());
				}
				_uniqueId = value;
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
		/// Location property
		/// </summary>		
		public virtual LocationEntity Location
		{
			get { return _location; }
			set { _location = value; }
		}
			
		/// <summary>
		/// LastVisitDate property
		/// </summary>		
		public virtual DateTime? LastVisitDate
		{
			get { return _lastVisitDate; }
			set { _lastVisitDate = value; }
		}
			
		/// <summary>
		/// AgeBasis property
		/// </summary>		
		public virtual DateTime? AgeBasis
		{
			get { return _ageBasis; }
			set { _ageBasis = value; }
		}
			
		/// <summary>
		/// HouseholdCount property
		/// </summary>		
		public virtual int? HouseholdCount
		{
			get { return _householdCount; }
			set { _householdCount = value; }
		}
			
		/// <summary>
		/// Income property
		/// </summary>		
		public virtual decimal? Income
		{
			get { return _income; }
			set { _income = value; }
		}
			
		/// <summary>
		/// Bibles property
		/// </summary>		
		public virtual int? Bibles
		{
			get { return _bibles; }
			set { _bibles = value; }
		}
			
		/// <summary>
		/// Member property
		/// </summary>		
		public virtual bool Member
		{
			get { return _member; }
			set { _member = value; }
		}
			
		/// <summary>
		/// FoodStamps property
		/// </summary>		
		public virtual bool FoodStamps
		{
			get { return _foodStamps; }
			set { _foodStamps = value; }
		}
			
		/// <summary>
		/// Tanf property
		/// </summary>		
		public virtual bool Tanf
		{
			get { return _tanf; }
			set { _tanf = value; }
		}
			
		/// <summary>
		/// Ssi property
		/// </summary>		
		public virtual bool Ssi
		{
			get { return _ssi; }
			set { _ssi = value; }
		}
			
		/// <summary>
		/// Unemployed property
		/// </summary>		
		public virtual bool Unemployed
		{
			get { return _unemployed; }
			set { _unemployed = value; }
		}
			
		/// <summary>
		/// Elderly property
		/// </summary>		
		public virtual bool Elderly
		{
			get { return _elderly; }
			set { _elderly = value; }
		}
			
		/// <summary>
		/// SelfDeclared property
		/// </summary>		
		public virtual bool SelfDeclared
		{
			get { return _selfDeclared; }
			set { _selfDeclared = value; }
		}
			
		/// <summary>
		/// Repeat property
		/// </summary>		
		public virtual bool Repeat
		{
			get { return _repeat; }
			set { _repeat = value; }
		}
			
		/// <summary>
		/// ClientChoice property
		/// </summary>		
		public virtual bool ClientChoice
		{
			get { return _clientChoice; }
			set { _clientChoice = value; }
		}
			
		/// <summary>
		/// HouseholdMembers collection
		/// </summary>		
		public virtual IList<HouseholdMemberEntity> HouseholdMembers
		{
			get { return _householdMembers; }
			set { _householdMembers = value; }
		}
		
		/// <summary>
		/// Visits collection
		/// </summary>		
		public virtual IList<VisitEntity> Visits
		{
			get { return _visits; }
			set { _visits = value; }
		}
		
		/// <summary>
		/// Comments collection
		/// </summary>		
		public virtual IList<ClientCommentEntity> Comments
		{
			get { return _comments; }
			set { _comments = value; }
		}
		
		#endregion
	}
}
