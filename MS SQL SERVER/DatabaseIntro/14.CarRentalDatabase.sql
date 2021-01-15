CREATE DATABASE CarRental

USE CarRental

CREATE TABLE Categories
(
Id INT PRIMARY KEY IDENTITY,
CategoryName VARCHAR(20) NOT NULL,
DailyRate VARCHAR(30),
WeeklyRate VARCHAR(30),
MonthlyRate VARCHAR(30),
WeekendRate VARCHAR(30),
)

INSERT INTO Categories VALUES
('BIG','1','6','5','2'),
('BIG','1','6','5','2'),
('BIG','1','6','5','2')

CREATE TABLE Cars
(
Id INT PRIMARY KEY IDENTITY,
PlateNumber INT,
Manufacturer VARCHAR(20),
Model VARCHAR(20),
CarYear DATE,
CategoryId INT,
Doors TINYINT,
Picture VARCHAR(MAX),
Condition VARCHAR(20),
Available BIT
)

INSERT INTO  Cars VALUES
(321,'Audi','S8','2020',1,5,'https://www.google.com/imgres?imgurl=https%3A%2F%2Fwww.sqlshack.com%2Fwp-content%2Fuploads%2F2017%2F06%2Fc-users-marko-appdata-local-microsoft-windows-ine-3.png&imgrefurl=https%3A%2F%2Fwww.sqlshack.com%2Fhow-to-create-and-configure-a-linked-server-in-sql-server-management-studio%2F&tbnid=ipeqB6RDYbvkaM&vet=12ahUKEwj42dfgqpvuAhVMiRoKHc4ZAisQMygBegUIARCWAQ..i&docid=yyd2mhEu0aJ8MM&w=704&h=632&q=sql%20picture%20photo%20link&ved=2ahUKEwj42dfgqpvuAhVMiRoKHc4ZAisQMygBegUIARCWAQ',
'ok',1),
(12,'BMW','m5','2020',1,5,'https://www.google.com/imgres?imgurl=https%3A%2F%2Fwww.sqlshack.com%2Fwp-content%2Fuploads%2F2017%2F06%2Fc-users-marko-appdata-local-microsoft-windows-ine-3.png&imgrefurl=https%3A%2F%2Fwww.sqlshack.com%2Fhow-to-create-and-configure-a-linked-server-in-sql-server-management-studio%2F&tbnid=ipeqB6RDYbvkaM&vet=12ahUKEwj42dfgqpvuAhVMiRoKHc4ZAisQMygBegUIARCWAQ..i&docid=yyd2mhEu0aJ8MM&w=704&h=632&q=sql%20picture%20photo%20link&ved=2ahUKEwj42dfgqpvuAhVMiRoKHc4ZAisQMygBegUIARCWAQ',
'ok',1),
(65,'skoda','fabia','2020',1,5,'https://www.google.com/imgres?imgurl=https%3A%2F%2Fwww.sqlshack.com%2Fwp-content%2Fuploads%2F2017%2F06%2Fc-users-marko-appdata-local-microsoft-windows-ine-3.png&imgrefurl=https%3A%2F%2Fwww.sqlshack.com%2Fhow-to-create-and-configure-a-linked-server-in-sql-server-management-studio%2F&tbnid=ipeqB6RDYbvkaM&vet=12ahUKEwj42dfgqpvuAhVMiRoKHc4ZAisQMygBegUIARCWAQ..i&docid=yyd2mhEu0aJ8MM&w=704&h=632&q=sql%20picture%20photo%20link&ved=2ahUKEwj42dfgqpvuAhVMiRoKHc4ZAisQMygBegUIARCWAQ',
'ok',1)

CREATE TABLE Employees
(
Id INT PRIMARY KEY IDENTITY,
FirstName VARCHAR(15) NOT NULL,
LastName VARCHAR(15) NOT NULL,
Title VARCHAR(30),
Notes VARCHAR(MAX)
)

INSERT INTO  Employees VALUES
('Georgi','dqddd','23233','dkfdhfhw'),
('Qnko','dqddd','23233','dkfdhfhw'),
('Mitko','dqddd','23233','dkfdhfhw')

CREATE TABLE Customers 
(
Id INT PRIMARY KEY IDENTITY,
DriverLicenceNumber INT NOT NULL,
FullName VARCHAR(40) NOT NULL,
[Address] VARCHAR(30) NOT NULL,
City VARCHAR(20),
ZIPCode VARCHAR(10),
Notes VARCHAR(MAX)
)

INSERT INTO Customers VALUES
(233,'gEORIG DDAS','bulSitnqkovo','Sofia','4000','difuhfh'),
(233,'gEORIG DDAS','bulSitnqkovo','Sofia','4000','difuhfh'),
(233,'gEORIG DDAS','bulSitnqkovo','Sofia','4000','difuhfh')

CREATE TABLE RentalOrders
(
Id INT PRIMARY KEY IDENTITY,
EmployeeId INT,
CustomerId INT,
CarId INT,
TankLevel VARCHAR(10),
KilometrageStart INT,
KilometrageEnd INT,
TotalKilometrage INT,
StartDate DATE,
EndDate DATE,
TotalDays INT,
RateApplied VARCHAR(10),
TaxRate DECIMAL(5,2),
OrderStatus VARCHAR(4),
Notes VARCHAR(MAX)
)

INSERT INTO RentalOrders VALUES
(2,3,6,'EWEE',9,1,7,'2012','2015',12,'323',3.23,'OK','12331'),
(2,3,6,'EWEE',9,1,7,'2012','2015',12,'323',3.23,'OK','12331'),
(2,3,6,'EWEE',9,1,7,'2012','2015',12,'323',3.23,'OK','12331')
