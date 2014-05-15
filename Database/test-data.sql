-- demo admin

INSERT INTO [sitebase].[Person]([Created], [FirstName], [LastName])
	VALUES(getdate(), 'Admin', 'Demo')

INSERT INTO [sitebase].[User]([Username], [DisplayName], [Email], [PersonId])
	SELECT 'demo.admin', 'Demo Admin', 'demo.admin@digitalbeacon.net', MAX([Id]) FROM [sitebase].[Person]

INSERT INTO [sitebase].[UserAssociation]([UserId], [AssociationId])
	SELECT MAX([Id]), 1 FROM [sitebase].[User]	

INSERT INTO [sitebase].[UserRole]([UserId], [RoleId], [AssociationId])
	SELECT MAX([Id]), 1, 1 FROM [sitebase].[User]

-- demo user

INSERT INTO [sitebase].[Person]([Created], [FirstName], [LastName])
	VALUES(getdate(), 'User', 'Demo')

INSERT INTO [sitebase].[User]([Username], [DisplayName], [Email], [PersonId])
	SELECT 'demo.user', 'Demo User', 'demo.user@digitalbeacon.net', MAX([Id]) FROM [sitebase].[Person]

INSERT INTO [sitebase].[UserAssociation]([UserId], [AssociationId])
	SELECT MAX([Id]), 1 FROM [sitebase].[User]	

INSERT INTO [sitebase].[UserRole]([UserId], [RoleId], [AssociationId])
	SELECT MAX([Id]), 3, 1 FROM [sitebase].[User]

-- test client

INSERT INTO [sitebase].[Address]([Email])
	VALUES(NULL)

INSERT INTO [sitebase].[Person]([AddressId], [Created], [FirstName], [LastName], [RaceId], [DateOfBirth])
	SELECT MAX([Id]), getdate(), 'Test', 'Client', 1, '1/1/1980' FROM [sitebase].[Address]

INSERT INTO [sitebase].[Contact]([PersonId], [AssociationId])
	SELECT MAX([Id]), 1 FROM [sitebase].[Person]

INSERT INTO [Client]([ContactId], [LastVisitDate], [LocationId])
	SELECT MAX([PersonId]), getdate(), 1 FROM [sitebase].[Contact]

-- test visit

INSERT INTO [Visit]([ClientId], [LocationId], [Date])
	SELECT MAX([ContactId]), 1, getdate() FROM [Client]

-- configure demo usernames and login content

INSERT INTO [sitebase].[ModuleSettingDefinition]([ModuleDefinitionId],[ModuleSettingTypeId],[DisplayOrder],[Global],[Localizable],[Key],[Name],[IntroducedInVersion],[DefaultSubject],[DefaultValue])
	VALUES(1, 1, 3, 1, 0, 'Demo.Usernames', 'Demo Usernames', '1.0.0', NULL, 'demo.admin,demo.user')

INSERT INTO [sitebase].[ModuleSettingDefinition]([ModuleDefinitionId],[ModuleSettingTypeId],[DisplayOrder],[Global],[Localizable],[Key],[Name],[IntroducedInVersion],[DefaultSubject],[DefaultValue])
	VALUES(1, 6, 4, 1, 1, 'Login.Content', 'Login Content', '1.0.0', 'Demo Sign In', '<p>  <a href="#" id="demoUserLink" style="color:black">Sign In as a Demo User</a></p> <p>  <a href="#" id="demoAdminLink" style="color:black">Sign In as a Demo Administrator</a></p>')

-- hide food network report

UPDATE [sitebase].[NavigationItem] SET [Enabled] = 0 WHERE [Url] = '/foodNetworkReport'
