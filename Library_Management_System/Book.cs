using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System
{
    public class Book
    {

        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public List<string> BookIds { get; } // List to hold multiple book IDs
        public int Available_Copies { get; set; }
        public int TotalCopies { get; set; }

        public Dictionary<string, bool> IS_Reserved { get; }

        public Book(string title, string author, string isbn, int availableCopies, int totalCopies, string bookId, bool isReserved)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            Available_Copies = availableCopies;
            TotalCopies = totalCopies;
            BookIds = new List<string> { bookId };
            IS_Reserved = new Dictionary<string, bool>();
        }
        public string DisplayInfo()
        {
            StringBuilder info = new StringBuilder();

            info.AppendLine($"The title of the book is {this.Title} ");
            info.AppendLine($"The Author of the book is {this.Author}");
            info.AppendLine($"The  ISBN  is {this.ISBN}");
            info.AppendLine($"the total copies is {this.TotalCopies}");
            info.AppendLine($"the available copies is {this.Available_Copies}\nbook IDs    :    Reserved Type  ");


            foreach (var bookId in BookIds)
            {
                info.Append("  "+bookId + "    :    ");

                //check reserved
                if (IS_Reserved.ContainsKey(bookId) && IS_Reserved[bookId])
                    info.AppendLine("Reserved");
                else
                    info.AppendLine("Not Reserved");
            }
            return info.ToString();
        }
        public int UpdateAvailability(int Choice, string ID)
        {
            if (Choice == -1)
            {
                Available_Copies--;
                IS_Reserved[ID] = true;
            }
            if (Choice == 1)
            {
                Available_Copies++;
                IS_Reserved[ID] = false;
            }
            return Available_Copies;
        }

        public void Reserve(string ID)
        {
            if (TotalCopies == 0)
            {
                throw new Exception("Total copies is zero");
            }
            if (Available_Copies == 0)
            {
                throw new Exception("No available copies");
            }
            else
            {
                IS_Reserved[ID] = true;
                Available_Copies--;

            }
        }

    }


}