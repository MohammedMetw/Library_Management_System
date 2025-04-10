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
            
            Reader user = new Reader("11", "Esraa", "User", "143");
            Reader user2 = new Reader("12", "Mohammed", "User", "143");
            Admin admin = new Admin("11", "Esraa", "Admin", "143");
            // ✅ Add books to the library
            USER.UserSignUp();
           // USER.UserLogin();
            Console.WriteLine("Adding books to the library...\n");
            Admin.Add_NEW_Book("The Great Gatsby", "F. Scott Fitzgerald", "12345", "GAT001");
            Admin.Add_NEW_Book("1984", "George Orwell", "67890", "ORW001");
            Admin.Add_NEW_Book("To Kill a Mockingbird", "Harper Lee", "11111", "LEE001");
            Admin.Add_NEW_Book("1984", "George Orwell", "67890", "ORW002");
            Admin.Add_NEW_Book("Mohammed", "George Orwell", "143670", "ORW0063");
            user.SearchBook("ORW0063");
            // ✅ View all books
            Console.WriteLine("\n📚 Library Books:");
            Admin.View_ALL_Books();
            Console.WriteLine("\n📚 Library Arranged Books:");
            user.viewArrangedBooks();
            Console.WriteLine(@"borrwo book


    
");
            user.BorrowBook(user, "ORW0063");
            user.BorrowBook(user2, "ORW0063");
            Console.WriteLine(@"finish borrwo book



");
            user.DisplayUserInfo();
            // ✅ View all books
            Console.WriteLine("\n-------📚 Library Books:");
            Admin.View_ALL_Books();
            user.DisplayUserInfo();
            Console.WriteLine(@"return book



");
            user.ReturnBook("ORW0063");
            // user.DisplayUserInfo();
            Console.WriteLine("-----------------------------------------------------");
            Admin.DisplayAllUsers();
            Console.WriteLine("-----------------------------------------------------");
            // ✅ Prevent console from closing immediately
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();


          
        }
    }



}
