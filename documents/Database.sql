USE [master]
GO
/****** Object:  Database [Axon]    Script Date: 11/15/2021 6:33:52 PM ******/
CREATE DATABASE [Axon]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Axon', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Axon.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Axon_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Axon_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Axon] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Axon].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Axon] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Axon] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Axon] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Axon] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Axon] SET ARITHABORT OFF 
GO
ALTER DATABASE [Axon] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Axon] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Axon] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Axon] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Axon] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Axon] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Axon] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Axon] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Axon] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Axon] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Axon] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Axon] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Axon] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Axon] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Axon] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Axon] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Axon] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Axon] SET RECOVERY FULL 
GO
ALTER DATABASE [Axon] SET  MULTI_USER 
GO
ALTER DATABASE [Axon] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Axon] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Axon] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Axon] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Axon] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Axon] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Axon', N'ON'
GO
ALTER DATABASE [Axon] SET QUERY_STORE = OFF
GO
USE [Axon]
GO
/****** Object:  Table [dbo].[GitHubProfile]    Script Date: 11/15/2021 6:33:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GitHubProfile](
	[Id] [uniqueidentifier] NOT NULL,
	[GitId] [int] NOT NULL,
	[NodeId] [varchar](150) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Login] [varchar](150) NOT NULL,
	[Email] [nvarchar](150) NULL,
	[Blog] [nvarchar](250) NULL,
	[Bio] [nvarchar](250) NULL,
	[Location] [nvarchar](100) NULL,
	[AvatarUrl] [nvarchar](500) NOT NULL,
	[Url] [nvarchar](500) NOT NULL,
	[HtmlUrl] [nvarchar](500) NOT NULL,
	[Followers] [int] NOT NULL,
	[Following] [int] NOT NULL,
	[Collaborators] [int] NULL,
	[DiskUsage] [int] NULL,
	[Company] [nvarchar](150) NULL,
	[Suspended] [bit] NOT NULL,
	[SuspendedAt] [datetime2](7) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedAt] [datetime2](7) NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_GitHubProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_GitHubProfile] UNIQUE NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GitHubRepository]    Script Date: 11/15/2021 6:33:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GitHubRepository](
	[Id] [uniqueidentifier] NOT NULL,
	[GitUserId] [uniqueidentifier] NOT NULL,
	[GitId] [bigint] NOT NULL,
	[NodeId] [varchar](150) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[FullName] [varchar](250) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Homepage] [varchar](250) NULL,
	[Language] [varchar](50) NULL,
	[DefaultBranch] [varchar](50) NOT NULL,
	[Private] [bit] NOT NULL,
	[Size] [bigint] NOT NULL,
	[WatchersCount] [int] NOT NULL,
	[StargazersCount] [int] NOT NULL,
	[ForksCount] [int] NOT NULL,
	[Url] [nvarchar](500) NOT NULL,
	[HtmlUrl] [nvarchar](500) NOT NULL,
	[CloneUrl] [nvarchar](500) NOT NULL,
	[GitUrl] [nvarchar](500) NOT NULL,
	[SshUrl] [nvarchar](500) NOT NULL,
	[PushedAt] [datetime2](7) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedAt] [datetime2](7) NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_GitHubRepository_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/15/2021 6:33:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Username] [varchar](150) NOT NULL,
	[RoleId] [int] NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[RefreshToken] [nvarchar](100) NOT NULL,
	[IsSuspended] [bit] NOT NULL,
	[SessionId] [uniqueidentifier] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedAt] [datetime2](7) NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[GitHubRepository]  WITH CHECK ADD  CONSTRAINT [FK_GitHubRepository_GitHubProfile] FOREIGN KEY([GitUserId])
REFERENCES [dbo].[GitHubProfile] ([Id])
GO
ALTER TABLE [dbo].[GitHubRepository] CHECK CONSTRAINT [FK_GitHubRepository_GitHubProfile]
GO
USE [master]
GO
ALTER DATABASE [Axon] SET  READ_WRITE 
GO
