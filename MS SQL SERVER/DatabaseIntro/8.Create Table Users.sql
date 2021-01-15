CREATE TABLE Users
(
Id BIGINT PRIMARY KEY IDENTITY,
Username VARCHAR(30) NOT NULL,
[Password] VARCHAR(26),
ProfilePicture VARCHAR(MAX),
LastLoginTime DATETIME,
IsDeleted BIT
)
INSERT INTO Users(Username,[Password],ProfilePicture,LastLoginTime,IsDeleted)
VALUES
('googp',3323,'https://www.google.com/imgres?imgurl=https%3A%2F%2Fmedia-exp1.licdn.com%2Fdms%2Fimage%2FC560BAQHMnA03XDdf3w%2Fcompany-logo_200_200%2F0%2F1519855918965%3Fe%3D2159024400%26v%3Dbeta%26t%3DCrP5Le1mWICRcaxIGNBuajHcHGFPuyNA5C8DI339lSk&imgrefurl=https%3A%2F%2Fwww.linkedin.com%2Fcompany%2Fimg&tbnid=d42LwtBDxTXoOM&vet=12ahUKEwiI09-Wj5nuAhUGbRoKHfyvBpgQMygBegUIARCWAQ..i&docid=huUCOMTfkRHQEM&w=200&h=200&q=img&ved=2ahUKEwiI09-Wj5nuAhUGbRoKHfyvBpgQMygBegUIARCWAQ',
'6/12/2021',0),
('daddsd',434,'https://www.google.com/imgres?imgurl=https%3A%2F%2Fmedia-exp1.licdn.com%2Fdms%2Fimage%2FC560BAQHMnA03XDdf3w%2Fcompany-logo_200_200%2F0%2F1519855918965%3Fe%3D2159024400%26v%3Dbeta%26t%3DCrP5Le1mWICRcaxIGNBuajHcHGFPuyNA5C8DI339lSk&imgrefurl=https%3A%2F%2Fwww.linkedin.com%2Fcompany%2Fimg&tbnid=d42LwtBDxTXoOM&vet=12ahUKEwiI09-Wj5nuAhUGbRoKHfyvBpgQMygBegUIARCWAQ..i&docid=huUCOMTfkRHQEM&w=200&h=200&q=img&ved=2ahUKEwiI09-Wj5nuAhUGbRoKHfyvBpgQMygBegUIARCWAQ',
'1/2/2023',0),
('ffad',31232,'https://www.google.com/imgres?imgurl=https%3A%2F%2Fmedia-exp1.licdn.com%2Fdms%2Fimage%2FC560BAQHMnA03XDdf3w%2Fcompany-logo_200_200%2F0%2F1519855918965%3Fe%3D2159024400%26v%3Dbeta%26t%3DCrP5Le1mWICRcaxIGNBuajHcHGFPuyNA5C8DI339lSk&imgrefurl=https%3A%2F%2Fwww.linkedin.com%2Fcompany%2Fimg&tbnid=d42LwtBDxTXoOM&vet=12ahUKEwiI09-Wj5nuAhUGbRoKHfyvBpgQMygBegUIARCWAQ..i&docid=huUCOMTfkRHQEM&w=200&h=200&q=img&ved=2ahUKEwiI09-Wj5nuAhUGbRoKHfyvBpgQMygBegUIARCWAQ',
'3/10/2021',0),
('bgbgb',4341,'https://www.google.com/imgres?imgurl=https%3A%2F%2Fmedia-exp1.licdn.com%2Fdms%2Fimage%2FC560BAQHMnA03XDdf3w%2Fcompany-logo_200_200%2F0%2F1519855918965%3Fe%3D2159024400%26v%3Dbeta%26t%3DCrP5Le1mWICRcaxIGNBuajHcHGFPuyNA5C8DI339lSk&imgrefurl=https%3A%2F%2Fwww.linkedin.com%2Fcompany%2Fimg&tbnid=d42LwtBDxTXoOM&vet=12ahUKEwiI09-Wj5nuAhUGbRoKHfyvBpgQMygBegUIARCWAQ..i&docid=huUCOMTfkRHQEM&w=200&h=200&q=img&ved=2ahUKEwiI09-Wj5nuAhUGbRoKHfyvBpgQMygBegUIARCWAQ',
'1/4/2015',0),
('nhnh',776,'https://www.google.com/imgres?imgurl=https%3A%2F%2Fmedia-exp1.licdn.com%2Fdms%2Fimage%2FC560BAQHMnA03XDdf3w%2Fcompany-logo_200_200%2F0%2F1519855918965%3Fe%3D2159024400%26v%3Dbeta%26t%3DCrP5Le1mWICRcaxIGNBuajHcHGFPuyNA5C8DI339lSk&imgrefurl=https%3A%2F%2Fwww.linkedin.com%2Fcompany%2Fimg&tbnid=d42LwtBDxTXoOM&vet=12ahUKEwiI09-Wj5nuAhUGbRoKHfyvBpgQMygBegUIARCWAQ..i&docid=huUCOMTfkRHQEM&w=200&h=200&q=img&ved=2ahUKEwiI09-Wj5nuAhUGbRoKHfyvBpgQMygBegUIARCWAQ',
'5/3/2012',0)