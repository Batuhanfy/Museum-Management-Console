 USE [master]
GO
/****** Object:  Database [Muzeler]    Script Date: 31.07.2024 00:36:26 ******/
CREATE DATABASE [Muzeler]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Muzeler', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Muzeler.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Muzeler_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Muzeler_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Muzeler] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
 EXEC [Muzeler].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Muzeler] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Muzeler] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Muzeler] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Muzeler] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Muzeler] SET ARITHABORT OFF 
GO
ALTER DATABASE [Muzeler] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Muzeler] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Muzeler] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Muzeler] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Muzeler] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Muzeler] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Muzeler] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Muzeler] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Muzeler] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Muzeler] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Muzeler] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Muzeler] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Muzeler] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Muzeler] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Muzeler] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Muzeler] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Muzeler] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Muzeler] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Muzeler] SET  MULTI_USER 
GO
ALTER DATABASE [Muzeler] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Muzeler] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Muzeler] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Muzeler] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Muzeler] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Muzeler] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Muzeler] SET QUERY_STORE = ON
GO
ALTER DATABASE [Muzeler] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Muzeler]
GO
/****** Object:  Table [dbo].[Halls]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Halls](
	[HallID] [int] IDENTITY(1,1) NOT NULL,
	[HallName] [nvarchar](100) NULL,
	[Price] [decimal](10, 2) NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[HallID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MuzeSalonlari]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[MuzeSalonlari]
as
select H.HallID as 'Salon ID',H.HallName as 'Salon Ad',H.Price as 'Salon Fiyat' from Halls as H
where H.IsDelete = 0

GO
/****** Object:  Table [dbo].[Exhibits]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exhibits](
	[ExhibitID] [int] IDENTITY(1,1) NOT NULL,
	[HallID] [int] NULL,
	[ExhibitName] [nvarchar](100) NULL,
	[Description] [nvarchar](255) NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ExhibitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personals]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[IsDelete] [bit] NULL,
	[IsActive] [bit] NULL,
	[yetki] [bit] NULL,
	[Balance] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tickets]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tickets](
	[TicketID] [int] IDENTITY(1,1) NOT NULL,
	[VisitorID] [int] NULL,
	[HallID] [int] NULL,
	[PurchaseTime] [datetime] NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[TicketID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Visitors]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Visitors](
	[VisitorID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[IsDelete] [bit] NULL,
	[Password] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[VisitorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Exhibits] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Halls] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Personals] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Personals] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Personals] ADD  DEFAULT ((0)) FOR [yetki]
GO
ALTER TABLE [dbo].[Personals] ADD  CONSTRAINT [Balance]  DEFAULT ((0)) FOR [Balance]
GO
ALTER TABLE [dbo].[Tickets] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Visitors] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Exhibits]  WITH CHECK ADD FOREIGN KEY([HallID])
REFERENCES [dbo].[Halls] ([HallID])
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD FOREIGN KEY([HallID])
REFERENCES [dbo].[Halls] ([HallID])
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD FOREIGN KEY([VisitorID])
REFERENCES [dbo].[Visitors] ([VisitorID])
GO
/****** Object:  StoredProcedure [dbo].[AddExhibit]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddExhibit]
    @HallID INT,
    @ExhibitName NVARCHAR(100),
    @Description NVARCHAR(255)
AS
BEGIN
    INSERT INTO Exhibits (HallID, ExhibitName, Description)
    VALUES (@HallID, @ExhibitName, @Description)
END
GO
/****** Object:  StoredProcedure [dbo].[AddHall]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddHall]
    @HallName NVARCHAR(100),
    @Price DECIMAL(10, 2)
AS
BEGIN
    INSERT INTO Halls (HallName, Price)
    VALUES (@HallName, @Price)
END
GO
/****** Object:  StoredProcedure [dbo].[AddTicket]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddTicket]
    @VisitorID INT,
    @HallID INT
AS
BEGIN
    INSERT INTO Tickets (VisitorID, HallID, PurchaseTime)
    VALUES (@VisitorID, @HallID, GETDATE())
END
GO
/****** Object:  StoredProcedure [dbo].[AddVisitor]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddVisitor]
    @Name NVARCHAR(100),
    @Email NVARCHAR(100)
AS
BEGIN
    INSERT INTO Visitors (Name, Email)
    VALUES (@Name, @Email)
END
GO
/****** Object:  StoredProcedure [dbo].[CheckBalance]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CheckBalance]
    @Email NVARCHAR(100)
AS
BEGIN
    SELECT Id, Balance
    FROM Personals
    WHERE Email = @Email;
END
GO
/****** Object:  StoredProcedure [dbo].[CheckTicket]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CheckTicket]
    @TicketID NVARCHAR(50),
    @VisitorID NVARCHAR(50)
AS
BEGIN
    SELECT *
    FROM Tickets
    WHERE TicketID = @TicketID
      AND VisitorID = @VisitorID
      AND IsDelete = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteExhibit]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteExhibit]
    @ExhibitID INT
AS
BEGIN
    DELETE FROM Exhibits
    WHERE ExhibitID = @ExhibitID
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteHall]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteHall]
    @HallID INT
AS
BEGIN
    DELETE FROM Halls
    WHERE HallID = @HallID
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteTicket]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteTicket]
    @TicketID INT
AS
BEGIN
    DELETE FROM Tickets
    WHERE TicketID = @TicketID
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteVisitor]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [dbo].[DeleteVisitor]
    @VisitorID INT
AS
BEGIN
    DELETE FROM Visitors
    WHERE VisitorID = @VisitorID
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllExhibits]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetAllExhibits]
as
begin
    select
        E.ExhibitID,
        E.ExhibitName,
        E.Description,
        H.HallName,
        H.Price
    from Exhibits as E
    inner join Halls as H
    on(E.HallID=H.HallID)
	where E.IsDelete = 0

end
GO
/****** Object:  StoredProcedure [dbo].[InsertTicket]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertTicket]
    @VisitorID NVARCHAR(50),
    @HallID NVARCHAR(50)
AS
BEGIN
    INSERT INTO Tickets (VisitorID, HallID, PurchaseTime)
    VALUES (@VisitorID, @HallID, GETDATE());
END
GO
/****** Object:  StoredProcedure [dbo].[PurchaseTicket]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PurchaseTicket]
    @VisitorID INT,
    @HallID INT
AS
BEGIN
    INSERT INTO Tickets (VisitorID, HallID, PurchaseTime)
    VALUES (@VisitorID, @HallID, GETDATE())
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateBalance]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateBalance]
    @UserId NVARCHAR(50),
    @NewBalance DECIMAL(18, 2)
AS
BEGIN
    UPDATE Personals
    SET Balance = @NewBalance
    WHERE Id = @UserId;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateExhibit]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateExhibit]
    @ExhibitID INT,
    @HallID INT,
    @ExhibitName NVARCHAR(100),
    @Description NVARCHAR(255)
AS
BEGIN
    UPDATE Exhibits
    SET HallID = @HallID, ExhibitName = @ExhibitName, Description = @Description
    WHERE ExhibitID = @ExhibitID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateHall]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateHall]
    @HallID INT,
    @HallName NVARCHAR(100),
    @Price DECIMAL(10, 2)
AS
BEGIN
    UPDATE Halls
    SET HallName = @HallName, Price = @Price
    WHERE HallID = @HallID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTicket]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [dbo].[UpdateTicket]
    @TicketID INT,
    @VisitorID INT,
    @HallID INT,
    @PurchaseTime DATETIME
AS
BEGIN
    UPDATE Tickets
    SET VisitorID = @VisitorID, HallID = @HallID, PurchaseTime = @PurchaseTime
    WHERE TicketID = @TicketID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateVisitor]    Script Date: 31.07.2024 00:36:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [dbo].[UpdateVisitor]
    @VisitorID INT,
    @Name NVARCHAR(100),
    @Email NVARCHAR(100)
AS
BEGIN
    UPDATE Visitors
    SET Name = @Name, Email = @Email
    WHERE VisitorID = @VisitorID
END
GO
USE [master]
GO
ALTER DATABASE [Muzeler] SET  READ_WRITE 
GO
