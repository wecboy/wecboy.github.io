USE [master]
GO
/****** Object:  Database [bug_tracker]    Script Date: 2021/7/6 上午 09:59:52 ******/
CREATE DATABASE [bug_tracker]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'bug_tracker', FILENAME = N'C:\db\sql2019\bug_tracker.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'bug_tracker_log', FILENAME = N'C:\db\sql2019\bug_tracker_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [bug_tracker] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [bug_tracker].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [bug_tracker] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [bug_tracker] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [bug_tracker] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [bug_tracker] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [bug_tracker] SET ARITHABORT OFF 
GO
ALTER DATABASE [bug_tracker] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [bug_tracker] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [bug_tracker] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [bug_tracker] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [bug_tracker] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [bug_tracker] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [bug_tracker] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [bug_tracker] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [bug_tracker] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [bug_tracker] SET  DISABLE_BROKER 
GO
ALTER DATABASE [bug_tracker] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [bug_tracker] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [bug_tracker] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [bug_tracker] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [bug_tracker] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [bug_tracker] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [bug_tracker] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [bug_tracker] SET RECOVERY FULL 
GO
ALTER DATABASE [bug_tracker] SET  MULTI_USER 
GO
ALTER DATABASE [bug_tracker] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [bug_tracker] SET DB_CHAINING OFF 
GO
ALTER DATABASE [bug_tracker] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [bug_tracker] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [bug_tracker] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [bug_tracker] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'bug_tracker', N'ON'
GO
ALTER DATABASE [bug_tracker] SET QUERY_STORE = OFF
GO
USE [bug_tracker]
GO
/****** Object:  User [LAPTOP-C8HFCDNG\wecbo]    Script Date: 2021/7/6 上午 09:59:52 ******/
CREATE USER [LAPTOP-C8HFCDNG\wecbo] FOR LOGIN [LAPTOP-C8HFCDNG\wecbo] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  UserDefinedFunction [dbo].[NewPID]    Script Date: 2021/7/6 上午 09:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create FUNCTION [dbo].[NewPID]()
RETURNS nvarchar(50)
BEGIN
    DECLARE @id nvarchar(50), @i int

    /* 找出目前最大的編號 */
    SELECT TOP 1 @id =uid
    FROM users
    ORDER BY uid DESC

    IF @@ROWCOUNT = 0   /* 如果沒有記錄 */
         RETURN 'P0001'

    SET @i = CAST(RIGHT(@id, 4) AS int) + 1
    SET @id = CAST(@i AS varchar)
    RETURN  'P' + REPLICATE('0', 4-LEN(@id)) + @id
END


GO
/****** Object:  UserDefinedFunction [dbo].[NewUID]    Script Date: 2021/7/6 上午 09:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[NewUID]()
RETURNS nvarchar(50)
BEGIN
    DECLARE @id nvarchar(50), @i int

    /* 找出目前最大的編號 */
    SELECT TOP 1 @id =uid
    FROM users
    ORDER BY uid DESC

    IF @@ROWCOUNT = 0   /* 如果沒有記錄 */
         RETURN 'U0001'

    SET @i = CAST(RIGHT(@id, 4) AS int) + 1
    SET @id = CAST(@i AS varchar)
    RETURN  'U' + REPLICATE('0', 4-LEN(@id)) + @id
END
GO
/****** Object:  Table [dbo].[AppCode]    Script Date: 2021/7/6 上午 09:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppCode](
	[rowid] [int] IDENTITY(1,1) NOT NULL,
	[type_no] [nvarchar](50) NULL,
	[mno] [nvarchar](50) NULL,
	[mname] [nvarchar](50) NULL,
	[remark] [nvarchar](250) NULL,
 CONSTRAINT [PK_AppCode] PRIMARY KEY CLUSTERED 
(
	[rowid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bugs]    Script Date: 2021/7/6 上午 09:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bugs](
	[rowid] [int] IDENTITY(1,1) NOT NULL,
	[bid] [nvarchar](50) NULL,
	[bsummary] [nvarchar](50) NULL,
	[bcreator] [nvarchar](50) NULL,
	[bstatus_id] [nvarchar](50) NULL,
	[bpriority_id] [nvarchar](50) NULL,
	[binit_time] [datetime] NULL,
	[bmodified_time] [datetime] NULL,
	[bpid] [nvarchar](50) NULL,
	[basignee] [nvarchar](50) NULL,
	[bdescription] [nvarchar](250) NULL,
 CONSTRAINT [PK_bugs] PRIMARY KEY CLUSTERED 
(
	[rowid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[priorities]    Script Date: 2021/7/6 上午 09:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[priorities](
	[rowid] [int] IDENTITY(1,1) NOT NULL,
	[mno] [nvarchar](50) NULL,
	[mname] [nvarchar](50) NULL,
	[remark] [nvarchar](250) NULL,
 CONSTRAINT [PK_priorities] PRIMARY KEY CLUSTERED 
(
	[rowid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[projects]    Script Date: 2021/7/6 上午 09:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[projects](
	[rowid] [int] IDENTITY(1,1) NOT NULL,
	[pid] [nvarchar](50) NULL,
	[pname] [nvarchar](50) NULL,
	[pdescription] [nvarchar](250) NULL,
	[pmanager_id] [nvarchar](50) NULL,
	[pinit_time] [datetime] NULL,
	[pmodified_time] [datetime] NULL,
 CONSTRAINT [PK_projects] PRIMARY KEY CLUSTERED 
(
	[rowid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[roles]    Script Date: 2021/7/6 上午 09:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roles](
	[rowid] [int] IDENTITY(1,1) NOT NULL,
	[rid] [nvarchar](50) NULL,
	[rrule] [nvarchar](50) NULL,
	[rname] [nvarchar](50) NULL,
	[rremark] [nvarchar](250) NULL,
 CONSTRAINT [PK_roles] PRIMARY KEY CLUSTERED 
(
	[rowid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[status]    Script Date: 2021/7/6 上午 09:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[status](
	[rowid] [int] IDENTITY(1,1) NOT NULL,
	[mno] [nvarchar](50) NULL,
	[mname] [nvarchar](50) NULL,
	[remark] [nvarchar](250) NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[rowid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserMessage]    Script Date: 2021/7/6 上午 09:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMessage](
	[rowid] [int] IDENTITY(1,1) NOT NULL,
	[code_no] [nvarchar](50) NULL,
	[date_sender] [datetime] NULL,
	[date_read] [datetime] NULL,
	[sender_no] [nvarchar](50) NULL,
	[sender_name] [nvarchar](50) NULL,
	[receive_no] [nvarchar](50) NULL,
	[receive_name] [nvarchar](50) NULL,
	[is_read] [bit] NOT NULL,
	[text_title] [nvarchar](50) NULL,
	[text_content] [nvarchar](max) NULL,
	[remark] [nvarchar](250) NULL,
 CONSTRAINT [PK_UserMessage] PRIMARY KEY CLUSTERED 
(
	[rowid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 2021/7/6 上午 09:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[rowid] [int] IDENTITY(1,1) NOT NULL,
	[uid] [nvarchar](50) NULL,
	[umno] [nvarchar](50) NULL,
	[uname] [nvarchar](50) NULL,
	[upassword] [nvarchar](50) NULL,
	[uphone] [nvarchar](50) NULL,
	[uemail] [nvarchar](250) NULL,
	[urole] [nvarchar](50) NULL,
	[ubirthday] [date] NULL,
	[remark] [nvarchar](250) NULL,
	[varify_code] [nvarchar](50) NULL,
	[isvarify] [bit] NOT NULL,
	[uinit_time] [datetime] NULL,
	[umodified_time] [datetime] NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[rowid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AppCode] ON 

INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (1, N'Notification', N'C', N'一般', N'Common')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (2, N'Notification', N'U', N'緊急', N'Urgent')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (3, N'Message', N'C', N'一般', N'Common')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (4, N'Message', N'M', N'會議', N'Meeting')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (5, N'Message', N'V', N'個人', N'Private')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (6, N'Message', N'P', N'專案', N'Project')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (7, N'Message', N'Q', N'問題', N'Question')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (8, N'Message', N'U', N'緊急', N'Urgent')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (9, N'Notification', N'W', N'簽核', N'WorkFlow')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (10, N'Notification', N'N', N'通知', N'Notice')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (11, N'Notification', N'S', N'系統', N'System')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (12, N'Member', N'V', N'VIP', N'VIP會員')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (13, N'Member', N'N', N'一般', N'一般會員')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (14, N'Member', N'D', N'鑽石', N'鑽石會員')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (15, N'Member', N'G', N'黃金', N'黃金會員')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (16, N'Role', N'A', N'管理者', N'管理者')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (17, N'Role', N'M', N'專案管理者', N'專案管理者')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (18, N'Role', N'D', N'開發者', N'開發者')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (19, N'Role', N'U', N'測試者', N'測試者')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (20, N'Status', N'N', N'新問題', N'新問題')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (21, N'Status', N'A', N'已分配', N'已分配')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (22, N'Status', N'R', N'已解決', N'已解決')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (23, N'Status', N'C', N'已結束', N'已結束')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (24, N'Priority', N'Z', N'立即', N'立即')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (25, N'Priority', N'O', N'緊急', N'緊急')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (26, N'Priority', N'T', N'高', N'高')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (27, N'Priority', N'H', N'一般', N'一般')
INSERT [dbo].[AppCode] ([rowid], [type_no], [mno], [mname], [remark]) VALUES (28, N'Priority', N'F', N'低', N'低')
SET IDENTITY_INSERT [dbo].[AppCode] OFF
GO
SET IDENTITY_INSERT [dbo].[bugs] ON 

INSERT [dbo].[bugs] ([rowid], [bid], [bsummary], [bcreator], [bstatus_id], [bpriority_id], [binit_time], [bmodified_time], [bpid], [basignee], [bdescription]) VALUES (44, N'BT0001', N'EVT:FCS:無法刪除檔案', N'admin', N'N', N'O', CAST(N'2021-06-23T09:11:33.127' AS DateTime), CAST(N'2021-06-23T17:08:32.933' AS DateTime), N'P0001', N'admin', N'EVT:FCS:無法刪除檔案')
INSERT [dbo].[bugs] ([rowid], [bid], [bsummary], [bcreator], [bstatus_id], [bpriority_id], [binit_time], [bmodified_time], [bpid], [basignee], [bdescription]) VALUES (45, N'BT0002', N'test1', N'admin', N'A', N'F', CAST(N'2021-06-23T13:12:39.183' AS DateTime), CAST(N'2021-06-23T17:20:32.863' AS DateTime), N'P0001', N'0010', N'test1')
INSERT [dbo].[bugs] ([rowid], [bid], [bsummary], [bcreator], [bstatus_id], [bpriority_id], [binit_time], [bmodified_time], [bpid], [basignee], [bdescription]) VALUES (46, N'BT0003', N'test2', N'admin', N'A', N'F', CAST(N'2021-06-23T13:12:47.673' AS DateTime), CAST(N'2021-06-23T17:20:47.230' AS DateTime), N'P0001', N'adm', N'test2')
INSERT [dbo].[bugs] ([rowid], [bid], [bsummary], [bcreator], [bstatus_id], [bpriority_id], [binit_time], [bmodified_time], [bpid], [basignee], [bdescription]) VALUES (47, N'BT0004', N'test3', N'admin', N'A', N'F', CAST(N'2021-06-23T13:12:57.300' AS DateTime), CAST(N'2021-06-23T17:20:55.317' AS DateTime), N'P0001', N'adm', N'test3')
INSERT [dbo].[bugs] ([rowid], [bid], [bsummary], [bcreator], [bstatus_id], [bpriority_id], [binit_time], [bmodified_time], [bpid], [basignee], [bdescription]) VALUES (48, N'BT0005', N'test4', N'admin', N'A', N'O', CAST(N'2021-06-23T13:13:05.757' AS DateTime), CAST(N'2021-06-23T17:21:02.023' AS DateTime), N'P0001', N'789', N'test4')
INSERT [dbo].[bugs] ([rowid], [bid], [bsummary], [bcreator], [bstatus_id], [bpriority_id], [binit_time], [bmodified_time], [bpid], [basignee], [bdescription]) VALUES (49, N'BT0006', N'test5', N'admin', N'A', N'F', CAST(N'2021-06-23T13:13:13.853' AS DateTime), CAST(N'2021-06-23T17:21:09.330' AS DateTime), N'P0001', N'001', N'test5')
INSERT [dbo].[bugs] ([rowid], [bid], [bsummary], [bcreator], [bstatus_id], [bpriority_id], [binit_time], [bmodified_time], [bpid], [basignee], [bdescription]) VALUES (50, N'BT0007', N'test6', N'admin', N'A', N'H', CAST(N'2021-06-23T13:13:20.953' AS DateTime), CAST(N'2021-06-23T17:21:18.607' AS DateTime), N'P0001', N'234', N'test6')
INSERT [dbo].[bugs] ([rowid], [bid], [bsummary], [bcreator], [bstatus_id], [bpriority_id], [binit_time], [bmodified_time], [bpid], [basignee], [bdescription]) VALUES (51, N'BT0008', N'test7', N'admin', N'A', N'O', CAST(N'2021-06-23T13:13:30.067' AS DateTime), CAST(N'2021-06-23T17:21:25.860' AS DateTime), N'P0001', N'9999', N'test7')
INSERT [dbo].[bugs] ([rowid], [bid], [bsummary], [bcreator], [bstatus_id], [bpriority_id], [binit_time], [bmodified_time], [bpid], [basignee], [bdescription]) VALUES (52, N'BT0009', N'test8', N'admin', N'N', N'F', CAST(N'2021-06-23T13:13:37.870' AS DateTime), CAST(N'2021-06-23T17:21:35.033' AS DateTime), N'P0001', N'9999', N'test8')
INSERT [dbo].[bugs] ([rowid], [bid], [bsummary], [bcreator], [bstatus_id], [bpriority_id], [binit_time], [bmodified_time], [bpid], [basignee], [bdescription]) VALUES (53, N'BT0010', N'test9', N'admin', N'N', N'O', CAST(N'2021-06-23T13:13:53.737' AS DateTime), CAST(N'2021-06-23T17:21:42.673' AS DateTime), N'P0001', N'adm', N'test9')
INSERT [dbo].[bugs] ([rowid], [bid], [bsummary], [bcreator], [bstatus_id], [bpriority_id], [binit_time], [bmodified_time], [bpid], [basignee], [bdescription]) VALUES (54, N'BT0011', N'test10', N'admin', N'A', N'O', CAST(N'2021-06-23T13:14:04.077' AS DateTime), CAST(N'2021-06-23T17:21:55.237' AS DateTime), N'P0001', N'222', N'test10')
INSERT [dbo].[bugs] ([rowid], [bid], [bsummary], [bcreator], [bstatus_id], [bpriority_id], [binit_time], [bmodified_time], [bpid], [basignee], [bdescription]) VALUES (55, N'BT0012', N'test11', N'admin', N'A', N'F', CAST(N'2021-06-23T13:14:23.460' AS DateTime), CAST(N'2021-06-23T17:22:04.067' AS DateTime), N'P0001', N'bbb', N'test11')
INSERT [dbo].[bugs] ([rowid], [bid], [bsummary], [bcreator], [bstatus_id], [bpriority_id], [binit_time], [bmodified_time], [bpid], [basignee], [bdescription]) VALUES (56, N'BT0013', N'刪除檔案失敗', N'test', N'A', N'F', CAST(N'2021-06-23T23:34:36.727' AS DateTime), NULL, N'P0001', N'0010', N'刪除檔案失敗
==========
')
INSERT [dbo].[bugs] ([rowid], [bid], [bsummary], [bcreator], [bstatus_id], [bpriority_id], [binit_time], [bmodified_time], [bpid], [basignee], [bdescription]) VALUES (57, N'BT0014', N'EVT:FCS:無法修改檔案', N'001', N'C', N'Z', CAST(N'2021-06-24T08:34:39.533' AS DateTime), CAST(N'2021-06-24T08:41:42.297' AS DateTime), N'P0001', N'001', N'EVT:FCS:無法修改檔案
===================
請在一天內修復完成
====================
修復已完成
====================
問題已修復')
INSERT [dbo].[bugs] ([rowid], [bid], [bsummary], [bcreator], [bstatus_id], [bpriority_id], [binit_time], [bmodified_time], [bpid], [basignee], [bdescription]) VALUES (58, N'BT0015', N'EVT:FCS:無法刪除檔案1', N'test1', N'C', N'Z', CAST(N'2021-06-24T13:51:07.613' AS DateTime), CAST(N'2021-06-24T13:54:55.730' AS DateTime), N'P0001', N'test1', N'EVT:FCS:無法刪除檔案1
===
Bug已修復')
SET IDENTITY_INSERT [dbo].[bugs] OFF
GO
SET IDENTITY_INSERT [dbo].[priorities] ON 

INSERT [dbo].[priorities] ([rowid], [mno], [mname], [remark]) VALUES (1, N'Z', N'立即', NULL)
INSERT [dbo].[priorities] ([rowid], [mno], [mname], [remark]) VALUES (2, N'O', N'緊急', NULL)
INSERT [dbo].[priorities] ([rowid], [mno], [mname], [remark]) VALUES (3, N'T', N'高', NULL)
INSERT [dbo].[priorities] ([rowid], [mno], [mname], [remark]) VALUES (4, N'H', N'一般', NULL)
INSERT [dbo].[priorities] ([rowid], [mno], [mname], [remark]) VALUES (5, N'F', N'低', NULL)
SET IDENTITY_INSERT [dbo].[priorities] OFF
GO
SET IDENTITY_INSERT [dbo].[projects] ON 

INSERT [dbo].[projects] ([rowid], [pid], [pname], [pdescription], [pmanager_id], [pinit_time], [pmodified_time]) VALUES (4, N'P0001', N'專案一', N'專案一', N'test2', CAST(N'2021-06-03T09:18:23.550' AS DateTime), CAST(N'2021-06-03T12:09:38.533' AS DateTime))
INSERT [dbo].[projects] ([rowid], [pid], [pname], [pdescription], [pmanager_id], [pinit_time], [pmodified_time]) VALUES (6, N'P0002', N'專案四', N'專案四', N'test2', CAST(N'2021-06-03T11:34:39.223' AS DateTime), CAST(N'2021-06-03T11:34:39.223' AS DateTime))
INSERT [dbo].[projects] ([rowid], [pid], [pname], [pdescription], [pmanager_id], [pinit_time], [pmodified_time]) VALUES (8, N'P0003', N'專案五', N'專案五
專案五SR1', N'adm', CAST(N'2021-06-08T13:21:47.980' AS DateTime), CAST(N'2021-06-23T17:10:39.583' AS DateTime))
INSERT [dbo].[projects] ([rowid], [pid], [pname], [pdescription], [pmanager_id], [pinit_time], [pmodified_time]) VALUES (9, N'P0004', N'專案三', N'專案三', N'test2', CAST(N'2021-06-08T16:20:25.817' AS DateTime), CAST(N'2021-06-08T16:20:25.817' AS DateTime))
INSERT [dbo].[projects] ([rowid], [pid], [pname], [pdescription], [pmanager_id], [pinit_time], [pmodified_time]) VALUES (10, N'P0005', N'專案六', N'專案六', N'admin', CAST(N'2021-06-15T12:05:21.100' AS DateTime), CAST(N'2021-06-15T12:05:21.100' AS DateTime))
INSERT [dbo].[projects] ([rowid], [pid], [pname], [pdescription], [pmanager_id], [pinit_time], [pmodified_time]) VALUES (11, N'P0006', N'專案七', N'專案七', N'adm', CAST(N'2021-06-20T17:59:27.803' AS DateTime), CAST(N'2021-06-20T18:25:23.603' AS DateTime))
INSERT [dbo].[projects] ([rowid], [pid], [pname], [pdescription], [pmanager_id], [pinit_time], [pmodified_time]) VALUES (15, N'P0008', N'專案八', N'專案八', N'ccc', CAST(N'2021-06-20T20:01:16.150' AS DateTime), CAST(N'2021-06-20T20:01:51.427' AS DateTime))
INSERT [dbo].[projects] ([rowid], [pid], [pname], [pdescription], [pmanager_id], [pinit_time], [pmodified_time]) VALUES (16, N'P0009', N'專案二', N'專案二', N'M01', CAST(N'2021-06-22T20:38:24.720' AS DateTime), CAST(N'2021-06-23T19:18:41.990' AS DateTime))
INSERT [dbo].[projects] ([rowid], [pid], [pname], [pdescription], [pmanager_id], [pinit_time], [pmodified_time]) VALUES (17, N'P0010', N'專案十', N'專案十', N'0010', CAST(N'2021-06-23T17:09:43.500' AS DateTime), CAST(N'2021-06-23T19:18:45.090' AS DateTime))
INSERT [dbo].[projects] ([rowid], [pid], [pname], [pdescription], [pmanager_id], [pinit_time], [pmodified_time]) VALUES (18, N'P0011', N'專案十一', N'專案十一', N'adm', CAST(N'2021-06-23T17:09:56.697' AS DateTime), CAST(N'2021-06-23T19:18:48.097' AS DateTime))
INSERT [dbo].[projects] ([rowid], [pid], [pname], [pdescription], [pmanager_id], [pinit_time], [pmodified_time]) VALUES (19, N'P0012', N'專案十二', N'專案十二', N'0010', CAST(N'2021-06-23T17:10:09.147' AS DateTime), CAST(N'2021-06-23T19:18:50.873' AS DateTime))
INSERT [dbo].[projects] ([rowid], [pid], [pname], [pdescription], [pmanager_id], [pinit_time], [pmodified_time]) VALUES (20, N'P0013', N'專案八點一', N'專案八點一', N'016', CAST(N'2021-06-24T08:51:18.347' AS DateTime), CAST(N'2021-06-24T08:51:31.590' AS DateTime))
INSERT [dbo].[projects] ([rowid], [pid], [pname], [pdescription], [pmanager_id], [pinit_time], [pmodified_time]) VALUES (21, N'P0014', N'專案八點二', N'專案八點二', N'M01', CAST(N'2021-06-24T13:58:30.897' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[projects] OFF
GO
SET IDENTITY_INSERT [dbo].[roles] ON 

INSERT [dbo].[roles] ([rowid], [rid], [rrule], [rname], [rremark]) VALUES (1, N'RO0001', N'U', N'測試者', NULL)
INSERT [dbo].[roles] ([rowid], [rid], [rrule], [rname], [rremark]) VALUES (2, N'RO0002', N'A', N'管理者', NULL)
INSERT [dbo].[roles] ([rowid], [rid], [rrule], [rname], [rremark]) VALUES (3, N'RO0003', N'D', N'開發者', NULL)
INSERT [dbo].[roles] ([rowid], [rid], [rrule], [rname], [rremark]) VALUES (4, N'RO0004', N'M', N'專案管理者', NULL)
SET IDENTITY_INSERT [dbo].[roles] OFF
GO
SET IDENTITY_INSERT [dbo].[status] ON 

INSERT [dbo].[status] ([rowid], [mno], [mname], [remark]) VALUES (1, N'N', N'新問題', NULL)
INSERT [dbo].[status] ([rowid], [mno], [mname], [remark]) VALUES (2, N'A', N'已分配', NULL)
INSERT [dbo].[status] ([rowid], [mno], [mname], [remark]) VALUES (4, N'R', N'已解決', NULL)
INSERT [dbo].[status] ([rowid], [mno], [mname], [remark]) VALUES (5, N'C', N'已結束', NULL)
SET IDENTITY_INSERT [dbo].[status] OFF
GO
SET IDENTITY_INSERT [dbo].[UserMessage] ON 

INSERT [dbo].[UserMessage] ([rowid], [code_no], [date_sender], [date_read], [sender_no], [sender_name], [receive_no], [receive_name], [is_read], [text_title], [text_content], [remark]) VALUES (7, N'C', CAST(N'2021-06-18T14:52:33.953' AS DateTime), NULL, N'', N'', N'9999', N'9999', 0, N'test', N'test', N'')
INSERT [dbo].[UserMessage] ([rowid], [code_no], [date_sender], [date_read], [sender_no], [sender_name], [receive_no], [receive_name], [is_read], [text_title], [text_content], [remark]) VALUES (9, N'C', CAST(N'2021-06-18T15:09:53.330' AS DateTime), CAST(N'2021-06-18T15:10:16.123' AS DateTime), N'001', N'王大明', N'ccc', N'李曉華', 1, N'test', N'test', N'')
INSERT [dbo].[UserMessage] ([rowid], [code_no], [date_sender], [date_read], [sender_no], [sender_name], [receive_no], [receive_name], [is_read], [text_title], [text_content], [remark]) VALUES (12, N'U', CAST(N'2021-06-21T09:57:03.427' AS DateTime), CAST(N'2021-06-21T09:57:29.033' AS DateTime), N'001', N'王大明', N'adm', N'adm', 1, N'系統當機問題', N'系統當機問題,請盡快解決', N'')
INSERT [dbo].[UserMessage] ([rowid], [code_no], [date_sender], [date_read], [sender_no], [sender_name], [receive_no], [receive_name], [is_read], [text_title], [text_content], [remark]) VALUES (13, N'U', CAST(N'2021-06-21T09:58:49.927' AS DateTime), CAST(N'2021-06-21T12:33:31.780' AS DateTime), N'adm', N'adm', N'001', N'王大明', 1, N'已收到Bug', N'已收到Bug,會盡快處理', N'')
INSERT [dbo].[UserMessage] ([rowid], [code_no], [date_sender], [date_read], [sender_no], [sender_name], [receive_no], [receive_name], [is_read], [text_title], [text_content], [remark]) VALUES (14, N'U', CAST(N'2021-06-21T13:25:25.123' AS DateTime), CAST(N'2021-06-21T13:26:35.550' AS DateTime), N'adm', N'adm', N'd01', N'黃小牛', 1, N'應用程式閃退,請盡快解決', N'應用程式閃退,請盡快解決,需要1天內處理好
謝謝', N'')
INSERT [dbo].[UserMessage] ([rowid], [code_no], [date_sender], [date_read], [sender_no], [sender_name], [receive_no], [receive_name], [is_read], [text_title], [text_content], [remark]) VALUES (15, N'U', CAST(N'2021-06-21T13:27:06.430' AS DateTime), NULL, N'd01', N'黃小牛', N'adm', N'adm', 0, N'收到會儘快處理好', N'收到會儘快處理好', N'')
INSERT [dbo].[UserMessage] ([rowid], [code_no], [date_sender], [date_read], [sender_no], [sender_name], [receive_no], [receive_name], [is_read], [text_title], [text_content], [remark]) VALUES (16, N'U', CAST(N'2021-06-21T13:28:37.147' AS DateTime), NULL, N'd01', N'黃小牛', N'adm', N'adm', 0, N'問題已解決', N'問題已解決', N'')
INSERT [dbo].[UserMessage] ([rowid], [code_no], [date_sender], [date_read], [sender_no], [sender_name], [receive_no], [receive_name], [is_read], [text_title], [text_content], [remark]) VALUES (17, N'U', CAST(N'2021-06-21T23:51:20.483' AS DateTime), NULL, N'003', N'王曉同', N'd01', N'黃小牛', 0, N'APP應用程式沒有回應', N'APP應用程式沒有回應,請幫我看一下問題
謝謝!', N'')
INSERT [dbo].[UserMessage] ([rowid], [code_no], [date_sender], [date_read], [sender_no], [sender_name], [receive_no], [receive_name], [is_read], [text_title], [text_content], [remark]) VALUES (19, N'U', CAST(N'2021-06-22T20:00:32.520' AS DateTime), NULL, N'005', N'葉蕭豐', N'd01', N'黃小牛', 0, N'無法刪除檔案', N'無法刪除檔案', N'')
INSERT [dbo].[UserMessage] ([rowid], [code_no], [date_sender], [date_read], [sender_no], [sender_name], [receive_no], [receive_name], [is_read], [text_title], [text_content], [remark]) VALUES (20, N'U', CAST(N'2021-06-22T20:01:09.063' AS DateTime), NULL, N'005', N'葉蕭豐', N'd01', N'黃小牛', 0, N'系統當機問題', N'系統當機問題', N'')
INSERT [dbo].[UserMessage] ([rowid], [code_no], [date_sender], [date_read], [sender_no], [sender_name], [receive_no], [receive_name], [is_read], [text_title], [text_content], [remark]) VALUES (21, N'U', CAST(N'2021-06-23T08:34:50.270' AS DateTime), NULL, N'001', N'王大明', N'd01', N'黃小牛', 0, N'BT0009	EVT:FCS:儲存檔案產生亂碼', N'BT0009	EVT:FCS:儲存檔案產生亂碼', N'')
INSERT [dbo].[UserMessage] ([rowid], [code_no], [date_sender], [date_read], [sender_no], [sender_name], [receive_no], [receive_name], [is_read], [text_title], [text_content], [remark]) VALUES (22, N'U', CAST(N'2021-06-23T08:40:00.223' AS DateTime), CAST(N'2021-06-24T08:42:36.990' AS DateTime), N'd01', N'黃小牛', N'001', N'王大明', 1, N'BT0009	EVT:FCS:儲存檔案產生亂碼--已解決', N'BT0009	EVT:FCS:儲存檔案產生亂碼--已解決
請幫忙驗證', N'')
INSERT [dbo].[UserMessage] ([rowid], [code_no], [date_sender], [date_read], [sender_no], [sender_name], [receive_no], [receive_name], [is_read], [text_title], [text_content], [remark]) VALUES (23, N'U', CAST(N'2021-06-24T13:52:46.957' AS DateTime), CAST(N'2021-06-24T13:53:04.263' AS DateTime), N'M01', N'樟柯啍', N'd01', N'黃小牛', 1, N'系統當機問題', N'系統當機問題', N'')
SET IDENTITY_INSERT [dbo].[UserMessage] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (4, N'U0004', N'admin', N'管理者', N'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=', N'0977865431', N'admin@gmail.com', N'A', CAST(N'1977-05-05' AS Date), N'test1', NULL, 1, NULL, CAST(N'2021-06-23T17:09:11.603' AS DateTime))
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (8, N'U0009', N'001', N'王大明', N'ej5rFst19I+4l+/zrnMvMVT20gO1PzNmDwG0w7a8Lfk=', N'0977865431', N'001@gmail.com', N'U', CAST(N'1977-05-05' AS Date), N'123123', NULL, 1, NULL, NULL)
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (36, N'U0012', N'aaa', N'aaa', N'mDSHbc+wXLFnpcJJU+uljErImxrfV/KPL50JrxB+6PA=', N'0937660099', N'456@gmail.com', N'U', CAST(N'1955-11-30' AS Date), N'test', NULL, 1, NULL, NULL)
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (37, N'U0013', N'222', N'222', N'm4cVEjJ8Cc6R3WSbP5amO3QI7yZ8jMVxARTmKXMMth8=', N'0937660099', N'222@ewrwr.com', N'U', CAST(N'1955-11-30' AS Date), N'3333', NULL, 1, NULL, CAST(N'2021-06-22T20:37:30.307' AS DateTime))
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (39, N'U0014', N'9999', N'9999', N'iI3yWuNXckJKVgxxUqHeeURA4Opc/uYoKDM6RWpQbgU=', N'9999', N'111@ewrwr.com', N'U', CAST(N'1955-11-30' AS Date), N'99999', NULL, 1, NULL, CAST(N'2021-06-23T17:12:27.957' AS DateTime))
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (40, N'U0015', N'bbb', N'bbb', N'PnRLncOTibrwxaBmBYm4QC89u0m4mz518sk1WFKjxnc=', N'0976543210', N'31@gmail.com', N'U', CAST(N'1977-07-08' AS Date), N'', NULL, 1, NULL, NULL)
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (41, N'U0016', N'ccc', N'李曉華', N'ZNqkStST/yipbv+rbnfxcyo9l9gyQVgbN9vXCnpJAP4=', N'0976543210', N'89@gmail.com', N'U', CAST(N'1991-06-05' AS Date), N'test', NULL, 1, NULL, NULL)
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (42, N'U0017', N'adm', N'adm', N'hvZeKKdU4acbLflANhWmxDbDLEKnWhDQKBOWG4bx5Cg=', N'0937660099', N'012@gmail.com', N'A', CAST(N'1981-06-17' AS Date), N'test', NULL, 1, NULL, NULL)
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (44, N'U0019', N'009', N'張小虹', N'U3HhoGTzz/9i3svBgf36D+jm/AeK+tDjbkOKlg04pOM=', N'0937660099', N'200000@gmail.com', N'A', CAST(N'2001-06-08' AS Date), N'test', NULL, 1, CAST(N'2021-06-20T20:03:22.593' AS DateTime), CAST(N'2021-06-23T17:12:49.823' AS DateTime))
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (45, N'U0020', N'd01', N'黃小牛', N'FJsmdZhLskOT66uFzDWOYsILZFeq1ho6zjmqh3OODcU=', N'0957-338970', N'd01@gmail.com', N'D', CAST(N'1991-06-14' AS Date), N'test', NULL, 1, CAST(N'2021-06-21T10:02:05.383' AS DateTime), CAST(N'2021-06-23T17:08:57.570' AS DateTime))
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (46, N'U0021', N'M01', N'樟柯啍', N'2Wj85CT2IY2y0vjh9Af/NRUK68DssQspcY0NtDWbBeM=', N'0977-369890', N'm01@gmail.com', N'A', CAST(N'1991-06-13' AS Date), N'test', NULL, 1, CAST(N'2021-06-21T10:03:43.377' AS DateTime), CAST(N'2021-06-23T12:39:28.167' AS DateTime))
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (47, N'U0022', N'003', N'王曉同', N'iMBBO/7x0FcKim+ceAqNLJ6QxNEHVR1ivzzsn/H1tjQ=', N'0933-786987', N'003@gmail.com', N'U', CAST(N'1991-06-05' AS Date), N'003', NULL, 1, CAST(N'2021-06-21T23:47:25.130' AS DateTime), NULL)
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (48, N'U0023', N'005', N'葉蕭豐', N'/N5gwi8IDIJaTX94/Ncqq13hXBnAENL8GIsPIHAxqXA=', N'0976-543298', N'300000@gmail.com', N'U', CAST(N'1991-06-13' AS Date), NULL, NULL, 1, NULL, CAST(N'2021-06-23T17:12:40.877' AS DateTime))
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (49, N'U0024', N'006', N'陳振', N'wPiQ8v1rBYED3tjSluSbo7kkZhIQyof/GQ3kHbuR3uA=', N'0937-009876', N'006@gmail.com', N'U', CAST(N'1991-06-14' AS Date), NULL, NULL, 1, CAST(N'2021-06-22T20:35:26.580' AS DateTime), NULL)
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (50, N'U0025', N'007', N'007', N'Yp9M+TN7DQx28wXYYPmIlM+ownlRa0JXR1FMqHEN65c=', N'0933-786987', N'007@gmail.com', N'U', CAST(N'2021-06-17' AS Date), N'test', NULL, 1, CAST(N'2021-06-22T20:36:35.087' AS DateTime), NULL)
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (52, N'U0027', N'456', N'456', N's6jg4fmrG/46NvIx9nb3i7MKUZ0rIebFMMDu6Ou0pdA=', N'0937-665431', N'456@gmail.com', N'U', CAST(N'2021-06-11' AS Date), N'456', NULL, 1, CAST(N'2021-06-22T20:41:32.580' AS DateTime), CAST(N'2021-06-22T20:42:10.653' AS DateTime))
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (53, N'U0028', N'789', N'789', N'NanjgbGidWdUm1+Kb3g8Fn6/gJ8cTWqeNnJASE2M4oE=', N'0933-786987', N'444@123.com', N'U', CAST(N'1955-11-24' AS Date), NULL, NULL, 1, CAST(N'2021-06-22T20:52:11.023' AS DateTime), NULL)
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (54, N'U0029', N'234', N'234', N'EUvRUfj7DFhkLSFw2krn18V5dyYKwsyJBTBsq2sqyrw=', N'0937660099', N'111@ewrwr.com', N'U', CAST(N'1955-11-30' AS Date), N'test', NULL, 0, CAST(N'2021-06-22T21:11:19.783' AS DateTime), NULL)
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (55, N'U0030', N'345', N'345', N'2nDfpNn5Wsl5+SHo5iM1gjYxPzNK/NBs3filYhz2oek=', N'0937660099', N'444@123.com', N'U', CAST(N'2021-06-06' AS Date), NULL, NULL, 0, CAST(N'2021-06-22T21:15:09.440' AS DateTime), NULL)
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (56, N'U0031', N'111', N'111', N'9uCh4qxBlFqap/+KiqoM68EqO8yYGpKa1c+BCgkOEa4=', N'1111', N'111@ewrwr.com', N'U', CAST(N'2011-05-04' AS Date), NULL, NULL, 0, CAST(N'2021-06-22T21:18:00.597' AS DateTime), NULL)
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (57, N'U0032', N'0010', N'0010', N'Cwjj3MUP5OXO6bCzpnGo25NvgzW6kFBpbUHLuaB/IuM=', N'0937660099', N'0010@ewrwr.com', N'U', CAST(N'2021-06-05' AS Date), N'test', NULL, 1, CAST(N'2021-06-23T12:37:26.450' AS DateTime), CAST(N'2021-06-23T17:15:55.083' AS DateTime))
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (58, N'U0033', N'011', N'011', N'iggM6oCdG50ztUHzSYsbY2b/xm33VCMQiUcIUFfPL5k=', N'011', N'011@ewrwr.com', N'A', CAST(N'1955-11-30' AS Date), NULL, NULL, 0, CAST(N'2021-06-23T12:40:10.190' AS DateTime), CAST(N'2021-06-23T17:13:33.127' AS DateTime))
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (59, N'U0034', N'012', N'012', N'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'0937660099', N'012@ewrwr.com', N'U', CAST(N'2001-06-01' AS Date), N'test', NULL, 1, CAST(N'2021-06-23T12:50:49.457' AS DateTime), NULL)
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (60, N'U0035', N'013', N'013', N'sW2eK7RKS3KNBDGiHBA+HAGLFySNPJXBkCUxyfwcCsM=', N'0937660099', N'013@ewrwr.com', N'U', CAST(N'1955-11-30' AS Date), N'test', NULL, 1, CAST(N'2021-06-23T13:05:21.887' AS DateTime), NULL)
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (61, N'U0036', N'014', N'014', N'cqvfzXVADP7icec3tPES5fZx02kdIV7WFtsq2NWnd40=', N'0937660099', N'014@ewrwr.com', N'U', CAST(N'1955-11-30' AS Date), N'test', NULL, 1, NULL, NULL)
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (62, N'U0037', N'015', N'015', N'LVSrNlgcfpBe/rPKaXMDioPPvhyxwlPNbCbbSgMg0O0=', N'015', N'015@ewrwr.com', N'A', CAST(N'1991-06-05' AS Date), N'test', NULL, 1, CAST(N'2021-06-23T17:17:15.467' AS DateTime), CAST(N'2021-06-23T17:22:49.863' AS DateTime))
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (63, N'U0038', N'test', N'測試人員', N'n4bQgYhMfWWaL+qgxVrQFaO/TxsrC4Is0V1sFbDwCgg=', N'0976543210', N'3000000000000@gmail.com', N'U', CAST(N'2011-06-03' AS Date), NULL, NULL, 1, NULL, NULL)
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (64, N'U0039', N'016', N'016', N'hEnCDtZjmS3ykUS2Y0VH/eVPpzzqpn9w3CEayxBS4po=', N'016', N'016@ewrwr.com', N'A', CAST(N'2001-06-06' AS Date), N'016', NULL, 1, CAST(N'2021-06-24T08:49:29.597' AS DateTime), CAST(N'2021-06-24T08:50:03.620' AS DateTime))
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (65, N'U0040', N'test1', N'test1', N'G08OmFGXGZjnMgeFRMlrNsPQHO33yqMyNZ1vHYNWcBQ=', N'0976543210', N'0@gmail.com', N'U', CAST(N'2011-06-01' AS Date), N'test', NULL, 1, NULL, NULL)
INSERT [dbo].[users] ([rowid], [uid], [umno], [uname], [upassword], [uphone], [uemail], [urole], [ubirthday], [remark], [varify_code], [isvarify], [uinit_time], [umodified_time]) VALUES (66, N'U0041', N'017', N'017', N'kwn2f9ZIBmOkjPkCNuwUwdthWDglrCEBEuKr8Z5H2Vw=', N'0937660099', N'017@ewrwr.com', N'U', CAST(N'2021-06-03' AS Date), N'test', NULL, 1, CAST(N'2021-06-24T13:56:45.083' AS DateTime), CAST(N'2021-06-24T13:57:23.327' AS DateTime))
SET IDENTITY_INSERT [dbo].[users] OFF
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_uid]  DEFAULT ([dbo].[NewUID]()) FOR [uid]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_isvarify]  DEFAULT ((0)) FOR [isvarify]
GO
USE [master]
GO
ALTER DATABASE [bug_tracker] SET  READ_WRITE 
GO
