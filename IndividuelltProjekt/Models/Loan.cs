using IndividuelltProjekt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IndividuelltProjekt.Models
{
    public class Loan
    {
        public int Id { get; set; }

        public int User_Id { get; set; }
        public User User { get; set; }

        public long Book_Id { get; set; }
        public Book Book { get; set; }

        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        //Lista alla tillgängliga böcker
        public static void ListAllAvailableBooks()
        {
            using (var context = new LoanContext())
            {
                var availableBooks = context.Books.Where(b => b.Available).ToList();
                foreach (var book in availableBooks)
                {
                    Console.WriteLine($"\t\tISBN: {book.Id} - {book.Title} av {book.Author}");
                }
            }
        }

        public static List<Book> GetLoanList(int User_id)
        {
            using var context = new LoanContext();
            var books = context.Loans.Where(l => l.User_Id == User_id && l.ReturnDate == null)
                .Include(l => l.Book).Select(l => l.Book).ToList();
            return books;
        }
        public static void LoanBook(int userId, long bookId)
        {
            using var context = new LoanContext();
            var book = context.Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
                throw new Exception("\t\tBoken finns inte!");

            if (!book.Available)
                throw new Exception("\t\tBoken är redan utlånad!");

            var loan = new Loan
            {
                User_Id = userId,
                Book_Id = bookId,
                LoanDate = DateTime.Now,
                ReturnDate = null
            };

            book.Available = false; // Markera som utlånad

            context.Loans.Add(loan);
            context.SaveChanges();

            Console.WriteLine("\t\tBoken är nu utlånad!");
        }
        public static void ReturnBook(long bookId)
        {
            using var context = new LoanContext();

            var loan = context.Loans
                .FirstOrDefault(l => l.Book_Id == bookId && l.ReturnDate == null);

            if (loan == null)
                throw new Exception("\t\tDenna bok är inte utlånad!");

            var book = context.Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
                throw new Exception("\t\tBoken saknas i biblioteket!");

            loan.ReturnDate = DateTime.Now;  // Registrera retur
            book.Available = true;           // Markera som ledig igen

            context.SaveChanges();

            Console.WriteLine("\t\tBoken har lämnats tillbaka!");
        }
    }
    
}
