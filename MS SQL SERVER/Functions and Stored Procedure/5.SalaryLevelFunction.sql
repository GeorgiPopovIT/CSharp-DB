CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS VARCHAR(20)
AS
BEGIN
DECLARE @salaryLevel VARCHAR(12)
	IF @salary < 30000
		BEGIN
			SET @salaryLevel= 'Low'
		END
	ELSE IF @salary BETWEEN 30000 AND 50000
		SET @salaryLevel= 'Average'
	ELSE
	SET @salaryLevel='High'

RETURN @salaryLevel
END
GO;

SELECT CAST(Salary AS decimal(18, 4)) AS Salary,
       dbo.ufn_GetSalaryLevel(Salary) AS 'Salary Level'
FROM Employees
