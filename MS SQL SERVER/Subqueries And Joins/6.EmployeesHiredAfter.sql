SELECT FirstName,LastName,HireDate,d.[Name] AS [DeptName] FROM Employees e
	JOIN Departments d ON e.DepartmentID = d.DepartmentID
	WHERE HireDate > '1999-1-1' 
	AND d.[Name] IN('Sales','Finance')
	ORDER BY HireDate