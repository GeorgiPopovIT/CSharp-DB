SELECT DepartmentID,Salary FROM(SELECT DepartmentID, Salary,
ROW_NUMBER() OVER (PARTITION BY DepartmentID ORDER BY Salary DESC) SalaryRank FROM Employees e
GROUP BY DepartmentID,Salary) e
WHERE SalaryRank = 3