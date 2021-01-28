SELECT CountryCode,MountainRange, PeakName,Elevation  FROM MountainsCountries mc
	JOIN Mountains m ON mc.MountainId = m.Id
	JOIN Peaks p ON m.Id = p.MountainId
	WHERE Elevation > 2835 AND CountryCode LIKE 'BG'
	ORDER BY Elevation DESC