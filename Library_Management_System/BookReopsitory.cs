using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Library_Management_System
{
    public class BookRepository
    {
        private readonly Connection _connection;

        public BookRepository(Connection connection)
        {
            _connection = connection;
        }

        private bool UserHasBorrowRecord(string userId, string bookId)
        {
            using (var conn = _connection.CreateConnection())
            using (var cmd = new SqlCommand(
                "SELECT COUNT(*) FROM BorrowBook WHERE UserID = @UserID AND BookID = @BookID", conn))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@BookID", bookId);
                conn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public DataTable GetAllBooks()
        {
            var dt = new DataTable();
            using (var conn = _connection.CreateConnection())
            using (var cmd = new SqlCommand(@"
                SELECT b.BookID,
                       b.ISBN,
                       bc.Title,
                       bc.Author,
                       bc.Total_Copies,
                       (bc.Total_Copies - ISNULL(bb.BorrowedCount, 0)) AS Available_Copies
                FROM Book b
                JOIN Book_Complement bc ON b.ISBN = bc.ISBN
                LEFT JOIN (
                    SELECT b.ISBN,
                           COUNT(*) AS BorrowedCount
                    FROM BorrowBook bb
                    JOIN Book b ON bb.BookID = b.BookID
                    GROUP BY b.ISBN
                ) bb ON bb.ISBN = bc.ISBN", conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                conn.Open();
                adapter.Fill(dt);
            }
            return dt;
        }

        public int GetAvailableCopiesByISBN(string isbn)
        {
            const string sql = @"
                SELECT bc.Total_Copies - COUNT(bb.BookID) AS Available_Copies
                FROM Book_Complement bc
                JOIN Book b ON bc.ISBN = b.ISBN
                LEFT JOIN BorrowBook bb ON bb.BookID = b.BookID
                WHERE bc.ISBN = @ISBN
                GROUP BY bc.Total_Copies";

            using (var conn = _connection.CreateConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ISBN", isbn);
                conn.Open();
                var result = cmd.ExecuteScalar();
                return result == null ? 0 : Convert.ToInt32(result);
            }
        }

        public bool BorrowBook(string userId, string bookId)
        {
            if (UserHasBorrowRecord(userId, bookId))
            {
                Console.WriteLine("User has already borrowed this book.");
                return false;
            }

            using (var conn = _connection.CreateConnection())
            {
                conn.Open();

                // Retrieve ISBN for the given BookID
                string isbn;
                using (var isbnCmd = new SqlCommand(
                    "SELECT ISBN FROM Book WHERE BookID = @BookID", conn))
                {
                    isbnCmd.Parameters.AddWithValue("@BookID", bookId);
                    isbn = isbnCmd.ExecuteScalar()?.ToString();
                    if (string.IsNullOrEmpty(isbn))
                    {
                        Console.WriteLine("Book not found.");
                        return false;
                    }
                }

                int availableCopies = GetAvailableCopiesByISBN(isbn);
                if (availableCopies <= 0)
                {
                    Console.WriteLine("No available copies. Adding user to waiting list...");
                    return false;
                }

                using (var transaction = conn.BeginTransaction())
                using (var cmd = new SqlCommand(@"
                    INSERT INTO BorrowBook (UserID, BookID)
                    VALUES (@UserID, @BookID);"
                    , conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@BookID", bookId);
                    cmd.Parameters.AddWithValue("@ISBN", isbn);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }

                Console.WriteLine($"Borrow successful for user {userId} : book {bookId}.");
                return true;
            }
        }

        public bool ReturnBook(string userId, string bookId)
        {
            if (!UserHasBorrowRecord(userId, bookId))
            {
                Console.WriteLine($"User {userId} has not borrowed book {bookId}.");
                return false;
            }

            using (var conn = _connection.CreateConnection())
            {
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                using (var cmd = new SqlCommand(@"
                    DELETE FROM BorrowBook
                    WHERE UserID = @UserID AND BookID = @BookID;
                   ", conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@BookID", bookId);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }

                Console.WriteLine($"User {userId} returned book {bookId}.");
                return true;
            }
        }
        public DataTable GetAllBorrowedBooks()
        {
            var dt = new DataTable();
            using (var conn = _connection.CreateConnection())
            using (var cmd = new SqlCommand(@"
        SELECT BookID, UserID
        FROM BorrowBook", conn))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                conn.Open();
                adapter.Fill(dt);
            }
            return dt;
        }

    }
}
