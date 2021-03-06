SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Location](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ModificationCounter] [bigint] NOT NULL CONSTRAINT [DF_Location_ModificationCounter] DEFAULT 0,
	[Name] [nvarchar](50) NOT NULL,
	[DisplayOrder] [int] NOT NULL CONSTRAINT [DF_Location_DisplayOrder] DEFAULT 1,
	[County] [nvarchar](50) NULL,
	[AgencyName] [nvarchar](50) NULL,
	[AgencyCode] [nvarchar](50) NULL,
	[ContactName] [nvarchar](100) NULL,
	[AlternateContactName] [nvarchar](100) NULL,
	[AddressId] [bigint] NOT NULL,
	CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY],
	CONSTRAINT [FK_Location_Address] FOREIGN KEY ([AddressId]) REFERENCES [sitebase].[Address] ([Id])
) ON [PRIMARY]
CREATE UNIQUE NONCLUSTERED INDEX [UK_Location_Name] ON [Location] ([Name]) ON [PRIMARY]

CREATE TABLE [Client](
	[ContactId] [bigint] NOT NULL,
	[UniqueId] [nvarchar](100) NULL,
	[Flagged] [bit] NOT NULL CONSTRAINT [DF_Client_Flagged] DEFAULT 0,
	[LocationId] [bigint] NULL,
	[LastVisitDate] [datetime] NULL,
	[AgeBasis] [datetime] NULL,
	[HouseholdCount] [int] NULL,
	[Income] [decimal] NULL,
	[Bibles] [int] NULL,
	[Member] [bit] NOT NULL CONSTRAINT [DF_Client_Member] DEFAULT 0,
	[FoodStamps] [bit] NOT NULL CONSTRAINT [DF_Client_FoodStamps] DEFAULT 0,
	[Tanf] [bit] NOT NULL CONSTRAINT [DF_Client_Tanf] DEFAULT 0,
	[Ssi] [bit] NOT NULL CONSTRAINT [DF_Client_Ssi] DEFAULT 0,
	[Unemployed] [bit] NOT NULL CONSTRAINT [DF_Client_Unemployed] DEFAULT 0,
	[Elderly] [bit] NOT NULL CONSTRAINT [DF_Client_Elderly] DEFAULT 0,
	[SelfDeclared] [bit] NOT NULL CONSTRAINT [DF_Client_SelfDeclared] DEFAULT 0,
	[Repeat] [bit] NOT NULL CONSTRAINT [DF_Client_Repeat] DEFAULT 0,
	[ClientChoice] [bit] NOT NULL CONSTRAINT [DF_Client_ClientChoice] DEFAULT 0,
	CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED ([ContactId]) ON [PRIMARY],
	CONSTRAINT [FK_Client_Contact] FOREIGN KEY ([ContactId]) REFERENCES [sitebase].[Contact] ([PersonId]),
	CONSTRAINT [FK_Client_Location] FOREIGN KEY ([LocationId]) REFERENCES [Location] ([Id])
) ON [PRIMARY]
--CREATE UNIQUE NONCLUSTERED INDEX [UK_Client_UniqueId] ON [Client] ([UniqueId]) ON [PRIMARY]

CREATE TABLE [CommentType](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ModificationCounter] [bigint] NOT NULL CONSTRAINT [DF_CommentType_ModificationCounter] DEFAULT 0,
	[Name] [nvarchar](50) NOT NULL,
	[Flagged] [bit] NOT NULL CONSTRAINT [DF_CommentType_Flagged] DEFAULT 0,
	[DisplayOrder] [int] NOT NULL CONSTRAINT [DF_CommentType_DisplayOrder] DEFAULT 999,
	CONSTRAINT [PK_CommentType] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
) ON [PRIMARY]
CREATE UNIQUE NONCLUSTERED INDEX [UK_CommentType_Name] ON [CommentType] ([Name]) ON [PRIMARY]

CREATE TABLE [ClientComment](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ModificationCounter] [bigint] NOT NULL CONSTRAINT [DF_ClientComment_ModificationCounter] DEFAULT 0,
	[ClientId] [bigint] NOT NULL,
	[CommentTypeId] [bigint] NOT NULL,
	[Text] [nvarchar](MAX) NULL,
	[Date] [datetime] NOT NULL,
	CONSTRAINT [PK_ClientComment] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY],
	CONSTRAINT [FK_ClientComment_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client] ([ContactId]),
	CONSTRAINT [FK_ClientComment_CommentType] FOREIGN KEY ([CommentTypeId]) REFERENCES [CommentType] ([Id])
) ON [PRIMARY]

CREATE TABLE [HouseholdMember](
	[ContactId] [bigint] NOT NULL,
	[ClientId] [bigint] NOT NULL,
	[AgeBasis] [datetime] NULL,
	CONSTRAINT [PK_HouseholdMember] PRIMARY KEY CLUSTERED ([ContactId]) ON [PRIMARY],
	CONSTRAINT [FK_HouseholdMember_Contact] FOREIGN KEY ([ContactId]) REFERENCES [sitebase].[Contact] ([PersonId]),
	CONSTRAINT [FK_HouseholdMember_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client] ([ContactId])
) ON [PRIMARY]

CREATE TABLE [Interviewer](
	[PersonId] [bigint] NOT NULL,
	[Enabled] [bit] NOT NULL CONSTRAINT [DF_Interviewer_Enabled] DEFAULT 1,
	CONSTRAINT [PK_Interviewer] PRIMARY KEY CLUSTERED ([PersonId]) ON [PRIMARY],
	CONSTRAINT [FK_Interviewer_Person] FOREIGN KEY ([PersonId]) REFERENCES [sitebase].[Person] ([Id])
) ON [PRIMARY]

CREATE TABLE [Visit](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ModificationCounter] [bigint] NOT NULL CONSTRAINT [DF_Visit_ModificationCounter] DEFAULT 0,
	[Date] [datetime] NOT NULL,
	[LocationId] [bigint] NOT NULL,
	[ClientId] [bigint] NOT NULL,
	[InterviewerId] [bigint] NULL,
	CONSTRAINT [PK_Visit] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY],
	CONSTRAINT [FK_Visit_Location] FOREIGN KEY ([LocationId]) REFERENCES [Location] ([Id]),
	CONSTRAINT [FK_Visit_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client] ([ContactId]),
	CONSTRAINT [FK_Visit_Interviewer] FOREIGN KEY ([InterviewerId]) REFERENCES [Interviewer] ([PersonId])
) ON [PRIMARY]
