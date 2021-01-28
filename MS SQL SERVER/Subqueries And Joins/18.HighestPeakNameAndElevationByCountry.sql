SELECT TOP(5) CountryName AS [Country],
ISNULL(
MAX(p.PeakName),'(no highestpeak)') AS [Highest Peak Name],
ISNULL(
MAX(p.Elevation),0) AS [Highest Peak Elevation],
ISNULL(
MAX(m.MountainRange),'(no mountain)') AS [Mountain]
FROM Countries c
	LEFT JOIN MountainsCountries mc ON c.CountryCode = mc.CountryCode
	LEFT JOIN Mountains m ON mc.MountainId = m.Id
	LEFT JOIN Peaks p ON m.Id = p.MountainId
	GROUP BY c.CountryName
	ORDER BY CountryName, [Highest Peak Name]