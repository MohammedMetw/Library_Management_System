CREATE DATABASE library;
GO
USE library;
GO

CREATE TABLE Book (
    BookID VARCHAR(10) PRIMARY KEY,
    ISBN   VARCHAR(20),
    Available_Copies INT
);

CREATE TABLE Book_Complement (
    ISBN         VARCHAR(20) PRIMARY KEY,
    Title        VARCHAR(100),
    Author       VARCHAR(100),
    Total_Copies INT
);

ALTER TABLE Book
ADD CONSTRAINT FK_Book_BookComplement
    FOREIGN KEY (ISBN) REFERENCES Book_Complement(ISBN);

CREATE TABLE Users (
    UserID   VARCHAR(10) PRIMARY KEY,
    Name     VARCHAR(50),
    Role     BIT,
    Password VARCHAR(50)
);

CREATE TABLE BorrowBook (
    UserID VARCHAR(10),
    BookID VARCHAR(10),
    PRIMARY KEY (UserID, BookID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (BookID) REFERENCES Book(BookID)
);
GO
USE library;
GO

ALTER TABLE Book
DROP COLUMN Available_Copies;
GO

USE library;
GO

-- بيانات جدول Book_Complement
INSERT INTO Book_Complement (ISBN, Title, Author, Total_Copies) VALUES
('345', 'Life of Pi',               'Yann Martel',      5),
('123', 'SQL Basics',               'Ahmed Alaa',       10),
('567', 'Advanced Database Design', 'Sara Mahmoud',      7),
('789', 'Learning C#',              'Mohamed Ali',       3);
GO

-- بيانات جدول Book (بدون Available_Copies)
INSERT INTO Book (BookID, ISBN) VALUES
('B001', '345'),
('B002', '123'),
('B003', '567'),
('B004', '789'),
('B005', '345');
GO

-- بيانات جدول Users
INSERT INTO Users (UserID, Name, Role, Password) VALUES
('U001', 'Esraa',      0, 'pass123'),   -- 0 = User
('U002', 'Ali',        0, 'ali2025'),
('U003', 'Amina',      1, 'adminA@1'),  -- 1 = Admin
('U004', 'Mohammed',   0, 'moh456'),
('U005', 'Sara',       1, 'saraAdmin');
GO

-- بيانات جدول BorrowBook
INSERT INTO BorrowBook (UserID, BookID) VALUES
('U001', 'B001'),
('U002', 'B002'),
('U001', 'B003'),
('U004', 'B001');
GO
UPDATE BorrowBook
SET BookID = 'B004'
WHERE UserID = 'U004';
USE library;
GO

ALTER TABLE BorrowBook
ADD CONSTRAINT UQ_BorrowBook_BookID UNIQUE (BookID);
GO
select* from Book
select* from Book_Complement
select * from BorrowBook
select * from Users