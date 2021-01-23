--SELECT
--EmployeeID,
--FirstName,
--LastName,
--Salary,
--DENSE_RANK () OVER ( PARTITION BY Salary ORDER BY EmployeeID ASC ) [Rank]
--FROM Employees
--WHERE Salary BETWEEN 10000 AND 50000
--	AND [Rank] = 2
--ORDER BY Salary DESC
SELECT
  *
FROM (
  SELECT
    EmployeeID,
    FirstName,
    LastName,
    Salary,
	 DENSE_RANK() OVER( PARTITION BY Salary ORDER BY EmployeeID ASC) Rank
  FROM Employees
  WHERE Salary BETWEEN 10000 AND 50000
) AS FF
WHERE Rank =  2
 ORDER BY Salary DESC
