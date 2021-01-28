SELECT TOP(1)
	(SELECT AVG(Salary) FROM Employees
	WHERE DepartmentID = d.DepartmentID) AS [MinAverageSalary]
FROM Departments d
ORDER BY MinAverageSalary