SELECT TOP(5) e.EmployeeID,FirstName,p.Name AS [ProjectName] FROM Employees e
	JOIN EmployeesProjects ep ON e.EmployeeID = ep.EmployeeID
	JOIN Projects p ON ep.ProjectID = p.ProjectID
	WHERE p.EndDate IS NULL
	ORDER BY EmployeeID

	