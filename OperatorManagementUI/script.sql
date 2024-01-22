USE [master]
GO
/****** Object:  Database [OperatorManagementDB]    Script Date: 1/21/2024 11:43:24 AM ******/
CREATE DATABASE [OperatorManagementDB]
 CONTAINMENT = NONE
GO
ALTER DATABASE [OperatorManagementDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OperatorManagementDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OperatorManagementDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OperatorManagementDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OperatorManagementDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OperatorManagementDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OperatorManagementDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [OperatorManagementDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OperatorManagementDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OperatorManagementDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OperatorManagementDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OperatorManagementDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OperatorManagementDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OperatorManagementDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OperatorManagementDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OperatorManagementDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OperatorManagementDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OperatorManagementDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OperatorManagementDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OperatorManagementDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OperatorManagementDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OperatorManagementDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OperatorManagementDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OperatorManagementDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OperatorManagementDB] SET RECOVERY FULL 
GO
ALTER DATABASE [OperatorManagementDB] SET  MULTI_USER 
GO
ALTER DATABASE [OperatorManagementDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OperatorManagementDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OperatorManagementDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OperatorManagementDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OperatorManagementDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'OperatorManagementDB', N'ON'
GO
USE [OperatorManagementDB]
GO
/****** Object:  Table [dbo].[Tbl_Cost]    Script Date: 1/21/2024 11:43:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Cost](
	[Fld_Cost_Id] [int] NOT NULL,
	[Fld_Cost_Value] [decimal](10, 2) NOT NULL,
	[Fld_Cost_Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Tbl_Cost_1] PRIMARY KEY CLUSTERED 
(
	[Fld_Cost_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Person]    Script Date: 1/21/2024 11:43:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Person](
	[Fld_Person_Id] [int] IDENTITY(1,1) NOT NULL,
	[Fld_Person_Fname] [nvarchar](50) NOT NULL,
	[Fld_Person_Lname] [nvarchar](150) NOT NULL,
	[Fld_Person_NationCode] [nvarchar](50) NOT NULL,
	[Fld_Person_IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Tbl_Person] PRIMARY KEY CLUSTERED 
(
	[Fld_Person_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Sim]    Script Date: 1/21/2024 11:43:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Sim](
	[Fld_Sim_Id] [int] IDENTITY(1,1) NOT NULL,
	[Fld_Sim_Number] [nvarchar](20) NOT NULL,
	[Fld_Person_Id] [int] NOT NULL,
	[Fld_Sim_IsActive] [bit] NOT NULL,
	[Fld_SimType_Id] [int] NOT NULL,
	[Fld_Sim_IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Tbl_Sim] PRIMARY KEY CLUSTERED 
(
	[Fld_Sim_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_SimType]    Script Date: 1/21/2024 11:43:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_SimType](
	[Fld_SimType_Id] [int] NOT NULL,
	[Fld_SimType_Value] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Tbl_SimType] PRIMARY KEY CLUSTERED 
(
	[Fld_SimType_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Transaction]    Script Date: 1/21/2024 11:43:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Transaction](
	[Fld_Transaction_Id] [int] IDENTITY(1,1) NOT NULL,
	[Fld_Transaction_Date] [datetime] NOT NULL,
	[Fld_Sim_FromSimId] [int] NOT NULL,
	[Fld_Sim_ToSimId] [int] NOT NULL,
	[Fld_TransactionType_Id] [int] NOT NULL,
	[Fld_Transaction_Duration] [time](7) NULL,
 CONSTRAINT [PK_Tbl_Transaction] PRIMARY KEY CLUSTERED 
(
	[Fld_Transaction_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_TransactionType]    Script Date: 1/21/2024 11:43:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_TransactionType](
	[Fld_TransactionType_Id] [int] NOT NULL,
	[Fld_TransactionType_Type] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Tbl_TransactionType] PRIMARY KEY CLUSTERED 
(
	[Fld_TransactionType_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Wallet]    Script Date: 1/21/2024 11:43:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Wallet](
	[Fld_Wallet_Id] [int] NOT NULL,
	[Fld_Wallet_Balance] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_Tbl_Wallet] PRIMARY KEY CLUSTERED 
(
	[Fld_Wallet_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Tbl_Cost] ([Fld_Cost_Id], [Fld_Cost_Value], [Fld_Cost_Description]) VALUES (1, CAST(0.30 AS Decimal(10, 2)), N'به ازای هر پیامک')
GO
INSERT [dbo].[Tbl_Cost] ([Fld_Cost_Id], [Fld_Cost_Value], [Fld_Cost_Description]) VALUES (2, CAST(0.50 AS Decimal(10, 2)), N'به ازای هر دقیقه مکالمه')
GO
INSERT [dbo].[Tbl_SimType] ([Fld_SimType_Id], [Fld_SimType_Value]) VALUES (1, N'دائمی')
GO
INSERT [dbo].[Tbl_SimType] ([Fld_SimType_Id], [Fld_SimType_Value]) VALUES (2, N'اعتباری')
GO
INSERT [dbo].[Tbl_TransactionType] ([Fld_TransactionType_Id], [Fld_TransactionType_Type]) VALUES (1, N'پیامک')
GO
INSERT [dbo].[Tbl_TransactionType] ([Fld_TransactionType_Id], [Fld_TransactionType_Type]) VALUES (2, N'تماس')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Tbl_Person_NationCode]    Script Date: 1/21/2024 11:43:24 AM ******/
ALTER TABLE [dbo].[Tbl_Person] ADD  CONSTRAINT [UQ_Tbl_Person_NationCode] UNIQUE NONCLUSTERED 
(
	[Fld_Person_NationCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Tbl_Sim_Fld_Sim_Number]    Script Date: 1/21/2024 11:43:24 AM ******/
ALTER TABLE [dbo].[Tbl_Sim] ADD  CONSTRAINT [UQ_Tbl_Sim_Fld_Sim_Number] UNIQUE NONCLUSTERED 
(
	[Fld_Sim_Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Tbl_Sim_Tbl_Person]    Script Date: 1/21/2024 11:43:24 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_Tbl_Sim_Tbl_Person] ON [dbo].[Tbl_Sim]
(
	[Fld_Person_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Tbl_SimTbl_SimType]    Script Date: 1/21/2024 11:43:24 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_Tbl_SimTbl_SimType] ON [dbo].[Tbl_Sim]
(
	[Fld_SimType_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Tbl_Transaction_Tbl_Sim]    Script Date: 1/21/2024 11:43:24 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_Tbl_Transaction_Tbl_Sim] ON [dbo].[Tbl_Transaction]
(
	[Fld_Sim_FromSimId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Tbl_Transaction_Tbl_Sim1]    Script Date: 1/21/2024 11:43:24 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_Tbl_Transaction_Tbl_Sim1] ON [dbo].[Tbl_Transaction]
(
	[Fld_Sim_ToSimId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Tbl_Transaction_Tbl_TransactionType]    Script Date: 1/21/2024 11:43:24 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_Tbl_Transaction_Tbl_TransactionType] ON [dbo].[Tbl_Transaction]
(
	[Fld_TransactionType_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Tbl_Cost]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Cost_Tbl_TransactionType] FOREIGN KEY([Fld_Cost_Id])
REFERENCES [dbo].[Tbl_TransactionType] ([Fld_TransactionType_Id])
GO
ALTER TABLE [dbo].[Tbl_Cost] CHECK CONSTRAINT [FK_Tbl_Cost_Tbl_TransactionType]
GO
ALTER TABLE [dbo].[Tbl_Sim]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Sim_Tbl_Person] FOREIGN KEY([Fld_Person_Id])
REFERENCES [dbo].[Tbl_Person] ([Fld_Person_Id])
GO
ALTER TABLE [dbo].[Tbl_Sim] CHECK CONSTRAINT [FK_Tbl_Sim_Tbl_Person]
GO
ALTER TABLE [dbo].[Tbl_Sim]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_SimTbl_SimType] FOREIGN KEY([Fld_SimType_Id])
REFERENCES [dbo].[Tbl_SimType] ([Fld_SimType_Id])
GO
ALTER TABLE [dbo].[Tbl_Sim] CHECK CONSTRAINT [FK_Tbl_SimTbl_SimType]
GO
ALTER TABLE [dbo].[Tbl_Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Transaction_Tbl_Sim] FOREIGN KEY([Fld_Sim_FromSimId])
REFERENCES [dbo].[Tbl_Sim] ([Fld_Sim_Id])
GO
ALTER TABLE [dbo].[Tbl_Transaction] CHECK CONSTRAINT [FK_Tbl_Transaction_Tbl_Sim]
GO
ALTER TABLE [dbo].[Tbl_Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Transaction_Tbl_Sim1] FOREIGN KEY([Fld_Sim_ToSimId])
REFERENCES [dbo].[Tbl_Sim] ([Fld_Sim_Id])
GO
ALTER TABLE [dbo].[Tbl_Transaction] CHECK CONSTRAINT [FK_Tbl_Transaction_Tbl_Sim1]
GO
ALTER TABLE [dbo].[Tbl_Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Transaction_Tbl_TransactionType] FOREIGN KEY([Fld_TransactionType_Id])
REFERENCES [dbo].[Tbl_TransactionType] ([Fld_TransactionType_Id])
GO
ALTER TABLE [dbo].[Tbl_Transaction] CHECK CONSTRAINT [FK_Tbl_Transaction_Tbl_TransactionType]
GO
ALTER TABLE [dbo].[Tbl_Wallet]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Wallet_Tbl_Sim] FOREIGN KEY([Fld_Wallet_Id])
REFERENCES [dbo].[Tbl_Sim] ([Fld_Sim_Id])
GO
ALTER TABLE [dbo].[Tbl_Wallet] CHECK CONSTRAINT [FK_Tbl_Wallet_Tbl_Sim]
GO
/****** Object:  StoredProcedure [dbo].[ss_selectPeople]    Script Date: 1/21/2024 11:43:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ss_selectPeople]
AS
SELECT * FROM Tbl_Person WHERE Fld_Person_IsDeleted=0
GO
/****** Object:  StoredProcedure [dbo].[ss_selectPerson]    Script Date: 1/21/2024 11:43:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ss_selectPerson] @PersonId int
AS
SELECT * FROM Tbl_Person WHERE  Fld_Person_Id=@PersonId
GO
/****** Object:  StoredProcedure [dbo].[ss_selectPersonSimcards]    Script Date: 1/21/2024 11:43:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ss_selectPersonSimcards] @PersonId int
AS
SELECT * FROM Tbl_Sim WHERE  Fld_Person_Id=@PersonId
GO
USE [master]
GO
ALTER DATABASE [OperatorManagementDB] SET  READ_WRITE 
GO


/*/////////////////////////// NEW ///////////////////////////////*/

CREATE TABLE [dbo].[Tbl_ChargeLog](
	[Fld_ChargeLog_Id] [int] IDENTITY(1,1) NOT NULL,
	[Fld_Sim_SimId] [int] NOT NULL,
	[Fld_ChargeLog_Date] [datetime] NOT NULL,
	[Fld_ChargeLog_Value] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_Tbl_ChargeLog] PRIMARY KEY CLUSTERED 
(
	[Fld_ChargeLog_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Tbl_ChargeLog]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_ChargeLog_Tbl_Sim] FOREIGN KEY([Fld_Sim_SimId])
REFERENCES [dbo].[Tbl_Sim] ([Fld_Sim_Id])
GO

ALTER TABLE [dbo].[Tbl_ChargeLog] CHECK CONSTRAINT [FK_Tbl_ChargeLog_Tbl_Sim]
GO