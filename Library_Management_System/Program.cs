using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using System.Xml.Linq;
using Library_Management_System;


namespace Program
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {

            // ✅ Create a library instance  
            
            USER user = new USER("11", "Esraa", "User", "");
            USER user2 = new USER("12", "Mohammed", "User", "");

            // ✅ Add books to the library
            Console.WriteLine("Adding books to the library...\n");
            LibraryHandler.Add_NEW_Book("The Great Gatsby", "F. Scott Fitzgerald", "12345", "GAT001");
            LibraryHandler.Add_NEW_Book("1984", "George Orwell", "67890", "ORW001");
            LibraryHandler.Add_NEW_Book("To Kill a Mockingbird", "Harper Lee", "11111", "LEE001");
            LibraryHandler.Add_NEW_Book("1984", "George Orwell", "67890", "ORW002");
            LibraryHandler.Add_NEW_Book("Mohammed", "George Orwell", "143670", "ORW0063");
            LibraryHandler.SearchBook("ORW0063");
            // ✅ View all books
            Console.WriteLine("\n📚 Library Books:");
            LibraryHandler.View_ALL_Books();
            Console.WriteLine("\n📚 Library Arranged Books:");
            LibraryHandler.viewArrangedBooks();
            Console.WriteLine(@"borrwo book


    
");
            LibraryHandler.BorrowBook(user, "ORW0063");
            LibraryHandler.BorrowBook(user2, "ORW0063");
            Console.WriteLine(@"finish borrwo book



");
            user.DisplayUserInfo();
            // ✅ View all books
            Console.WriteLine("\n-------📚 Library Books:");
            LibraryHandler.View_ALL_Books();
            user.DisplayUserInfo();
            Console.WriteLine(@"return book



");
            LibraryHandler.ReturnBook(user, "ORW0063");
            user.DisplayUserInfo();

            // ✅ Prevent console from closing immediately
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();


          
        }
    }



}