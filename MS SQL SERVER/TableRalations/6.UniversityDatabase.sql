--CREATE DATABASE University
--USE University

CREATE TABLE Subjects
(
	SubjectID INT PRIMARY KEY NOT NULL,
	SubjectName VARCHAR(50)
)
CREATE TABLE Majors
(
	MajorID INT PRIMARY KEY,
	[Name] VARCHAR(20)
)
CREATE TABLE Students
(
	StudentID INT PRIMARY KEY NOT NULL,
	StudentNumber CHAR(10),
	StudentName VARCHAR(30),
	MajorID INT  NOT NULL
)
ALTER TABLE Students
ADD FOREIGN KEY (MajorID) REFERENCES Majors(MajorID)

CREATE TABLE Agenda
(
	StudentID INT REFERENCES Students(StudentID),
	SubjectID INT REFERENCES Subjects(SubjectID)
	PRIMARY KEY (StudentID,SubjectID)
)
CREATE TABLE Payments
(
	PaymentID INT PRIMARY KEY NOT NULL,
	PaymentDate DATE,
	PaymentAmount DECIMAL(5,2),
	StudentID INT REFERENCES Students(StudentID) NOT NULL
)