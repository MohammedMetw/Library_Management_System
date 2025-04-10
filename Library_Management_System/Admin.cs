using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System
{
    internal class Admin : USER
    {
       
        public Admin(string id, string name, string role, string password) : base(id, name, role, password)
        {
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
    }
}
