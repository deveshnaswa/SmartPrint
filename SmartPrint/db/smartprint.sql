/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.4001)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [master]
GO
/****** Object:  Database [SmartPrint]    Script Date: 9/28/2017 12:17:35 PM ******/
CREATE DATABASE [SmartPrint]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SmartPrint', FILENAME = N'C:\Users\Devesh\SmartPrint.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SmartPrint_log', FILENAME = N'C:\Users\Devesh\SmartPrint_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [SmartPrint] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SmartPrint].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SmartPrint] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SmartPrint] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SmartPrint] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SmartPrint] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SmartPrint] SET ARITHABORT OFF 
GO
ALTER DATABASE [SmartPrint] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [SmartPrint] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SmartPrint] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SmartPrint] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SmartPrint] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SmartPrint] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SmartPrint] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SmartPrint] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SmartPrint] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SmartPrint] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SmartPrint] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SmartPrint] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SmartPrint] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SmartPrint] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SmartPrint] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SmartPrint] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SmartPrint] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SmartPrint] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SmartPrint] SET  MULTI_USER 
GO
ALTER DATABASE [SmartPrint] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SmartPrint] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SmartPrint] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SmartPrint] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SmartPrint] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SmartPrint] SET QUERY_STORE = OFF
GO
USE [SmartPrint]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [SmartPrint]
GO
/****** Object:  Table [dbo].[Configurations]    Script Date: 9/28/2017 12:17:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configurations](
	[ConfigId] [int] IDENTITY(1,1) NOT NULL,
	[ConfigKey] [nvarchar](50) NOT NULL,
	[ConfigVal] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ConfigId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocTypes]    Script Date: 9/28/2017 12:17:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocTypes](
	[DocTypeId] [int] IDENTITY(1,1) NOT NULL,
	[DocType] [nvarchar](50) NOT NULL,
	[DocExt] [nvarchar](50) NOT NULL,
	[DocIcon] [nvarchar](50) NULL,
	[IsActive] [int] NOT NULL,
	[AddedBy] [int] NOT NULL,
	[AddedOn] [datetime2](7) NULL,
	[EditedBy] [int] NOT NULL,
	[EditedOn] [datetime2](7) NULL,
	[StatusId] [int] NOT NULL,
 CONSTRAINT [PK__DocTypes__055E26A331353D5A] PRIMARY KEY CLUSTERED 
(
	[DocTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PrintCosts]    Script Date: 9/28/2017 12:17:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrintCosts](
	[PrintCostId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Width] [decimal](18, 2) NOT NULL,
	[Height] [decimal](18, 2) NOT NULL,
	[MonoCostPerPage] [decimal](18, 2) NOT NULL,
	[ColorCostPerPage] [decimal](18, 2) NOT NULL,
	[IsActive] [int] NOT NULL,
	[AddedBy] [int] NOT NULL,
	[AddedOn] [datetime2](7) NULL,
	[EditedBy] [int] NOT NULL,
	[EditedOn] [datetime2](7) NULL,
	[StatusId] [int] NOT NULL,
 CONSTRAINT [PK__tmp_ms_x__C4718F8955EBCF62] PRIMARY KEY CLUSTERED 
(
	[PrintCostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PrintJobs]    Script Date: 9/28/2017 12:17:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrintJobs](
	[JobId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[DocId] [int] NOT NULL,
	[DocName] [nvarchar](250) NOT NULL,
	[DocTypeId] [int] NOT NULL,
	[DocExt] [nvarchar](50) NOT NULL,
	[DocFileNameOnServer] [nvarchar](250) NOT NULL,
	[DocFilePath] [nvarchar](255) NOT NULL,
	[DocTotalPages] [int] NOT NULL,
	[PrintCostId] [int] NOT NULL,
	[MonoUnitCost] [decimal](18, 2) NOT NULL,
	[ColorUnitCost] [decimal](18, 2) NOT NULL,
	[MonoPages] [int] NOT NULL,
	[ColorPages] [int] NOT NULL,
	[PrinterName] [nvarchar](250) NULL,
	[PrinterPath] [nvarchar](250) NULL,
	[IsColor] [bit] NOT NULL,
	[IsDuplex] [bit] NOT NULL,
	[IsCollate] [bit] NOT NULL,
	[PagesFrom] [int] NOT NULL,
	[PagesTo] [int] NOT NULL,
	[NumCopies] [int] NOT NULL,
	[UnitCost] [decimal](18, 2) NOT NULL,
	[UnitItem] [int] NOT NULL,
	[JobRemarks] [nvarchar](250) NULL,
	[TotalPageCount] [int] NOT NULL,
	[TotalPageCost] [decimal](18, 2) NOT NULL,
	[CreditUsed] [decimal](18, 2) NULL,
	[PrintJobQueueRefId] [int] NOT NULL,
	[JobError] [nvarchar](50) NULL,
	[JobErrorRemarks] [nvarchar](50) NULL,
	[JobStatusId] [int] NOT NULL,
	[AddedBy] [int] NOT NULL,
	[AddedOn] [datetime2](7) NULL,
	[EditedBy] [int] NULL,
	[EditedOn] [datetime2](7) NULL,
	[StatusId] [int] NOT NULL,
 CONSTRAINT [PK__tmp_ms_x__056690C28EE2CCBD] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RStatus]    Script Date: 9/28/2017 12:17:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RStatus](
	[StatusId] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TTypes]    Script Date: 9/28/2017 12:17:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TTypes](
	[TxnTypeId] [int] IDENTITY(1,1) NOT NULL,
	[TxnTypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TxnTypes] PRIMARY KEY CLUSTERED 
(
	[TxnTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TxnStatus]    Script Date: 9/28/2017 12:17:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TxnStatus](
	[StatusId] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](50) NULL,
 CONSTRAINT [PK_TxnStatus] PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDocs]    Script Date: 9/28/2017 12:17:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDocs](
	[DocId] [int] IDENTITY(1,1) NOT NULL,
	[DocName] [nvarchar](50) NULL,
	[DocTypeId] [int] NOT NULL,
	[DocExt] [nvarchar](50) NULL,
	[DocFileName] [nvarchar](200) NULL,
	[DocFilePath] [nvarchar](200) NULL,
	[UserId] [int] NOT NULL,
	[DocCreatedDate] [datetime2](7) NOT NULL,
	[AddedBy] [int] NOT NULL,
	[AddedOn] [datetime2](7) NULL,
	[EditedBy] [int] NOT NULL,
	[EditedOn] [datetime2](7) NULL,
	[StatusId] [int] NOT NULL,
 CONSTRAINT [PK_UserDocs] PRIMARY KEY CLUSTERED 
(
	[DocId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/28/2017 12:17:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FName] [nvarchar](50) NULL,
	[LName] [nvarchar](50) NULL,
	[UserTypeId] [int] NOT NULL,
	[UserCode] [nvarchar](50) NULL,
	[UserEmail] [nvarchar](50) NOT NULL,
	[UserPass] [nvarchar](50) NOT NULL,
	[UserPhone] [nvarchar](50) NULL,
	[UStatusId] [int] NOT NULL,
	[AddedBy] [int] NOT NULL,
	[AddedOn] [datetime2](7) NULL,
	[EditedBy] [int] NOT NULL,
	[EditedOn] [datetime2](7) NULL,
	[StatusId] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTxns]    Script Date: 9/28/2017 12:17:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTxns](
	[TxnId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TxnTypeId] [int] NOT NULL,
	[TxnAmount] [decimal](18, 2) NOT NULL,
	[TxnDateTime] [datetime2](7) NOT NULL,
	[TxnBalance] [decimal](18, 2) NOT NULL,
	[TxnRefJobId] [int] NOT NULL,
	[TxnStatusId] [int] NOT NULL,
	[AddedBy] [int] NOT NULL,
	[AddedOn] [datetime2](7) NULL,
	[EditedBy] [int] NOT NULL,
	[EditedOn] [datetime2](7) NULL,
	[StatusId] [int] NOT NULL,
 CONSTRAINT [PK__UserTxn__C19608744E16DFE6] PRIMARY KEY CLUSTERED 
(
	[TxnId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTypes]    Script Date: 9/28/2017 12:17:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTypes](
	[UserTypeId] [int] IDENTITY(1,1) NOT NULL,
	[UserType] [nvarchar](50) NOT NULL,
	[AddedBy] [int] NOT NULL,
	[AddedOn] [datetime2](7) NULL,
	[EditedBy] [int] NOT NULL,
	[EditedOn] [datetime2](7) NULL,
	[StatusId] [int] NOT NULL,
 CONSTRAINT [PK__UserType__40D2D81607FCD816] PRIMARY KEY CLUSTERED 
(
	[UserTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UStatus]    Script Date: 9/28/2017 12:17:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UStatus](
	[UStatusId] [int] IDENTITY(1,1) NOT NULL,
	[UStatusName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UStatus] PRIMARY KEY CLUSTERED 
(
	[UStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DocTypes] ON 
GO
INSERT [dbo].[DocTypes] ([DocTypeId], [DocType], [DocExt], [DocIcon], [IsActive], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (1, N'Word', N'doc', N'docico', 1, 1, CAST(N'2017-09-13T22:01:34.2682896' AS DateTime2), 1, CAST(N'2017-09-14T12:26:05.4604365' AS DateTime2), 1)
GO
SET IDENTITY_INSERT [dbo].[DocTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[PrintCosts] ON 
GO
INSERT [dbo].[PrintCosts] ([PrintCostId], [Name], [Width], [Height], [MonoCostPerPage], [ColorCostPerPage], [IsActive], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (1, N'Legal', CAST(7.00 AS Decimal(18, 2)), CAST(8.00 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), 1, 1, CAST(N'2017-09-13T22:05:50.5723317' AS DateTime2), 1, CAST(N'2017-09-13T22:55:13.6647640' AS DateTime2), 1)
GO
INSERT [dbo].[PrintCosts] ([PrintCostId], [Name], [Width], [Height], [MonoCostPerPage], [ColorCostPerPage], [IsActive], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (3, N'A1', CAST(23.00 AS Decimal(18, 2)), CAST(33.00 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), 1, 1, CAST(N'2017-09-13T22:05:50.5723317' AS DateTime2), 1, CAST(N'2017-09-13T22:05:50.5723317' AS DateTime2), 1)
GO
INSERT [dbo].[PrintCosts] ([PrintCostId], [Name], [Width], [Height], [MonoCostPerPage], [ColorCostPerPage], [IsActive], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (4, N'A3', CAST(11.00 AS Decimal(18, 2)), CAST(18.00 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), 1, 1, CAST(N'2017-09-13T22:05:50.5723317' AS DateTime2), 1, CAST(N'2017-09-13T22:05:50.5723317' AS DateTime2), 1)
GO
SET IDENTITY_INSERT [dbo].[PrintCosts] OFF
GO
SET IDENTITY_INSERT [dbo].[PrintJobs] ON 
GO
INSERT [dbo].[PrintJobs] ([JobId], [UserId], [DocId], [DocName], [DocTypeId], [DocExt], [DocFileNameOnServer], [DocFilePath], [DocTotalPages], [PrintCostId], [MonoUnitCost], [ColorUnitCost], [MonoPages], [ColorPages], [PrinterName], [PrinterPath], [IsColor], [IsDuplex], [IsCollate], [PagesFrom], [PagesTo], [NumCopies], [UnitCost], [UnitItem], [JobRemarks], [TotalPageCount], [TotalPageCost], [CreditUsed], [PrintJobQueueRefId], [JobError], [JobErrorRemarks], [JobStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (1, 1002, 1, N'test', 1, N'doc', N'1010', N'c:\documents', 4, 1, CAST(111.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 1, 0, N'111', N'111', 0, 0, 0, 1, 111, 1, CAST(100.00 AS Decimal(18, 2)), 111, N'kkkkkkkkkk', 111, CAST(111.00 AS Decimal(18, 2)), CAST(111.00 AS Decimal(18, 2)), 0, NULL, NULL, 1, 1, CAST(N'2017-09-13T23:12:32.5263143' AS DateTime2), 1, CAST(N'2017-09-14T13:34:16.1795037' AS DateTime2), 1)
GO
INSERT [dbo].[PrintJobs] ([JobId], [UserId], [DocId], [DocName], [DocTypeId], [DocExt], [DocFileNameOnServer], [DocFilePath], [DocTotalPages], [PrintCostId], [MonoUnitCost], [ColorUnitCost], [MonoPages], [ColorPages], [PrinterName], [PrinterPath], [IsColor], [IsDuplex], [IsCollate], [PagesFrom], [PagesTo], [NumCopies], [UnitCost], [UnitItem], [JobRemarks], [TotalPageCount], [TotalPageCost], [CreditUsed], [PrintJobQueueRefId], [JobError], [JobErrorRemarks], [JobStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (2, 1, 4, N'Test document', 1, N'.docx', N'3e0ce247-2d46-469b-810e-90d40fd8e734.docx', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\3e0ce247-2d46-469b-810e-90d40fd8e734.docx', 0, 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, N'Send To OneNote 2010', NULL, 0, 0, 0, 0, 0, 0, CAST(0.00 AS Decimal(18, 2)), 0, NULL, 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, NULL, NULL, 0, 1, CAST(N'2017-09-22T17:55:18.7173657' AS DateTime2), 1, CAST(N'2017-09-22T17:55:18.7173657' AS DateTime2), 0)
GO
INSERT [dbo].[PrintJobs] ([JobId], [UserId], [DocId], [DocName], [DocTypeId], [DocExt], [DocFileNameOnServer], [DocFilePath], [DocTotalPages], [PrintCostId], [MonoUnitCost], [ColorUnitCost], [MonoPages], [ColorPages], [PrinterName], [PrinterPath], [IsColor], [IsDuplex], [IsCollate], [PagesFrom], [PagesTo], [NumCopies], [UnitCost], [UnitItem], [JobRemarks], [TotalPageCount], [TotalPageCost], [CreditUsed], [PrintJobQueueRefId], [JobError], [JobErrorRemarks], [JobStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (3, 1, 4, N'Test document', 1, N'.docx', N'3e0ce247-2d46-469b-810e-90d40fd8e734.docx', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\3e0ce247-2d46-469b-810e-90d40fd8e734.docx', 0, 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, N'Send To OneNote 2010', NULL, 0, 0, 0, 0, 0, 0, CAST(0.00 AS Decimal(18, 2)), 0, NULL, 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, NULL, NULL, 0, 1, CAST(N'2017-09-22T17:55:30.3963692' AS DateTime2), 1, CAST(N'2017-09-22T17:55:30.3963692' AS DateTime2), 0)
GO
INSERT [dbo].[PrintJobs] ([JobId], [UserId], [DocId], [DocName], [DocTypeId], [DocExt], [DocFileNameOnServer], [DocFilePath], [DocTotalPages], [PrintCostId], [MonoUnitCost], [ColorUnitCost], [MonoPages], [ColorPages], [PrinterName], [PrinterPath], [IsColor], [IsDuplex], [IsCollate], [PagesFrom], [PagesTo], [NumCopies], [UnitCost], [UnitItem], [JobRemarks], [TotalPageCount], [TotalPageCost], [CreditUsed], [PrintJobQueueRefId], [JobError], [JobErrorRemarks], [JobStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (4, 1, 4, N'Test document', 1, N'.docx', N'3e0ce247-2d46-469b-810e-90d40fd8e734.docx', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\3e0ce247-2d46-469b-810e-90d40fd8e734.docx', 0, 0, CAST(3.00 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), 0, 0, N'Microsoft Print to PDF', NULL, 1, 0, 0, 1, 7, 1, CAST(0.00 AS Decimal(18, 2)), 1, N'print to pdf values with color printing true', 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, NULL, NULL, 0, 1, CAST(N'2017-09-23T12:27:20.9634491' AS DateTime2), 1, CAST(N'2017-09-23T12:27:20.9634491' AS DateTime2), 0)
GO
INSERT [dbo].[PrintJobs] ([JobId], [UserId], [DocId], [DocName], [DocTypeId], [DocExt], [DocFileNameOnServer], [DocFilePath], [DocTotalPages], [PrintCostId], [MonoUnitCost], [ColorUnitCost], [MonoPages], [ColorPages], [PrinterName], [PrinterPath], [IsColor], [IsDuplex], [IsCollate], [PagesFrom], [PagesTo], [NumCopies], [UnitCost], [UnitItem], [JobRemarks], [TotalPageCount], [TotalPageCost], [CreditUsed], [PrintJobQueueRefId], [JobError], [JobErrorRemarks], [JobStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (5, 1, 4, N'Test document', 1, N'.docx', N'3e0ce247-2d46-469b-810e-90d40fd8e734.docx', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\3e0ce247-2d46-469b-810e-90d40fd8e734.docx', 0, 3, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, N'Microsoft Print to PDF', NULL, 1, 0, 0, 1, 10, 2, CAST(0.00 AS Decimal(18, 2)), 1, N'color printing', 20, CAST(40.00 AS Decimal(18, 2)), CAST(40.00 AS Decimal(18, 2)), 0, NULL, NULL, 0, 1, CAST(N'2017-09-24T17:09:52.2156387' AS DateTime2), 1, CAST(N'2017-09-24T17:09:52.2156387' AS DateTime2), 0)
GO
INSERT [dbo].[PrintJobs] ([JobId], [UserId], [DocId], [DocName], [DocTypeId], [DocExt], [DocFileNameOnServer], [DocFilePath], [DocTotalPages], [PrintCostId], [MonoUnitCost], [ColorUnitCost], [MonoPages], [ColorPages], [PrinterName], [PrinterPath], [IsColor], [IsDuplex], [IsCollate], [PagesFrom], [PagesTo], [NumCopies], [UnitCost], [UnitItem], [JobRemarks], [TotalPageCount], [TotalPageCost], [CreditUsed], [PrintJobQueueRefId], [JobError], [JobErrorRemarks], [JobStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (6, 1, 4, N'Test document', 1, N'.docx', N'3e0ce247-2d46-469b-810e-90d40fd8e734.docx', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\3e0ce247-2d46-469b-810e-90d40fd8e734.docx', 0, 1, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, N'Microsoft Print to PDF', NULL, 0, 0, 0, 1, 5, 1, CAST(0.00 AS Decimal(18, 2)), 0, N'color printing', 5, CAST(10.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), 0, NULL, NULL, 0, 1, CAST(N'2017-09-24T22:03:57.8069878' AS DateTime2), 1, CAST(N'2017-09-24T22:03:57.8069878' AS DateTime2), 0)
GO
INSERT [dbo].[PrintJobs] ([JobId], [UserId], [DocId], [DocName], [DocTypeId], [DocExt], [DocFileNameOnServer], [DocFilePath], [DocTotalPages], [PrintCostId], [MonoUnitCost], [ColorUnitCost], [MonoPages], [ColorPages], [PrinterName], [PrinterPath], [IsColor], [IsDuplex], [IsCollate], [PagesFrom], [PagesTo], [NumCopies], [UnitCost], [UnitItem], [JobRemarks], [TotalPageCount], [TotalPageCost], [CreditUsed], [PrintJobQueueRefId], [JobError], [JobErrorRemarks], [JobStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (7, 1, 5, N'my pdf', 1, N'.pdf', N'239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', 0, 3, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, N'Microsoft Print to PDF', NULL, 0, 0, 0, 1, 3, 1, CAST(0.00 AS Decimal(18, 2)), 0, N'color printing', 3, CAST(9.00 AS Decimal(18, 2)), CAST(9.00 AS Decimal(18, 2)), 0, NULL, NULL, 0, 1, CAST(N'2017-09-24T22:05:52.6170163' AS DateTime2), 1, CAST(N'2017-09-24T22:05:52.6170163' AS DateTime2), 0)
GO
INSERT [dbo].[PrintJobs] ([JobId], [UserId], [DocId], [DocName], [DocTypeId], [DocExt], [DocFileNameOnServer], [DocFilePath], [DocTotalPages], [PrintCostId], [MonoUnitCost], [ColorUnitCost], [MonoPages], [ColorPages], [PrinterName], [PrinterPath], [IsColor], [IsDuplex], [IsCollate], [PagesFrom], [PagesTo], [NumCopies], [UnitCost], [UnitItem], [JobRemarks], [TotalPageCount], [TotalPageCost], [CreditUsed], [PrintJobQueueRefId], [JobError], [JobErrorRemarks], [JobStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (8, 1, 5, N'my pdf', 1, N'.pdf', N'239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', 0, 1, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, N'Microsoft Print to PDF', NULL, 0, 0, 0, 1, 2, 1, CAST(0.00 AS Decimal(18, 2)), 0, N'color printing', 2, CAST(4.00 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)), 0, NULL, NULL, 0, 1, CAST(N'2017-09-24T22:20:58.9715737' AS DateTime2), 1, CAST(N'2017-09-24T22:20:58.9715737' AS DateTime2), 0)
GO
INSERT [dbo].[PrintJobs] ([JobId], [UserId], [DocId], [DocName], [DocTypeId], [DocExt], [DocFileNameOnServer], [DocFilePath], [DocTotalPages], [PrintCostId], [MonoUnitCost], [ColorUnitCost], [MonoPages], [ColorPages], [PrinterName], [PrinterPath], [IsColor], [IsDuplex], [IsCollate], [PagesFrom], [PagesTo], [NumCopies], [UnitCost], [UnitItem], [JobRemarks], [TotalPageCount], [TotalPageCost], [CreditUsed], [PrintJobQueueRefId], [JobError], [JobErrorRemarks], [JobStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (9, 1, 5, N'my pdf', 1, N'.pdf', N'239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', 0, 1, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, N'Microsoft Print to PDF', NULL, 0, 0, 0, 1, 2, 1, CAST(0.00 AS Decimal(18, 2)), 0, NULL, 2, CAST(4.00 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)), 0, NULL, NULL, 0, 1, CAST(N'2017-09-24T23:25:05.3197233' AS DateTime2), 1, CAST(N'2017-09-24T23:25:05.3197233' AS DateTime2), 0)
GO
INSERT [dbo].[PrintJobs] ([JobId], [UserId], [DocId], [DocName], [DocTypeId], [DocExt], [DocFileNameOnServer], [DocFilePath], [DocTotalPages], [PrintCostId], [MonoUnitCost], [ColorUnitCost], [MonoPages], [ColorPages], [PrinterName], [PrinterPath], [IsColor], [IsDuplex], [IsCollate], [PagesFrom], [PagesTo], [NumCopies], [UnitCost], [UnitItem], [JobRemarks], [TotalPageCount], [TotalPageCost], [CreditUsed], [PrintJobQueueRefId], [JobError], [JobErrorRemarks], [JobStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (10, 1, 5, N'my pdf', 1, N'.pdf', N'239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', 0, 1, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, N'Microsoft Print to PDF', NULL, 1, 0, 0, 1, 1, 1, CAST(0.00 AS Decimal(18, 2)), 0, NULL, 1, CAST(1.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), 0, NULL, NULL, 0, 1, CAST(N'2017-09-25T01:45:42.9231540' AS DateTime2), 1, CAST(N'2017-09-25T01:45:42.9231540' AS DateTime2), 0)
GO
INSERT [dbo].[PrintJobs] ([JobId], [UserId], [DocId], [DocName], [DocTypeId], [DocExt], [DocFileNameOnServer], [DocFilePath], [DocTotalPages], [PrintCostId], [MonoUnitCost], [ColorUnitCost], [MonoPages], [ColorPages], [PrinterName], [PrinterPath], [IsColor], [IsDuplex], [IsCollate], [PagesFrom], [PagesTo], [NumCopies], [UnitCost], [UnitItem], [JobRemarks], [TotalPageCount], [TotalPageCost], [CreditUsed], [PrintJobQueueRefId], [JobError], [JobErrorRemarks], [JobStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (11, 1, 5, N'my pdf', 1, N'.pdf', N'239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', 0, 3, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, N'Microsoft Print to PDF', NULL, 0, 0, 0, 1, 1, 1, CAST(0.00 AS Decimal(18, 2)), 0, NULL, 1, CAST(3.00 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), 0, NULL, NULL, 0, 1, CAST(N'2017-09-25T01:55:44.0589435' AS DateTime2), 1, CAST(N'2017-09-25T01:55:44.0589435' AS DateTime2), 0)
GO
INSERT [dbo].[PrintJobs] ([JobId], [UserId], [DocId], [DocName], [DocTypeId], [DocExt], [DocFileNameOnServer], [DocFilePath], [DocTotalPages], [PrintCostId], [MonoUnitCost], [ColorUnitCost], [MonoPages], [ColorPages], [PrinterName], [PrinterPath], [IsColor], [IsDuplex], [IsCollate], [PagesFrom], [PagesTo], [NumCopies], [UnitCost], [UnitItem], [JobRemarks], [TotalPageCount], [TotalPageCost], [CreditUsed], [PrintJobQueueRefId], [JobError], [JobErrorRemarks], [JobStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (12, 1, 5, N'my pdf', 1, N'.pdf', N'239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', 0, 1, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, N'HP LaserJet Professional M1213nf MFP', NULL, 1, 1, 0, 1, 2, 1, CAST(0.00 AS Decimal(18, 2)), 0, N'color printing', 2, CAST(2.00 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), 0, NULL, NULL, 0, 1, CAST(N'2017-09-26T14:13:08.0160391' AS DateTime2), 1, CAST(N'2017-09-26T14:13:08.0160391' AS DateTime2), 0)
GO
INSERT [dbo].[PrintJobs] ([JobId], [UserId], [DocId], [DocName], [DocTypeId], [DocExt], [DocFileNameOnServer], [DocFilePath], [DocTotalPages], [PrintCostId], [MonoUnitCost], [ColorUnitCost], [MonoPages], [ColorPages], [PrinterName], [PrinterPath], [IsColor], [IsDuplex], [IsCollate], [PagesFrom], [PagesTo], [NumCopies], [UnitCost], [UnitItem], [JobRemarks], [TotalPageCount], [TotalPageCost], [CreditUsed], [PrintJobQueueRefId], [JobError], [JobErrorRemarks], [JobStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (13, 1, 5, N'my pdf', 1, N'.pdf', N'239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', 0, 1, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, N'HP LaserJet Professional M1213nf MFP', NULL, 1, 1, 0, 1, 1, 1, CAST(0.00 AS Decimal(18, 2)), 0, N'color printing', 1, CAST(1.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), 0, NULL, NULL, 0, 1, CAST(N'2017-09-26T14:36:50.7554919' AS DateTime2), 1, CAST(N'2017-09-26T14:36:50.7564920' AS DateTime2), 0)
GO
INSERT [dbo].[PrintJobs] ([JobId], [UserId], [DocId], [DocName], [DocTypeId], [DocExt], [DocFileNameOnServer], [DocFilePath], [DocTotalPages], [PrintCostId], [MonoUnitCost], [ColorUnitCost], [MonoPages], [ColorPages], [PrinterName], [PrinterPath], [IsColor], [IsDuplex], [IsCollate], [PagesFrom], [PagesTo], [NumCopies], [UnitCost], [UnitItem], [JobRemarks], [TotalPageCount], [TotalPageCost], [CreditUsed], [PrintJobQueueRefId], [JobError], [JobErrorRemarks], [JobStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (14, 1, 5, N'my pdf', 1, N'.pdf', N'239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', 0, 1, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, N'HP LaserJet Professional M1213nf MFP', NULL, 1, 0, 0, 1, 1, 1, CAST(0.00 AS Decimal(18, 2)), 0, N'check1', 1, CAST(1.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), 0, NULL, NULL, 0, 1, CAST(N'2017-09-26T14:46:01.7546384' AS DateTime2), 1, CAST(N'2017-09-26T14:46:01.7546384' AS DateTime2), 0)
GO
INSERT [dbo].[PrintJobs] ([JobId], [UserId], [DocId], [DocName], [DocTypeId], [DocExt], [DocFileNameOnServer], [DocFilePath], [DocTotalPages], [PrintCostId], [MonoUnitCost], [ColorUnitCost], [MonoPages], [ColorPages], [PrinterName], [PrinterPath], [IsColor], [IsDuplex], [IsCollate], [PagesFrom], [PagesTo], [NumCopies], [UnitCost], [UnitItem], [JobRemarks], [TotalPageCount], [TotalPageCost], [CreditUsed], [PrintJobQueueRefId], [JobError], [JobErrorRemarks], [JobStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (15, 1, 5, N'my pdf', 1, N'.pdf', N'239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', 0, 1, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, N'HP LaserJet Professional M1213nf MFP', NULL, 1, 1, 0, 1, 1, 1, CAST(0.00 AS Decimal(18, 2)), 0, N'mmm', 1, CAST(1.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), 26, N'Some new value', NULL, 0, 1, CAST(N'2017-09-26T15:30:20.9303627' AS DateTime2), 1, CAST(N'2017-09-26T23:45:40.7327264' AS DateTime2), 0)
GO
INSERT [dbo].[PrintJobs] ([JobId], [UserId], [DocId], [DocName], [DocTypeId], [DocExt], [DocFileNameOnServer], [DocFilePath], [DocTotalPages], [PrintCostId], [MonoUnitCost], [ColorUnitCost], [MonoPages], [ColorPages], [PrinterName], [PrinterPath], [IsColor], [IsDuplex], [IsCollate], [PagesFrom], [PagesTo], [NumCopies], [UnitCost], [UnitItem], [JobRemarks], [TotalPageCount], [TotalPageCost], [CreditUsed], [PrintJobQueueRefId], [JobError], [JobErrorRemarks], [JobStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (16, 1, 5, N'my pdf', 1, N'.pdf', N'239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', 0, 1, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, N'HP LaserJet Professional M1213nf MFP', NULL, 1, 1, 0, 1, 2, 1, CAST(0.00 AS Decimal(18, 2)), 0, N'mmm1', 2, CAST(2.00 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), 27, N'Some new value', NULL, 0, 1, CAST(N'2017-09-26T15:57:08.4064009' AS DateTime2), 1, CAST(N'2017-09-26T23:45:40.9727271' AS DateTime2), 0)
GO
INSERT [dbo].[PrintJobs] ([JobId], [UserId], [DocId], [DocName], [DocTypeId], [DocExt], [DocFileNameOnServer], [DocFilePath], [DocTotalPages], [PrintCostId], [MonoUnitCost], [ColorUnitCost], [MonoPages], [ColorPages], [PrinterName], [PrinterPath], [IsColor], [IsDuplex], [IsCollate], [PagesFrom], [PagesTo], [NumCopies], [UnitCost], [UnitItem], [JobRemarks], [TotalPageCount], [TotalPageCost], [CreditUsed], [PrintJobQueueRefId], [JobError], [JobErrorRemarks], [JobStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (17, 1, 5, N'my pdf', 1, N'.pdf', N'239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', 0, 1, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, N'HP LaserJet Professional M1213nf MFP', NULL, 1, 1, 0, 1, 1, 1, CAST(0.00 AS Decimal(18, 2)), 0, N'Testing first time', 1, CAST(1.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), 32, N'Some new value', NULL, 0, 1, CAST(N'2017-09-26T16:14:02.4285148' AS DateTime2), 1, CAST(N'2017-09-26T23:45:41.0167279' AS DateTime2), 0)
GO
INSERT [dbo].[PrintJobs] ([JobId], [UserId], [DocId], [DocName], [DocTypeId], [DocExt], [DocFileNameOnServer], [DocFilePath], [DocTotalPages], [PrintCostId], [MonoUnitCost], [ColorUnitCost], [MonoPages], [ColorPages], [PrinterName], [PrinterPath], [IsColor], [IsDuplex], [IsCollate], [PagesFrom], [PagesTo], [NumCopies], [UnitCost], [UnitItem], [JobRemarks], [TotalPageCount], [TotalPageCost], [CreditUsed], [PrintJobQueueRefId], [JobError], [JobErrorRemarks], [JobStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (18, 1, 5, N'my pdf', 1, N'.pdf', N'239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', 0, 1, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, N'HP LaserJet Professional M1213nf MFP', NULL, 1, 1, 0, 1, 1, 1, CAST(0.00 AS Decimal(18, 2)), 0, NULL, 1, CAST(1.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), 37, N'Some new value', NULL, 0, 1, CAST(N'2017-09-26T16:48:40.6430792' AS DateTime2), 1, CAST(N'2017-09-26T23:48:25.8564246' AS DateTime2), 0)
GO
INSERT [dbo].[PrintJobs] ([JobId], [UserId], [DocId], [DocName], [DocTypeId], [DocExt], [DocFileNameOnServer], [DocFilePath], [DocTotalPages], [PrintCostId], [MonoUnitCost], [ColorUnitCost], [MonoPages], [ColorPages], [PrinterName], [PrinterPath], [IsColor], [IsDuplex], [IsCollate], [PagesFrom], [PagesTo], [NumCopies], [UnitCost], [UnitItem], [JobRemarks], [TotalPageCount], [TotalPageCost], [CreditUsed], [PrintJobQueueRefId], [JobError], [JobErrorRemarks], [JobStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (19, 1, 5, N'my pdf', 1, N'.pdf', N'239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', 0, 1, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, 0, N'HP LaserJet Professional M1213nf MFP', NULL, 1, 1, 0, 1, 2, 1, CAST(0.00 AS Decimal(18, 2)), 0, NULL, 2, CAST(2.00 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), 38, N'Some new value', NULL, 0, 1, CAST(N'2017-09-26T16:49:05.8030854' AS DateTime2), 1, CAST(N'2017-09-26T23:48:25.9204251' AS DateTime2), 0)
GO
SET IDENTITY_INSERT [dbo].[PrintJobs] OFF
GO
SET IDENTITY_INSERT [dbo].[RStatus] ON 
GO
INSERT [dbo].[RStatus] ([StatusId], [StatusName]) VALUES (1, N'Active')
GO
INSERT [dbo].[RStatus] ([StatusId], [StatusName]) VALUES (2, N'Deleted')
GO
SET IDENTITY_INSERT [dbo].[RStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[TTypes] ON 
GO
INSERT [dbo].[TTypes] ([TxnTypeId], [TxnTypeName]) VALUES (1, N'Credit')
GO
INSERT [dbo].[TTypes] ([TxnTypeId], [TxnTypeName]) VALUES (2, N'Debit')
GO
SET IDENTITY_INSERT [dbo].[TTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[TxnStatus] ON 
GO
INSERT [dbo].[TxnStatus] ([StatusId], [StatusName]) VALUES (1, N'Success')
GO
INSERT [dbo].[TxnStatus] ([StatusId], [StatusName]) VALUES (2, N'Pending')
GO
SET IDENTITY_INSERT [dbo].[TxnStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[UserDocs] ON 
GO
INSERT [dbo].[UserDocs] ([DocId], [DocName], [DocTypeId], [DocExt], [DocFileName], [DocFilePath], [UserId], [DocCreatedDate], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (1, N'test', 1, N'doc', N'asb.doc', N'c:\documents', 1, CAST(N'2017-02-09T00:00:00.0000000' AS DateTime2), 1, CAST(N'2017-09-13T23:05:28.1048724' AS DateTime2), 1, CAST(N'2017-09-14T12:57:23.5808981' AS DateTime2), 1)
GO
INSERT [dbo].[UserDocs] ([DocId], [DocName], [DocTypeId], [DocExt], [DocFileName], [DocFilePath], [UserId], [DocCreatedDate], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (2, N'My Cover letter', 1, N'.docx', N'079d695f-68f7-4ff5-b087-b5943751192e.docx', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\079d695f-68f7-4ff5-b087-b5943751192e.docx', 1, CAST(N'2017-09-15T14:25:04.9618231' AS DateTime2), 1, CAST(N'2017-09-15T14:25:05.0508206' AS DateTime2), 1, CAST(N'2017-09-15T14:25:05.0508206' AS DateTime2), 1)
GO
INSERT [dbo].[UserDocs] ([DocId], [DocName], [DocTypeId], [DocExt], [DocFileName], [DocFilePath], [UserId], [DocCreatedDate], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (3, N'Test', 1, N'.pdf', N'ca6ad0af-55b4-4fd3-9cad-8ce559aee688.pdf', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\ca6ad0af-55b4-4fd3-9cad-8ce559aee688.pdf', 1, CAST(N'2017-09-18T17:30:43.8830512' AS DateTime2), 1, CAST(N'2017-09-18T17:30:43.8900490' AS DateTime2), 1, CAST(N'2017-09-18T17:30:43.8900490' AS DateTime2), 1)
GO
INSERT [dbo].[UserDocs] ([DocId], [DocName], [DocTypeId], [DocExt], [DocFileName], [DocFilePath], [UserId], [DocCreatedDate], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (4, N'Test document', 1, N'.docx', N'3e0ce247-2d46-469b-810e-90d40fd8e734.docx', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\3e0ce247-2d46-469b-810e-90d40fd8e734.docx', 1, CAST(N'2017-09-20T15:07:44.3866012' AS DateTime2), 1, CAST(N'2017-09-20T15:07:44.4985945' AS DateTime2), 1, CAST(N'2017-09-20T15:07:44.4985945' AS DateTime2), 1)
GO
INSERT [dbo].[UserDocs] ([DocId], [DocName], [DocTypeId], [DocExt], [DocFileName], [DocFilePath], [UserId], [DocCreatedDate], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (5, N'my pdf', 1, N'.pdf', N'239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', N'c:\users\devesh\documents\visual studio 2017\Projects\SmartPrint\SmartPrint\UserFileUploads\239dcde0-5263-4ea9-8d5a-5d42d61ca767.pdf', 1, CAST(N'2017-09-24T22:05:20.2520099' AS DateTime2), 1, CAST(N'2017-09-24T22:05:20.2530111' AS DateTime2), 1, CAST(N'2017-09-24T22:05:20.2530111' AS DateTime2), 1)
GO
SET IDENTITY_INSERT [dbo].[UserDocs] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([UserId], [FName], [LName], [UserTypeId], [UserCode], [UserEmail], [UserPass], [UserPhone], [UStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (1, N'Admin', N'Admin Manager', 1, N'ADM0001', N'admin@admin.com', N'jXHWlQH5qBSH+OZHmgykg6wr820hbNCBKECgQuxpB3Y=', N'9818990404', 1, 1, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2017-09-13T00:36:44.4418703' AS DateTime2), 1)
GO
INSERT [dbo].[Users] ([UserId], [FName], [LName], [UserTypeId], [UserCode], [UserEmail], [UserPass], [UserPhone], [UStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (2, N'Rohit', N'Vermas', 3, N'ST0001', N'r.verma@rohit.com', N'7Z95szIAqiUeWuX8ASgndw==', N'9090909090', 1, 1, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2017-09-13T00:03:30.3759171' AS DateTime2), 1)
GO
INSERT [dbo].[Users] ([UserId], [FName], [LName], [UserTypeId], [UserCode], [UserEmail], [UserPass], [UserPhone], [UStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (1002, N'Dishita', N'NAswa', 3, N'ST0002', N'dishita@gmail.com', N'Ij8YqSZ8fS993YH4qS/V6w==', N'9898989898', 1, 1, CAST(N'2017-09-13T01:41:28.8414196' AS DateTime2), 1, CAST(N'2017-09-13T01:41:28.8414196' AS DateTime2), 1)
GO
INSERT [dbo].[Users] ([UserId], [FName], [LName], [UserTypeId], [UserCode], [UserEmail], [UserPass], [UserPhone], [UStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (1003, N'sid', N'Man', 1, N'ST0009', N's.mam@gmail.com', N'fORRNGKa8yYj1WaiuGE0728PF1fkz+NDVB10/sXLmcY=', N'9999999999', 4, 1, CAST(N'2017-09-13T01:41:28.8414196' AS DateTime2), 1, CAST(N'2017-09-13T10:14:31.1918823' AS DateTime2), 2)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[UserTxns] ON 
GO
INSERT [dbo].[UserTxns] ([TxnId], [UserId], [TxnTypeId], [TxnAmount], [TxnDateTime], [TxnBalance], [TxnRefJobId], [TxnStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (1, 1, 1, CAST(120.00 AS Decimal(18, 2)), CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), CAST(125.00 AS Decimal(18, 2)), 1, 1, 1, CAST(N'2017-09-13T22:57:41.3670411' AS DateTime2), 1, CAST(N'2017-09-14T12:42:58.1216847' AS DateTime2), 1)
GO
INSERT [dbo].[UserTxns] ([TxnId], [UserId], [TxnTypeId], [TxnAmount], [TxnDateTime], [TxnBalance], [TxnRefJobId], [TxnStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (2, 1, 1, CAST(40.00 AS Decimal(18, 2)), CAST(N'2017-09-24T17:09:52.2000152' AS DateTime2), CAST(85.00 AS Decimal(18, 2)), 5, 0, 1, CAST(N'2017-09-24T17:09:52.3406404' AS DateTime2), 1, CAST(N'2017-09-24T17:09:52.3406404' AS DateTime2), 1)
GO
INSERT [dbo].[UserTxns] ([TxnId], [UserId], [TxnTypeId], [TxnAmount], [TxnDateTime], [TxnBalance], [TxnRefJobId], [TxnStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (3, 1, 1, CAST(10.00 AS Decimal(18, 2)), CAST(N'2017-09-24T22:03:40.0819816' AS DateTime2), CAST(75.00 AS Decimal(18, 2)), 6, 0, 1, CAST(N'2017-09-24T22:03:58.1769845' AS DateTime2), 1, CAST(N'2017-09-24T22:03:58.1769845' AS DateTime2), 1)
GO
INSERT [dbo].[UserTxns] ([TxnId], [UserId], [TxnTypeId], [TxnAmount], [TxnDateTime], [TxnBalance], [TxnRefJobId], [TxnStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (4, 1, 1, CAST(9.00 AS Decimal(18, 2)), CAST(N'2017-09-24T22:05:49.1550183' AS DateTime2), CAST(66.00 AS Decimal(18, 2)), 7, 0, 1, CAST(N'2017-09-24T22:05:52.6230169' AS DateTime2), 1, CAST(N'2017-09-24T22:05:52.6230169' AS DateTime2), 1)
GO
INSERT [dbo].[UserTxns] ([TxnId], [UserId], [TxnTypeId], [TxnAmount], [TxnDateTime], [TxnBalance], [TxnRefJobId], [TxnStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (5, 1, 1, CAST(4.00 AS Decimal(18, 2)), CAST(N'2017-09-24T22:20:34.3435688' AS DateTime2), CAST(62.00 AS Decimal(18, 2)), 8, 0, 1, CAST(N'2017-09-24T22:20:59.0225723' AS DateTime2), 1, CAST(N'2017-09-24T22:20:59.0225723' AS DateTime2), 1)
GO
INSERT [dbo].[UserTxns] ([TxnId], [UserId], [TxnTypeId], [TxnAmount], [TxnDateTime], [TxnBalance], [TxnRefJobId], [TxnStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (6, 1, 1, CAST(4.00 AS Decimal(18, 2)), CAST(N'2017-09-24T23:24:51.1888459' AS DateTime2), CAST(58.00 AS Decimal(18, 2)), 9, 0, 1, CAST(N'2017-09-24T23:25:05.4010396' AS DateTime2), 1, CAST(N'2017-09-24T23:25:05.4010396' AS DateTime2), 1)
GO
INSERT [dbo].[UserTxns] ([TxnId], [UserId], [TxnTypeId], [TxnAmount], [TxnDateTime], [TxnBalance], [TxnRefJobId], [TxnStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (7, 1, 1, CAST(1.00 AS Decimal(18, 2)), CAST(N'2017-09-25T01:45:29.6628389' AS DateTime2), CAST(57.00 AS Decimal(18, 2)), 10, 0, 1, CAST(N'2017-09-25T01:45:42.9941528' AS DateTime2), 1, CAST(N'2017-09-25T01:45:42.9941528' AS DateTime2), 1)
GO
INSERT [dbo].[UserTxns] ([TxnId], [UserId], [TxnTypeId], [TxnAmount], [TxnDateTime], [TxnBalance], [TxnRefJobId], [TxnStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (8, 1, 1, CAST(3.00 AS Decimal(18, 2)), CAST(N'2017-09-25T01:55:33.3006303' AS DateTime2), CAST(54.00 AS Decimal(18, 2)), 11, 0, 1, CAST(N'2017-09-25T01:55:44.1370690' AS DateTime2), 1, CAST(N'2017-09-25T01:55:44.1370690' AS DateTime2), 1)
GO
INSERT [dbo].[UserTxns] ([TxnId], [UserId], [TxnTypeId], [TxnAmount], [TxnDateTime], [TxnBalance], [TxnRefJobId], [TxnStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (9, 1, 1, CAST(2.00 AS Decimal(18, 2)), CAST(N'2017-09-26T14:13:03.3390376' AS DateTime2), CAST(52.00 AS Decimal(18, 2)), 12, 0, 1, CAST(N'2017-09-26T14:13:09.6240398' AS DateTime2), 1, CAST(N'2017-09-26T14:13:09.6240398' AS DateTime2), 1)
GO
INSERT [dbo].[UserTxns] ([TxnId], [UserId], [TxnTypeId], [TxnAmount], [TxnDateTime], [TxnBalance], [TxnRefJobId], [TxnStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (10, 1, 1, CAST(1.00 AS Decimal(18, 2)), CAST(N'2017-09-26T14:32:25.6464165' AS DateTime2), CAST(51.00 AS Decimal(18, 2)), 13, 0, 1, CAST(N'2017-09-26T14:36:50.9174875' AS DateTime2), 1, CAST(N'2017-09-26T14:36:50.9174875' AS DateTime2), 1)
GO
INSERT [dbo].[UserTxns] ([TxnId], [UserId], [TxnTypeId], [TxnAmount], [TxnDateTime], [TxnBalance], [TxnRefJobId], [TxnStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (11, 1, 1, CAST(1.00 AS Decimal(18, 2)), CAST(N'2017-09-26T14:45:32.3196298' AS DateTime2), CAST(50.00 AS Decimal(18, 2)), 14, 0, 1, CAST(N'2017-09-26T14:46:01.8396380' AS DateTime2), 1, CAST(N'2017-09-26T14:46:01.8396380' AS DateTime2), 1)
GO
INSERT [dbo].[UserTxns] ([TxnId], [UserId], [TxnTypeId], [TxnAmount], [TxnDateTime], [TxnBalance], [TxnRefJobId], [TxnStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (12, 1, 1, CAST(1.00 AS Decimal(18, 2)), CAST(N'2017-09-26T15:30:20.0403596' AS DateTime2), CAST(49.00 AS Decimal(18, 2)), 15, 0, 1, CAST(N'2017-09-26T15:30:21.0443600' AS DateTime2), 1, CAST(N'2017-09-26T15:30:21.0443600' AS DateTime2), 1)
GO
INSERT [dbo].[UserTxns] ([TxnId], [UserId], [TxnTypeId], [TxnAmount], [TxnDateTime], [TxnBalance], [TxnRefJobId], [TxnStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (13, 1, 1, CAST(2.00 AS Decimal(18, 2)), CAST(N'2017-09-26T15:57:06.6294035' AS DateTime2), CAST(47.00 AS Decimal(18, 2)), 16, 0, 1, CAST(N'2017-09-26T15:57:08.4704002' AS DateTime2), 1, CAST(N'2017-09-26T15:57:08.4704002' AS DateTime2), 1)
GO
INSERT [dbo].[UserTxns] ([TxnId], [UserId], [TxnTypeId], [TxnAmount], [TxnDateTime], [TxnBalance], [TxnRefJobId], [TxnStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (14, 1, 1, CAST(1.00 AS Decimal(18, 2)), CAST(N'2017-09-26T16:13:58.7195153' AS DateTime2), CAST(46.00 AS Decimal(18, 2)), 17, 0, 1, CAST(N'2017-09-26T16:14:02.4845167' AS DateTime2), 1, CAST(N'2017-09-26T16:14:02.4845167' AS DateTime2), 1)
GO
INSERT [dbo].[UserTxns] ([TxnId], [UserId], [TxnTypeId], [TxnAmount], [TxnDateTime], [TxnBalance], [TxnRefJobId], [TxnStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (15, 1, 1, CAST(1.00 AS Decimal(18, 2)), CAST(N'2017-09-26T16:48:32.4290787' AS DateTime2), CAST(45.00 AS Decimal(18, 2)), 18, 0, 1, CAST(N'2017-09-26T16:48:40.7470801' AS DateTime2), 1, CAST(N'2017-09-26T16:48:40.7470801' AS DateTime2), 1)
GO
INSERT [dbo].[UserTxns] ([TxnId], [UserId], [TxnTypeId], [TxnAmount], [TxnDateTime], [TxnBalance], [TxnRefJobId], [TxnStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (16, 1, 1, CAST(2.00 AS Decimal(18, 2)), CAST(N'2017-09-26T16:49:04.5430856' AS DateTime2), CAST(43.00 AS Decimal(18, 2)), 19, 0, 1, CAST(N'2017-09-26T16:49:05.8320865' AS DateTime2), 1, CAST(N'2017-09-26T16:49:05.8320865' AS DateTime2), 1)
GO
INSERT [dbo].[UserTxns] ([TxnId], [UserId], [TxnTypeId], [TxnAmount], [TxnDateTime], [TxnBalance], [TxnRefJobId], [TxnStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (17, 1, 1, CAST(10.50 AS Decimal(18, 2)), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(43.00 AS Decimal(18, 2)), 0, 1, 1, CAST(N'2017-09-28T02:59:39.7395161' AS DateTime2), 1, CAST(N'2017-09-28T02:59:39.7405156' AS DateTime2), 1)
GO
INSERT [dbo].[UserTxns] ([TxnId], [UserId], [TxnTypeId], [TxnAmount], [TxnDateTime], [TxnBalance], [TxnRefJobId], [TxnStatusId], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (18, 1, 1, CAST(10.50 AS Decimal(18, 2)), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(0.00 AS Decimal(18, 2)), 0, 1, 1, CAST(N'2017-09-28T03:04:40.4485989' AS DateTime2), 1, CAST(N'2017-09-28T03:32:25.7506141' AS DateTime2), 1)
GO
SET IDENTITY_INSERT [dbo].[UserTxns] OFF
GO
SET IDENTITY_INSERT [dbo].[UserTypes] ON 
GO
INSERT [dbo].[UserTypes] ([UserTypeId], [UserType], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (1, N'Administrator', 1, CAST(N'2017-09-11T22:23:13.9166667' AS DateTime2), 1, CAST(N'2017-09-28T03:13:25.0527428' AS DateTime2), 1)
GO
INSERT [dbo].[UserTypes] ([UserTypeId], [UserType], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (2, N'Manager', 1, CAST(N'2017-09-11T22:24:47.3500000' AS DateTime2), 1, CAST(N'2017-09-11T22:24:47.3500000' AS DateTime2), 1)
GO
INSERT [dbo].[UserTypes] ([UserTypeId], [UserType], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (3, N'Student', 1, CAST(N'2017-09-11T22:24:47.3500000' AS DateTime2), 1, CAST(N'2017-09-11T22:24:47.3500000' AS DateTime2), 1)
GO
INSERT [dbo].[UserTypes] ([UserTypeId], [UserType], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (1002, N'Test', 1, CAST(N'2017-09-13T18:40:06.4344317' AS DateTime2), 1, CAST(N'2017-09-27T12:51:18.8934083' AS DateTime2), 1)
GO
INSERT [dbo].[UserTypes] ([UserTypeId], [UserType], [AddedBy], [AddedOn], [EditedBy], [EditedOn], [StatusId]) VALUES (1003, N'SuperUser', 1, CAST(N'2017-09-13T18:47:38.5732383' AS DateTime2), 1, CAST(N'2017-09-13T18:59:11.7408919' AS DateTime2), 2)
GO
SET IDENTITY_INSERT [dbo].[UserTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[UStatus] ON 
GO
INSERT [dbo].[UStatus] ([UStatusId], [UStatusName]) VALUES (1, N'Active')
GO
INSERT [dbo].[UStatus] ([UStatusId], [UStatusName]) VALUES (2, N'In Active')
GO
INSERT [dbo].[UStatus] ([UStatusId], [UStatusName]) VALUES (3, N'Suspended')
GO
INSERT [dbo].[UStatus] ([UStatusId], [UStatusName]) VALUES (4, N'Depricated')
GO
SET IDENTITY_INSERT [dbo].[UStatus] OFF
GO
ALTER TABLE [dbo].[PrintJobs] ADD  CONSTRAINT [DF_PrintJobs_IsColor]  DEFAULT ((0)) FOR [IsColor]
GO
ALTER TABLE [dbo].[PrintJobs] ADD  CONSTRAINT [DF_PrintJobs_IsDuplex]  DEFAULT ((0)) FOR [IsDuplex]
GO
ALTER TABLE [dbo].[PrintJobs] ADD  CONSTRAINT [DF_PrintJobs_IsCollate]  DEFAULT ((0)) FOR [IsCollate]
GO
ALTER TABLE [dbo].[PrintJobs] ADD  CONSTRAINT [DF_PrintJobs_PrintJobQueueRefId]  DEFAULT ((0)) FOR [PrintJobQueueRefId]
GO
USE [master]
GO
ALTER DATABASE [SmartPrint] SET  READ_WRITE 
GO
