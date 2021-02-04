--USE Bank
--GO
CREATE PROC usp_CalculateFutureValueForAccount(@AccountID INT,@R FLOAT)
AS
	SELECT
		ah.Id AS [Account Id],
		ah.FirstName,ah.LastName,a.Balance,
		 (SELECT dbo.ufn_CalculateFutureValue(Balance,@R,5)) AS [Balance in 5 years]
		FROM AccountHolders ah
	JOIN Accounts a ON ah.Id = a.AccountHolderId
	WHERE a.Id = @AccountID
GO

EXEC dbo.usp_CalculateFutureValueForAccount 1,0.1