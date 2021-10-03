CREATE TABLE [dbo].[User](
	[Code] [varchar](32) NOT NULL,
	[UserName] [varchar](32) NULL,
	[Password] [varchar](124) NULL,
	[FullName] [nvarchar](50) NULL,
	[Dob] [varchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Address] [nvarchar](255) NULL,
	[Avatar] [varchar](max) NULL,
	[Gender] [nvarchar](10) NULL,
	[LastLogin] [datetime] NULL,
	[CurrentSession] [varchar](500) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
)

CREATE TABLE [dbo].[Contact](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserCode] [varchar](32) NOT NULL,
	[UserCode2] [varchar](32) NOT NULL,
	[isMatch] [boolean] NOT NULL,
	[Created] [datetime] NOT NULL
)

CREATE TABLE [dbo].[Group](
	[Code] [varchar](32) NOT NULL,
	[Type] [varchar](32) NOT NULL,
	[Avatar] [varchar](max) NULL,
	[Name] [nvarchar](250) NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [varchar](32) NOT NULL,
	[LastActive] [datetime] NOT NULL
)

CREATE TABLE [dbo].[GroupUser](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupCode] [varchar](32) NOT NULL,
	[UserCode] [varchar](32) NOT NULL
)

CREATE TABLE [dbo].[Message](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](10) NOT NULL,
	[GroupCode] [varchar](32) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[Path] [nvarchar](255) NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [varchar](32) NOT NULL,
)