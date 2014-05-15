// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using DigitalBeacon.Business;
using DigitalBeacon.Model;
using DigitalBeacon.SiteBase.Controllers;
using DigitalBeacon.SiteBase.Model;
using DigitalBeacon.SiteBase.Web;
using DigitalBeacon.SiteBase.Web.Models;
using DigitalBeacon.Util;
using DigitalBeacon.Web.Formatters;
using DigitalBeacon.Web.Util;
using DigitalBeacon.CareCenter.Business;
using DigitalBeacon.CareCenter.Model;
using DigitalBeacon.CareCenter.Site.Models.Clients;
using Spark;

namespace DigitalBeacon.CareCenter.Site.Controllers
{
	[Precompile("Blank")]
	[Precompile("Message")]
	[Precompile("Delete")]
	[Precompile("LookupList")]
	[Precompile("LookupEdit")]
	[Precompile("CommentsList")]
	[Precompile("CommentsEdit")]
	[Precompile("EditorTemplates/String")]
	[Precompile("EditorTemplates/ReadOnlyField")]
	[Precompile("EditorTemplates/DateTime")]
	[Authorization(Role.Administrator,Exclude="ProcessInactive")]
	public class ClientsController : EntityController<ClientEntity, EditModel>
	{
		private const string ImageMaxWidthPath = "/clients/image/maxWidth";
		private const string ImageMaxHeightPath = "/clients/image/maxHeight";
		private const string DeletePath = "/clients/delete";
		private const string RedirectFromSsnSearch = "RedirectFromSsnSearch";

		private const int DefaultImageMaxWidth = 400;
		private const int DefaultImageMaxHeight = 300;

		private static readonly SsnFormatter SsnFormatter = new SsnFormatter();

		private static readonly IEnumerable<SelectListItem> NumericSelectList =
			new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }.Select(x => new SelectListItem { Text = x.ToSafeString(), Value = x.ToSafeString() });

		private static readonly IClientService ClientService = ServiceFactory.Instance.GetService<IClientService>();

		private byte[] _photoData;
		private int _photoWidth;
		private int _photoHeight;

		static ClientsController()
		{
			Mapper.CreateMap<ClientEntity, ListItem>();
			Mapper.CreateMap<ClientEntity, EditModel>()
				.ForMember(t => t.Ssn, o => o.Ignore());
			Mapper.CreateMap<AddressEntity, EditModel>()
				.ForMember(t => t.Id, o => o.Ignore());
			Mapper.CreateMap<EditModel, ClientEntity>()
				.ForMember(t => t.Address, o => o.Ignore())
				.ForMember(t => t.Photo, o => o.Ignore())
				.ForMember(t => t.Ssn, o => o.Ignore())
				.ForMember(t => t.HouseholdCount, o => o.Ignore())
				.ForMember(t => t.Repeat, o => o.Ignore());
			Mapper.CreateMap<EditModel, AddressEntity>()
				.ForMember(t => t.Id, o => o.Ignore());
		}

		public ClientsController()
		{
			DefaultSort = new[] { new SortItem { Member = ClientEntity.LastVisitDateProperty, SortDirection = ListSortDirection.Descending } };
		}

		public ActionResult ValidateSsn(long id, string ssn)
		{
			bool retVal = true;
			var client = new ClientEntity { Ssn = ssn };
			if (client.Ssn != CareCenterConstants.DefaultSsn)
			{
				client = ClientService.GetClientByEncryptedSsn(client.EncryptedSsn);
				retVal = client == null || client.Id == id;
			}
			return Json(retVal, JsonRequestBehavior.AllowGet);
		}

		public ActionResult ValidateUniqueId(long id, string uniqueId)
		{
			var search = new ClientSearchInfo();
			search.AddFilter(ClientEntity.UniqueIdProperty, uniqueId);
			search.AddFilter(ClientEntity.IdProperty, ComparisonOperator.NotEqual, id);
			return Json(ClientService.GetClientCount(search) == 0, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult SearchBySsn(string ssn)
		{
			long id = 0;
			var client = new ClientEntity { Ssn = ssn };
			if (client.Ssn != CareCenterConstants.DefaultSsn)
			{
				client = ClientService.GetClientByEncryptedSsn(client.EncryptedSsn);
				if (client != null)
				{
					id = client.Id;
				}
			}
			return Json(id);
		}

		[HttpPost]
		public ActionResult CareCenter(long id)
		{
			var pref = PreferenceService.GetPreference(CurrentAssociationId, SiteConstants.DefaultLocationIdPreferenceKey);
			LocationEntity defaultLocation = null;
			if (pref != null && pref.ValueAsInt64 != null)
			{
				defaultLocation = LookupService.Get<LocationEntity>(pref.ValueAsInt64.Value);
			}
			if (defaultLocation == null)
			{
				return Json(new { Response = false, Message = "Default Location not available." }, JsonRequestBehavior.AllowGet);
			}
			var interviewerSearch = new SearchInfo<InterviewerEntity>();
			interviewerSearch.AddFilter(x => x.FirstName, CurrentUser.Person.FirstName);
			interviewerSearch.AddFilter(x => x.LastName, CurrentUser.Person.LastName);
			var interviewer = LookupService.GetEntityList(interviewerSearch).FirstOrDefault();
			if (interviewer == null)
			{
				interviewer = new InterviewerEntity
				{
					Enabled = true,
					FirstName = CurrentUser.Person.FirstName,
					LastName = CurrentUser.Person.LastName,
				};
				interviewer.Address = new AddressEntity();
				interviewer.Address.Email = CurrentUser.Email;
				interviewer = LookupService.SaveEntity(interviewer);
			}

			var visitSearch = new SearchInfo<VisitEntity>();
			visitSearch.AddFilter(x => x.ClientId, id);
			visitSearch.AddFilter(x => x.Date, DateTime.Today);
			if (LookupService.GetEntityCount(visitSearch) == 0)
			{
				var visit = new VisitEntity
				{
					ClientId = id,
					Date = DateTime.Today,
					Location = defaultLocation,
					Interviewer = interviewer
				};
				LookupService.SaveEntity(visit);
				ClientService.UpdateClientLocation(id);
			}
			return Json(new { Response = true }, JsonRequestBehavior.AllowGet);
				
			//ActionResult retVal = null;
			//ClientService.DeleteClientPhoto(id);
			//if (RenderPartial)
			//{
			//    retVal = RedirectToMessageAction(SingularLabel, "Clients.DeletePhoto.Confirmation");
			//    MessageModel.ReturnUrl = Url.Action(EditActionName, new { renderType = WebConstants.RenderTypePartial });
			//    MessageModel.ReturnText = ReturnTextSingular;
			//}
			//else
			//{
			//    AddTransientMessage("Common.DeletePhoto.Confirmation");
			//}
			//return retVal ?? RedirectToAction(EditActionName);
		}

		[HttpPost]
		public ActionResult DeletePhoto(long id)
		{
			ActionResult retVal = null;
			ClientService.DeleteClientPhoto(id);
			if (RenderPartial)
			{
				retVal = RedirectToMessageAction(SingularLabel, "Common.DeletePhoto.Confirmation");
				MessageModel.ReturnUrl = Url.Action(EditActionName, new { renderType = WebConstants.RenderTypePartial });
				MessageModel.ReturnText = ReturnTextSingular;
			}
			else
			{
				AddTransientMessage("Common.DeletePhoto.Confirmation");
			}
			return retVal ?? RedirectToAction(EditActionName);
		}

		[Authorization(RequireLocal = true)]
		public ActionResult ProcessInactive()
		{
			return MessageAction(PluralLabel, "Clients.ProcessInactive.Confirmation", ClientService.SetDormantClientsToInactive());
		}

		#region EntityController

		protected override string NewView
		{
			get { return NewViewName; }
		}

		protected override ListModelBase ConstructListModel()
		{
			var model = new ListModel();

			// semantics of model.Inactive value are non-standard
			model.Inactive = GetParamAsString(model.PropertyName(x => x.Inactive)).ToBoolean() ?? false;
			model.ListItems[model.PropertyName(x => x.Inactive)] = new[] { 
				new SelectListItem { Value = Boolean.FalseString, Text = GetLocalizedText("Clients.Enabled.Both.Label") },
				new SelectListItem { Value = String.Empty, Text = GetLocalizedText("Clients.Enabled.Active.Label") },
				new SelectListItem { Value = Boolean.TrueString, Text = GetLocalizedText("Clients.Enabled.Inactive.Label") }
			};

			model.CommentTypeId = GetParamAsString(model.PropertyName(x => x.CommentTypeId)).ToInt64();
			AddSelectList(model, model.PropertyName(x => x.CommentTypeId), 
				LookupService.GetNameList<CommentTypeEntity>());

			model.LocationId = GetParamAsString(model.PropertyName(x => x.LocationId)).ToInt64();
			
			//var locationId = GetParamAsString(model.PropertyName(x => x.LocationId));
			//if (locationId == null)
			//{
			//    model.LocationId = GetCookieValue(SiteConstants.LocationIdCookieKey).ToInt64();
			//}
			//else
			//{
			//    model.LocationId = locationId.ToInt64();
			//};

			AddSelectList(model, model.PropertyName(x => x.LocationId),
				LookupService.GetNameList<LocationEntity>());

			model.CanDelete = PermissionService.HasPermissionToSitePath(CurrentUser, DeletePath);
			return model;
		}

		protected override EditModel PopulateSelectLists(EditModel model)
		{
			AddSelectList(model, model.PropertyName(m => m.GenderId), LookupService.GetNameList<GenderEntity>());
			AddSelectList(model, ClientEntity.RaceIdProperty, LookupService.GetNameList<RaceEntity>());
			AddSelectList(model, model.PropertyName(m => m.StateId), LookupService.GetNameList<StateEntity>());
			AddSelectList(model, model.PropertyName(m => m.DefaultPhoneId), LookupService.GetNameList<PhoneTypeEntity>());

			model.ListItems[ClientEntity.BiblesProperty] = NumericSelectList;

			var createModel = model as CreateModel;
			if (createModel != null)
			{
				AddSelectList(model, createModel.PropertyName(x => x.LocationId),
					LookupService.GetNameList<LocationEntity>());
				var interviewerSearch = new SearchInfo<InterviewerEntity>();
				interviewerSearch.AddFilter(x => x.Enabled, true);
				interviewerSearch.AddSort(x => x.LastName);
				model.ListItems[createModel.PropertyName(x => x.InterviewerId)] = new SelectList(
					LookupService.GetEntityList(interviewerSearch),
					InterviewerEntity.IdProperty, InterviewerEntity.DisplayNameProperty);
				AddSelectList(model, createModel.PropertyName(x => x.CommentType), LookupService.GetNameList<CommentTypeEntity>());
			}

			return model;
		}

		protected override string GetSortMember(string member)
		{
			if (member == "LocationName")
			{
				return GetPropertyName(ClientEntity.LocationProperty, LocationEntity.NameProperty);
			}
			if (member == "Age")
			{
				return ClientEntity.AgeBasisProperty;
			}
			if (member == "Inactive")
			{
				return ClientEntity.EnabledProperty;
			}
			return base.GetSortMember(member);
		}

		protected override ListSortDirection GetSortDirection(string member, ListSortDirection sortDirection)
		{
			if (member == "Age" || member == "Flagged" || member == "Repeat")
			{
				return sortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
			}
			return base.GetSortDirection(member, sortDirection);
		}

		protected override string GetDescription(EditModel model)
		{
			return "{0} {1}".FormatWith(model.FirstName, model.LastName);
		}

		protected override SearchInfo<ClientEntity> ConstructSearchInfo()
		{
			return new ClientSearchInfo();
		}

		protected override SearchInfo<ClientEntity> PrepareSearchInfo(SearchInfo<ClientEntity> searchInfo, ListModelBase model)
		{
			var listModel = (ListModel)model;
			var clientSearch = (ClientSearchInfo)searchInfo;
			// semantics of model.Inactive value are non-standard
			if (listModel.Inactive == null)
			{
				clientSearch.Inactive = false;
			}
			else
			{
				clientSearch.Inactive = listModel.Inactive.Value ? true : (bool?)null;
			}
			clientSearch.CommentTypeId = listModel.CommentTypeId;
			clientSearch.LocationId = listModel.LocationId;
			return base.PrepareSearchInfo(searchInfo, listModel);
		}

		protected override IEnumerable<ClientEntity> GetEntities(SearchInfo<ClientEntity> searchInfo, ListModelBase model)
		{
			//if (((ListModel)model).LocationId.HasValue)
			//{
			//    SetCookieValue(SiteConstants.LocationIdCookieKey, ((ListModel)model).LocationId.Value);
			//}
			return ClientService.GetClients((ClientSearchInfo)searchInfo);
		}

		protected override long GetEntityCount(SearchInfo<ClientEntity> searchInfo, ListModelBase model)
		{
			return ClientService.GetClientCount((ClientSearchInfo)searchInfo);
		}

		protected override ClientEntity GetEntity(long id)
		{
			var entity = ClientService.GetClient(id);
			if (entity.Photo != null)
			{
				entity.Photo.DataChanged = false;
			}
			return entity;
		}

		protected override EditModel ConstructCreateModel()
		{
			var model = new CreateModel 
			{ 
				Date = DateTime.Today,
				DefaultPhoneId = (long)PhoneType.Mobile,
				LocationId = GetCookieValue(SiteConstants.LocationIdCookieKey).ToInt64(),
				InterviewerId = GetCookieValue(SiteConstants.InterviewerIdCookieKey).ToInt64()
			};
			return model;
		}

		protected override EditModel ConstructCreateModel(FormCollection form)
		{
			var model = (CreateModel)PopulateModelForValidation(new CreateModel(), form);
			TryUpdateModel(model);
			if (model.LocationId.HasValue)
			{
				SetCookieValue(SiteConstants.LocationIdCookieKey, model.LocationId.Value);
			}
			if (model.InterviewerId.HasValue)
			{
				SetCookieValue(SiteConstants.InterviewerIdCookieKey, model.InterviewerId.Value);
			}
			return model;
		}

		protected override EditModel ConstructUpdateModel(long id)
		{
			var model = base.ConstructUpdateModel(id);
			if (GetParamAsString(RedirectFromSsnSearch).ToBoolean() ?? false)
			{
				AddTransientMessage("Clients.RedirectFromSsnSearch.Message");
			}
			if (model.Flagged ?? false)
			{
				AddMessage(model, "Clients.Flagged.Message");
			}
			if (model.Inactive ?? false)
			{
				AddError(model, "Clients.Inactive.Warning");
			}
			return model;
		}

		protected override void Validate(ClientEntity entity, EditModel model)
		{
			base.Validate(entity, model);
			//if (model is CreateModel && model.Ssn.HasText() && entity.Ssn.IsNullOrBlank())
			//{
			//    AddPropertyValidationError(m => m.Ssn, "Validation.Error.Required", "Common.Ssn.Label");
			//}
			//else if (entity.Ssn.HasText() && entity.Ssn != CheckInConstants.DefaultSsn)
			//{
			//    var c = ClientService.GetClientByEncryptedSsn(entity.EncryptedSsn);
			//    if (c != null && c.Id != entity.Id)
			//    {
			//        AddPropertyValidationError(m => m.Ssn, CheckInConstants.DuplicateSsn);
			//    }
			//}
			try
			{
				int maxWidth = PreferenceService.GetPreference(CurrentAssociationId, ImageMaxWidthPath).IfNotNull(x => x.Value.ToInt32()) ?? DefaultImageMaxWidth;
				int maxHeight = PreferenceService.GetPreference(CurrentAssociationId, ImageMaxHeightPath).IfNotNull(x => x.Value.ToInt32()) ?? DefaultImageMaxHeight;
				_photoData = ImageUtil.GetPhotoData(Request, out _photoWidth, out _photoHeight, maxWidth, maxHeight);
			}
			catch (Exception)
			{
				AddPropertyValidationError(m => m.Photo, "Common.Error.Photo.Invalid");
			}
			if (!ModelState.IsValid && entity.Photo != null)
			{
				model.PhotoId = entity.Photo.Id;
			}
		}

		protected override ClientEntity SaveEntity(ClientEntity entity, EditModel model)
		{
			if (_photoData != null)
			{
				entity.Photo = new FileEntity
				{
					AssociationId = CurrentAssociationId,
					DataChanged = true,
					ContentType = "image/jpeg",
					DataCompressed = false,
					Filename = Guid.NewGuid() + ".jpg",
					FileData = new FileDataEntity { Data = _photoData }
				};
				entity.PhotoWidth = _photoWidth;
				entity.PhotoHeight = _photoHeight;
			}
			var createModel = model as CreateModel;
			if (createModel != null)
			{
				entity.AssociationId = CurrentAssociationId;
				entity.Location = LookupService.Get<LocationEntity>(createModel.LocationId.Value);
				entity.LastVisitDate = createModel.Date.Value;
				entity.HouseholdCount = 1;
			}
			else if (entity.HouseholdCount == null || entity.HouseholdCount.Value <= 0)
			{
				var search = new SearchInfo<HouseholdMemberEntity>();
				search.AddFilter(x => x.ClientId, entity.Id);
				entity.HouseholdCount = ((int)LookupService.GetEntityCount(search)) + 1;
			}
			var client = ClientService.SaveClient(entity);
			if (createModel != null)
			{
				LookupService.SaveEntity(new VisitEntity
				{
					ClientId = client.Id,
					Date = createModel.Date.Value,
					Location = entity.Location,
					Interviewer = createModel.InterviewerId.HasValue ? LookupService.Get<InterviewerEntity>(createModel.InterviewerId.Value) : null
				});
				if (createModel.Comments.HasText() && createModel.CommentType.ToInt64().HasValue)
				{
					LookupService.SaveEntity(new ClientCommentEntity
					{
						ClientId = client.Id,
						Date = createModel.Date.Value,
						CommentType = LookupService.Get<CommentTypeEntity>(createModel.CommentType.ToInt64().Value),
						Text = createModel.Comments
					});
				}
			}
			return client;
		}

		protected override void DeleteEntity(long id)
		{
			ClientService.DeleteClient(id);
		}

		protected override EditModel ConstructModel(ClientEntity entity)
		{
			var model = Mapper.Map<EditModel>(entity);
			Mapper.Map(entity.Address, model);
			model.Age = entity.DateOfBirth.HasValue ? entity.DateOfBirth.Value.Age() : (entity.AgeBasis.HasValue ? entity.AgeBasis.Value.Age() : (int?)null);
			model.CanDelete = PermissionService.HasPermissionToSitePath(CurrentUser, DeletePath);
			return model;
		}

		protected override ClientEntity ConstructEntity(EditModel model)
		{
			var entity = model.Id == 0 ? new ClientEntity() : GetEntity(model.Id);
			if (entity.Address == null)
			{
				entity.Address = new AddressEntity();
			}
			Mapper.Map(model, entity);
			Mapper.Map(model, entity.Address);
			if (SsnFormatter.Parse(model.Ssn).HasText())
			{
				entity.Ssn = model.Ssn;
				entity.Ssn4 = entity.LastFourSsn;
			}
			if (model.DateOfBirth.HasValue)
			{
				entity.AgeBasis = model.DateOfBirth;
			}
			else if (model.Age.HasValue)
			{
				if (entity.AgeBasis == null || entity.AgeBasis.Value.Age() != model.Age.Value)
				{
					entity.AgeBasis = DateTime.Today.AddYears(-model.Age.Value).AddDays(-182);
				}
			}
			else
			{
				entity.AgeBasis = null;
			}
			return entity;
		}

		protected override IEnumerable ConstructGridItems(IEnumerable<ClientEntity> source, ListModelBase model)
		{
			var lastWeekMinDate = DateTime.Today.AddDays(-7).StartOfWeek(DayOfWeek.Monday);
			var lastWeekMaxDate = lastWeekMinDate.AddDays(6);
			return source.Select(entity => 
				{
					var item = Mapper.Map<ListItem>(entity);
					//item.LastWeek = ClientService.GetClientCount(new ClientSearchInfo { ClientId = entity.Id, MinDate = lastWeekMinDate, MaxDate = lastWeekMaxDate }) > 0;
					var flaggedCommentSearch = new SearchInfo<ClientCommentEntity>();
					flaggedCommentSearch.AddFilter(x => x.ClientId, entity.Id);
					flaggedCommentSearch.AddFilter(x => x.CommentType.Flagged, true);
					item.HasFlaggedComment = LookupService.GetEntityCount(flaggedCommentSearch) > 0;
					return item;
				});
		}

		#endregion
	}
}
