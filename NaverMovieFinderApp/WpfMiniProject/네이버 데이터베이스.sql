USE [OpenApiLab]
GO
/****** Object:  Table [dbo].[NaverFavoiriteMovies]    Script Date: 2021-04-05 오후 4:45:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NaverFavoiriteMovies](
	[idx] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Link] [varchar](500) NULL,
	[Image] [varchar](500) NULL,
	[SubTitle] [varchar](1000) NULL,
	[PubDate] [varchar](20) NULL,
	[Director] [nvarchar](100) NULL,
	[actor] [nvarchar](1000) NULL,
	[UserRating] [varchar](10) NULL,
	[RegDate] [datetime] NULL,
 CONSTRAINT [PK_NaverFavoiriteMovies] PRIMARY KEY CLUSTERED 
(
	[idx] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
