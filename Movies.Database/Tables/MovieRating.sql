CREATE TABLE [dbo].[MovieRating](
	[MovieId] UNIQUEIDENTIFIER NOT NULL,
	[UserId] UNIQUEIDENTIFIER NOT NULL,
	[Rating] [tinyint] NOT NULL,
	CONSTRAINT [PK_MovieRating] PRIMARY KEY CLUSTERED ([MovieId] ASC, [UserId] ASC),
	CONSTRAINT [FK_MoveiRating_Movie] FOREIGN KEY([MovieId]) REFERENCES [dbo].[Movie] ([Id]),
	CONSTRAINT [FK_MovieRating_User] FOREIGN KEY([UserId]) REFERENCES [dbo].[User] ([Id])
) 
GO

CREATE TRIGGER trgMovieRating on MovieRating
AFTER UPDATE, INSERT
AS
	UPDATE Movie
	SET AverageRating = dbo.fnCalculateMovieAverageRating(mov.Id)
	FROM	Movie mov
	JOIN	inserted insrt on insrt.MovieId = mov.Id

GO