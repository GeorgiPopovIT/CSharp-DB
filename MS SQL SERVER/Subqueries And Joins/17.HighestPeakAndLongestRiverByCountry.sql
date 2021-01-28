SELECT TOP(5) c.CountryName,
MAX(p.Elevation) AS [HighestPeakElevation]
,MAX(r.[Length]) AS [LongestRiverLength]
FROM Countries c
	LEFT JOIN  MountainsCountries mc ON c.CountryCode =mc.CountryCode
	LEFT JOIN Peaks p ON mc.MountainId = p.MountainId
	LEFT JOIN CountriesRivers cr ON c.CountryCode = cr.CountryCode
	LEFT JOIN Rivers r ON cr.RiverId = r.Id
	GROUP BY c.CountryName
ORDER BY HighestPeakElevation DESC,LongestRiverLength DESC,CountryName