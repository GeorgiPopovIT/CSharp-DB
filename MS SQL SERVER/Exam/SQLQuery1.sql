-- ex.1
CREATE DATABASE Bitbucket
CREATE TABLE Users
(
Id INT PRIMARY KEY IDENTITY,
Username VARCHAR(30) NOT NULL,
[Password] VARCHAR(30) NOT NULL,
Email VARCHAR(50) NOT NULL
)
CREATE TABLE Repositories
(
Id INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(50) NOT NULL
)
CREATE TABLE RepositoriesContributors
(
RepositoryId INT REFERENCES Repositories(Id),
ContributorId INT REFERENCES Users(Id)
PRIMARY KEY (RepositoryId,ContributorId)
)
CREATE TABLE Issues
(
Id INT PRIMARY KEY IDENTITY,
Title VARCHAR(255) NOT NULL,
IssueStatus CHAR(6) NOT NULL,
RepositoryId INT REFERENCES Repositories(Id) NOT NULL,
AssigneeId INT REFERENCES Users(Id) NOT NULL
)
CREATE TABLE Commits
(
Id INT PRIMARY KEY IDENTITY,
[Message] VARCHAR(255) NOT NULL,
IssueId INT REFERENCES Issues(Id),
RepositoryId INT REFERENCES Repositories(Id) NOT NULL,
ContributorId INT REFERENCES Users(Id) NOT NULL
)
CREATE TABLE Files
(
Id INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(100) NOT NULL,
Size DECIMAL(18,2) NOT NULL,
ParentId INT REFERENCES Files(Id),
CommitId INT REFERENCES Commits(Id) NOT NULL
)

--- ex.2
INSERT INTO Files VALUES
('Trade.idk',2598.0,1,1),
('menu.net',9238.31,2,2),
('Administrate.soshy',1246.93,3,3),
('Controller.php',	7353.15,4,4),
('Find.java',9957.86,5,5),
('Controller.json',14034.87,3,6),
('Operate.xix',	7662.92,7,7)

INSERT INTO Issues VALUES
('Critical Problem with HomeController.cs file','	open',	1,4),
('Typo fix in Judge.html'	,'open'	,4	,3),
('Implement documentation for UsersService.cs'	,'closed',	8,	2),
('Unreachable code in Index.cs','open',	9,	8)
----- ex.3
UPDATE Issues
SET IssueStatus = 'closed'
WHERE AssigneeId = 6

---- ex.4
DELETE FROM Files
WHERE CommitId = (SELECT Id FROM Commits WHERE RepositoryId = 3)

DELETE FROM Commits
WHERE RepositoryId = (SELECT Id FROM Repositories
WHERE [Name] LIKE 'Softuni-Teamwork')

DELETE FROM Issues
WHERE RepositoryId = (SELECT Id FROM Repositories
WHERE [Name] LIKE 'Softuni-Teamwork')

DELETE FROM RepositoriesContributors
WHERE RepositoryId = (SELECT Id FROM Repositories
WHERE [Name] LIKE 'Softuni-Teamwork')

---- ex.5
SELECT Id,[Message],RepositoryId,ContributorId FROM Commits
ORDER BY Id,[Message],RepositoryId,ContributorId

---- ex.6
SELECT Id,[Name],Size FROM Files
WHERE Size > 1000 AND [Name] LIKE '%html%'
ORDER BY Size DESC,Id,[Name]

--- ex.7
SELECT i.Id, CONCAT(u.Username,' : ',i.Title) AS [IssueAssignee] FROM Issues i
JOIN Users u ON i.AssigneeId = u.Id
ORDER BY I.Id DESC,[IssueAssignee]

---- ex.8
SELECT Id,[Name], CONCAT(Size,'KB') AS Size FROM Files
WHERE ID NOT IN (
    SELECT ParentID FROM Files WHERE ParentID IS NOT NULL)
ORDER BY Id,[Name],Size DESC

---- ex.9
SELECT TOP(5) r.Id,r.[Name],COUNT(*) AS Commits FROM RepositoriesContributors rc
JOIN Repositories r ON rc.RepositoryId = r.Id
JOIN Commits c ON r.Id = c.RepositoryId
GROUP BY r.Id,r.Name
ORDER BY Commits DESC,r.Id,r.Name

-- ex.10
SELECT Username,AVG(Size) AS Size FROM Users u
JOIN Commits c on u.Id = c.ContributorId
JOIN Files f ON c.Id = f.CommitId
GROUP BY Username
ORDER BY Size DESC,Username

---- ex.11
CREATE FUNCTION udf_AllUserCommits(@username VARCHAR(30))
RETURNS INT
AS BEGIN
	DECLARE @currUserId INT = (SELECT Id FROM Users WHERE Username = @username)

	RETURN (SELECT COUNT(*) FROM Users u JOIN Commits c ON c.ContributorId = u.Id
	WHERE u.Id = @currUserId)

END

SELECT dbo.udf_AllUserCommits('UnderSinduxrein')

---- ex.12
CREATE PROC usp_SearchForFiles(@fileExtension VARCHAR(20))
AS

		SELECT Id,[Name],CONCAT(Size,'KB') FROM Files 
			WHERE [Name] LIKE '%' + @fileExtension
GO

EXEC usp_SearchForFiles 'txt'

