SELECT TOP(2) DepositGroup FROM  WizzardDeposits w
GROUP BY DepositGroup
ORDER BY AVG(w.MagicWandSize)