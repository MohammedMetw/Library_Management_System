using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Library_Management_System;

namespace Library_Management_System
{

    //Book handler
    public class Library
    {
        public Dictionary<string, Book> books;

        public Library()
        {
            books = new Dictionary<string, Book>();
        }

        public void Add_NEW_Book(string title, string author, string isbn,  string bookId)
        {
            if (!books.Values.Any(book => book.BookIds.Contains(bookId)))
            {
                if (books.ContainsKey(isbn))
                {
                    books[isbn].BookIds.Add(bookId);
                    books[isbn].TotalCopies++;
                    books[isbn].Available_Copies++;
                    books[isbn].IS_Reserved.Add(bookId, false);
                }
                else
                {

                    Book newbook = new Book(title, author, isbn, 1, 1, bookId, false);
                    newbook.IS_Reserved.Add(bookId, false);
                    books.Add(isbn, newbook);
                }
            }
            else
            {
                Console.WriteLine($"This book ID  {bookId}  already exists in the library.");
            }

            
        }

        public void Remove_Book(string book_id)
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
        public List<Book> Search_Book(string search_term)
        {
            search_term = search_term.ToLower(); // search by title or Author
            var result = books.Values.Where(book => book.Title.ToLower().Contains(search_term) ||
            book.Author.ToLower().Contains(search_term)).ToList();
            return result;

        }

        public void View_ALL_Books()
        {

            foreach (var book in books.Values)
            {
                Console.WriteLine(book.DisplayInfo());
            }

        }

        public void BorrowBook(USER user, string BookID)
        {
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
            else
            {
                Console.WriteLine("Book doesn't already exist");
            }
        }
        public void ReturnBook(USER user , string BookID)
        {
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

    }
}
