using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace Library_Management_System
{

    internal class Borrow
    {
        public delegate void waitingDelegate();
        public event waitingDelegate OnWaiting;
        public event waitingDelegate NoCopies;
        public event waitingDelegate OnReturning;
        Queue<Tuple<USER, Book>> _waitingUsers;
        private Dictionary<USER, Book>? _userBookBorrowing;
        public Borrow()
        {
            _waitingUsers = new Queue<Tuple<USER, Book>>();
            _userBookBorrowing = new Dictionary<USER, Book>();
        }
        public void _borrowBook(ref USER user, ref Book book)
        {
            //this condition check the number of availabe copies and if it zero 
            //it waill invoke an event to publish that the user put into wainting queue
            //S=> single responsibility 
            
            if (book.Available_Copies == 0 )
            {
                _waitingUsers?.Enqueue(Tuple.Create(user, book));
                OnWaiting.Invoke();
            }
            if (book.TotalCopies == 0)
            {
                //this even will be used in handler to call a funciton to
                //indicate that there is no copies in the book
                NoCopies.Invoke();
            }
            else
            {
                bool CheckAvaiability = false;
                foreach (var id in book.IS_Reserved)
                {
                    if (id.Value == true)
                    {
                        CheckAvaiability = true;
                        _userBookBorrowing[user] = book;
                        book.Reserve(id.Key);
                    }
                }
                if (!CheckAvaiability)
                {
                    _waitingUsers?.Enqueue(Tuple.Create(user, book));
                    OnWaiting.Invoke();
                }
            }

        }
       
        public void _returnBook(ref USER user, ref Book book)
        {
            var @this = _userBookBorrowing[user];
            //update availability not done correctly here 
            book.Available_Copies++;
            _userBookBorrowing.Remove(user);
            OnReturning.Invoke();

        }
        public void ReturnBook(string User, string BookTitle)
        {

        }
        public void ManageingReservations()
        {

            foreach (var onwait in _waitingUsers)
            {
                if (onwait.Item2.Available_Copies > 0)
                {
                    onwait.Item2.Available_Copies--;
                    _userBookBorrowing[onwait.Item1 as USER] = onwait.Item2 as Book;
                    // the remining is to remove it from waigingusers
                }
            }


        }
    }
}