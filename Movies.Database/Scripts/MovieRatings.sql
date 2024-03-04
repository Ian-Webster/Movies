DELETE FROM MovieRating

DECLARE @LastUserId UNIQUEIDENTIFIER = (select top 1 Id from [User])

--insert dummy movie ratings
INSERT INTO MovieRating
SELECT	mve.Id as MovieId,
		usr.id as UserId,
		((10 - 0) * RAND(convert(varbinary, newid())) + 0) as Rating
FROM	Movie mve
JOIN	[user] usr on usr.Id != @LastUserId --leave the last user so that record can be used to test saving ratings

--reset average movie ratings
UPDATE Movie
SET AverageRating = (SELECT	CAST(AVG(CAST(rating as decimal(4,2))) as decimal(4,2))
					FROM	MovieRating
					WHERE	MovieId = Movie.Id
					GROUP BY (MovieId))
