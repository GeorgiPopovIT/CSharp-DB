CREATE TABLE Manufacturers
(
	ManufacturerID INT PRIMARY KEY IDENTITY  NOT NULL,
	[Name] VARCHAR(30),
	EstablishedOn DATE 
)
CREATE TABLE Models
(
	ModelID INT PRIMARY KEY NOT NULL,
	[Name] VARCHAR(30),
	ManufacturerID INT  REFERENCES Manufacturers(ManufacturerID) NOT NULL
)