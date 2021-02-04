CREATE PROC usp_GetEmployeesFromTown (@townName VARCHAR(30))
AS
	SELECT FirstName,LastName FROM Employees e
	LEFT JOIN Addresses a ON e.AddressID = a.AddressID
	LEFT JOIN Towns t ON A.TownID = T.TownID
	WHERE t.[Name] = @townName
GO

EXEC dbo.usp_GetEmployeesFromTown 'Sofia'