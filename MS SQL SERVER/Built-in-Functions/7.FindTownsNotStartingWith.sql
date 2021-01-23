SELECT TownID,[Name] FROM Towns
WHERE LEFT([Name],1)   LIKE '[^RBD]%'
ORDER BY [Name] ASC