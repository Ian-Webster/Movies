--insert dummy users
MERGE INTO [User] AS Target
USING (VALUES
	('6d256119-f227-4cbf-895c-c5a00a499c82','TestUser1',dbo.[fnHashPassword]('password1')),
	('3487b4b6-d4d2-4b50-88f9-01f6d32d2597','TestUser2',dbo.[fnHashPassword]('password2')),
	('fc6c1a95-0242-4f63-9581-843d2831b63f','TestUser3',dbo.[fnHashPassword]('password3')),
	('2e11d176-c654-4bb3-9f81-913cf2cea479','TestUser4',dbo.[fnHashPassword]('password4')),
	('1c71534e-b2d0-468c-b3c5-12af19c70d15','TestUser5',dbo.[fnHashPassword]('password5')),
	('c3fa34d1-3122-483a-8276-ed25a6706802','TestUser6',dbo.[fnHashPassword]('password6')),
	('443de7ee-e3c0-443f-89f5-01b9f7b2d4e8','TestUser7',dbo.[fnHashPassword]('password7')),
	('81c25f1a-a0ca-434d-a5b7-e78440df9a1d','TestUser8',dbo.[fnHashPassword]('password8')),
	('c145eda1-abce-4bb2-ae31-ec184505a5f0','TestUser9',dbo.[fnHashPassword]('password9')),
	('b54acd1f-3e4c-425b-8149-e642f8bc5769','TestUser10',dbo.[fnHashPassword]('password10'))
) AS Source([Id], [UserName], [Password])
ON Target.[UserName] = Source.[UserName]
WHEN NOT MATCHED THEN
	INSERT ([Id], [UserName], [Password])
	VALUES (source.[Id], source.[UserName], source.[Password]);