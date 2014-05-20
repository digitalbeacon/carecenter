UPDATE [sitebase].[Address] SET [Email] = 'carecenterdemo@digitalbeacon.net' WHERE [Id] = 1
UPDATE [sitebase].[Association] SET [Key] = 'CareCenter', [Name] = 'Food Pantry' WHERE [Id] = 1
UPDATE [sitebase].[ModuleSettingDefinition] SET [DefaultValue] = 'https://digitalbeacon.net/carecenterdemo' WHERE [Key] = 'Global.SiteUrl'
UPDATE [sitebase].[Module] SET [Url] = 'https://digitalbeacon.net/carecenterdemo' WHERE [Id] = 1

INSERT INTO [SqlUpdate]([Version],[PatchNumber],[Module]) VALUES('1.0.0', 999, 'CareCenter')

-- default navigation

SET IDENTITY_INSERT [sitebase].[NavigationItem] ON

INSERT INTO [sitebase].[NavigationItem] ([Id],[ModificationCounter],[AssociationId],[NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url],[ImageUrl])VALUES(1,0,NULL,1,NULL,1,'Home',NULL,'/',NULL)
INSERT INTO [sitebase].[NavigationItem] ([Id],[ModificationCounter],[AssociationId],[NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url],[ImageUrl])VALUES(2,0,NULL,2,NULL,1,'Sign In',NULL,'/identity/signIn',NULL)
INSERT INTO [sitebase].[NavigationItem] ([Id],[ModificationCounter],[AssociationId],[NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url],[ImageUrl])VALUES(3,0,NULL,2,NULL,1,'Sign Out',NULL,'/identity/signOut',NULL)
INSERT INTO [sitebase].[NavigationItem] ([Id],[ModificationCounter],[AssociationId],[NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url],[ImageUrl])VALUES(4,0,NULL,1,NULL,2,'Register',NULL,'/identity/register',NULL)

INSERT INTO [sitebase].[NavigationItem] ([Id],[ModificationCounter],[AssociationId],[NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url],[ImageUrl])VALUES(5,0,NULL,1,NULL,99,'Account',NULL,'/account',NULL)
INSERT INTO [sitebase].[NavigationItem] ([Id],[ModificationCounter],[AssociationId],[NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url],[ImageUrl])VALUES(6,0,NULL,1,5,1,'Update Profile',NULL,'/account/updateProfile',NULL)
INSERT INTO [sitebase].[NavigationItem] ([Id],[ModificationCounter],[AssociationId],[NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url],[ImageUrl])VALUES(7,0,NULL,1,5,2,'Change Password',NULL,'/account/changePassword',NULL)
INSERT INTO [sitebase].[NavigationItem] ([Id],[ModificationCounter],[AssociationId],[NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url],[ImageUrl])VALUES(8,0,NULL,1,5,3,'Change Security Question',NULL,'/account/changeSecurityQuestion',NULL)

INSERT INTO [sitebase].[NavigationItem] ([Id],[ModificationCounter],[AssociationId],[NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url],[ImageUrl])VALUES(9,0,NULL,1,NULL,98,'Admin',NULL,'/admin',NULL)
INSERT INTO [sitebase].[NavigationItem] ([Id],[ModificationCounter],[AssociationId],[NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url],[ImageUrl])VALUES(10,0,NULL,1,9,1,'Users',NULL,'/users',NULL)
INSERT INTO [sitebase].[NavigationItem] ([Id],[ModificationCounter],[AssociationId],[NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url],[ImageUrl])VALUES(11,0,NULL,1,9,1,'Modules',NULL,'/modules',NULL)
INSERT INTO [sitebase].[NavigationItem] ([Id],[ModificationCounter],[AssociationId],[NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url],[ImageUrl])VALUES(12,0,NULL,1,9,1,'Navigation',NULL,'/navigationItems',NULL)
INSERT INTO [sitebase].[NavigationItem] ([Id],[ModificationCounter],[AssociationId],[NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url],[ImageUrl])VALUES(13,0,NULL,1,9,1,'Email Queue',NULL,'/queuedEmails',NULL)
INSERT INTO [sitebase].[NavigationItem] ([Id],[ModificationCounter],[AssociationId],[NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url],[ImageUrl])VALUES(14,0,NULL,1,9,1,'Role Groups',NULL,'/roleGroups',NULL)
INSERT INTO [sitebase].[NavigationItem] ([Id],[ModificationCounter],[AssociationId],[NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url],[ImageUrl])VALUES(15,0,NULL,1,9,1,'Roles',NULL,'/roles',NULL)
INSERT INTO [sitebase].[NavigationItem] ([Id],[ModificationCounter],[AssociationId],[NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url],[ImageUrl])VALUES(16,0,NULL,1,9,1,'Permissions',NULL,'/permissions',NULL)
INSERT INTO [sitebase].[NavigationItem] ([Id],[ModificationCounter],[AssociationId],[NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url],[ImageUrl])VALUES(17,0,NULL,1,9,1,'Audit Log',NULL,'/auditLog',NULL)
INSERT INTO [sitebase].[NavigationItem] ([Id],[ModificationCounter],[AssociationId],[NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url],[ImageUrl])VALUES(18,0,NULL,1,9,1,'Localization',NULL,'/localization',NULL)

SET IDENTITY_INSERT [sitebase].[NavigationItem] OFF
	
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/',4,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/identity/signIn',4,2,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/identity/signOut',4,3,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/identity/register',4,2,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/account',4,3,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/account/updateProfile',4,3,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/account/changePassword',4,3,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/account/changeSecurityQuestion',4,3,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/admin',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/roleGroups',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/roles',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/permissions',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/users',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/modules',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/navigationItems',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/auditLog',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/localization',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/queuedEmails',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/queuedEmails/processQueue',4,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/content/flexible/default',4,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/content/basic',4,1,1)

-- Check In roles

UPDATE [sitebase].[Role] SET [RoleGroupId] = 4 WHERE [Name] IN ('Guest')

SET IDENTITY_INSERT [sitebase].[Role] ON
INSERT INTO [sitebase].[Role]([Id],[Name]) VALUES(101, 'Reports')
SET IDENTITY_INSERT [sitebase].[Role] OFF

-- Locations

INSERT INTO [sitebase].[Address]([ModificationCounter]) VALUES(1)
INSERT INTO [Location]([Name],[DisplayOrder],[AddressId]) SELECT 'Demo', 1, MAX([Id]) FROM [sitebase].[Address]

-- Comment Types

INSERT INTO [CommentType]([Name],[Flagged],[DisplayOrder]) VALUES('Note', 0, 1)

-- Races

INSERT INTO [sitebase].[Race]([Name],[Code],[DisplayOrder]) VALUES('White', 'W', 1)
INSERT INTO [sitebase].[Race]([Name],[Code],[DisplayOrder]) VALUES('Black', 'B', 2)
INSERT INTO [sitebase].[Race]([Name],[Code],[DisplayOrder]) VALUES('Hispanic', 'S', 3)
INSERT INTO [sitebase].[Race]([Name],[Code],[DisplayOrder]) VALUES('Asian', 'A', 4)
INSERT INTO [sitebase].[Race]([Name],[Code],[DisplayOrder]) VALUES('Other', 'O', 999)

-- Check In specific navigation

DELETE FROM [sitebase].[Permission] WHERE [Key1] = 'SitePath' AND [Key3] = '/'
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/',4,2,1)

UPDATE [sitebase].[NavigationItem] SET [Enabled] = 0 WHERE [Text] = 'Register'

INSERT INTO [sitebase].[NavigationItem] ([NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url])
	VALUES(1,NULL,1,'Clients',NULL,'/clients')
INSERT INTO [sitebase].[NavigationItem] ([NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url])
	VALUES(1,NULL,2,'Visits',NULL,'/visits')

SET IDENTITY_INSERT [sitebase].[NavigationItem] ON

INSERT INTO [sitebase].[NavigationItem] ([Id],[ModificationCounter],[AssociationId],[NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url])
	VALUES(301,0,NULL,1,NULL,97,'Manage',NULL,'/manage')
INSERT INTO [sitebase].[NavigationItem] ([Id],[ModificationCounter],[AssociationId],[NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url])
	VALUES(302,0,NULL,1,NULL,3,'Reports',NULL,'/reports')

SET IDENTITY_INSERT [sitebase].[NavigationItem] OFF

UPDATE [sitebase].[NavigationItem] SET [ParentId] = 301, [DisplayOrder] = 1 WHERE [Text] = 'Users'

INSERT INTO [sitebase].[NavigationItem] ([NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url])
	VALUES(1,301,1,'Comment Types',NULL,'/commentTypes')
INSERT INTO [sitebase].[NavigationItem] ([NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url])
	VALUES(1,301,1,'Locations',NULL,'/locations')
INSERT INTO [sitebase].[NavigationItem] ([NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url])
	VALUES(1,301,1,'Races',NULL,'/races')
INSERT INTO [sitebase].[NavigationItem] ([NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url])
	VALUES(1,301,1,'Interviewers',NULL,'/interviewers')
INSERT INTO [sitebase].[NavigationItem] ([NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url])
	VALUES(1,301,1,'Zip Codes',NULL,'/postalCodes')

INSERT INTO [sitebase].[NavigationItem] ([NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url])
	VALUES(1,302,1,'Mississippi Food Network',NULL,'/foodNetworkReport')
INSERT INTO [sitebase].[NavigationItem] ([NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url])
	VALUES(1,302,1,'Detail',NULL,'/detailReport')
INSERT INTO [sitebase].[NavigationItem] ([NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url])
	VALUES(1,302,1,'Visit Counts By Zip Code',NULL,'/zipCodeReport')
INSERT INTO [sitebase].[NavigationItem] ([NavigationId],[ParentId],[DisplayOrder],[Text],[ModuleId],[Url])
	VALUES(1,302,1,'New Clients',NULL,'/newClientReport')

-- authenticated permissions
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/clients',4,3,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/clients/delete',4,3,0)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/clientComments',4,3,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/clientComments/delete',4,3,0)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/clientComments/update',4,3,0)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/household',4,3,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/household/delete',4,3,0)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/visits',4,3,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/visits/delete',4,3,0)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/interviewers/new',4,3,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/interviewers/create',4,3,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/postalCodes/json',4,3,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/locations/county',4,3,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/files/show',4,3,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/files/download',4,3,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/clientReport',4,3,1)

-- administrator permissions
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/manage',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/locations',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/races',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/interviewers',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/postalCodes',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/clients/delete',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/clientComments/delete',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/clientComments/update',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/visits/delete',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/commentTypes',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/household/delete',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/reports',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/foodNetworkReport',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/detailReport',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/zipCodeReport',3,1,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/newClientReport',3,1,1)

-- site manager permissions
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/users',3,2,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/manage',3,2,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/locations',3,2,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/races',3,2,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/interviewers',3,2,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/postalCodes',3,2,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/clients/delete',3,2,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/clientComments/delete',3,2,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/clientComments/update',3,2,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/visits/delete',3,2,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/commentTypes',3,2,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/household/delete',3,2,1)

-- reports permissions
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/reports',3,101,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/foodNetworkReport',3,101,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/detailReport',3,101,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/zipCodeReport',3,101,1)
INSERT [sitebase].[Permission] ([Key1],[Key3],[EntityTypeId],[EntityId],[Mask])
	VALUES('SitePath','/newClientReport',3,101,1)
