using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Library_Management_System
{
 
    public class LibraryService
    {
        private readonly BookRepository _repository;
        private Dictionary<string, Book> _bookCache = new();

        public LibraryService(BookRepository repository)
        {
            _repository = repository;
            LoadCache();
        }


        public void LoadCache()
        {
            var tempDict = new Dictionary<string, Book>(StringComparer.OrdinalIgnoreCase);

            var table = _repository.GetAllBooks();

            var borrowedBooks = _repository.GetAllBorrowedBooks();
            var borrowedBookIds = new HashSet<string>(
                borrowedBooks.AsEnumerable().Select(row => row["BookID"].ToString()!)
            );

      
            foreach (DataRow row in table.Rows)
            {
                string isbn = row["ISBN"].ToString()!;
                string title = row["Title"].ToString()!;
                string author = row["Author"].ToString()!;
                int totalCopies = Convert.ToInt32(row["Total_Copies"]);
                int availableCopies = Convert.ToInt32(row["Available_Copies"]);
                string bookId = row["BookID"].ToString()!;

                
                bool isReserved = borrowedBookIds.Contains(bookId);

                if (!tempDict.ContainsKey(isbn))
                {
                    tempDict[isbn] = new Book(
                        title: title,
                        author: author,
                        isbn: isbn,
                        availableCopies: availableCopies,
                        totalCopies: totalCopies,
                        bookId: bookId,
                        isReserved: isReserved 
                    );
                }
                else
                {
                   
                    tempDict[isbn].BookIds.Add(bookId);
                    tempDict[isbn].IS_Reserved[bookId] = isReserved;
                }
            }

           
            _bookCache = tempDict;
        }



        public void ViewAllBooks()
        {
            if (_bookCache.Count == 0)
            {
                Console.WriteLine("There are no books available currently.");
                return;
            }

            foreach (var book in _bookCache.Values)
            {
                Console.WriteLine(book.DisplayInfo());
            }
        }


        public Book? SearchBook(string isbn)
        {
            
            if (_bookCache.TryGetValue(isbn, out var book))
            {
                Console.WriteLine("Book found:");
                Console.WriteLine(book.DisplayInfo());
                return book;
            }

            Console.WriteLine("Book not found.");
            return null;
        }


    }
}
