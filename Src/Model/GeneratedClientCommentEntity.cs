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
	public class GeneratedClientCommentEntity : BaseEntity
	{
		#region Private Members
		private long _clientId;
		private CommentTypeEntity _commentType;
		private string _text;
		private DateTime _date;
		#endregion
		
		#region Properties Names
			
		public const string ClientIdProperty = "ClientId";
		public const string CommentTypeProperty = "CommentType";
		public const string TextProperty = "Text";
		public const string DateProperty = "Date";
			
		#endregion
		
		#region String Length Constants
			
		public const int TextMaxLength = 1073741823;
			
		#endregion

		#region Constructor
		/// <summary>
		/// default constructor
		/// </summary>
		public GeneratedClientCommentEntity()
		{
			_clientId = 0;
			_text = null;
			_date = DateTime.MinValue;
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
		/// CommentType property
		/// </summary>		
		public virtual CommentTypeEntity CommentType
		{
			get { return _commentType; }
			set { _commentType = value; }
		}
			
		/// <summary>
		/// Text property
		/// </summary>		
		public virtual string Text
		{
			get { return _text; }
			set	
			{
				if (value != null && value.Length > 1073741823)
				{
					throw new ArgumentOutOfRangeException("Invalid value for Text", value, value.ToString());
				}
				_text = value;
			}
		}
			
		/// <summary>
		/// Date property
		/// </summary>		
		public virtual DateTime Date
		{
			get { return _date; }
			set { _date = value; }
		}
			
		#endregion
	}
}
