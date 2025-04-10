using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System
{
    class Reader : USER
    {
        public Reader(string id, string name, string role, string password) : base(id, name, role, password)
        {
        }
        public void ReserveBook(string bookId)
        {
            if (!ReservedBooks.Contains(bookId))
            {
                ReservedBooks.Add(bookId);
                Console.WriteLine($"User {Name} reserved book {bookId}.");
            }
            else
            {
                Console.WriteLine($"User {Name} has already reserved book {bookId}.");
            }
        }
        public void ReturnBook(string bookId)
        {
            if (ReservedBooks.Contains(bookId))
            {
                ReservedBooks.Remove(bookId);
                Console.WriteLine($"User {Name} returned book {bookId}.");
            }
            else
            {
                Console.WriteLine($"User {Name} has not reserved book {bookId} anymore.");
            }
        }
        public Book SearchBook(string BookID)
        {
            Book book = null!;
            if (books is null)
            {
                Console.WriteLine("there is no books in the library right now");
                return book;
            }
            else
            {
                var returnedBook = books.Values.FirstOrDefault(b => b.BookIds.Contains(BookID));
                if (returnedBook != null)
                {
                    Console.WriteLine("The book was found successfully");

                    Console.WriteLine(returnedBook.DisplayInfo());
                    return returnedBook;
                }
                else return book;
            }
        }
        public void viewArrangedBooks()
        {
            var arrangedBooks = books.Values.OrderBy(book => book.Title).ToList();
            foreach (var book in arrangedBooks)
            {
                Console.WriteLine(book.DisplayInfo());
            }
        }
        public void BorrowBook(USER user, string BookID)
        {
            if (books is null)
            {
                Console.WriteLine("Book doesn't already exist");
                return;
            }
            var book = books.Values.FirstOrDefault(b => b.BookIds.Contains(BookID));

            if (book != null)
            {
                if (book.Available_Copies > 0)
                {
                    book.Reserve(BookID);
                    ReserveBook(BookID);
                }
                else
                {
                    Console.WriteLine("No available copies. Adding user to waiting list...");
                }
            }

        }
    }
}
