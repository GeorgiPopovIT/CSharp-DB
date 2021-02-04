CREATE OR ALTER PROC usp_DeleteEmployeesFromDepartment (@departmentId INT)
AS
	DELETE FROM Employees
	WHERE DepartmentID = @departmentId

	DELETE FROM Departments
	WHERE DepartmentID = @departmentId

	SELECT COUNT(*) FROM Employees
	WHERE DepartmentID = @departmentId

	 UPDATE Departments
    SET ManagerID = NULL
    WHERE ManagerID IN (SELECT EmployeeID
                        FROM Employees
                        WHERE DepartmentID = @departmentId);

GO 

EXEC dbo.usp_DeleteEmployeesFromDepartment 1