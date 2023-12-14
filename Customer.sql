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
INSERT [dbo].[Customer] ([FirstName], [LastName], [Email], [Phone], [Address], [Gender], [Age], [Password]) VALUES (N'Dat', N'Truong', N'nhoktora0108@gmail.com                            ', N'0901234567', N'Tp HCM', N'Male      ', 21, N'AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAALkJkWOECOE6bajguubGl3QAAAAACAAAAAAAQZgAAAAEAACAAAABA4/HF+OEZB8x2nyOevwVWzO0pAq5IPQjTGe/ZOQ6JSwAAAAAOgAAAAAIAACAAAAC9eijZV2cskjDX96ZeAwJGXwYe664AAUMXtgsPtgSvExAAAABaAySSZTF8mTbMco7p0Z/oQAAAABybYgXCa7gL9HRQ0AGHe5lEGmABa4bm40h5kvBQ/Mq6ZV3dIMgk1PIx1Y/EY4MiwlobyBj5czF06aUsuae1ILc=@@@@wga4244+uVU8rtl5UWu4f6uN4Xs=')
GO
