USE [master]
GO

CREATE DATABASE [TransactionDB] 
GO

ALTER DATABASE [TransactionDB] SET COMPATIBILITY_LEVEL = 100
GO

Use [TransactionDB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Transactions' AND xtype='U')


BEGIN
	CREATE TABLE [dbo].[Transactions](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Account] [nvarchar](100) NULL,
		[Description] [nvarchar](100) NULL,
		[CurrencyCode] [nvarchar](3) NULL,
		[Amount] [decimal](18, 4) NULL,
	 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO
