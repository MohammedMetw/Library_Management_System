using System;
using System.Collections.Generic;
using System.Linq;

namespace Library_Management_System
{
    public class USER
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public List<string> ReservedBooks { get; private set; }

        public USER(string id, string name, string role, string password)
        {
            Id = id;
            Name = name;
            Role = role;
            Password = password;
            ReservedBooks = new List<string>();
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
        private List<USER> _users;

        public UserHandler()
        {
            _users = new List<USER>();
            _users.Add(new USER("1", "esraa", "User", "1234"));
        }

        public USER Login()
        {
            Console.WriteLine("Enter your username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();
            USER foundUser = _users.FirstOrDefault(u =>
                u.Name.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);
            if (foundUser != null)
            {
                Console.WriteLine("Login successful.");
                return foundUser;
            }
            else
            {
                Console.WriteLine("Login failed. Please try again.");
                return null;
            }
        }

        public USER SignUp()
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
            Console.WriteLine("Set your password:");
            string password = Console.ReadLine();
            string id = (_users.Count + 1).ToString();
            USER newUser = new USER(id, name, "User", password);
            _users.Add(newUser);
            Console.WriteLine("Sign up successful.");
            return newUser;
        }

        public USER ListAndChooseUser()
        {
            Console.WriteLine("Select an option: 1 - Log in, 2 - Sign up");
            string input = Console.ReadLine();
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
                return null;
            }
        }

        public void DisplayAllUsers()
        {
            foreach (var user in _users)
            {
                user.DisplayUserInfo();
            }
        }
    }
}
