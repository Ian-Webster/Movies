DELETE FROM MovieRating

DECLARE @LastUserId int = (select max(Id) from [User])

--insert dummy users
INSERT INTO MovieRating
SELECT	mve.Id as MovieId,
		usr.id as UserId,
		((10 - 0) * RAND(convert(varbinary, newid())) + 0) as Rating
FROM	Movie mve
JOIN	[user] usr on usr.id > 0 AND usr.Id < @LastUserId --leave the last user so that record can be used to test saving ratings

--insert some dummy movie ratings for each of the users
UPDATE Movie
SET AverageRating = (SELECT	CAST(AVG(CAST(rating as decimal(4,2))) as decimal(4,2))
					FROM	MovieRating
					WHERE	MovieId = Movie.Id
					GROUP BY (MovieId))