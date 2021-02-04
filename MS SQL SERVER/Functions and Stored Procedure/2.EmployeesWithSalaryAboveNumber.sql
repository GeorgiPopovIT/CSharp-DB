CREATE PROC usp_GetEmployeesSalaryAboveNumber(@acceptNum DECIMAL(18,4))
AS
BEGIN
	SELECT FirstName,LastName FROM Employees
	WHERE Salary >= @acceptNum
END

EXEC dbo.usp_GetEmployeesSalaryAboveNumber 48100