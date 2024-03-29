﻿CREATE FUNCTION [dbo].[fnHashPassword]
(
	@PlainTextPassword NVARCHAR(MAX)
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	RETURN (SELECT CONVERT(NVARCHAR(64), HASHBYTES('SHA2_256', @PlainTextPassword), 2))
END
