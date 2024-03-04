CREATE FUNCTION [dbo].[fnCalculateMovieAverageRating]
(
	@MovieId UNIQUEIDENTIFIER
)
RETURNS decimal(4,2)
AS
BEGIN
	return (SELECT CAST(AVG(CAST(rating as decimal(4,2))) as decimal(4,2))
			FROM	MovieRating
			WHERE	MovieId = @MovieId
			GROUP BY (MovieId))
END
