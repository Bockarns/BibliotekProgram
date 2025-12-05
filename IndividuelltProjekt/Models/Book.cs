using IndividuelltProjekt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IndividuelltProjekt.Models
{
    public class Book
    {
        public long Id { get; set; }
        public string? Author { get; set; }
        public string? Title { get; set; }
        public bool Available { get; set; }
        public bool OnLoan { get; set; }
        public List<Loan> Loans { get; set; } = new();
        public Book()
        {
            if (Author != null)
                Author = "";
            if (Title != null)
                Title = "";
        }

        //Metoder för CRUD operationer

        //Lägger till en bok
        public static void AddBook(long isbn, string author, string title, bool available)
        {
            using (var context = new BookContext())
            {
                var book = new Book { Id = isbn, Author = author, Title = title, Available = available };
                context.Books.Add(book);
                context.SaveChanges();
            }
        }
        //Lista alla tillgängliga böcker
        public static void ListAllAvailableBooks()
        {
            using (var context = new BookContext())
            {
                //skapa en lista och lägg böckerna för att sedan läsa ut dom en efter en..
                var availableBooks = context.Books.Where(b => b.Available && b.OnLoan == false).ToList();
                foreach (var book in availableBooks)
                    Menus.DisplayAvailableBookInfo(book); //Hämtar printout från Menus.cs
            }
        }
        //Lista alla böcker
        public static void ListAllBooksAdmin()
        {
            using (var context = new BookContext())
            {
                //skapa en lista och lägg böckerna för att sedan läsa ut dom en efter en..
                var allBooks = context.Books.ToList();
                foreach (var book in allBooks)
                {
                    if (book.Available == true && book.OnLoan == false)
                    {
                        Menus.DisplayAvailableBookInfo(book); //Hämtar printout från Menus.cs
                    }
                    else if (book.Available == false)
                    {
                        Menus.DisplayUnavailableBookInfo(book);
                    }
                    else if (book.OnLoan == true)
                    {
                        Menus.DisplayOnLoanBookInfo(book); //Hämtar printout från Menus.cs
                    }
                }
            }
        }
        public static void ListAllBooks(int userid)
        {
            using (var context = new BookContext())
            {
                //skapa en lista och lägg böckerna för att sedan läsa ut dom en efter en..
                var allBooks = context.Books.ToList();
                if (allBooks.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\t\tInga böcker hittades i databasen.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n\t\tTryck på valfri tangent för att återgå till menyn...");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
                else
                {
                    foreach (var book in allBooks)
                    {
                        if (book.Available == true && book.OnLoan == false)
                        {
                            Menus.DisplayAvailableBookInfo(book); //Hämtar printout från Menus.cs
                        }
                        else if (book.Available == false)
                        {
                            Menus.DisplayUnavailableBookInfo(book);
                        }
                        else if (book.OnLoan == true)
                        {
                            Menus.DisplayOnLoanBookInfo(book); //Hämtar printout från Menus.cs
                        }
                    }
                    Loan.LoanQuestion(userid);
                }

            }
        }

        //Kontrollera tillgänglighet på en bock
        public static bool CheckBookAvaliable(bool available)
        {
            using (var context = new BookContext())
            {
                return context.Books.Any(b => b.Available == available);
            }
        }
        //Kontrollera om bok är på lån
        public static bool CheckBookOnLoan(bool onloan)
        {
            using (var context = new BookContext())
            {
                return context.Books.Any(b => b.OnLoan == onloan);
            }
        }
        //Ska bygga om dessa nedan så dom visar böckerna här ifrån som void metoder för att städa program.cs

        //Hämtar bok via författare
        public static Book GetBookByAuthor(string author)
        {
            using (var context = new BookContext())
            {
                return context.Books.FirstOrDefault(b => b.Author == author)!;
            }
        }
        //Listar samtliga böcker från en författare
        public static void SearchBookByAuthorSorted(string author, int userid)
        {
            using (var context = new BookContext())
            {
                var allBooksByAuthor = context.Books.Where(b => b.Author!.Contains(author)).OrderBy(b => b.Author).ToList();
                if (allBooksByAuthor.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\t\tInga böcker hittades för författaren du sökte efter.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n\t\tTryck på valfri tangent för att återgå till menyn...");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
                else
                {
                    foreach (var book in allBooksByAuthor)
                    {
                        if (book.Available == true && book.OnLoan == false)
                        {
                            Menus.DisplayAvailableBookInfo(book); //Hämtar printout från Menus.cs
                        }
                        else if (book.Available == false)
                        {
                            Menus.DisplayUnavailableBookInfo(book);
                        }
                        else if (book.OnLoan == true)
                        {
                            Menus.DisplayOnLoanBookInfo(book); //Hämtar printout från Menus.cs
                        }
                    }
                    Loan.LoanQuestion(userid);
                }
            }
        }
        //Lista alla böcker sorterat på författare
        public static void ListAllBooksSortedAuthor(int userid)
        {
            using (var context = new BookContext())
            {
                var allBooks = context.Books.OrderBy(b => b.Author).ToList();
                if (allBooks.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\t\tInga böcker hittades för författaren du sökte efter.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n\t\tTryck på valfri tangent för att återgå till menyn...");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
                else
                {
                    foreach (var book in allBooks)
                    {
                        if (book.Available == true && book.OnLoan == false)
                        {
                            Menus.DisplayAvailableBookInfo(book);
                        }
                        else if (book.Available == false)
                        {
                            Menus.DisplayUnavailableBookInfo(book);
                        }
                        else if (book.OnLoan == true)
                        {
                            Menus.DisplayOnLoanBookInfo(book); //Hämtar printout från Menus.cs
                        }
                    }
                    Loan.LoanQuestion(userid);
                }
            }
        }
        //Hämtar bok via titel
        public static Book GetBookByTitle(string title)
        {
            using (var context = new BookContext())
            {
                return context.Books.FirstOrDefault(b => b.Title == title)!;
            }
        }
        //Listar böckwr via titel
        public static void SearchBookByTitleSorted(string title, int userid)
        {
            using (var context = new BookContext())
            {
                var books = context.Books.Where(b => b.Title!.Contains(title)).OrderBy(b => b.Title).ToList();
                if (books.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\t\tInga böcker hittades med den titeln du sökte efter.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n\t\tTryck på valfri tangent för att återgå till menyn...");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
                else
                {
                    foreach (var book in books)
                    {
                        if (book.Available == true && book.OnLoan == false)
                        {
                            Menus.DisplayAvailableBookInfo(book); //Hämtar printout från Menus.cs
                        }
                        else if (book.Available == false)
                        {
                            Menus.DisplayUnavailableBookInfo(book); //Hämtar printout från Menus.cs
                        }
                        else if (book.OnLoan == true)
                        {
                            Menus.DisplayOnLoanBookInfo(book); //Hämtar printout från Menus.cs
                        }
                        Loan.LoanQuestion(userid);
                    }
                }
            }
        }
        //Lista alla böcker sorterat på titel
        public static void ListAllBooksSortedTitle(int userid)
        {
            using (var context = new BookContext())
            {
                var allBooks = context.Books.OrderBy(b => b.Title).ToList();
                if (allBooks.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\t\tInga böcker hittades med den titeln du sökte efter.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n\t\tTryck på valfri tangent för att återgå till menyn...");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
                else
                {
                    foreach (var book in allBooks)
                    {
                        if (book.Available == true && book.OnLoan == false)
                        {
                            Menus.DisplayAvailableBookInfo(book);
                        }
                        else if (book.Available == false)
                        {
                            Menus.DisplayUnavailableBookInfo(book);
                        }
                        else if (book.OnLoan == true)
                        {
                            Menus.DisplayOnLoanBookInfo(book); //Hämtar printout från Menus.cs
                        }
                    }
                    Loan.LoanQuestion(userid);
                }
            }
        }
        //Hämtar bok via författare och titel
        public static Book GetBookByAuthorAndTitle(string author, string title)
        {
            using (var context = new BookContext())
            {
                var book = context.Books.FirstOrDefault(b => b.Author == author && b.Title == title);
                return book!;
            }
        }
        //För att söka efter en bok med ett nyckelord/sträng i titel eller författare
        public static void SearchBooksByKeyword(string keyword, int userid)
        {
            using (var context = new BookContext())
            {
                var booksWithKeyword = context.Books
                    .Where(b => b.Title!.Contains(keyword) || b.Author!.Contains(keyword))
                    .ToList();
                if (booksWithKeyword.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\t\tInga böcker hittades med det nyckelordet/strängen.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n\t\tTryck på valfri tangent för att återgå till menyn...");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
                else
                {
                    foreach (var book in booksWithKeyword)
                    {
                        if (book.Available == true && book.OnLoan == false)
                        {
                            Menus.DisplayAvailableBookInfo(book); //Hämtar printout från Menus.cs
                        }
                        else if (book.Available == false)
                        {
                            Menus.DisplayUnavailableBookInfo(book);
                        }
                        else if (book.OnLoan == true)
                        {
                            Menus.DisplayOnLoanBookInfo(book); //Hämtar printout från Menus.cs
                        }
                    }
                    Loan.LoanQuestion(userid);
                }
            }
        }
        public static void SearchBooksByKeywordSortedAuthor(string keyword, int userid)
        {
            using (var context = new BookContext())
            {
                var booksWithKeyword = context.Books
                    .Where(b => b.Title!.Contains(keyword) || b.Author!.Contains(keyword))
                    .OrderBy(b => b.Author).ToList();
                if (booksWithKeyword.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\t\tInga böcker hittades med det nyckelordet/strängen.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n\t\tTryck på valfri tangent för att återgå till menyn...");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
                else
                {
                    foreach (var book in booksWithKeyword)
                    {
                        if (book.Available == true && book.OnLoan == false)
                        {
                            Menus.DisplayAvailableBookInfo(book);
                        }
                        else if (book.Available == false)
                        {
                            Menus.DisplayUnavailableBookInfo(book);
                        }
                        else if (book.OnLoan == true)
                        {
                            Menus.DisplayOnLoanBookInfo(book); //Hämtar printout från Menus.cs
                        }
                    }
                    Loan.LoanQuestion(userid);
                }
            }
        }
        public static void SearchBooksByKeywordSortedTitle(string keyword, int userid)
        {
            using (var context = new BookContext())
            {
                var booksWithKeyword = context.Books
                    .Where(b => b.Title!.Contains(keyword) || b.Author!.Contains(keyword))
                    .OrderBy(b => b.Title).ToList();
                if (booksWithKeyword.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\t\tInga böcker hittades med det nyckelordet/strängen.");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n\t\tTryck på valfri tangent för att återgå till menyn...");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
                else
                {
                    foreach (var book in booksWithKeyword)
                    {
                        if (book.Available == true && book.OnLoan == false)
                        {
                            Menus.DisplayAvailableBookInfo(book);
                        }
                        else if (book.Available == false)
                        {
                            Menus.DisplayUnavailableBookInfo(book);
                        }
                        else if (book.OnLoan == true)
                        {
                            Menus.DisplayOnLoanBookInfo(book); //Hämtar printout från Menus.cs
                        }
                    }
                    Loan.LoanQuestion(userid);
                }
            }
        }
        //Hämtar bok via ISBN
        public static Book GetBookByISBN(long isbn)
        {
            using (var context = new BookContext())
            {
                return context.Books.FirstOrDefault(b => b.Id == isbn)!;
            }
        }
        //Fråga efter ISBN
        public static long AskForISBN()
        {
            Console.Write("\t\t13 siffror: ");
            var inputISBN = Console.ReadLine()!;
            if (inputISBN.Length != 13)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\t\tISBN Måste bestå av 13 siffror!");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n\t\tTryck på valfri tangent för att återgå till menyn...");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
                return 0;
            }
            else if (inputISBN.Length == 13)
            {
                if (!long.TryParse(inputISBN, out _))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\t\tISBN får endast innehålla siffror!");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n\t\tTryck på valfri tangent för att återgå till menyn...");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                    return 0;
                }
                else
                {
                    long parsedISBN = long.Parse(inputISBN);
                    return parsedISBN;
                }
            }
            else
            {
                return 0;
            }
        }
        //Uppdatera författare information
        public static Book UpdateAuthor(string author, string updateAuthor)
        {
            using (var context = new BookContext())
            {
                var book = context.Books.FirstOrDefault(b => b.Author == author);
                if (book == null)
                    return null!;
                book.Author = updateAuthor;
                context.SaveChanges();
                return book;
            }
        }
        //Uppdatera titel information
        public static Book UpdateTitle(string title, string updatetitle)
        {
            using (var context = new BookContext())
            {
                var book = context.Books.FirstOrDefault(b => b.Title == title);
                if (book == null)
                    return null!;
                book.Title = title;
                context.SaveChanges();
                return book;
            }
        }
        //Uppdatera tillgänglighet
        public static Book UpdateAvailable(long isbn, bool checkout)
        {
            using (var context = new BookContext())
            {
                var book = context.Books.FirstOrDefault(b => b.Id == isbn);
                //var isAvailable = context.Books.FirstOrDefault(b => b.Available == checkout);
                if (book!.Available == true)
                {
                    book.Available = false;
                }
                else
                    book.Available = true;
                context.SaveChanges();
                return book;
            }
        }
        //Ta bort en bok
        public static void DeleteBook(long isbn)
        {
            using (var context = new BookContext())
            {
                var book = context.Books.Include(b => b.Loans).FirstOrDefault(b => b.Id == isbn);
                context.RemoveRange(book!.Loans); //Manuellt borttagning av relaterade lån
                context.Books.Remove(book!);
                context.SaveChanges();
            }
        }
    }             
}
