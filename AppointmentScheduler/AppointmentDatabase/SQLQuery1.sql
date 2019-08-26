Create Table Users
(UserId bigint NOT NULL PRIMARY KEY IDENTITY,
UserName nvarchar(256) NOT NULL UNIQUE,
Password nvarchar(MAX) NOT NULL,
FirstName nvarchar(256) NOT NULL,
LastName nvarchar(256),
EmailID nvarchar(256) NOT NULL UNIQUE,
PhoneNumber BIGINT 
)
DESC Users;

Select * from Users

INSERT INTO Users(UserName,Password, FirstName,LastName,EmailID,PhoneNumber)
VALUES (gokul.goel,gokul,gokul,goel,gokul.goel@gmsil.com,432)
VALUES('arpit211190', 'Arpit','Arpit','Gupta','arpitgupta.211190@gmail.com',4);

alter table Users
add PhoneNumber bigint

Update Users
SET PhoneNumber = 42544232538
where UserId = 2

Update Users
SET UserName = 'richa',
FirstName = 'Richa'
where UserId = 2


CREATE PROCEDURE VALIDATE_USER @EMAILID NVARCHAR(256)
As
SELECT 1 FROM USERS WHERE EMAILID=@EMAILID
GO


exec VALIDATE_USER "richa.goyal90@gmail.com"

DROP PROCEDURE VALIDATE_USER

CREATE PROCEDURE LOGIN_USER @USERNAME NVARCHAR(256),@PASSWORD NVARCHAR(256)
As
SELECT FIRSTNAME,LASTNAME,EMAILID,PHONENUMBER FROM USERS WHERE USERNAME=@USERNAME AND @PASSWORD=PASSWORD
GO

EXEC LOGIN_USER "richa.goyal90","richa"


CREATE PROCEDURE INSERT_USER @USERNAME nvarchar(256),@PASSWORD nvarchar(MAX),@FIRSTNAME nvarchar(256),@LASTNAME nvarchar(256),@EMAILID nvarchar(256),@PHONENUMBER BIGINT 
AS
INSERT INTO USERS(USERNAME,PASSWORD,FIRSTNAME,LASTNAME,EMAILID,PHONENUMBER)
VALUES(@USERNAME,@PASSWORD,@FIRSTNAME,@LASTNAME,@EMAILID,@PHONENUMBER)
GO