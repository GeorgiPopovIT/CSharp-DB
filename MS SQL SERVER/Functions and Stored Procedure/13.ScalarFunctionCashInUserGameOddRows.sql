CREATE FUNCTION  ufn_CashInUsersGames (@gameName VARCHAR(100))
RETURNS TABLE 
AS
	RETURN  (SELECT SUM(Cash) as SumCash  FROM
		(SELECT Cash, ROW_NUMBER() OVER(ORDER BY ug.Cash DESC) AS RowNum
			FROM Games g JOIN UsersGames ug ON g.Id = ug.GameId
		WHERE g.[Name] = @gameName) AS k
		WHERE k.RowNum % 2 =1)

SELECT * FROM dbo.ufn_CashInUsersGames('Love in a mist')	