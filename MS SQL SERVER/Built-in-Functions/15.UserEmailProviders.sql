SELECT Username,SUBSTRING(Email,(CHARINDEX('@',Email)+1),len(Email)) AS [Email Provider] FROM Users
ORDER BY [Email Provider], Username