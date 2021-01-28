SELECT c.ContinentCode, c.CurrencyCode, 
       COUNT(*) AS [CurrencyUsage]
FROM Countries AS c
GROUP BY c.ContinentCode,c.CurrencyCode
HAVING COUNT(*) > 1
ORDER BY ContinentCode	