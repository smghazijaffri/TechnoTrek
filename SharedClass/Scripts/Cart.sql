USE [Computer]
GO

/****** Object:  Table [dbo].[Cart]    Script Date: 01-Jul-24 6:28:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cart](
	[UserID] [varchar](50) NOT NULL,
	[CartItemsJson] [nvarchar](max) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Cart] ADD  DEFAULT (getdate()) FOR [CreationDate]
GO

ALTER TABLE [dbo].[Cart] ADD  DEFAULT (getdate()) FOR [LastUpdated]
GO


