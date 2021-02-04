SELECT SUBSTRING(Email,CHARINDEX('@',Email) + 1,LEN(Email)) [Email Provider],
COUNT(*) [Number Of Users]
FROM Users u
GROUP BY SUBSTRING(Email,CHARINDEX('@',Email) + 1,LEN(Email))
ORDER BY [Number Of Users] DESC, [Email Provider]
