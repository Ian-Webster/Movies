MERGE INTO Movie AS Target
USING (VALUES
	('33490f20-72ca-41c2-b2b9-bc45abe706a7','Mad Max: Fury Road', 2015, 1, 120, 0),
	('13770857-94ef-44c5-81d1-8ea46bc8b51c','Wonder Woman', 2017, 1, 141, 0),
	('a17991d9-9e43-4c4b-a10f-4bea836bea41','Baby Driver', 2017, 1, 113, 0),
	('00d1528e-f6d1-4092-8fef-62589f89f175','The Hurt Locker', 2009, 1, 131, 0),
	('153dff4b-75b3-4ae8-9b5f-e75625631a5b','Skyfall', 2012, 1, 145, 0),
	('38d9106c-a6a0-4b0c-ba4b-f93438d5b5bf','True Grit', 2010, 1, 110, 0),
	('cb6c101a-bd2d-493b-86e2-4782fa2b0f1b','Sicario', 2015, 1, 121, 0),
	('ae7c1ac0-3f58-464a-901a-39a1db281fdb','13 Assassins ', 2011, 1, 126, 0),
	('f24bf868-684a-4411-855f-465831d58fb0','John Wick', 2014, 1, 96, 0),
	('aa24e137-0b54-4f85-970b-26e199452f73','Rush', 2013, 1, 123, 0),
	('9abc7d30-13f3-40fb-b28a-c752bcca1a36','The Lego Movie', 2014, 2, 101, 0),
	('6d269124-1a41-4527-b56a-1bf0a778f209','The Nice Guys', 2016, 2, 116, 0),
	('12740034-abe6-4024-bc67-00a21f2b00b7','Zombie Land', 2009, 2, 88, 0),
	('d230e8cb-a4cf-4e7a-ad30-01788d5b02ec','The Worlds End', 2013, 2, 109, 0),
	('f436d0e9-0b54-40aa-bd7a-63f225bdf969','Tuck & Dale vs. Evil', 2011, 2, 88, 0),
	('afa8c63e-b43f-441a-82ee-02218e20575e','Spy', 2015, 2, 117, 0),
	('8e4de8d3-2c24-4509-a3ca-6b59673470b4','The Grand Budapest Hotel', 2014, 2, 99, 0),
	('f886825f-e0bc-4a76-bf24-03e35afe5315','Enough Said', 2014, 2, 93, 0),
	('4e7bd373-1042-4bef-8552-6e2fd974c975','Midnight in Paris', 2011, 2, 94, 0),
	('7931189b-24a5-4912-8347-6feca8598652','It Follows', 2015, 3, 94, 0),
	('83de5c65-78ac-4a64-9a10-5ab40ce71ca9','Let The Right One In', 2009, 3, 114, 0),
	('3e423624-6535-443d-8a3c-0728a8920505','Night of The Living Dead', 1968, 3, 90, 0),
	('f81e4ff5-5399-4a40-905e-3749d536a106','The Birds', 1963, 3, 119, 0),
	('79eb8d92-2b11-4b84-b5ba-2fae43e18a9b','The Cabin in The Woods', 2012, 3, 95, 0),
	('2fb12071-f994-40ea-8225-6c24f15c689d','Splice', 2010, 3, 100, 0),
	('ba6e1e5c-d9cf-422c-95c4-99238ce587f5','Fright Night', 2011, 3, 101, 0),
	('3fb970bc-75cd-44c0-9c28-8ab3b5a8c230','Stake Land', 2011, 3, 96, 0),
	('eb359e33-b40e-42ae-ba3a-64468b5aa2cc','The Crazies', 2010, 3, 101, 0),
	('ae6220da-8832-4a61-905e-f1645e9170a1','Evil Dead 2: Dead by Dawn', 1987, 3, 84, 0),
	('6e64a2d2-b47e-4fb6-be57-9d12d74a8970','Metropolis ', 1927, 4, 115, 0),
	('7f5b6daf-7b02-4cb7-aeff-7025534c4e88','Star Trek', 2009, 4, 127, 0),
	('14f1d5e1-9d0b-4f96-8050-bbe13f4e2bb2','Her', 2014, 4, 126, 0),
	('b7a7faf6-6165-4eb4-97df-69b57f2fdbed','Ex Machina ', 2015, 4, 108, 0),
	('ca1983d3-951f-4176-a8a4-601597437b8b','Avatar', 2009, 4, 162, 0),
	('634014eb-2191-408c-9993-ee182dcd6598','Akira', 1988, 4, 124, 0),
	('df0e62f0-de54-4d0d-be83-c28d19ea39fe','Super 8', 2011, 4, 112, 0),
	('b967a2bd-17eb-4a9b-afb6-3b8281db5512','Total Recall ', 1990, 4, 113, 0),
	('9c141030-951d-412c-b351-a93ea60ff688','Prometheus', 2012, 4, 123, 0),
	('92c0f7f2-4c74-4a0b-9812-97e9d53c0eda','Bladerunner', 1982, 4, 114, 0),
	('B3F14861-F4A1-4472-8285-495A832147AE','2001: A Space Odyssey', 1968, 4, 139, 0)
) AS Source(Id, Title, YearOfRelease, GenreId, RunningTime, AverageRating)
ON Target.Title = Source.Title
WHEN MATCHED THEN
	UPDATE SET 
	YearOfRelease = Source.YearOfRelease,
	GenreId = Source.GenreId,
	RunningTime = Source.RunningTime
WHEN NOT MATCHED THEN
	INSERT (Id, Title, YearOfRelease, GenreId, RunningTime, AverageRating)
	VALUES (source.Id, source.Title, Source.YearOfRelease, Source.GenreId, Source.RunningTime, Source.AverageRating);