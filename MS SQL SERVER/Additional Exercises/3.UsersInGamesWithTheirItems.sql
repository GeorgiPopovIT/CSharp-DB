SELECT Username,* FROM Users u
JOIN UsersGames ug ON u.Id = ug.UserId
JOIN Games g ON ug.