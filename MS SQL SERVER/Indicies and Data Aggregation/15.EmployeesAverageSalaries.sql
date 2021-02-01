SELECT * INTO #Employess FROM Employees
WHERE Salary > 30000

DELETE FROM #Employess 
WHERE ManagerID = 42

UPDATE #Employess
SET Salary+=5000
WHERE DepartmentID = 1

SELECT DepartmentID,AVG(Salary) AS AverageSalary FROM #Employess
GROUP BY DepartmentID