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
/****** Object:  Database [SmartPrint]    Script Date: 9/23/2017 12:08:21 PM ******/
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
/****** Object:  Table [dbo].[Configurations]    Script Date: 9/23/2017 12:08:22 PM ******/
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
/****** Object:  Table [dbo].[DocTypes]    Script Date: 9/23/2017 12:08:22 PM ******/
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
/****** Object:  Table [dbo].[PrintCosts]    Script Date: 9/23/2017 12:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrintCosts](
	[PrintCostId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Width] [decimal](18, 0) NOT NULL,
	[Height] [decimal](18, 0) NOT NULL,
	[MonoCostPerPage] [decimal](18, 0) NOT NULL,
	[ColorCostPerPage] [decimal](18, 0) NOT NULL,
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
/****** Object:  Table [dbo].[PrintJobs]    Script Date: 9/23/2017 12:08:22 PM ******/
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
	[MonoUnitCost] [decimal](18, 0) NOT NULL,
	[ColorUnitCost] [decimal](18, 0) NOT NULL,
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
	[UnitCost] [decimal](18, 0) NOT NULL,
	[UnitItem] [int] NOT NULL,
	[JobRemarks] [nvarchar](250) NULL,
	[TotalPageCount] [int] NOT NULL,
	[TotalPageCost] [decimal](18, 0) NOT NULL,
	[CreditUsed] [decimal](18, 0) NULL,
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
/****** Object:  Table [dbo].[RStatus]    Script Date: 9/23/2017 12:08:22 PM ******/
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
/****** Object:  Table [dbo].[UserDocs]    Script Date: 9/23/2017 12:08:22 PM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 9/23/2017 12:08:22 PM ******/
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
/****** Object:  Table [dbo].[UserTxns]    Script Date: 9/23/2017 12:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTxns](
	[TxnId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TxnTypeId] [int] NOT NULL,
	[TxnAmount] [decimal](18, 0) NOT NULL,
	[TxnDateTime] [datetime2](7) NOT NULL,
	[TxnBalance] [decimal](18, 0) NOT NULL,
	[TxnRefJobId] [int] NOT NULL,
	[TxnStatus] [int] NOT NULL,
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
/****** Object:  Table [dbo].[UserTypes]    Script Date: 9/23/2017 12:08:22 PM ******/
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
/****** Object:  Table [dbo].[UStatus]    Script Date: 9/23/2017 12:08:22 PM ******/
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
ALTER TABLE [dbo].[PrintJobs] ADD  CONSTRAINT [DF_PrintJobs_IsColor]  DEFAULT ((0)) FOR [IsColor]
GO
ALTER TABLE [dbo].[PrintJobs] ADD  CONSTRAINT [DF_PrintJobs_IsDuplex]  DEFAULT ((0)) FOR [IsDuplex]
GO
ALTER TABLE [dbo].[PrintJobs] ADD  CONSTRAINT [DF_PrintJobs_IsCollate]  DEFAULT ((0)) FOR [IsCollate]
GO
USE [master]
GO
ALTER DATABASE [SmartPrint] SET  READ_WRITE 
GO
