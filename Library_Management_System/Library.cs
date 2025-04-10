using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Library_Management_System;

namespace Library_Management_System
{

    //Book handler
    public class LibraryHandler
    {
        public static Dictionary<string, Book> books = null!;

        public LibraryHandler()
        {
            books = new Dictionary<string, Book>();
        }

        public static void Add_NEW_Book(string title, string author, string isbn, string bookId)
        {
            if (books is null)
            {
                books = new Dictionary<string, Book>();
                Book newbook = new Book(title, author, isbn, 1, 1, bookId, false);
                newbook.IS_Reserved.Add(bookId, false);
                books?.Add(isbn, newbook);
            }
            else if (!books.Values.Any(book => book.BookIds.Contains(bookId)))
            {
                if (books.ContainsKey(isbn))
                {
                    books[isbn].BookIds.Add(bookId);
                    books[isbn].TotalCopies++;
                    books[isbn].Available_Copies++;
                    books[isbn].IS_Reserved.Add(bookId, false);
                    Console.WriteLine("book added successfully");

                }
                else
                {

                    Book newbook = new Book(title, author, isbn, 1, 1, bookId, false);
                    newbook.IS_Reserved.Add(bookId, false);
                    books.Add(isbn, newbook);
                    Console.WriteLine("book added successfully");
                }
            }
            else
            {
                Console.WriteLine($"This book ID  {bookId}  already exists in the library.");
            }


        }

        public static void Remove_Book(string book_id)
        {
            //find book with book ID
            var find_book = books.FirstOrDefault(b => b.Value.BookIds.Contains(book_id));
            if (find_book.Value.TotalCopies > 1)
            {
                find_book.Value.BookIds.Remove(book_id);

                find_book.Value.TotalCopies--;
                find_book.Value.Available_Copies--;
                Console.WriteLine($"Book copy with ID {book_id} has been removed.");
            }
            else if (find_book.Value.TotalCopies == 1)
            {
                books.Remove(find_book.Key);
                Console.WriteLine($"Book with ISBN {find_book.Key}  has been completely removed.");
            }
            else
            {
                Console.WriteLine("BookID Not found");
            }
        }
        public static List<Book> Search_Book(string search_term)
        {
            search_term = search_term.ToLower(); // search by title or Author
            var result = books.Values.Where(book => book.Title.ToLower().Contains(search_term) ||
            book.Author.ToLower().Contains(search_term)).ToList();
            return result;

        }

        public static void View_ALL_Books()
        {
            if (books is null)
            {
                Console.WriteLine("There is no books in the library right now ");
                return;
            }

            foreach (var book in books.Values)
            {
                Console.WriteLine(book.DisplayInfo());
            }

        }

        public static void BorrowBook(USER user, string BookID)
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
                    user.ReserveBook(BookID);
                }
                else
                {
                    Console.WriteLine("No available copies. Adding user to waiting list...");
                }
            }

        }
        public static void ReturnBook(USER user, string BookID)
        {
            if (books is null)
            {
                Console.WriteLine("Book doesn't already exist");
                return;
            }
            var book = books.Values.FirstOrDefault(b => b.BookIds.Contains(BookID));

            if (book != null)
            {
                book.UpdateAvailability(1, BookID);
                user.ReturnBook(BookID);
                Console.WriteLine($"the book {BookID} returned to the library");
            }
            else
            {
                Console.WriteLine("Book doesn't already exist");
            }
        }
        public static Book SearchBook(string BookID)
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
        public static void viewArrangedBooks()
        {
            var arrangedBooks = books.Values.OrderBy(book => book.Title).ToList();
            foreach (var book in arrangedBooks)
            {
                Console.WriteLine(book.DisplayInfo());
            }
        }
    }
}
