using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Library_Management_System
{
    public class USER
    {
        private static List<USER> _users = new List<USER>();

        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public List<string> ReservedBooks { get; private set; }
        public static Dictionary<string, Book> books = null!;
        public USER(string id, string name, string role, string password)
        {
            Id = id;
            Name = name;
            Role = role;
            Password = password;
            ReservedBooks = new List<string>();
            _users.Add(this);
        }
        public static List<USER> GetList()
        {
            return _users;
        }

        public static void UserLogin()
        {
            UserHandler.Login();
        }
        public static void UserSignUp()
        {

            UserHandler.SignUp();
        }

        public void DisplayUserInfo()
        {
            Console.WriteLine("----- User Information -----");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Role: {Role}");
            Console.WriteLine($"Password: {Password}");
            Console.WriteLine("Reserved Books:");
            if (ReservedBooks.Count > 0)
            {
                foreach (var bookId in ReservedBooks)
                {
                    Console.WriteLine($"BookID: {bookId}");
                }
            }
            else
            {
                Console.WriteLine("No reserved books.");
            }
        }
    }

    public class UserHandler
    {
       
        public static USER Login()
        {
            Console.WriteLine("Enter your username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();
            USER foundUser = USER.GetList().FirstOrDefault(u =>
               u.Name.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);
            if (foundUser != null)
            {
                Console.WriteLine($"Login successful. hello {username}");
                return foundUser;
            }
            else
            {
                Console.WriteLine("Login failed. Please try again.");
                return null;
            }
        }

        public  static USER SignUp()
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
            Console.WriteLine("Set your password:");
            string password = Console.ReadLine();
            string id = (USER.GetList()
                 .Select(u => int.TryParse(u.Id, out int num) ? num : 0)
                 .DefaultIfEmpty(0)
                 .Max() + 1)
                 .ToString();
            USER newUser = new USER(id, name, "User", password);
            Console.WriteLine("Sign up successful.");
            return newUser;
        }

        public static USER ListAndChooseUser()
        {
            Console.WriteLine("Select an option: 1 - Log in, 2 - Sign up");
            string ?input = Console.ReadLine();
            if (input == "1")
            {
                return Login();
            }
            else if (input == "2")
            {
                return SignUp();
            }
            else
            {
                Console.WriteLine("Invalid option.");
                return null!;
            }
        }

        
    }
}
