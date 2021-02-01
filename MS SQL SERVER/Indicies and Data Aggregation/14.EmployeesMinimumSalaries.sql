SELECT DepartmentID,MIN(Salary) AS MinimumSalary FROM Employees
WHERE HireDate > '2000-1-1' AND DepartmentID IN(2,5,7)
GROUP BY DepartmentID