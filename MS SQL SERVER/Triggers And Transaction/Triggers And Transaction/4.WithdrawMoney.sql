CREATE  PROC usp_WithdrawMoney(@accountId INT, @moneyAmount DECIMAL(15,4))
AS
BEGIN TRANSACTION

DECLARE @account INT = (SELECT Id FROM Accounts WHERE Id = @accountId)
DECLARE @balance DECIMAL(15,4) = (SELECT Balance FROM Accounts WHERE Id = @accountId)


	IF @account IS NULL
		BEGIN
			ROLLBACK;
		END

	IF @moneyAmount < 0
		BEGIN
			ROLLBACK;
		END
	IF(@balance- @moneyAmount < 0)
		BEGIN
			ROLLBACK;
		END
	
	UPDATE Accounts
	SET Balance-= @moneyAmount
	WHERE Id = @accountId
COMMIT

EXEC dbo.usp_WithdrawMoney 5,25