SELECT TOP(3) e.EmployeeID,FirstName FROM Employees e
	LEFT JOIN EmployeesProjects ep ON e.EmployeeID = ep.EmployeeID
	WHERE ProjectID  IS NULL
	ORDER BY EmployeeID