CREATE PROC usp_GetTownsStartingWith (@parameter VARCHAR(40))
AS
	SELECT [Name] FROM Towns
	WHERE [Name] LIKE @parameter + '%'
GO

EXEC dbo.usp_GetTownsStartingWith 'b'