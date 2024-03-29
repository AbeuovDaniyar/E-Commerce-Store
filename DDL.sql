USE [aspnet-CapstoneProject-96D9AAD4-C520-424C-AF81-4A8C2D540CE1]
GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 4/22/2023 12:45:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[orderId] [int] NULL,
	[productId] [int] NULL,
	[quantity] [int] NULL,
	[subtotal] [float] NULL,
 CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 4/22/2023 12:45:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[dateTime] [varchar](70) NULL,
	[total] [float] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 4/22/2023 12:45:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[id] [int] NOT NULL,
	[ProductName] [nvarchar](200) NULL,
	[ProductManufacturer] [nvarchar](200) NULL,
	[ProductDescription] [text] NULL,
	[ProductImages] [varchar](100) NULL,
	[ProductPrice] [float] NULL,
	[ProductStock] [int] NULL,
	[ProductStripeId] [nvarchar](300) NULL,
	[ProductStripePriceId] [nvarchar](300) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplements]    Script Date: 4/22/2023 12:45:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplements](
	[id] [int] NOT NULL,
	[ProductExpirationDate] [nvarchar](100) NULL,
	[ProductCode] [nvarchar](300) NULL,
	[ProductUPCCode] [nvarchar](100) NULL,
	[ProductPckgQuantity] [int] NULL,
	[ProductShippingWeight] [float] NULL,
	[ProductDimensions] [nvarchar](300) NULL,
	[ProductKeyWords] [text] NULL,
	[ProductSuggestedUse] [text] NULL,
	[ProductIngridients] [text] NULL,
	[ProductWarnings] [text] NULL,
	[ProductDisclaimer] [text] NULL,
	[ProductId] [int] NULL,
	[ProductManufacturerLink] [text] NULL,
 CONSTRAINT [PK_Supplements] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 4/22/2023 12:45:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](70) NOT NULL,
	[LastName] [nvarchar](70) NOT NULL,
	[Email] [nvarchar](70) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Role] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Orders] FOREIGN KEY([orderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Orders]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Product] FOREIGN KEY([productId])
REFERENCES [dbo].[Product] ([id])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Product]
GO
ALTER TABLE [dbo].[Supplements]  WITH CHECK ADD  CONSTRAINT [FK_Product_Supplements] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([id])
GO
ALTER TABLE [dbo].[Supplements] CHECK CONSTRAINT [FK_Product_Supplements]
GO
