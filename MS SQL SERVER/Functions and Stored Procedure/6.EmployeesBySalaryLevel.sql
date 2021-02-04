CREATE PROCEDURE usp_EmployeesBySalaryLevel (@salaryLevel VARCHAR(8))
AS
BEGIN
	SELECT FirstName,LastName FROM Employees
	WHERE @salaryLevel = dbo.ufn_GetSalaryLevel(Salary);
END
GO

EXEC dbo.usp_EmployeesBySalaryLevel 'High'
