CREATE TABLE NotificationEmails
(
Id INT PRIMARY KEY IDENTITY NOT NULL,
Recipient INT NOT NULL,
[Subject] VARCHAR(MAX),
Body VARCHAR(MAX)
)

CREATE TRIGGER tr_InsertNewRecord
ON Logs FOR INSERT
AS
	DECLARE @accountId INT = (SELECT TOP(1) AccountId FROM inserted)
	DECLARE @oldSum DECIMAL(15,2) = (SELECT TOP(1) OldSum FROM inserted)
	DECLARE @newSum DECIMAL(15,2) = (SELECT TOP(1) NewSum FROM inserted)

	INSERT INTO NotificationEmails(Recipient,[Subject],Body) VALUES
	(
	@accountId,
	'Balance change for account: ' + CAST(@accountId AS varchar(20)),
	'On Sep ' + CAST(GETDATE() AS varchar(40)) + ' your balance was changed from '+ CAST(@oldSum AS varchar(40)) +' to '+ CAST(@newSum AS varchar(40))
	)
GO

