--CREATE DATABASE LibraryDb;
use LibraryDb;
--список абонентов
CREATE TABLE Subs
(
	SubsId INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    SubsName NTEXT NOT NULL
);

--спиок книг
CREATE TABLE Books
(
	BookId INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    BookName NTEXT NOT NULL
);

--спиок выданных книг
CREATE TABLE Orders
(
	OrderBookId INT NOT NULL, 
    OrderSubsId INT NOT NULL,
	OrderDate DATETIME NOT NULL, 
	CONSTRAINT book_id FOREIGN KEY (OrderBookId) REFERENCES Books(BookId),
	CONSTRAINT subs_id FOREIGN KEY (OrderSubsId) REFERENCES Subs(SubsId),
	CONSTRAINT order_id PRIMARY KEY (OrderBookId, OrderSubsId)
);

--история выдачи книг
CREATE TABLE Hist
(
	HistId INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
	HistBookId INT NOT NULL, 
    HistSubsId INT NOT NULL,
	HistDate DATETIME NOT NULL, 
	CONSTRAINT hist_book_id FOREIGN KEY (HistBookId) REFERENCES Books(BookId),
	CONSTRAINT hist_subs_id FOREIGN KEY (HistSubsId) REFERENCES Subs(SubsId)
);

INSERT INTO Subs
	VALUES 
		(N'Иванов Иван Иваныч'),
		(N'Гарри Потер'),
		(N'Джек Воробей'),
		(N'Билл Клинтон'),
		(N'Билл Гейтс'),
		(N'Cтив Балмер'),
		(N'Владимир Путин');

INSERT INTO Books 
	VALUES 
		(N'Г.Г. Маркес "Сто лет одиночества"'),
		(N'Л.Н. Толстой "Война и мир"'),
		(N'Толкиен "Властелин колец"'),
		(N'А.С. Пушкин "Евгений Онегин"'),
		(N'М.Горький "Мать"'),
		(N'Джек Лондон "Белый Клык"'),
		(N'Иванов Иван Иваныч "Моя жизнь"');

INSERT INTO Orders 
	VALUES 
		(1, 2, GETDATE()),
		(2, 2, GETDATE()),
		(3, 5, GETDATE()),
		(4, 5, GETDATE()) ;

INSERT INTO Hist 
	VALUES 
		(1, 2, GETDATE()),
		(2, 2, GETDATE()),
		(3, 5, GETDATE()),
		(4, 5, GETDATE()) ;
