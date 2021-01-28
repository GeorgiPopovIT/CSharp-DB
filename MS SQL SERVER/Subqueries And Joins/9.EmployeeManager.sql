SELECT e.EmployeeID,e.FirstName,e.ManagerID,
(SELECT FirstName FROM Employees WHERE EmployeeID = e.ManagerID) AS [ManagerName]
 FROM Employees e
	WHERE e.ManagerID IN(3,7)
	ORDER BY e.EmployeeID