CREATE TABLE Deleted_Employees
(
EmployeeId INT PRIMARY KEY,
FirstName VARCHAR(50),
LastName VARCHAR(50),
MiddleName VARCHAR(50),
JobTitle VARCHAR(50),
DepartmentId INT NOT NULL,
Salary MONEY
)

CREATE TRIGGER tr_DeletedEmployees
ON Employees
FOR DELETE
AS
BEGIN
	INSERT INTO Deleted_Employees (FirstName,LastName,MiddleName,JobTitle,DepartmentId,Salary)
	SELECT FirstName,LastName,MiddleName,JobTitle,DepartmentID,Salary FROM deleted
	
END