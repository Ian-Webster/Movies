MERGE INTO Genre AS Target
USING (VALUES
	(1, 'Action'),
	(2, 'Comedy'),
	(3, 'Horror'),
	(4, 'Science Fiction')
) AS Source(Id, [Description])
ON Target.Id = Source.Id
WHEN MATCHED THEN
	UPDATE SET 
	[Description] = source.[Description]
WHEN NOT MATCHED THEN
	INSERT (Id, [Description])
	VALUES (source.Id, source.[Description]);