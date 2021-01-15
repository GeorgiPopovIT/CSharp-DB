--CREATE DATABASE Hotel
--USE Hotel

CREATE TABLE Employees
(
Id INT PRIMARY KEY,
FirstName VARCHAR(90) NOT NULL,
LastName VARCHAR(90) NOT NULL,
Title VARCHAR(50) NOT NULL,
Notes VARCHAR(MAX)
)

INSERT INTO Employees(Id,FirstName,LastName,Title,Notes) VALUES
 (1,'Gosho','Goshev','CEO',NULL),
 (2,'Petur','Petrov','CFO','random note'),
 (3,'Petrov','Goshev','CTO',NULL)
 

CREATE TABLE Customers 
(
AccountNumber INT PRIMARY KEY,
FirstName VARCHAR(90) NOT NULL, 
LastName VARCHAR(90) NOT NULL,
PhoneNumber CHAR(10) NOT NULL,
EmergencyName VARCHAR(90) NOT NULL,
EmergencyNumber CHAR(10) NOT NULL,
Notes VARCHAR(MAX)
)

INSERT INTO Customers VALUES
 (120,'G','P','123445','T','12345678',NULL),
 (121,'G','P','123445','T','12345678',NULL),
 (122,'G','P','123445','T','12345678',NULL)

CREATE TABLE RoomStatus
(
C VARCHAR(20) NOT NULL,
Notes VARCHAR(MAX)
)
INSERT INTO RoomStatus VALUES
('23','12313313'),
('233','4443434'),
('434','1233')


CREATE TABLE RoomTypes
(
RoomType VARCHAR(20) NOT NULL,
Notes VARCHAR(MAX)
)

INSERT INTO RoomTypes VALUES
('Single',NULL),
('Double',NULL),
('TRIPLE',NULL)

CREATE TABLE BedTypes
(
BedType VARCHAR(20) NOT NULL,
Notes VARCHAR(MAX)
)

INSERT INTO BedTypes VALUES
('Single',NULL),
('Double',NULL),
('TRIPLE',NULL)

CREATE TABLE Rooms
(
RoomNumber INT PRIMARY KEY,
RoomType VARCHAR(20) NOT NULL,
BedType VARCHAR(20) ,
Rate INT,
RoomStatus VARCHAR(20),
Notes VARCHAR(MAX)
)

INSERT INTO Rooms VALUES
(120,'apartament','Single',10,'21',NULL),
(121,'apartament','Single',10,'32',NULL),
(122,'apartament','Single',10,'32',NULL)

CREATE TABLE Payments
(
Id INT PRIMARY KEY,
EmployeeId INT NOT NULL,
PaymentDate DATETIME NOT NULL,
AccountNumber INT NOT NULL,
FirstDateOccupied DATETIME NOT NULL,
LastDateOccupied DATETIME NOT NULL,
TotalDays INT NOT NULL,
AmountCharged DECIMAL (15,2),
TaxRate INT ,
TaxAmount INT,
PaymentTotal DECIMAL(15,2),
Notes VARCHAR(MAX)
)

INSERT INTO Payments VALUES
(1,1,GETDATE(),1,'5/5/2012','5/8/2012',3,450.23,NULL,NULL,1233123.23,NULL),
(2,1,GETDATE(),1,'5/9/2012','5/3/2012',5,450.23,NULL,NULL,45330.23,NULL),
(3,1,GETDATE(),1,'5/13/2012', '5/4/2012' , 7 ,450.23,NULL,NULL,45330.23,NULL)


CREATE TABLE Occupancies 
(
Id INT PRIMARY KEY,
EmployeeId INT,
DateOccupied DATETIME,
AccountNumber INT NOT NULL,
RoomNumber INT,
RateApplied INT,
PhoneCharge DECIMAL(15,2),
Notes VARCHAR(MAX)
)

INSERT INTO Occupancies VALUES
(1, 120, GETDATE(), 120, 120, 0, 0, NULL),
(2, 120, GETDATE(), 120, 120, 0, 0, NULL),
(3, 120, GETDATE(), 120, 120, 0, 0, NULL)