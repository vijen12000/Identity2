use master
go
drop Database UserManager
go
Create Database UserManager
go
use UserManager
go
CREATE TABLE [dbo].[Users](
	[Id] nvarchar(128) NOT NULL,
	[FullName] [nvarchar](256) NOT NULL,	
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,		
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	CreatedBy nvarchar(128),
	CreatedDate datetime,
	LastUpdatedBy nvarchar(128),
	LastUpdatedDate datetime
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
CREATE TABLE [dbo].[Roles](
	[Id] nvarchar(128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	CreatedBy nvarchar(128),
	CreatedDate datetime,
	LastUpdatedBy nvarchar(128),
	LastUpdatedDate datetime
 CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] nvarchar(128) NOT NULL,
	[RoleId] nvarchar(128) NOT NULL,
 CONSTRAINT [PK_dbo.UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Rights](
	[Id] nvarchar(128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	CreatedBy nvarchar(128),
	CreatedDate datetime,
	LastUpdatedBy nvarchar(128),
	LastUpdatedDate datetime
 CONSTRAINT [PK_dbo.Rights] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
go
--CREATE TABLE [dbo].[UserRights](
--	[UserId] nvarchar(128) NOT NULL,
--	[RightId] nvarchar(128) NOT NULL,
-- CONSTRAINT [PK_dbo.UserRights] PRIMARY KEY CLUSTERED 
--(
--	[UserId] ASC,
--	[RightId] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--) ON [PRIMARY]


----Role Rights --starts
CREATE TABLE [dbo].[RoleRights](
	[RoleId] nvarchar(128) NOT NULL,
	[RightId] nvarchar(128) NOT NULL,
 CONSTRAINT [PK_dbo.RoleRights] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[RightId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[RoleRights]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RoleRights_dbo.Roles_RoleId] FOREIGN KEY(RoleId)
REFERENCES [dbo].Roles ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[RoleRights] CHECK CONSTRAINT [FK_dbo.RoleRights_dbo.Roles_RoleId]
GO

ALTER TABLE [dbo].[RoleRights]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Right_dbo.RightId] FOREIGN KEY([RightId])
REFERENCES [dbo].[Rights] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[RoleRights] CHECK CONSTRAINT [FK_dbo.Right_dbo.RightId]
GO


--ALTER TABLE [dbo].[UserRights]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserRights_dbo.Roles_RoleId] FOREIGN KEY(RightId)
--REFERENCES [dbo].Rights ([Id])
--ON DELETE CASCADE
--GO

--ALTER TABLE [dbo].[UserRights] CHECK CONSTRAINT [FK_dbo.UserRights_dbo.Roles_RoleId]
--GO

--ALTER TABLE [dbo].[UserRights]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserRights_dbo.Users_UserId] FOREIGN KEY([UserId])
--REFERENCES [dbo].[Users] ([Id])
--ON DELETE CASCADE
--GO

--ALTER TABLE [dbo].[UserRights] CHECK CONSTRAINT [FK_dbo.UserRights_dbo.Users_UserId]
--GO


ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserRoles_dbo.Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_dbo.UserRoles_dbo.Roles_RoleId]
GO

ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserRoles_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_dbo.UserRoles_dbo.Users_UserId]
GO

CREATE TABLE [dbo].[UserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserLogins_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO

CREATE TABLE [dbo].[UserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserClaims]  WITH CHECK ADD  CONSTRAINT [FK_UserClaims_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserClaims] CHECK CONSTRAINT [FK_UserClaims_User_UserId]
GO



