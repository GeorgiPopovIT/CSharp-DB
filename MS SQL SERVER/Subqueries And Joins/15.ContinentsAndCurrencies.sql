SELECT ContinentCode,CurrencyCode,CurrencyUsage FROM(
SELECT ContinentCode,CurrencyCode,COUNT(CurrencyCode) AS CurrencyUsage,
DENSE_RANK () OVER(PARTITION BY ContinentCode ORDER BY COUNT(CurrencyCode)DESC) AS Ranked
 FROM Countries
 GROUP BY ContinentCode,CurrencyCode) AS k
WHERE  Ranked = 1 AND CurrencyUsage > 1
ORDER BY ContinentCode	