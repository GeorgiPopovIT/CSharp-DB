SELECT COUNT(*) AS [Count] FROM Countries c
	LEFT JOIN MountainsCountries mc ON mc.CountryCode = c.CountryCode
	LEFT  JOIN Mountains m ON mc.MountainId = m.Id
	WHERE MountainId IS NULL