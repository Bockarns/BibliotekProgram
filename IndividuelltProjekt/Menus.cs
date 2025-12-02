using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndividuelltProjekt.Models;
using IndividuelltProjekt.Data;

namespace IndividuelltProjekt
{
    static class Menus
    {
        //Försökte göra enums för menyvalen men fick inte riktigt till det som jag ville.
        //public enum MainMenuOptions
        //{
        //    CreateAccount = 1,
        //    Login = 2,
        //    ReturnToMainMenu = 3,
        //    Exit = 0
        //}

        //public enum AdminMenuOptions
        //{
        //    AddBook = 1,
        //    EditBook = 2,
        //    RemoveBook = 3,
        //    ListAllBooks = 4,
        //    EditProfile = 5,
        //    Logout = 9,
        //    Exit = 0
        //}

        //public enum UserMenuOptions
        //{
        //    SearchBook = 1,
        //    BorrowBook = 2,
        //    ReturnBook = 3,
        //    ListBorrowedBooks = 4,
        //    EditProfile = 5,
        //    Logout = 9,
        //    Exit = 0
        //}

        //Mainmeny
        public static void MainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t*********************************************************************************");
            Console.WriteLine("\t*\t\t\t\t\t\t\t\t\t\t*");
            Console.Write($"\t*\t\t\t***");
            Console.ResetColor();
            Console.Write(" Välkommen till biblioteket! ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("***\t\t\t*\n");
            Console.WriteLine("\t*\t\t\t\t\t\t\t\t\t\t*");
            Console.WriteLine("\t*********************************************************************************\n");
            Console.ResetColor();
            Console.WriteLine("\t\tVälj ett av följande alternativ:");
            Console.WriteLine("\t\t1. Skapa nytt konto");
            Console.WriteLine("\t\t2. Logga in");
            Console.WriteLine("\t\t0. Avsluta");
            Console.Write("\t\tSkriv in ditt val här: ");
        }
        //Adminmeny efter inloggning
        public static void AdminMainMenu(string adminUserName)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"\n\t\t\t\t***");
            Console.ResetColor();
            Console.Write($" Välkommen Admin {adminUserName} ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("***\t\t\t\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t*********************************************************************************");
            Console.WriteLine("\t*\t\t\t\t\t\t\t\t\t\t*");
            Console.Write("\t*\t");
            Console.ResetColor();
            Console.Write("Här i adminmenyn nedan kan du lägga till/redigera eller ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\t\t*");
            Console.Write("\n\t*\t");
            Console.ResetColor();
            Console.Write("ta bort böcker men även söka efter böcker och lista alla böcker");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\t\t*");
            Console.Write("\n\t*\t");
            Console.ResetColor();
            Console.Write("Välj nedan vad du vill göra!");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\t\t\t\t\t\t*\n");
            Console.Write("\t*\t");
            Console.ResetColor();
            Console.Write("OBS! Glöm ej att logga ut när du är klar för att skydda ditt konto.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\t*\n");
            Console.WriteLine("\t*\t\t\t\t\t\t\t\t\t\t*");
            Console.WriteLine("\t*********************************************************************************\n");
            Console.ResetColor();
            Console.WriteLine("\t\t1.Lägg till ny bok");
            Console.WriteLine("\t\t2.Redigera befintlig bok");
            Console.WriteLine("\t\t3.Ta bort bok");
            Console.WriteLine("\t\t4.Lista alla böcker");
            Console.WriteLine("\t\t5.Redigera profil");
            Console.WriteLine("\t\t9.Logga ut och återgå till huvudmenyn");
            Console.WriteLine("\t\t0.Avsluta");
            Console.Write("\t\tSkriv in ditt val här: ");
        }
        //Användaremeny efter inloggning
        public static void UserMainMenu(string userName)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"\n\t\t\t\t***");
            Console.ResetColor();
            Console.Write($" Välkommen {userName} ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("***\t\t\t\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t*********************************************************************************");
            Console.WriteLine("\t*\t\t\t\t\t\t\t\t\t\t*");
            Console.Write("\t*\t");
            Console.ResetColor();
            Console.Write("Här i menyn nedan kan du söka efter böcker, låna böcker,");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\t\t*");
            Console.Write("\n\t*\t");
            Console.ResetColor();
            Console.Write("lämna tillbaka lånade böcker men också se alla dina lånade böcker.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\t*");
            Console.Write("\n\t*\t");
            Console.ResetColor();
            Console.Write("Välj nedan vad du vill göra!");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\t\t\t\t\t\t*\n");
            Console.Write("\t*\t");
            Console.ResetColor();
            Console.Write("OBS! Glöm ej att logga ut när du är klar för att skydda ditt konto.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\t*\n");
            Console.WriteLine("\t*\t\t\t\t\t\t\t\t\t\t*");
            Console.WriteLine("\t*********************************************************************************\n");
            Console.ResetColor();
            Console.WriteLine("\t\t1.Sök bok");
            Console.WriteLine("\t\t2.Låna bok");
            Console.WriteLine("\t\t3.Lämna tillbaka bok");
            Console.WriteLine("\t\t4.Lista alla dina lånade böcker");
            Console.WriteLine("\t\t5.Redigera profil");
            Console.WriteLine("\t\t9.Logga ut och återgå till huvudmenyn");
            Console.WriteLine("\t\t0.Avsluta");
            Console.Write("\t\tSkriv in ditt val här: ");
        }
        //Användaremeny för sökning av bok
        public static void UserSearchMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t*********************************************************************************");
            Console.WriteLine("\t*\t\t\t\t\t\t\t\t\t\t*");
            Console.Write($"\t*\t***");
            Console.ResetColor();
            Console.Write(" För att söka efter en särskild bok eller författare");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" ***\t\t*\n");
            Console.Write("\t*\t");
            Console.ResetColor();
            Console.Write("\t\tvälj nedan vad du vill söka efter:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\t\t\t*\n");
            Console.WriteLine("\t*\t\t\t\t\t\t\t\t\t\t*");
            Console.WriteLine("\t*********************************************************************************\n");
            Console.ResetColor();
            Console.WriteLine("\t\t1. Sök via ISBN nummer:");
            Console.WriteLine("\t\t2. Sök via titel på bok:");
            Console.WriteLine("\t\t3. Sök via författare:");
            Console.WriteLine("\t\t4. Sök via nyckelord i titel eller författare: ");
            Console.WriteLine("\t\t5. Visa alla tillgängliga böcker: ");
            Console.WriteLine("\t\t6. Visa alla böcker: ");
            Console.WriteLine("\t\t9. återgå till föregående sida");
            Console.WriteLine("\t\t0. Logga ut och avsluta");
            Console.Write("\t\tSkriv in ditt val här: ");
        }
        //Redigera profil meny
        public static void EditProfile()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t*********************************************************************************");
            Console.WriteLine("\t*\t\t\t\t\t\t\t\t\t\t*");
            Console.Write($"\t*\t\t\t\t***");
            Console.ResetColor();
            Console.Write(" Redigera din profil ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("***\t\t\t*\n");
            Console.WriteLine("\t*\t\t\t\t\t\t\t\t\t\t*");
            Console.WriteLine("\t*********************************************************************************\n");
            Console.ResetColor();
            Console.WriteLine("\t\tVälj ett av följande alternativ:");
            Console.WriteLine("\t\t1. Byt användarnamn");
            Console.WriteLine("\t\t2. Byt lösenord");
            Console.WriteLine("\t\t3. Radera konto");
            Console.WriteLine("\t\t9. Gå tillbaka till föregående meny");
            Console.WriteLine("\t\t0. Avsluta");
            Console.Write("\t\tSkriv in ditt val här: ");
        }
        //Redigera bokmeny
        public static void EditBook()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t*********************************************************************************");
            Console.WriteLine("\t*\t\t\t\t\t\t\t\t\t\t*");
            Console.Write($"\t*\t\t\t\t***");
            Console.ResetColor();
            Console.Write(" Redigera böcker ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("***\t\t\t\t*\n");
            Console.WriteLine("\t*\t\t\t\t\t\t\t\t\t\t*");
            Console.WriteLine("\t*********************************************************************************\n");
            Console.ResetColor();
            Console.WriteLine("\t\tVad vill du uppdatera?");
            Console.WriteLine("\t\t1. Författare ");
            Console.WriteLine("\t\t2. Titel ");
            Console.WriteLine("\t\t3. Tillgänglighet ");
            Console.WriteLine("\t\t9. Återgå till föregående meny ");
            Console.WriteLine("\t\t0. Avsluta programmet "); ;
            Console.Write("\t\tSkriv in ditt val här: ");
        }
        //Visar bokinfo för tillgängliga böcker
        public static void DisplayAvailableBookInfo(Book book)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"\n***|");
            Console.ResetColor();
            Console.WriteLine($" Titel: {book.Title}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"***|");
            Console.ResetColor();
            Console.WriteLine($" Författare: {book.Author}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"***|");
            Console.ResetColor();
            Console.WriteLine($" ISBN: {book.Id}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"***|");
            Console.ResetColor();
            Console.WriteLine($" Finns tillgänglig för lån! ");
        }
        //Visar bokinfo för otillgängliga böcker
        public static void DisplayUnavailableBookInfo(Book book)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"\n***|");
            Console.ResetColor();
            Console.WriteLine($" Titel: {book.Title}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"***|");
            Console.ResetColor();
            Console.WriteLine($" Författare: {book.Author}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"***|");
            Console.ResetColor();
            Console.WriteLine($" ISBN: {book.Id}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"***|");
            Console.ResetColor();
            Console.WriteLine($" För tillfället utlånad! ");
        }
        public static void DisplayBooksWithoutAvailability(Book book)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"\n***|");
            Console.ResetColor();
            Console.WriteLine($" Titel: {book.Title}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"***|");
            Console.ResetColor();
            Console.WriteLine($" Författare: {book.Author}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"***|");
            Console.ResetColor();
            Console.WriteLine($" ISBN: {book.Id}");
        }
    } 
}
