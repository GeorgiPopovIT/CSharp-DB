SELECT
c.CountryCode,
(SELECT
COUNT(*)
FROM MountainsCountries WHERE CountryCode = c.CountryCode
)
FROM Countries c
WHERE CountryName IN('Russia','United States','Bulgaria')