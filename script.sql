USE [master]
GO
/****** Object:  Database [MyShopDB]    Script Date: 1/5/2024 10:54:17 AM ******/
CREATE DATABASE [MyShopDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MyShopDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\MyShopDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MyShopDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\MyShopDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MyShopDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MyShopDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MyShopDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MyShopDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MyShopDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MyShopDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MyShopDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [MyShopDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MyShopDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MyShopDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MyShopDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MyShopDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MyShopDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MyShopDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MyShopDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MyShopDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MyShopDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MyShopDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MyShopDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MyShopDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MyShopDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MyShopDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MyShopDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MyShopDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MyShopDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MyShopDB] SET  MULTI_USER 
GO
ALTER DATABASE [MyShopDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MyShopDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MyShopDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MyShopDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MyShopDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MyShopDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [MyShopDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [MyShopDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MyShopDB]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 1/5/2024 10:54:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[FirstName] [nvarchar](30) NULL,
	[LastName] [nvarchar](30) NULL,
	[Email] [char](50) NULL,
	[Phone] [nchar](10) NULL,
	[Address] [nvarchar](50) NULL,
	[Gender] [nchar](10) NULL,
	[Age] [int] NULL,
	[Password] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerClass]    Script Date: 1/5/2024 10:54:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerClass](
	[name] [nvarchar](50) NULL,
	[address] [nvarchar](100) NULL,
	[phoneNum] [char](10) NOT NULL,
	[gender] [nvarchar](3) NULL,
	[Email] [nvarchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerOrder]    Script Date: 1/5/2024 10:54:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerOrder](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[phoneNum] [char](10) NULL,
	[createDate] [date] NULL,
	[shipmentDate] [date] NULL,
	[status] [nvarchar](9) NULL,
	[totalCost] [int] NULL,
 CONSTRAINT [PK_Key] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 1/5/2024 10:54:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderId] [int] NOT NULL,
	[PhoneName] [nvarchar](300) NULL,
	[amount] [int] NULL,
	[total] [int] NULL,
	[image] [nvarchar](300) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[Customer] ([FirstName], [LastName], [Email], [Phone], [Address], [Gender], [Age], [Password]) VALUES (N'nam', N'hoang', N'nam@gmail.com                                     ', N'0359935724', N'hcm', N'          ', 21, N'DGYYLscQhABl66pHxebOkA==')
INSERT [dbo].[Customer] ([FirstName], [LastName], [Email], [Phone], [Address], [Gender], [Age], [Password]) VALUES (N'nam', N'hoang', N'htn@gmail.com                                     ', N'0399      ', N'hcm', N'Male      ', 21, N'yXVUkR45PFz0UfpbDB8/ew==')
GO
INSERT [dbo].[CustomerClass] ([name], [address], [phoneNum], [gender], [Email]) VALUES (N'Nam', N'abc', N'0359935724', N'Nam', N'htn@gmail.com')
INSERT [dbo].[CustomerClass] ([name], [address], [phoneNum], [gender], [Email]) VALUES (N'Nam', N'New test', N'0359935725', N'Nam', N'htn@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[CustomerOrder] ON 

INSERT [dbo].[CustomerOrder] ([OrderId], [phoneNum], [createDate], [shipmentDate], [status], [totalCost]) VALUES (2, N'0359935725', CAST(N'2024-01-05' AS Date), CAST(N'2024-01-08' AS Date), N'pending', 40000000)
SET IDENTITY_INSERT [dbo].[CustomerOrder] OFF
GO
INSERT [dbo].[OrderDetail] ([OrderId], [PhoneName], [amount], [total], [image]) VALUES (2, N'Google Pixel 4', 1, 20000000, N'https://i.imgur.com/iaFsx2r.jpg')
INSERT [dbo].[OrderDetail] ([OrderId], [PhoneName], [amount], [total], [image]) VALUES (2, N'iPhone 14', 1, 20000000, N'https://i.imgur.com/iaFsx2r.jpg')
INSERT [dbo].[OrderDetail] ([OrderId], [PhoneName], [amount], [total], [image]) VALUES (2, N'iPhone 7', 2, 40000000, N'https://i.imgur.com/iaFsx2r.jpg')
GO
ALTER TABLE [dbo].[CustomerOrder]  WITH CHECK ADD  CONSTRAINT [Check_create_shipmentDate] CHECK  (([createDate]<=[shipmentDate]))
GO
ALTER TABLE [dbo].[CustomerOrder] CHECK CONSTRAINT [Check_create_shipmentDate]
GO
USE [master]
GO
ALTER DATABASE [MyShopDB] SET  READ_WRITE 
GO
