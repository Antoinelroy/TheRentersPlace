USE [master]
GO
/****** Object:  Database [TheRentersPlace]    Script Date: 2021-10-19 10:45:44 AM ******/
CREATE DATABASE [TheRentersPlace]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TheRentersPlace', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TheRentersPlace.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TheRentersPlace_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TheRentersPlace_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TheRentersPlace] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TheRentersPlace].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TheRentersPlace] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TheRentersPlace] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TheRentersPlace] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TheRentersPlace] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TheRentersPlace] SET ARITHABORT OFF 
GO
ALTER DATABASE [TheRentersPlace] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TheRentersPlace] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TheRentersPlace] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TheRentersPlace] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TheRentersPlace] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TheRentersPlace] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TheRentersPlace] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TheRentersPlace] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TheRentersPlace] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TheRentersPlace] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TheRentersPlace] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TheRentersPlace] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TheRentersPlace] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TheRentersPlace] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TheRentersPlace] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TheRentersPlace] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TheRentersPlace] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TheRentersPlace] SET RECOVERY FULL 
GO
ALTER DATABASE [TheRentersPlace] SET  MULTI_USER 
GO
ALTER DATABASE [TheRentersPlace] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TheRentersPlace] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TheRentersPlace] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TheRentersPlace] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TheRentersPlace] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TheRentersPlace] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TheRentersPlace', N'ON'
GO
ALTER DATABASE [TheRentersPlace] SET QUERY_STORE = OFF
GO
USE [TheRentersPlace]
GO
/****** Object:  Table [dbo].[Appartments]    Script Date: 2021-10-19 10:45:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appartments](
	[AppartmentId] [int] IDENTITY(1,1) NOT NULL,
	[ManagerId] [int] NOT NULL,
	[CivicAddress] [nvarchar](max) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[Province] [nvarchar](50) NOT NULL,
	[NoOfBedrooms] [decimal](3, 1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Price] [decimal](7, 2) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[PictureURL] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Appartments] PRIMARY KEY CLUSTERED 
(
	[AppartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Appointments]    Script Date: 2021-10-19 10:45:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointments](
	[AppartmentId] [int] NOT NULL,
	[TenantId] [int] NOT NULL,
	[ManagerId] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[Time] [nvarchar](50) NOT NULL,
	[Purpose] [nvarchar](50) NOT NULL,
	[Note] [nvarchar](50) NULL,
 CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED 
(
	[AppartmentId] ASC,
	[TenantId] ASC,
	[ManagerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Managers]    Script Date: 2021-10-19 10:45:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Managers](
	[ManagerId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Managers] PRIMARY KEY CLUSTERED 
(
	[ManagerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 2021-10-19 10:45:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[MessageId] [int] IDENTITY(1,1) NOT NULL,
	[TenantId] [int] NOT NULL,
	[ManagerId] [int] NOT NULL,
	[SenderUsername] [nvarchar](50) NOT NULL,
	[ReceiverUsername] [nvarchar](50) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Owners]    Script Date: 2021-10-19 10:45:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Owners](
	[OwnerId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Owners] PRIMARY KEY CLUSTERED 
(
	[OwnerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tenants]    Script Date: 2021-10-19 10:45:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tenants](
	[TenantId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Tenants] PRIMARY KEY CLUSTERED 
(
	[TenantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Appartments] ON 

INSERT [dbo].[Appartments] ([AppartmentId], [ManagerId], [CivicAddress], [City], [Province], [NoOfBedrooms], [Type], [Price], [Status], [PictureURL]) VALUES (1, 1, N'7202 Avenue Henri-Julien', N'Montréal', N'QC', CAST(3.5 AS Decimal(3, 1)), N'Duplex', CAST(900.00 AS Decimal(7, 2)), N'Available', N'../Content/Images/appartment1.jpg')
INSERT [dbo].[Appartments] ([AppartmentId], [ManagerId], [CivicAddress], [City], [Province], [NoOfBedrooms], [Type], [Price], [Status], [PictureURL]) VALUES (2, 2, N'3935 rue de Rouen', N'Montréal', N'QC', CAST(4.5 AS Decimal(3, 1)), N'Triplex', CAST(1200.00 AS Decimal(7, 2)), N'Available', N'../Content/Images/appartment2.jpg')
INSERT [dbo].[Appartments] ([AppartmentId], [ManagerId], [CivicAddress], [City], [Province], [NoOfBedrooms], [Type], [Price], [Status], [PictureURL]) VALUES (3, 2, N'2525 rue Saint-Denis', N'Montréal', N'QC', CAST(4.5 AS Decimal(3, 1)), N'Condo', CAST(1600.00 AS Decimal(7, 2)), N'Available', N'../Content/Images/appartment3.jpg')
INSERT [dbo].[Appartments] ([AppartmentId], [ManagerId], [CivicAddress], [City], [Province], [NoOfBedrooms], [Type], [Price], [Status], [PictureURL]) VALUES (4, 3, N'90 rue Beaubien O', N'Montréal', N'QC', CAST(5.5 AS Decimal(3, 1)), N'Condo', CAST(2500.00 AS Decimal(7, 2)), N'Available', N'../Content/Images/appartment5.jpg')
INSERT [dbo].[Appartments] ([AppartmentId], [ManagerId], [CivicAddress], [City], [Province], [NoOfBedrooms], [Type], [Price], [Status], [PictureURL]) VALUES (5, 3, N'1057 Bernard ave', N'Montréal', N'QC', CAST(3.5 AS Decimal(3, 1)), N'Triplex', CAST(1100.00 AS Decimal(7, 2)), N'Available', N'../Content/Images/appartment6.jpg')
INSERT [dbo].[Appartments] ([AppartmentId], [ManagerId], [CivicAddress], [City], [Province], [NoOfBedrooms], [Type], [Price], [Status], [PictureURL]) VALUES (6, 1, N'182 rue Saint-Paul', N'Montréal', N'QC', CAST(4.5 AS Decimal(3, 1)), N'Condo', CAST(2150.00 AS Decimal(7, 2)), N'Available', N'../Content/Images/appartment4.jpg')
INSERT [dbo].[Appartments] ([AppartmentId], [ManagerId], [CivicAddress], [City], [Province], [NoOfBedrooms], [Type], [Price], [Status], [PictureURL]) VALUES (7, 1, N'1960 rue Parthenais', N'Montréal', N'QC', CAST(3.5 AS Decimal(3, 1)), N'Duplex', CAST(750.00 AS Decimal(7, 2)), N'Available', N'../Content/Images/appartment7.jpg')
SET IDENTITY_INSERT [dbo].[Appartments] OFF
GO
INSERT [dbo].[Appointments] ([AppartmentId], [TenantId], [ManagerId], [Date], [Time], [Purpose], [Note]) VALUES (1, 1, 1, CAST(N'2021-10-25' AS Date), N'13:00', N'Visit', NULL)
GO
SET IDENTITY_INSERT [dbo].[Managers] ON 

INSERT [dbo].[Managers] ([ManagerId], [Username], [Password], [FirstName], [LastName], [Email]) VALUES (1, N'Manager1', N'Manager1', N'Man', N'Ager', N'manager1@gmail.com')
INSERT [dbo].[Managers] ([ManagerId], [Username], [Password], [FirstName], [LastName], [Email]) VALUES (2, N'Manager2', N'Manager2', N'Man', N'Ager', N'manager2@gmail.com')
INSERT [dbo].[Managers] ([ManagerId], [Username], [Password], [FirstName], [LastName], [Email]) VALUES (3, N'Manager3', N'Manager3', N'Man', N'Ager', N'manager3@gmail.com')
SET IDENTITY_INSERT [dbo].[Managers] OFF
GO
SET IDENTITY_INSERT [dbo].[Messages] ON 

INSERT [dbo].[Messages] ([MessageId], [TenantId], [ManagerId], [SenderUsername], [ReceiverUsername], [Content]) VALUES (1, 1, 1, N'Tenant1', N'Manager1', N'Hello, this is a test. 3')
INSERT [dbo].[Messages] ([MessageId], [TenantId], [ManagerId], [SenderUsername], [ReceiverUsername], [Content]) VALUES (2, 1, 1, N'Tenant1', N'Manager1', N'Hello, this is a test. 2')
INSERT [dbo].[Messages] ([MessageId], [TenantId], [ManagerId], [SenderUsername], [ReceiverUsername], [Content]) VALUES (3, 1, 2, N'Tenant1', N'Manager2', N'Hello, this is a test.')
INSERT [dbo].[Messages] ([MessageId], [TenantId], [ManagerId], [SenderUsername], [ReceiverUsername], [Content]) VALUES (4, 2, 3, N'Tenant2', N'Manager3', N'Hello I want to rent this appartment, Thank you.')
INSERT [dbo].[Messages] ([MessageId], [TenantId], [ManagerId], [SenderUsername], [ReceiverUsername], [Content]) VALUES (5, 1, 1, N'Tenant1', N'Manager1', N'blalbal')
INSERT [dbo].[Messages] ([MessageId], [TenantId], [ManagerId], [SenderUsername], [ReceiverUsername], [Content]) VALUES (6, 1, 1, N'Manager1', N'Tenant1', N'Ok!')
INSERT [dbo].[Messages] ([MessageId], [TenantId], [ManagerId], [SenderUsername], [ReceiverUsername], [Content]) VALUES (7, 1, 3, N'Tenant1', N'Manager3', N'Ok!')
INSERT [dbo].[Messages] ([MessageId], [TenantId], [ManagerId], [SenderUsername], [ReceiverUsername], [Content]) VALUES (8, 1, 1, N'Manager1', N'Tenant1', N'sdasda')
SET IDENTITY_INSERT [dbo].[Messages] OFF
GO
SET IDENTITY_INSERT [dbo].[Owners] ON 

INSERT [dbo].[Owners] ([OwnerId], [Username], [Password]) VALUES (1, N'Owner1', N'Owner1')
SET IDENTITY_INSERT [dbo].[Owners] OFF
GO
SET IDENTITY_INSERT [dbo].[Tenants] ON 

INSERT [dbo].[Tenants] ([TenantId], [Username], [Password], [FirstName], [LastName], [Email]) VALUES (1, N'Tenant1', N'Tenant1', N'Ten', N'Ant', N'tenant1@gmail.com')
INSERT [dbo].[Tenants] ([TenantId], [Username], [Password], [FirstName], [LastName], [Email]) VALUES (2, N'Tenant2', N'Tenant2', N'Ten', N'Ant', N'tenant2@gmail.com')
SET IDENTITY_INSERT [dbo].[Tenants] OFF
GO
ALTER TABLE [dbo].[Appartments]  WITH CHECK ADD  CONSTRAINT [FK_Appartments_Managers] FOREIGN KEY([ManagerId])
REFERENCES [dbo].[Managers] ([ManagerId])
GO
ALTER TABLE [dbo].[Appartments] CHECK CONSTRAINT [FK_Appartments_Managers]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Appartments] FOREIGN KEY([AppartmentId])
REFERENCES [dbo].[Appartments] ([AppartmentId])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Appartments]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Managers] FOREIGN KEY([ManagerId])
REFERENCES [dbo].[Managers] ([ManagerId])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Managers]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Tenants] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants] ([TenantId])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Tenants]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Managers] FOREIGN KEY([ManagerId])
REFERENCES [dbo].[Managers] ([ManagerId])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Managers]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Tenants] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants] ([TenantId])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Tenants]
GO
USE [master]
GO
ALTER DATABASE [TheRentersPlace] SET  READ_WRITE 
GO
