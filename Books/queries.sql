--drop DATABASE LibraryDb;

--use LibraryDb;
--drop TABLE Orders;
--drop TABLE Hist;

--drop TABLE Subs;
--drop TABLE Books;


--CREATE DATABASE LibraryDb;

--SELECT * FROM Books;
--SELECT * FROM Subs;
--SELECT * FROM Orders;
--SELECT * FROM Hist;

--INSERT INTO Orders 
--	OUTPUT INSERTED.*
--	VALUES 
--		(0, 0, GETDATE()) ;

--INSERT INTO Orders 
--	VALUES 
--		(1, 2, GETDATE()),
--		(2, 2, GETDATE()),
--		(3, 5, GETDATE()),
--		(4, 5, GETDATE()) ;
--SELECT * FROM Orders;