CREATE PROC usp_GetHoldersWithBalanceHigherThan(@parameter DECIMAL(18,4))
AS
	SELECT  FirstName,LastName FROM AccountHolders ac
	 JOIN Accounts a ON ac.Id = a.AccountHolderId
	GROUP BY ac.FirstName,ac.LastName
	HAVING SUM(Balance) > @parameter
	ORDER BY FirstName,LastName

GO

EXEC dbo.usp_GetHoldersWithBalanceHigherThan 21000