CREATE PROC usp_TransferMoney(@senderId INT, @receiverId INT, @amount DECIMAL(15,4))
AS
BEGIN TRANSACTION
	DECLARE @currSender INT = (SELECT Id FROM Accounts WHERE Id = @senderId)
	DECLARE @senderBalance INT = (SELECT Balance FROM Accounts WHERE Id = @senderId)
	DECLARE @currReceiver INT = (SELECT Id FROM Accounts WHERE Id = @receiverId)
	DECLARE @receiverBalance INT = (SELECT Balance FROM Accounts WHERE Id = @receiverId)
			IF @amount < 0
				BEGIN
					ROLLBACK
					RETURN
			END
		
		IF (@senderBalance - @amount < 0)
			BEGIN
				ROLLBACK
				RETURN
			END

	UPDATE Accounts
	SET Balance-=@amount
	WHERE Id = @senderId

	UPDATE Accounts
	SET Balance+=@amount
	WHERE Id = @receiverId
COMMIT