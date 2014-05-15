UPDATE [sitebase].[Preference] SET [Key] = 'DigitalBeacon.CareCenter.DefaultLocationId' WHERE [Key] = 'CareCenter.DefaultLocationId'

INSERT INTO [sitebase].[Preference]([AssociationId],[Key],[Value]) VALUES(1, 'DigitalBeacon.CareCenter.DaysBeforeInactive', '0')
