CREATE DATABASE [VChat]
GO

USE [VChat]
GO

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
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[Contact](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserCode] [varchar](32) NOT NULL,
	[ContactCode] [varchar](32) NOT NULL,
	[isMatch] [bit] NOT NULL,
	[Created] [datetime] NOT NULL
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Group](
	[Code] [varchar](32) NOT NULL,
	[Type] [varchar](32) NOT NULL,
	[Avatar] [varchar](max) NULL,
	[Name] [nvarchar](250) NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [varchar](32) NOT NULL,
	[LastActive] [datetime] NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[GroupUser](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupCode] [varchar](32) NOT NULL,
	[UserCode] [varchar](32) NOT NULL,
 CONSTRAINT [PK_GroupUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Message](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](10) NOT NULL,
	[GroupCode] [varchar](32) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[Path] [nvarchar](255) NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [varchar](32) NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[Admin](
	[Code] [varchar](32) NOT NULL,
	[UserName] [varchar](32) NOT NULL,
	[Password] [varchar](124) NULL,
	[FullName] [nvarchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Role] [int] NOT NULL,		/*1: Admin, 0: Supporter*/
	[LastLogin] [datetime] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_User] FOREIGN KEY([UserCode])
REFERENCES [dbo].[User] ([Code])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_User]
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_User1] FOREIGN KEY([ContactCode])
REFERENCES [dbo].[User] ([Code])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_User1]
GO
ALTER TABLE [dbo].[GroupUser]  WITH CHECK ADD  CONSTRAINT [FK_GroupUser_Group] FOREIGN KEY([GroupCode])
REFERENCES [dbo].[Group] ([Code])
GO
ALTER TABLE [dbo].[GroupUser] CHECK CONSTRAINT [FK_GroupUser_Group]
GO
ALTER TABLE [dbo].[GroupUser]  WITH CHECK ADD  CONSTRAINT [FK_GroupUser_GroupUser] FOREIGN KEY([Id])
REFERENCES [dbo].[GroupUser] ([Id])
GO
ALTER TABLE [dbo].[GroupUser] CHECK CONSTRAINT [FK_GroupUser_GroupUser]
GO
ALTER TABLE [dbo].[GroupUser]  WITH CHECK ADD  CONSTRAINT [FK_GroupUser_User] FOREIGN KEY([UserCode])
REFERENCES [dbo].[User] ([Code])
GO
ALTER TABLE [dbo].[GroupUser] CHECK CONSTRAINT [FK_GroupUser_User]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_Group] FOREIGN KEY([GroupCode])
REFERENCES [dbo].[Group] ([Code])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_Group]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([Code])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_User]
GO