CREATE DATABASE Contacts

CREATE TABLE Users (
Username VARCHAR (50)NOT NULL PRIMARY KEY,
Password VARCHAR (50)NOT NULL,
);

CREATE TABLE Contacts (
ID int  NOT NULL IDENTITY(100,1) PRIMARY KEY,
FirstName VARCHAR (50) NOT NULL ,
Surname VARCHAR (50) NOT NULL,
Email VARCHAR (50) NOT NULL,
Telephone VARCHAR (50) NOT NULL,
Username VARCHAR (50)  NOT NULL,
FOREIGN KEY (Username) REFERENCES Users (Username)
);

INSERT INTO Users VALUES
('cedy','123'),

INSERT INTO Contacts VALUES
('Cedric','Ntumba','Cedy@gmail.com','0834567898','cedy'),

SELECT*FROM USERS
SELECT*FROM Contacts

drop database Contacts
