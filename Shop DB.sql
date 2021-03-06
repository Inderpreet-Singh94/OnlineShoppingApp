USE [master]
GO
/****** Object:  Database [shopdb]    Script Date: 2020-07-25 17:46:59 ******/
CREATE DATABASE [shopdb]
GO
USE [shopdb]
GO
/****** Object:  Table [dbo].[products]    Script Date: 2020-07-25 17:47:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[prodid] [int] IDENTITY(101,1) NOT NULL,
	[pname] [varchar](50) NOT NULL,
	[pcat] [varchar](50) NOT NULL,
	[descr] [varchar](255) NULL,
	[discount] [int] NULL,
	[price] [int] NULL,
	[aqty] [int] NULL,
	[minqty] [int] NULL,
	[maxqty] [int] NULL,
	[stdate] [date] NULL,
	[enddate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[prodid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cart]    Script Date: 2020-07-25 17:47:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart](
	[userid] [varchar](50) NULL,
	[prodid] [int] NULL,
	[qty] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vcart]    Script Date: 2020-07-25 17:47:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vcart]
as
select cart.prodid,pname,price,qty,aqty,discount,(price-discount)*qty as amount,userid from cart inner join products
on cart.prodid=products.prodid

GO
/****** Object:  Table [dbo].[orders]    Script Date: 2020-07-25 17:47:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[orderid] [int] NOT NULL,
	[userid] [varchar](30) NULL,
	[orderdate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[orderid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_details]    Script Date: 2020-07-25 17:47:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_details](
	[orderid] [int] NULL,
	[prodid] [int] NULL,
	[qty] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vorders]    Script Date: 2020-07-25 17:47:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vorders]
as
select a.orderid,orderdate,userid,b.prodid,pname,qty,price from orders a inner join order_details b
on a.orderid=b.orderid
inner join products p
on p.prodid=b.prodid
GO
/****** Object:  View [dbo].[vsoldqty]    Script Date: 2020-07-25 17:47:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[vsoldqty]
as
select prodid,sum(qty) qtysold from order_details
group by prodid
GO
/****** Object:  View [dbo].[inventory]    Script Date: 2020-07-25 17:47:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[inventory]
as
select p.prodid,qtysold,pname,discount,price,aqty,price*qtysold amount from vsoldqty v inner join products p
on v.prodid=p.prodid
GO
/****** Object:  Table [dbo].[customers]    Script Date: 2020-07-25 17:47:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customers](
	[custid] [int] IDENTITY(1001,1) NOT NULL,
	[cname] [varchar](50) NOT NULL,
	[address] [varchar](150) NULL,
	[gender] [varchar](15) NULL,
	[email] [varchar](20) NULL,
	[phone] [varchar](20) NULL,
	[userid] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[custid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 2020-07-25 17:47:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[userid] [varchar](50) NOT NULL,
	[pwd] [varchar](50) NOT NULL,
	[role] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[customers] ON 

INSERT [dbo].[customers] ([custid], [cname], [address], [gender], [email], [phone], [userid]) VALUES (1001, N'Jaskaran Singh', N'Montreal', N'Male', N'jaskara@gmail.com', N'5410987634', N'jaskaransingh')
INSERT [dbo].[customers] ([custid], [cname], [address], [gender], [email], [phone], [userid]) VALUES (1002, N'Inderpreet Singh', N'Montreal', N'Male', N'inder@gmail.com', N'4384926829', N'inderpreetsingh')
INSERT [dbo].[customers] ([custid], [cname], [address], [gender], [email], [phone], [userid]) VALUES (1003, N'Satnam Singh', N'Montreal', N'Male', N'satnam@gmail.com', N'5145145140', N'satnamsingh')
INSERT [dbo].[customers] ([custid], [cname], [address], [gender], [email], [phone], [userid]) VALUES (1003, N'Sagar Kumar', N'Montreal', N'Male', N'sagar@gmail.com', N'5145145122', N'sagarkumar')
SET IDENTITY_INSERT [dbo].[customers] OFF
INSERT [dbo].[order_details] ([orderid], [prodid], [qty]) VALUES (1001, 107, 1)
INSERT [dbo].[order_details] ([orderid], [prodid], [qty]) VALUES (1001, 109, 3)
INSERT [dbo].[order_details] ([orderid], [prodid], [qty]) VALUES (1001, 110, 2)
INSERT [dbo].[order_details] ([orderid], [prodid], [qty]) VALUES (1003, 104, 1)
INSERT [dbo].[order_details] ([orderid], [prodid], [qty]) VALUES (1003, 108, 1)
INSERT [dbo].[order_details] ([orderid], [prodid], [qty]) VALUES (1004, 109, 1)
INSERT [dbo].[order_details] ([orderid], [prodid], [qty]) VALUES (1005, 106, 1)
INSERT [dbo].[order_details] ([orderid], [prodid], [qty]) VALUES (1006, 108, 1)
INSERT [dbo].[order_details] ([orderid], [prodid], [qty]) VALUES (1006, 111, 1)
INSERT [dbo].[order_details] ([orderid], [prodid], [qty]) VALUES (1007, 109, 2)
INSERT [dbo].[orders] ([orderid], [userid], [orderdate]) VALUES (1001, N'jaskaransingh', CAST(N'2020-07-25' AS Date))
INSERT [dbo].[orders] ([orderid], [userid], [orderdate]) VALUES (1002, N'jaskaransingh', CAST(N'2020-07-25' AS Date))
INSERT [dbo].[orders] ([orderid], [userid], [orderdate]) VALUES (1003, N'jaskaransingh', CAST(N'2020-07-25' AS Date))
INSERT [dbo].[orders] ([orderid], [userid], [orderdate]) VALUES (1004, N'jaskaransingh', CAST(N'2020-07-25' AS Date))
INSERT [dbo].[orders] ([orderid], [userid], [orderdate]) VALUES (1005, N'jaskaransingh', CAST(N'2020-07-25' AS Date))
INSERT [dbo].[orders] ([orderid], [userid], [orderdate]) VALUES (1006, N'inderpreetsingh', CAST(N'2020-07-25' AS Date))
INSERT [dbo].[orders] ([orderid], [userid], [orderdate]) VALUES (1007, N'inderpreetsingh', CAST(N'2020-07-25' AS Date))
SET IDENTITY_INSERT [dbo].[products] ON 

INSERT [dbo].[products] ([prodid], [pname], [pcat], [descr], [discount], [price], [aqty], [minqty], [maxqty], [stdate], [enddate]) VALUES (104, N'Pepsi', N'Cold Drink', N'Cold drinks', 5, 50, 7, 4, 10, CAST(N'2020-07-25' AS Date), CAST(N'2020-07-25' AS Date))
INSERT [dbo].[products] ([prodid], [pname], [pcat], [descr], [discount], [price], [aqty], [minqty], [maxqty], [stdate], [enddate]) VALUES (105, N'Thumps Up', N'Cold Drink', N'Cold drinks Black', 0, 50, 20, 0, 0, CAST(N'2020-07-25' AS Date), CAST(N'2020-07-25' AS Date))
INSERT [dbo].[products] ([prodid], [pname], [pcat], [descr], [discount], [price], [aqty], [minqty], [maxqty], [stdate], [enddate]) VALUES (106, N'Samsung Mouse', N'Electronic Items', N'Computer Items', 5, 350, 17, 0, 0, CAST(N'2020-07-25' AS Date), CAST(N'2020-07-25' AS Date))
INSERT [dbo].[products] ([prodid], [pname], [pcat], [descr], [discount], [price], [aqty], [minqty], [maxqty], [stdate], [enddate]) VALUES (107, N'Keyboard', N'Electronic Items', N'Keyboard for computer', 5, 600, 17, 4, 10, CAST(N'2020-07-25' AS Date), CAST(N'2020-07-31' AS Date))
INSERT [dbo].[products] ([prodid], [pname], [pcat], [descr], [discount], [price], [aqty], [minqty], [maxqty], [stdate], [enddate]) VALUES (108, N'RAM 4 GB', N'Electronic Items', N'Computer Items', 10, 3000, 5, 0, 0, CAST(N'2020-07-25' AS Date), CAST(N'2020-07-25' AS Date))
INSERT [dbo].[products] ([prodid], [pname], [pcat], [descr], [discount], [price], [aqty], [minqty], [maxqty], [stdate], [enddate]) VALUES (109, N'Pen Drive 8 GB', N'Electronic Items', N'Computer Items', 10, 700, 1, 0, 0, CAST(N'2020-07-25' AS Date), CAST(N'2020-07-25' AS Date))
INSERT [dbo].[products] ([prodid], [pname], [pcat], [descr], [discount], [price], [aqty], [minqty], [maxqty], [stdate], [enddate]) VALUES (110, N'RAM 16 GB', N'Electronic Items', N'Computer Item', 10, 6000, 14, 0, 0, CAST(N'2020-07-25' AS Date), CAST(N'2020-07-25' AS Date))
INSERT [dbo].[products] ([prodid], [pname], [pcat], [descr], [discount], [price], [aqty], [minqty], [maxqty], [stdate], [enddate]) VALUES (111, N'Pen Drive 32 GB', N'Electronic Items', N'Computer Item', 10, 8000, 13, 3, 5, CAST(N'2020-07-25' AS Date), CAST(N'2020-07-25' AS Date))
INSERT [dbo].[products] ([prodid], [pname], [pcat], [descr], [discount], [price], [aqty], [minqty], [maxqty], [stdate], [enddate]) VALUES (112, N'Wireless mouse', N'Electornic Items', N'Computer mouse', 10, 600, 50, 5, 10, CAST(N'2020-07-25' AS Date), CAST(N'2020-07-31' AS Date))
SET IDENTITY_INSERT [dbo].[products] OFF
INSERT [dbo].[users] ([userid], [pwd], [role]) VALUES (N'Admin', N'anand', N'admin')
INSERT [dbo].[users] ([userid], [pwd], [role]) VALUES (N'jaskaransingh', N'jaskrans17', N'customer')
INSERT [dbo].[users] ([userid], [pwd], [role]) VALUES (N'inderpreetsingh', N'inder', N'customer')
INSERT [dbo].[users] ([userid], [pwd], [role]) VALUES (N'satnamsingh', N'satnam', N'customer')
INSERT [dbo].[users] ([userid], [pwd], [role]) VALUES (N'sagarkumar', N'sagar', N'customer')
ALTER TABLE [dbo].[orders] ADD  DEFAULT (getdate()) FOR [orderdate]
GO
USE [master]
GO
ALTER DATABASE [shopdb] SET  READ_WRITE 
GO
