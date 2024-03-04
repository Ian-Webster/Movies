CREATE TABLE [dbo].[Movie](
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[YearOfRelease] [smallint] NOT NULL,
	[GenreId] [smallint] NOT NULL,
	[RunningTime] [tinyint] NOT NULL,
	[AverageRating] [decimal](4, 2) NOT NULL,
	CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Movie_Genre] FOREIGN KEY([GenreId]) REFERENCES [dbo].[Genre] ([Id])
) 
GO
