BACKUP DATABASE SoftUni
TO DISK = 'softuni-backup.bak'

DROP DATABASE SoftUni
RESTORE DATABASE SoftUni FROM DISK = 'softuni-backup.bak'