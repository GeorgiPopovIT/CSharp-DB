SELECT DepositGroup,IsDepositExpired,AVG(DepositInterest) FROM WizzardDeposits
WHERE DepositStartDate > '1985-1-1'
GROUP BY DepositGroup,IsDepositExpired
ORDER BY DepositGroup DESC, IsDepositExpired 