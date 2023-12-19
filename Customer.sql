CREATE DATABASE [MyShopDB]
GO

USE [MyShopDB]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 12/14/2023 7:36:16 PM ******/
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