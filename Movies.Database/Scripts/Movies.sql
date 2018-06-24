MERGE INTO Movie AS Target
USING (VALUES
	('Mad Max: Fury Road', 2015, 1, 120, 0),
	('Wonder Woman', 2017, 1, 141, 0),
	('Baby Driver', 2017, 1, 113, 0),
	('The Hurt Locker', 2009, 1, 131, 0),
	('Skyfall', 2012, 1, 145, 0),
	('True Grit', 2010, 1, 110, 0),
	('Sicario', 2015, 1, 121, 0),
	('13 Assassins ', 2011, 1, 126, 0),
	('John Wick', 2014, 1, 96, 0),
	('Rush', 2013, 1, 123, 0),
	('The Lego Movie', 2014, 2, 101, 0),
	('The Nice Guys', 2016, 2, 116, 0),
	('Zombie Land', 2009, 2, 88, 0),
	('The Worlds End', 2013, 2, 109, 0),
	('Tuck & Dale vs. Evil', 2011, 2, 88, 0),
	('Spy', 2015, 2, 117, 0),
	('The Grand Budapest Hotel', 2014, 2, 99, 0),
	('Enough Said', 2014, 2, 93, 0),
	('Midnight in Paris', 2011, 2, 94, 0),
	('It Follows', 2015, 3, 94, 0),
	('Let The Right One In', 2009, 3, 114, 0),
	('Night of The Living Dead', 1968, 3, 90, 0),
	('The Birds', 1963, 3, 119, 0),
	('The Cabin in The Woods', 2012, 3, 95, 0),
	('Splice', 2010, 3, 100, 0),
	('Fright Night', 2011, 3, 101, 0),
	('Stake Land', 2011, 3, 96, 0),
	('The Crazies', 2010, 3, 101, 0),
	('Evil Dead 2: Dead by Dawn', 1987, 3, 84, 0),
	('Metropolis ', 1927, 4, 115, 0),
	('Star Trek', 2009, 4, 127, 0),
	('Her', 2014, 4, 126, 0),
	('Ex Machina ', 2015, 4, 108, 0),
	('Avatar', 2009, 4, 162, 0),
	('Akira', 1988, 4, 124, 0),
	('Super 8', 2011, 4, 112, 0),
	('Total Recall ', 1990, 4, 113, 0),
	('Prometheus', 2012, 4, 123, 0),
	('Bladerunner', 1982, 4, 114, 0),
	('2001: A Space Odyssey', 1968, 4, 139, 0)
) AS Source(Title, YearOfRelease, GenreId, RunningTime, AverageRating)
ON Target.Title = Source.Title
WHEN MATCHED THEN
	UPDATE SET 
	YearOfRelease = Source.YearOfRelease,
	GenreId = Source.GenreId,
	RunningTime = Source.RunningTime
WHEN NOT MATCHED THEN
	INSERT (Title, YearOfRelease, GenreId, RunningTime, AverageRating)
	VALUES (source.Title, Source.YearOfRelease, Source.GenreId, Source.RunningTime, Source.AverageRating);