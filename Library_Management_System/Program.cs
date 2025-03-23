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
            Library myLibrary = new Library();
            USER user = new USER("11", "Esraa", "User", "");
            USER user2 = new USER("12", "Mohammed", "User", "");

            // ✅ Add books to the library
            Console.WriteLine("Adding books to the library...\n");
            myLibrary.Add_NEW_Book("The Great Gatsby", "F. Scott Fitzgerald", "12345", "GAT001");
            myLibrary.Add_NEW_Book("1984", "George Orwell", "67890", "ORW001");
            myLibrary.Add_NEW_Book("To Kill a Mockingbird", "Harper Lee", "11111", "LEE001");


            myLibrary.Add_NEW_Book("1984", "George Orwell", "67890", "ORW002");

            myLibrary.Add_NEW_Book("Mohammed", "George Orwell", "143670", "ORW0063");
            // ✅ View all books
            Console.WriteLine("\n📚 Library Books:");
            myLibrary.View_ALL_Books();
            myLibrary.BorrowBook(user, "ORW0063");
            myLibrary.BorrowBook(user2, "ORW0063");

            // ✅ View all books
            Console.WriteLine("\n-------📚 Library Books:");
            myLibrary.View_ALL_Books();

            
            user.DisplayUserInfo();

            // ✅ Prevent console from closing immediately
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();

          
        }
    }



}