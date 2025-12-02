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
        public User? User { get; set; }

        public long Book_Id { get; set; }
        public Book? Book { get; set; }

        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }

       
        //Lista lånade böcker - här skulle jag vilja göra utan return med en void men kan inte komma på just nu då klockan är för mycket hur man ska göra det med en lista,
        //jag vill dessutom att LoanDate ska visas. men klurar vidare måndag då söndag blir vilodag..
        public static List<Book> GetLoanList(int User_id)
        {
            using var context = new LoanContext();
            var books = context.Loans.Where(l => l.User_Id == User_id && l.ReturnDate == null).Include(l => l.Book).Select(l => l.Book).ToList();
            return books!;
        }
        public static void LoanBook(int userId, long bookId)
        {
            using var context = new LoanContext();
            var book = context.Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
                throw new Exception("\n\t\tBoken finns inte!");
            if (!book.Available)
                throw new Exception("\n\t\tBoken är redan utlånad!");
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
            Console.WriteLine("\n\t\tBoken är nu utlånad!");
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

        // Lånehistorik för en användare - Ska fortfarande testas om den fungerar
        public static void LoanHistory(int userId)
        {
            using var context = new LoanContext();
            var loans = context.Loans
                .Where(l => l.User_Id == userId)
                .Include(l => l.Book)
                .ToList();
            Console.WriteLine("\n\t\tLånehistorik:");
            foreach (var loan in loans)
            {
                string returnDate = loan.ReturnDate.HasValue ? loan.ReturnDate.Value.ToString("yyyy-MM-dd") : "Ej återlämnad";
                Console.WriteLine($"\t\tBoktitel: {loan.Book?.Title}, Lånedatum: {loan.LoanDate:yyyy-MM-dd}, Återlämningsdatum: {returnDate}");
            }
        }
        // Rapport över försenade lån (över 30 dagar) - Ska fortfarande testas om den fungerar
        public static void OverdueLoans()
        {
            using var context = new LoanContext();
            var overdueLoans = context.Loans
                .Where(l => l.ReturnDate == null && l.LoanDate <= DateTime.Now.AddDays(-30))
                .Include(l => l.User)
                .Include(l => l.Book)
                .ToList();
            Console.WriteLine("\n\t\tFörsenade lån (över 30 dagar):");
            foreach (var loan in overdueLoans)
            {
                Console.WriteLine($"\t\tAnvändare: {loan.User?.Username}, Boktitel: {loan.Book?.Title}, Lånedatum: {loan.LoanDate:yyyy-MM-dd}");
            }
        }
        // Rapport över alla utlånade böcker - Ska fortfarande testas om den fungerar
        public static void LoanedBooksReport()
        {
            using var context = new LoanContext();
            var loanedBooks = context.Loans
                .Where(l => l.ReturnDate == null)
                .Include(l => l.Book)
                .Include(l => l.User)
                .ToList();
            Console.WriteLine("\n\t\tUtlånade böcker:");
            foreach (var loan in loanedBooks)
            {
                Console.WriteLine($"\t\tBoktitel: {loan.Book?.Title}, Lånad av: {loan.User?.Username}, Lånedatum: {loan.LoanDate:yyyy-MM-dd}");
            }
        }
        // Fråga användaren om de vill låna en bok från en visad lista.
        public static void LoanQuestion(int userId)
        {
            Console.WriteLine("Vill du låna någon av ovanstående böcker?");
            Console.Write("Ja/Nej: ");
            var loanChoice = Console.ReadLine()!.ToUpper();
            if (loanChoice == "JA")
            {
                Console.WriteLine("Skriv in ISBN nummer på boken du vill låna:");
                Console.Write("13 siffror: ");
                var inputISBN = long.Parse(Console.ReadLine()!);
                if (inputISBN.ToString().Length != 13)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\t\tISBN Måste bestå av 13 siffror!");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Loan.LoanBook(userId, inputISBN);
                }
            }
        }
    }
    
}
