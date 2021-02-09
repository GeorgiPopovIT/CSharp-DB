CREATE TABLE Logs
(
LogId INT PRIMARY KEY IDENTITY,
AccountId INT NOT NULL,
OldSum  DECIMAL(18,4),
NewSum DECIMAL(18,4)
)

CREATE TRIGGER tr_AccountChanges
ON Accounts FOR UPDATE
AS
DECLARE @newSum DECIMAL(18,2) = (SELECT Balance FROM inserted) 
DECLARE @oldSum DECIMAL(18,2) = (SELECT Balance FROM deleted) 
DECLARE @accountId INT = (SELECT Id FROM inserted)

INSERT INTO Logs(AccountId,NewSum,OldSum) VALUES
(@accountId,@newSum,@oldSum)

GO

UPDATE Accounts
SET Balance +=10
WHERE Id = 1
SELECT * FROM Logs
--WHERE Id = 1