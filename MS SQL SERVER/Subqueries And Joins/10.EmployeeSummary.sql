SELECT TOP(50) EmployeeID,CONCAT(FirstName,' ',LastName) AS [EmployeeName],
(SELECT FirstName + ' ' + LastName FROM Employees en WHERE en.EmployeeID = e.ManagerID) AS [ManagerName],
(SELECT [Name] FROM Departments WHERE DepartmentID = e.DepartmentID ) AS [DepartmentName]
FROM Employees e
LEFT JOIN Departments d ON e.ManagerID = d.ManagerID
ORDER BY EmployeeID