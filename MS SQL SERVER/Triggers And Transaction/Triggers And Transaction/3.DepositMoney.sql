CREATE  PROC usp_DepositMoney(@accountId INT, @moneyAmount DECIMAL(15,4))
AS
BEGIN TRANSACTION

DECLARE @account INT = (SELECT Id FROM Accounts WHERE Id = @accountId)

	IF @account IS NULL
		BEGIN
			ROLLBACK
			RAISERROR('Acount cannot be null',16,1)
			RETURN
		END

	IF @moneyAmount < 0
		BEGIN
			ROLLBACK;
			RAISERROR('Negative money',17,1)
			RETURN
		END
	
	UPDATE Accounts
	SET Balance+=@moneyAmount
	WHERE Id = @accountId
COMMIT

EXEC dbo.usp_DepositMoney 1,10.00