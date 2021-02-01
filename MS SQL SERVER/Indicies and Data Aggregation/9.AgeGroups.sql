SELECT *,COUNT(*) FROM 
(
select 
  case
   when Age <=10 then '[0-10]'
   when Age between 11 and 20 then '[11-20]'
   when Age between 21 and 30then '[21-30]'
    when Age between 31 and 40then '[31-40]'
	 when Age between 41 and 50then '[41-50]'
	  when Age between 51 and 60then '[51-60]'
	   when Age >= 61 then '[61+]'
 END as AgeGroup 
 from WizzardDeposits
) t
group by AgeGroup
order by AgeGroup