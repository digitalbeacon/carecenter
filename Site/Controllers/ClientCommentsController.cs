// ---------------------------------------------------------------------- //
//                                                                        //
//                       Copyright (c) 2010-2014                          //
//                         Digital Beacon, LLC                            //
//                                                                        //
// ---------------------------------------------------------------------- //

using System.Web.Mvc;
using AutoMapper;
using DigitalBeacon.Business;
using DigitalBeacon.SiteBase.Controllers;
using DigitalBeacon.SiteBase.Model;
using DigitalBeacon.SiteBase.Models.Comments;
using DigitalBeacon.SiteBase.Web;
using DigitalBeacon.Util;
using DigitalBeacon.CareCenter.Business;
using DigitalBeacon.CareCenter.Model;

namespace DigitalBeacon.CareCenter.Site.Controllers
{
	[Authorization(Role.Administrator)]
	public class ClientCommentsController : CommentsController<ClientCommentEntity>
	{
		private const string CreatePath = "/clientComments/create";
		private const string DeletePath = "/clientComments/delete";
		private const string UpdatePath = "/clientComments/update";

		private static readonly IClientService ClientService = ServiceFactory.Instance.GetService<IClientService>();

		static ClientCommentsController()
		{
			Mapper.CreateMap<ClientCommentEntity, ListItem>();
			Mapper.CreateMap<ClientCommentEntity, EditModel>()
				.ForMember(t => t.CommentType, o => o.Ignore());
			Mapper.CreateMap<EditModel, ClientCommentEntity>()
				.ForMember(t => t.CommentType, o => o.Ignore());
		}

		public ClientCommentsController()
		{
			BaseName = "ClientComments";
			CheckCustomResources = true;
		}

		#region CommentsController

		protected override string GetParentName(long parentId)
		{
			return ClientService.GetClient(parentId).DisplayName;
		}

		protected override string ParentIdProperty 
		{ 
			get { return ClientCommentEntity.ClientIdProperty; }
		}

		protected override bool CanAdd
		{
			get { return PermissionService.HasPermissionToSitePath(CurrentUser, CreatePath); }
		}

		protected override bool CanDelete
		{
			get { return PermissionService.HasPermissionToSitePath(CurrentUser, DeletePath); }
		}

		protected override bool CanUpdate
		{
			get { return PermissionService.HasPermissionToSitePath(CurrentUser, UpdatePath); }
		}

		protected override bool ShowTextInline
		{
			get { return false; }
		}

		#endregion

		#region EntityController

		protected override EditModel PopulateSelectLists(EditModel model)
		{
			base.PopulateSelectLists(model);
			AddSelectList(model, model.PropertyName(x => x.CommentType), LookupService.GetNameList<CommentTypeEntity>());
			return model;
		}

		protected override ClientCommentEntity ConstructEntity(EditModel model)
		{
			var entity = base.ConstructEntity(model);
			entity.CommentType = LookupService.Get<CommentTypeEntity>(model.CommentType.ToInt64().Value);
			return entity;
		}

		protected override EditModel ConstructModel(ClientCommentEntity entity)
		{
			var model = base.ConstructModel(entity);
			model.CommentType = entity.CommentType.IfNotNull(x => x.Id.ToSafeString());
			return model;
		}

		[HttpPost]
		public override ActionResult Create(FormCollection form)
		{
			var retVal = base.Create(form);
			if (!CanUpdate && MessageModel != null)
			{
				MessageModel.ReturnUrl = null;
			}
			return retVal;
		}

		#endregion
	}
}