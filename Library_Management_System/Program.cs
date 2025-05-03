using System;
using Library_Management_System;

namespace Library_Management_System
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // 1. Set up connection, repository, and service
                var connection = new Connection();
                var bookRepo = new BookRepository(connection);
                var libraryService = new LibraryService(bookRepo);

                // 2. Load books into memory (cache)
                libraryService.LoadCache();
                Console.WriteLine("✅ Book cache loaded successfully.\n");

                bool exit = false;

                while (!exit)
                {
                    // Display the menu options
                    Console.WriteLine("\n===== Library Management System =====");
                    Console.WriteLine("1. View all books");
                    Console.WriteLine("2. Search for a book by ISBN");
                    Console.WriteLine("3. Borrow a book");
                    Console.WriteLine("4. Return a book");
                    Console.WriteLine("5. Reload book cache");
                    Console.WriteLine("6. Exit");
                    Console.Write("Enter your choice: ");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            // 3. View all books
                            Console.WriteLine("\nAll available books:");
                            libraryService.ViewAllBooks();
                            break;

                        case "2":
                            // 4. Search for a book by ISBN
                            Console.Write("\nEnter ISBN to search: ");
                            string isbn = Console.ReadLine();
                            var book = libraryService.SearchBook(isbn);

                            if (book == null)
                            {
                                Console.WriteLine("❌ Book not found.");
                            }
                            break;

                        case "3":
                            // 5. Borrow a book
                            Console.Write("\nEnter Book ID to borrow: ");
                            string borrowBookId = Console.ReadLine();
                            Console.Write("Enter User ID: ");
                            string borrowUserId = Console.ReadLine();

                            // Directly borrow the book using the Book ID and User ID
                            bookRepo.BorrowBook(borrowUserId, borrowBookId);
                            Console.WriteLine("✅ Book borrowed successfully.");
                            break;

                        case "4":
                            // 6. Return a book
                            Console.Write("\nEnter Book ID to return: ");
                            string returnBookId = Console.ReadLine();
                            Console.Write("Enter User ID: ");
                            string returnUserId = Console.ReadLine();

                            // Directly return the book using the Book ID and User ID
                            bookRepo.ReturnBook(returnUserId, returnBookId);
                            Console.WriteLine("✅ Book returned successfully.");
                            break;

                        case "5":
                            // 7. Reload book cache
                            Console.WriteLine("\nReloading book cache...");
                            libraryService.LoadCache();
                            Console.WriteLine("✅ Book cache reloaded successfully.");
                            break;

                        case "6":
                            // Exit the application
                            exit = true;
                            Console.WriteLine("Exiting the application...");
                            break;

                        default:
                            Console.WriteLine("❌ Invalid choice. Please try again.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("💥 Error: " + ex.Message);
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
