--insert dummy users
MERGE INTO [User] AS Target
USING (VALUES
	('Test User 1'),
	('Test User 2'),
	('Test User 3'),
	('Test User 4'),
	('Test User 5'),
	('Test User 6'),
	('Test User 7'),
	('Test User 8'),
	('Test User 9'),
	('Test User 10')
) AS Source([Name])
ON Target.[Name] = Source.[Name]
WHEN NOT MATCHED THEN
	INSERT ([Name])
	VALUES (source.[Name]);