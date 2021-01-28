SELECT e.EmployeeID,FirstName,p.[Name]
FROM Employees e
	 JOIN EmployeesProjects ep ON e.EmployeeID = ep.EmployeeID
	LEFT JOIN Projects p ON p.ProjectID = ep.ProjectID AND StartDate < '2005-1-1'
	WHERE e.EmployeeID = 24
	