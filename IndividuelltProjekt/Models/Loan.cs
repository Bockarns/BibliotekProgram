using IndividuelltProjekt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndividuelltProjekt.Models
{
    public class Loan
    {
        public int Id { get; set; }
        [ForeignKey("User")] //tillägg för att ange foreign key relation då User_Id refererar till User tabellen
        public int User_Id { get; set; }
        public User? User { get; set; }
        [ForeignKey("Book")] //tillägg för att ange foreign key relation då Book_Id refererar till Book tabellen
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
            {
                throw new Exception("\n\t\tBoken finns inte!");
            }
                
            if (!book.Available)
            {
                throw new Exception("\n\t\tBoken är redan utlånad!");
            }
                
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
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\t\tTryck på valfri tangent för att återgå till menyn...");
            Console.ResetColor();
            Console.ReadKey();
        }
        public static void ReturnBook(long bookId)
        {
            using var context = new LoanContext();
            var loan = context.Loans
                .FirstOrDefault(l => l.Book_Id == bookId && l.ReturnDate == null);
            if (loan == null)
            {
                throw new Exception("\t\tDenna bok är inte utlånad!");
            }
                
            var book = context.Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
            {
                throw new Exception("\t\tBoken saknas i biblioteket!");
            }
                
            loan.ReturnDate = DateTime.Now;  // Registrera retur
            book.Available = true;           // Markera som ledig igen

            context.SaveChanges();

            Console.WriteLine("\n\t\tBoken har lämnats tillbaka!");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\t\tTryck på valfri tangent för att fortsätta...");
            Console.ResetColor();
            Console.ReadKey();
        }

        // Lånehistorik för en användare
        public static void LoanHistory(int userId)
        {
            using var context = new LoanContext();
            var loans = context.Loans.Where(l => l.User_Id == userId).Include(l => l.Book).ToList();
            Console.WriteLine("\n\t\tLånehistorik:");
            if (loans.Count == 0)
            {
                Console.WriteLine("\t\tInga lån hittades för denna användare.");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n\t\tTryck på valfri tangent för att återgå till menyn...");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }
            else
            {
                foreach (var loan in loans)
                {
                    string returnDate = loan.ReturnDate.HasValue ? loan.ReturnDate.Value.ToString("yyyy-MM-dd") : "Ej återlämnad";
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"\n***|");
                    Console.ResetColor();
                    Console.WriteLine($" Titel: {loan.Book?.Title}");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"***|");
                    Console.ResetColor();
                    Console.WriteLine($" Författare: {loan.Book?.Author}");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"***|");
                    Console.ResetColor();
                    Console.WriteLine($" Lånedatum: {loan.LoanDate:yyyy-MM-dd}");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"***|");
                    Console.ResetColor();
                    Console.WriteLine($" Återlämningsdatum: {returnDate}");
                }
            }
                
        }
        // Rapport över försenade lån (över 30 dagar) - Ska fortfarande testas om den fungerar
        public static void OverdueLoans()
        {
            using var context = new LoanContext();
            var overdueLoans = context.Loans.Where(l => l.ReturnDate == null && l.LoanDate <= DateTime.Now.AddDays(-30)).Include(l => l.User).Include(l => l.Book).ToList();
            Console.WriteLine("\n\t\tFörsenade lån (över 30 dagar):");
            foreach (var loan in overdueLoans)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\n***|");
                Console.ResetColor();
                Console.WriteLine($" Titel: {loan.Book?.Title}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"***|");
                Console.ResetColor();
                Console.WriteLine($" Lånad av: {loan.User?.Username}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"***|");
                Console.ResetColor();
                Console.WriteLine($" Lånedatum: {loan.LoanDate:yyyy-MM-dd}");
                Console.ResetColor();
            }
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\t\tTryck på valfri tangent för att återgå till menyn...");
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("\x1b[3J");
        }
        // Rapport över alla utlånade böcker - Ska fortfarande testas om den fungerar
        public static void LoanedBooksReport()
        {
            using var context = new LoanContext();
            var loanedBooks = context.Loans.Where(l => l.ReturnDate == null).Include(l => l.Book).Include(l => l.User).ToList();
            Console.WriteLine("\n\t\tUtlånade böcker:");
            foreach (var loan in loanedBooks)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\n***|");
                Console.ResetColor();
                Console.WriteLine($" Titel: {loan.Book?.Title}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"***|");
                Console.ResetColor();
                Console.WriteLine($" Lånad av: {loan.User?.Username}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"***|");
                Console.ResetColor();
                Console.WriteLine($" Lånedatum: {loan.LoanDate:yyyy-MM-dd}");
                Console.ResetColor();
            }
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\t\tTryck på valfri tangent för att återgå till menyn...");
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("\x1b[3J");
        }
        // Fråga användaren om de vill låna en bok från en visad lista.
        public static void LoanQuestion(int userId)
        {
            Console.WriteLine("\nVill du låna någon av ovanstående böcker?");
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
                }
                else
                {
                    Loan.LoanBook(userId, inputISBN);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n\t\tTryck på valfri tangent för att återgå till menyn...");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("\x1b[3J");
            }
                
        }
        
        public static void ReturnQuestion()
        {
            Console.WriteLine("\n\t\tVill du lämna tillbaka någon av ovanstående böcker?");
            Console.Write("\t\tJa/Nej: ");
            var returnChoice = Console.ReadLine()!.ToUpper();
            if (returnChoice == "JA")
            {
                Console.WriteLine("\n\t\tSkriv in ISBN nummer på boken du vill lämna tillbaka:");
                Console.Write("\t\t13 siffror: ");
                var inputISBN = Console.ReadLine();
                if (inputISBN!.Length != 13)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n\t\tISBN Måste bestå av 13 siffror!");
                    Console.ResetColor();
                }
                else
                {
                    long parsedISBN = long.Parse(inputISBN);
                    Loan.ReturnBook(parsedISBN);
                    Loan.ReturnQuestion();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n\t\tTryck på valfri tangent för att återgå till menyn...");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("\x1b[3J");
            }
        }
    }
}
