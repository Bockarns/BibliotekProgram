
using IndividuelltProjekt;
using IndividuelltProjekt.Data;
using IndividuelltProjekt.Models;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices;

//Jag har valt att lägga menyerna i en egen klass som void, för annars blev koden så plottrig med alla extra rader.

Console.Title = "JGB Bibliotek";

////Rensa konsolen och scrollback buffern ska klistras in där det behövs
//Console.Clear();
//Console.WriteLine("\x1b[3J");


//Jag har kvitterar null varningar med ? eller ! för att lättare få överblick
//när jag stöter på ett en varning som kan vara problimatisk.

//Mainloops bool för att programmet ska vara igång tills användaren stänger av.
bool running = true;

//Programstart
while (running)
{
//Bool för nestlade whilelooper
bool insidemenurunning = true;
bool insidemenu2running = true;

//Kalla på mainmenu printout
    Menus.MainMenu();
    var Choice = Console.ReadLine();
    switch (Choice)
    {
        case "1":
            try
            {
                Console.WriteLine("\n\t\tSkapa nytt konto\n");
                Console.WriteLine("\t\tFyll i önskat användarnamn: ");
                Console.Write("\t\t");
                var newUsername = Console.ReadLine();
                Console.WriteLine("\t\tFyll i önskat lösenord: ");
                Console.Write("\t\t");
                var newPassword = Console.ReadLine();
                Console.WriteLine("\t\tUpprepa lösenordet: ");
                Console.Write("\t\t");
                var newPassword2 = Console.ReadLine();
                if (newPassword != newPassword2)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n\t\tLösenorden matchar inte...");
                    Console.ResetColor();
                    Console.ReadKey();  
                    Console.Clear();
                    break;
                }
                Console.WriteLine("\n\t\tSka användaren vara Admin?");
                Console.Write("\t\tJa/Nej: ");
                var admincheck = Console.ReadLine()!.ToUpper();
                if (admincheck == "JA")
                {
                    //Kontroll om användarnamnet redan finns
                    bool checker = User.CheckUser(newUsername!);
                    bool isAdmin = true;
                    if (checker == false)
                    {
                        //Lägg till användare som admin
                        User.AddUser(newUsername!, newPassword!, isAdmin);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\n\t\tKonto skapat för användare: {newUsername}");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("\n\t\tTryck på valfri tangent för att återgå till menyn...");
                        Console.ResetColor();
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"\n\t\tAnvändarnamnet {newUsername} används redan");
                        Console.ResetColor();
                        Console.ReadKey(); 
                        Console.Clear();
                    }  
                }
                else if (admincheck == "NEJ")
                {
                    //Kontrollera att användarnamnet inte finns redan registrerat
                    bool checker = User.CheckUser(newUsername!);
                    bool isNotAdmin = false;
                    if (checker == false)
                    {
                        //Lägg till användare
                        User.AddUser(newUsername!, newPassword!, isNotAdmin);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\n\t\tKonto skapat för användare: {newUsername}");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("\n\t\tTryck på valfri tangent för att återgå till menyn...");
                        Console.ResetColor();
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"\n\t\tAnvändarnamnet {newUsername} används redan");
                        Console.ResetColor();
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }
            catch (Exception MyExcep)
            {
                Console.WriteLine(MyExcep.ToString());
            }
            break;
        case "2":
            //Logga in, denna kollar om användaren existerar i DB och om den är admin eller vanlig användare
            Console.WriteLine("\n\t\tLogga in användare\n");
            Console.WriteLine("\t\tFyll i ditt användarnamn: ");
            Console.Write("\t\t");
            var existingUsername = Console.ReadLine();
            Console.WriteLine("\n\t\tFyll i ditt lösenord: ");
            Console.Write("\t\t");
            var existingPassword = Console.ReadLine();
            var user = User.GetUser(existingUsername!);
            if (user == null)
            {
                Console.ForegroundColor = ConsoleColor. DarkYellow;
                Console.WriteLine("\n\t\tAnvändaren finns ej, Skapa ny användare först!");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
                break;
            }            
            var adminCheck = User.CheckUserAdmin(existingUsername!);
            if (adminCheck == true)
            {
                //Om användaren är admin ska denna del starta för admin menyer
                user = User.GetUser(existingUsername!);
                if (user != null && user.Password == existingPassword)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n\t\tInloggning lyckades för användare {existingUsername}");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                    insidemenurunning = true;
                    while (insidemenurunning)
                    {
                        Menus.AdminMainMenu(existingUsername!);
                        Choice = Console.ReadLine();
                        switch (Choice)
                        {
                            case "1":
                                //Lägg till bok
                                Console.WriteLine("\n\t\tSkriv in ISBN (13 siffror): ");
                                Console.Write("\t\t");
                                long newIsbn = long.Parse(Console.ReadLine()!);
                                if (newIsbn.ToString().Length != 13)
                                {
                                    Console.WriteLine("\n\t\tISBN Måste bestå av 13 siffror!");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("\n\t\tSkriv in författare (Separera för- och efternamn med mellanrum): ");
                                    Console.Write("\t\t");
                                    var newAuthor = Console.ReadLine();
                                    Console.WriteLine("\t\tSkriv in titel på boken: ");
                                    Console.Write("\t\t");
                                    var newTitle = Console.ReadLine();
                                    bool isAvaliable = true;
                                    Book.AddBook(newIsbn, newAuthor!, newTitle!, isAvaliable);
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("\n\t\tNy bok tillagd i biblioteket");
                                    Console.ResetColor();
                                    var book = Book.GetBookByISBN(newIsbn);
                                    if (book != null)
                                        Menus.DisplayBooksWithoutAvailability(book);
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                break;
                            case "2":
                                //Redigera böcker, ger användaren val att ändra författare, titel eller tillgänglighet
                                //samt hur dom vill söka upp boken (ISBN eller författare och titel)
                                Console.Clear();
                                Menus.EditBook();
                                Choice = Console.ReadLine();
                                switch (Choice)
                                {
                                    case "1":
                                        Console.WriteLine("\n\t\tVill du söka via ISBN eller via författare och titel?");
                                        Console.Write("\t\t1 = ISBN eller 2 = Författare och titel: ");
                                        var searchChoice = Console.ReadLine();
                                        if (searchChoice == "1")
                                        {
                                            Console.Write("\t\tSkriv in ISBN nummer (13 siffror): ");
                                            var ISBN = long.Parse(Console.ReadLine()!);
                                            if (ISBN.ToString().Length != 13)
                                            {
                                                Console.WriteLine("\t\tISBN Måste bestå av 13 siffror!");
                                                Console.ReadKey();
                                                Console.Clear();
                                            }
                                            else
                                            {
                                                var book = Book.GetBookByISBN(ISBN);
                                                Console.WriteLine("\n\n\t\tStämmer det att du vill ändra författare för denna bok?");
                                                Menus.DisplayBooksWithoutAvailability(book);
                                                Console.Write("\nJA/NEJ: ");
                                                var confirm = Console.ReadLine()!.ToUpper();
                                                if (confirm == "JA")
                                                {
                                                    Console.Write("\n\t\tSkriv in ny författare: ");
                                                    var newAuthor = Console.ReadLine();
                                                    Book.UpdateAuthor(book.Author!, newAuthor!);
                                                    book = Book.GetBookByISBN(ISBN);
                                                    Console.WriteLine("\n\t\tBoken har uppdaterat enligt nedan: \n");
                                                    Menus.DisplayBooksWithoutAvailability(book);
                                                    Console.ReadKey();
                                                    Console.Clear();
                                                }
                                            } 
                                            break;
                                        }
                                        else if (searchChoice == "2")
                                        {
                                            Console.WriteLine("\n\t\tSkriv in författarens förnamn och efternamn: ");
                                            var author = Console.ReadLine();
                                            Console.Write("\t\tSkriv in titel på boken: ");
                                            var title = Console.ReadLine();
                                            var book = Book.GetBookByAuthorAndTitle(author!, title!);
                                            if (book == null)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                Console.WriteLine("\n\t\tBoken hittades inte, kontrollera stavning och försök igen.");
                                                Console.ResetColor();
                                                Console.ReadKey();
                                                Console.Clear();
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("\n\t\tStämmer det att du vill ändra författare för denna bok?");
                                                Menus.DisplayBooksWithoutAvailability(book);
                                                Console.Write("\nJA/NEJ: ");
                                                var confirm = Console.ReadLine()!.ToUpper();
                                                if (confirm == "JA")
                                                {
                                                    Console.WriteLine("\n\t\tSkriv in ny författare:");
                                                    var newAuthor = Console.ReadLine();
                                                    Book.UpdateAuthor(book.Author!, newAuthor!);
                                                    book = Book.GetBookByAuthorAndTitle(author!, title!);
                                                    Console.WriteLine("\n\t\tBoken har uppdaterat enligt nedan: \n");
                                                    Menus.DisplayBooksWithoutAvailability(book);
                                                    Console.ReadKey();
                                                    Console.Clear();
                                                }
                                            } 
                                        }
                                        break;
                                    case "2":
                                        Console.WriteLine("\n\t\tVill du söka via ISBN eller via författare och titel?");
                                        Console.Write("\t\t1 = ISBN eller 2 = Författare och titel: ");
                                        searchChoice = Console.ReadLine();
                                        if (searchChoice == "1")
                                        {
                                            Console.WriteLine("\n\t\tSkriv in ISBN nummer (13 siffror):");
                                            var ISBN = long.Parse(Console.ReadLine()!);
                                            if (ISBN.ToString().Length != 13)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkYellow; 
                                                Console.WriteLine("\t\tISBN Måste bestå av 13 siffror!");
                                                Console.ResetColor();
                                                Console.ReadKey();
                                                Console.Clear();
                                            }
                                            else
                                            {
                                                var book = Book.GetBookByISBN(ISBN);
                                                Console.WriteLine("\n\t\tStämmer det att du vill ändra titel för denna bok?");
                                                Menus.DisplayBooksWithoutAvailability(book);
                                                Console.Write("\nJA/NEJ: ");
                                                var confirm = Console.ReadLine()!.ToUpper();
                                                if (confirm == "JA")
                                                {
                                                    Console.WriteLine("\n\t\tSkriv in ny titel:");
                                                    var newTitle = Console.ReadLine();
                                                    Book.UpdateTitle(book.Title!, newTitle!);
                                                    book = Book.GetBookByISBN(ISBN);
                                                    Console.WriteLine("\n\t\tBoken har uppdaterat enligt nedan: \n");
                                                    Menus.DisplayBooksWithoutAvailability(book);
                                                    Console.ReadKey();
                                                    Console.Clear();
                                                }
                                            }
                                            break;
                                        }
                                        else if (searchChoice == "2")
                                        {
                                            Console.WriteLine("\n\t\tSkriv in författarens förnamn och efternamn: ");
                                            var author = Console.ReadLine();
                                            Console.Write("S\t\tkriv in titel på boken: ");
                                            var title = Console.ReadLine();
                                            var book = Book.GetBookByAuthorAndTitle(author!, title!);
                                            if (book == null)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                Console.WriteLine("\n\t\tBoken hittades inte, kontrollera stavning och försök igen.");
                                                Console.ResetColor();
                                                Console.ReadKey();
                                                Console.Clear();
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("\n\t\tStämmer det att du vill ändra titel för denna bok?");
                                                Menus.DisplayBooksWithoutAvailability(book);
                                                Console.Write("\nJA/NEJ: ");
                                                var confirm = Console.ReadLine()!.ToUpper();
                                                if (confirm == "JA")
                                                {
                                                    Console.WriteLine("\t\tSkriv in ny titel:");
                                                    var newTitle = Console.ReadLine();
                                                    Book.UpdateTitle(book.Title!, newTitle!);
                                                    book = Book.GetBookByAuthorAndTitle(author!, title!);
                                                    Console.WriteLine("\n\t\tBoken har uppdaterat enligt nedan: \n");
                                                    Menus.DisplayBooksWithoutAvailability(book);
                                                    Console.ReadKey();
                                                    Console.Clear();
                                                }
                                            }
                                        }
                                        break;
                                    case "3":
                                        Console.WriteLine("\n\t\tVill du söka via ISBN eller via författare och titel?");
                                        Console.Write("\t\t1 = ISBN eller 2 = Författare och titel: ");
                                        searchChoice = Console.ReadLine();
                                        if (searchChoice == "1")
                                        {
                                            Console.WriteLine("\t\tSkriv in ISBN nummer (13 siffror):");
                                            var ISBN = long.Parse(Console.ReadLine()!);
                                            if (ISBN.ToString().Length != 13)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                Console.WriteLine("\t\tISBN Måste bestå av 13 siffror!");
                                                Console.ResetColor();
                                                Console.ReadKey();
                                                Console.Clear();
                                            }
                                            else
                                            {
                                                var book = Book.GetBookByISBN(ISBN);
                                                Console.WriteLine("Stämmer det att du vill ändra tillgängligheten för denna bok?");
                                                Menus.DisplayBooksWithoutAvailability(book);
                                                Console.WriteLine($"\n***| Tillgänglighet: {book.Available}: ");
                                                Console.Write("\nJA/NEJ: ");
                                                var confirm = Console.ReadLine()!.ToUpper();
                                                if (confirm == "JA")
                                                {
                                                    Book.UpdateAvailable(ISBN, book.Available);
                                                    book = Book.GetBookByISBN(ISBN);
                                                    Console.WriteLine("\n\t\tBoken har uppdaterat enligt nedan: \n");
                                                    Menus.DisplayBooksWithoutAvailability(book);
                                                    Console.WriteLine($"\nTillgänglighet har uppdaterats till: {book.Available}");
                                                    Console.ReadKey();
                                                    Console.Clear();
                                                }
                                            }
                                            break;
                                        }
                                        else if (searchChoice == "2")
                                        {
                                            Console.WriteLine("\n\t\tSkriv in författarens förnamn och efternamn: ");
                                            var author = Console.ReadLine();
                                            Console.Write("\t\tSkriv in titel på boken: ");
                                            var title = Console.ReadLine();
                                            var book = Book.GetBookByAuthorAndTitle(author!, title!);
                                            if (book == null)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                Console.WriteLine("\n\t\tBoken hittades inte, kontrollera stavning och försök igen.");
                                                Console.ResetColor();
                                                Console.ReadKey();
                                                Console.Clear();
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("\t\tStämmer det att du vill ändra tillgängligheten för denna bok?");
                                                Menus.DisplayBooksWithoutAvailability(book);
                                                Console.WriteLine($"\n***| Tillgänglighet: {book.Available}: ");
                                                Console.Write("\nJA/NEJ: ");
                                                var confirm = Console.ReadLine()!.ToUpper();
                                                if (confirm == "JA")
                                                {
                                                    Book.UpdateAvailable(book.Id, book.Available);
                                                    book = Book.GetBookByISBN(book.Id);
                                                    Console.WriteLine("\n\t\tBoken har uppdaterat enligt nedan: \n");
                                                    Menus.DisplayBooksWithoutAvailability(book);
                                                    Console.WriteLine($"\nTillgänglighet har uppdaterats till: {book.Available}");
                                                    Console.ReadKey();
                                                    Console.Clear();
                                                }
                                            }
                                        }
                                        break;
                                    case "9":
                                        Console.WriteLine("\n\t\tÅtergår till föregående meny");
                                        break;
                                    case "0":
                                        Console.WriteLine("\n\t\t\tVälkommen åter");
                                        insidemenu2running = false;
                                        insidemenurunning = false;
                                        running = false;
                                        Console.ReadKey();
                                        Console.Clear();
                                        break;
                                    default:
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        Console.WriteLine("\n\t\t\tFelaktigt val, Försök igen.");
                                        Console.ResetColor();
                                        Console.ReadKey();
                                        Console.Clear();
                                        break;
                                }
                                break;
                            case "3":
                                //radera en bok
                                Console.WriteLine("\n\t\tVill du söka up boken via ISBN eller via författare och titel?");
                                Console.Write("\t\t1 = ISBN eller 2 = Författare och titel ");
                                var deleteSearchChoice = Console.ReadLine();
                                if (deleteSearchChoice == "1")
                                {
                                    Console.WriteLine("\n\t\tSkriv in ISBN nummer (13 siffror):");
                                    var ISBN = long.Parse(Console.ReadLine()!);
                                    if (ISBN.ToString().Length != 13)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        Console.WriteLine("\t\tISBN Måste bestå av 13 siffror!");
                                        Console.ResetColor();
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        var book = Book.GetBookByISBN(ISBN);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\t\tStämmer det att du vill radera denna bok?");
                                        Menus.DisplayBooksWithoutAvailability(book);
                                        Console.ResetColor();
                                        Console.Write("\nJA/NEJ: ");
                                        var confirm = Console.ReadLine()!.ToUpper();
                                        if (confirm == "JA")
                                        {
                                            Book.DeleteBook(ISBN);
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("\n\t\tBoken är raderad från biblioteket.");
                                            Console.ResetColor();
                                            Console.ReadKey();
                                            Console.Clear();
                                        }
                                    }
                                    break;
                                }
                                else if (deleteSearchChoice == "2")
                                {
                                    Console.WriteLine("\n\t\tSkriv in författarens förnamn och efternamn: ");
                                    var author = Console.ReadLine();
                                    Console.Write("\t\tSkriv in titel på boken: ");
                                    var title = Console.ReadLine();
                                    var book = Book.GetBookByAuthorAndTitle(author!, title!);
                                    if (book == null)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        Console.WriteLine("\t\tBoken hittades inte, kontrollera stavning och försök igen.");
                                        Console.ResetColor();
                                        Console.ReadKey();
                                        Console.Clear();
                                        break;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\t\tStämmer det att du vill radera denna bok?");
                                        Console.ResetColor();
                                        Menus.DisplayBooksWithoutAvailability(book);
                                        Console.Write("\nJA/NEJ: ");
                                        var confirm = Console.ReadLine()!.ToUpper();
                                        if (confirm == "JA")
                                        {
                                            Book.DeleteBook(book.Id);
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("\t\tBoken är raderad från biblioteket.");
                                            Console.ResetColor();
                                            Console.ReadKey();
                                            Console.Clear();
                                        }
                                    }
                                }
                                break;
                            case "4":
                                Console.Clear();
                                //Listar alla böcker och visar om de är lånade eller ej
                                Console.WriteLine("Lista böcker");
                                Book.ListAllBooks();
                                break;
                            case "5":
                                Console.Clear();
                                Menus.AdminLoanMenu();
                                Choice = Console.ReadLine();
                                if (Choice == "1")
                                {
                                    //Rapport över försenade lån (över 30 dagar)
                                    Loan.OverdueLoans();
                                }
                                else if (Choice == "2")
                                {
                                    //Rapport över alla utlånade böcker
                                    Loan.LoanedBooksReport();
                                }
                                break;
                            case "6":
                                //Redigera profil
                                Console.Clear();
                                var editProfile = true;
                                while (editProfile)
                                {
                                    Menus.EditProfile();
                                    Choice = Console.ReadLine();
                                    if (Choice == "1")
                                    {
                                        //Byta användarnamn
                                        Console.WriteLine("Skriv in nytt önskat användarnamn: ");
                                        var newUsername = Console.ReadLine();
                                        //Kontroll om nya användarnamnet redan finns
                                        bool checker = User.CheckUser(newUsername!);
                                        if (checker == false)
                                        {
                                            //Byt namn om nya användarnamnet är ledigt.
                                            user = User.UpdateUsername(existingUsername!, newUsername!);
                                            Console.WriteLine($"Nytt användarnamn {newUsername} registrerat för {existingUsername}");
                                            Console.ReadKey();
                                            Console.Clear();
                                        }
                                        else
                                            Console.WriteLine($"Användarnamnet {newUsername} används redan");
                                        break;
                                    }
                                    else if (Choice == "2")
                                    {
                                        //Byt lösenord
                                        Console.WriteLine("Skriv in nya lösenordet:");
                                        var newPassword = Console.ReadLine();
                                        Console.WriteLine("Skriv in nya lösenordet igen:");
                                        var newPassword2 = Console.ReadLine();
                                        //Ber användaren upprepa lösenord för att säkerställa att dom skriver rätt
                                        if (newPassword == newPassword2)
                                        {
                                            user = User.UpdatePassword(existingUsername!, existingPassword!, newPassword!);
                                            Console.WriteLine($"Nytt lösenord registrerat för {existingUsername}");
                                            break;
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                                            Console.WriteLine("Lösenorden matchar inte! Försök igen.");
                                            Console.ResetColor();
                                            Console.ReadKey();
                                            Console.Clear();
                                            continue;
                                        }
                                    }
                                    else if (Choice == "3")
                                    {
                                        Console.WriteLine("Är du säker på att du vill radera användaren? JA/NEJ");
                                        var deletechoice = Console.ReadLine()!.ToUpper();
                                        if (deletechoice == "JA")
                                        {
                                            User.DeleteUser(existingUsername!);
                                            Console.WriteLine("Användaren är borttagen, Tråkigt att se dig lämna oss :´(");
                                            Console.WriteLine("Du blir automatiskt tagen till huvudmenyn.");
                                            Console.ReadKey();
                                            Console.Clear();
                                            editProfile = false;
                                            insidemenu2running = false;
                                            insidemenurunning = false;
                                            break;
                                        }
                                        else if (deletechoice == "NEJ")
                                        {
                                            Console.WriteLine("Vad glada vi blir att du valt att stanna hos oss <3");
                                            Console.ReadKey();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Felaktigt val!");
                                            Console.ReadKey();
                                        }
                                        break;
                                    }
                                    else if (Choice == "9")
                                    {
                                        editProfile = false;
                                        insidemenu2running = false;
                                        Console.Clear();
                                        break;
                                    }
                                    else if (Choice == "0")
                                    {
                                        Console.WriteLine("Välkommen åter");
                                        editProfile = false;
                                        insidemenu2running = false;
                                        insidemenurunning = false;
                                        running = false;
                                        Console.ReadKey();
                                        Console.Clear();
                                        break;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        Console.WriteLine("\n\t\t\tFelaktigt val, Försök igen.");
                                        Console.ResetColor();
                                        Console.ReadKey();
                                        Console.Clear();
                                        continue;
                                    }
                                }
                                break;
                            case "9":
                                Console.WriteLine("\n\t\t\tUtloggad");
                                insidemenu2running = false;
                                insidemenurunning = false;
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case "0":
                                Console.WriteLine("\n\t\t\tVälkommen åter");
                                insidemenu2running = false;
                                insidemenurunning = false;
                                running = false;
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine("\n\t\t\tFelaktigt val, Försök igen.");
                                Console.ResetColor();
                                Console.ReadKey();
                                Console.Clear();
                                break;
                        }
                    }
                    break;
                }
            }
            else if (adminCheck == false)
            {
                user = User.GetUser(existingUsername!);
                var userId = User.GetUserId(existingUsername!);
                //Kontrollera att användaren finns och att lösenordet stämmer
                if (user != null && user.Password == existingPassword)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n\t\tInloggning lyckades för användare {existingUsername}");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                    insidemenurunning = true;
                    while (insidemenurunning)
                    {
                        //Användarmenyn
                        Console.Clear();
                        Menus.UserMainMenu(existingUsername!);
                        Choice = Console.ReadLine();
                        switch (Choice)
                        {
                            case "1":
                                //Sökmenyn
                                insidemenu2running = true;
                                while (insidemenu2running)
                                {
                                    Console.Clear();
                                    Menus.UserSearchMenu();
                                    Choice = Console.ReadLine();
                                    switch (Choice)
                                    {
                                        case "1":
                                            Console.WriteLine("\n\t\tSök på ISBN nummer:");
                                            Console.Write("\t\t13 siffror: ");
                                            var inputISBN = long.Parse(Console.ReadLine()!);
                                            var book = Book.GetBookByISBN(inputISBN);
                                            var available = false;
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
                                                //Kontroll om boken finns i databasen
                                                if (book != null)
                                                {
                                                    available = book.Available;
                                                    //Kontroll om boken finns tillgänglig för lån eller redan är utlånad.
                                                    if (available == true)
                                                    {
                                                        Menus.DisplayAvailableBookInfo(book);
                                                        Console.WriteLine("\nVill du låna denna?");
                                                        Console.Write("JA/NEJ: ");
                                                        var checkingOutBook = Console.ReadLine()!.ToUpper();
                                                        if (checkingOutBook == "JA")
                                                        {
                                                            Loan.LoanBook(userId, book.Id);
                                                        }
                                                        else
                                                            break;
                                                    }
                                                    else
                                                        Menus.DisplayUnavailableBookInfo(book);
                                                }
                                            }
                                            Console.ReadKey();
                                            break;
                                        case "2":
                                            Console.WriteLine("\t\tSök på titel på boken:");
                                            Console.Write("\t\tTitel: ");
                                            var inputTitle = Console.ReadLine();
                                            Book.SearchBookByTitleSorted(inputTitle!);
                                            Loan.LoanQuestion(userId);
                                            Console.ReadKey();
                                            break;
                                        case "3":
                                            Console.WriteLine("\t\tSök på författarens för- och efternamn:");
                                            Console.Write("\t\tFörfattare: ");
                                            var inputAuthor = Console.ReadLine();
                                            Book.SearchBookByAuthorSorted(inputAuthor!);
                                            Loan.LoanQuestion(userId);
                                            Console.ReadKey();
                                            break;
                                        case "4":
                                            Console.WriteLine("\t\tSök med nyckelord (i titel eller författare):");
                                            Console.Write("\t\tNyckelord: ");
                                            var inputKeyword = Console.ReadLine();
                                            Console.WriteLine("\n\t\tVill du sortera böckerna?");
                                            Console.WriteLine("\t\t1. På författare");
                                            Console.WriteLine("\t\t2. På Titel");
                                            Console.Write("\n\t\tSvar: ");
                                            var sortChoice = Console.ReadLine();
                                            if (sortChoice == "1")
                                            {
                                                Book.SearchBooksByKeywordSortedAuthor(inputKeyword!);
                                            }
                                            else if (sortChoice == "2")
                                            {
                                                Book.SearchBooksByKeywordSortedTitle(inputKeyword!);
                                            }
                                            else
                                            {
                                                Console.WriteLine("\n\t\tOsorterad Lista\n");
                                                Book.SearchBooksByKeyword(inputKeyword!);
                                            }
                                            Loan.LoanQuestion(userId);
                                            break;
                                        case "5":
                                            //Listar alla tillgängliga böcker
                                            Book.ListAllAvailableBooks();
                                            Loan.LoanQuestion(userId);
                                            break;
                                        case "6":
                                            Console.WriteLine("\n\t\tVill du sortera böckerna?");
                                            Console.WriteLine("\t\t1. På författare");
                                            Console.WriteLine("\t\t2. På Titel");
                                            Console.WriteLine("\n\t\tVill du sortera böckerna?");
                                            Console.Write("\n\t\tSvar: ");
                                            sortChoice = Console.ReadLine();
                                            if (sortChoice == "1")
                                            {
                                                Book.ListAllBooksSortedAuthor();
                                            }
                                            else if (sortChoice == "2")
                                            {
                                                Book.ListAllBooksSortedTitle();
                                            }
                                            else
                                            {
                                                Console.WriteLine("\n\t\tOsorterad Lista\n");
                                                Book.ListAllBooks();
                                            }
                                            Loan.LoanQuestion(userId);
                                            Console.ReadKey();
                                            break;
                                        case "9":
                                            Console.WriteLine("\n\t\tÅter till föregående sida!");
                                            Console.ReadKey();
                                            Console.Clear();
                                            insidemenu2running = false;
                                            break;
                                        case "0":
                                            Console.WriteLine("\n\t\tVälkommen åter!");
                                            insidemenu2running = false;
                                            insidemenurunning = false;
                                            running = false;
                                            Console.ReadKey();
                                            Console.Clear();
                                            break;
                                        default:
                                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                                            Console.WriteLine("\n\t\t\tFelaktigt val, Försök igen.");
                                            Console.ResetColor();
                                            Console.ReadKey();
                                            Console.Clear();
                                            break;
                                    }
                                }
                                break;
                            case "2":
                                Console.WriteLine("\n\t\tLämna tillbaka en bok");
                                Console.WriteLine("\t\tSkriv in ISBN nummer på den bok du vill lämna tillbaka");
                                Console.Write("\t\t13 siffror: ");
                                long isbnReturn = long.Parse(Console.ReadLine()!);
                                if (isbnReturn.ToString().Length != 13)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("\t\tISBN Måste bestå av 13 siffror!");
                                    Console.ResetColor();
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                else
                                {
                                    Loan.ReturnBook(isbnReturn);
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                break;
                            case "3":
                                Console.Clear();
                                Menus.UserLoanMenu();
                                Choice = Console.ReadLine();
                                if (Choice == "1")
                                {
                                    var bookLoanList = Loan.GetLoanList(userId);
                                    if (bookLoanList.Count != 0)
                                    {
                                        Console.WriteLine("\n\t\tLista alla dina lånade böcker");
                                        foreach (var book in bookLoanList)
                                            Menus.DisplayBooksWithoutAvailability(book);
                                        Loan.ReturnQuestion();
                                        break;
                                    }
                                    else
                                        Console.WriteLine("\n\t\tDu har inga lånade böcker just nu!");
                                }
                                else if (Choice == "2")
                                {
                                    Loan.LoanHistory(userId);
                                    Loan.ReturnQuestion();
                                }
                                else
                                    Console.WriteLine("\n\t\tDu har inga återlämnade böcker just nu!");

                                //else if (Choice == "3")
                                //{
                                //    var overdueBooks = Loan.OverdueLoans(userId);
                                //    if (overdueBooks.Count != 0)
                                //    {
                                //        Console.WriteLine("\n\t\tDina försenade böcker:");
                                //        foreach (var book in overdueBooks)
                                //            Menus.DisplayBooksWithoutAvailability(book);
                                //        Loan.ReturnQuestion();
                                //        break;
                                //    }
                                //}
                                //else
                                //    Console.WriteLine("\n\t\tDu har inga försenade böcker just nu!");
                                break;
                            case "4":
                                Console.Clear();
                                var editProfile = true;
                                while(editProfile)
                                {
                                    Menus.EditProfile();
                                    Choice = Console.ReadLine();
                                    if ( Choice == "1")
                                    {
                                        Console.WriteLine("Skriv in nytt önskat användarnamn: ");
                                        var newUsername = Console.ReadLine();
                                        bool checker = User.CheckUser(newUsername!);
                                        if (checker == false)
                                        {
                                            user = User.UpdateUsername(existingUsername!, newUsername!);
                                            Console.WriteLine($"Nytt användarnamn {newUsername} registrerat för {existingUsername}");
                                            Console.ReadKey();
                                            Console.Clear();
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                                            Console.WriteLine($"Användarnamnet {newUsername} används redan");
                                            Console.ResetColor();
                                            Console.ReadKey();
                                            Console.Clear();
                                            break;
                                        }
                                        break;
                                    }
                                    else if (Choice == "2")
                                    {
                                        Console.WriteLine("Skriv in nya lösenordet:");
                                        var newPassword = Console.ReadLine();
                                        Console.WriteLine("Skriv in nya lösenordet igen:");
                                        var newPassword2 = Console.ReadLine();
                                        if (newPassword == newPassword2)
                                        {
                                            user = User.UpdatePassword(existingUsername!, existingPassword!, newPassword!);
                                            Console.WriteLine($"Nytt lösenord registrerat för {existingUsername}");
                                            break;
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                                            Console.WriteLine("Lösenorden matchar inte! Försök igen.");
                                            Console.ResetColor();
                                            Console.ReadKey();
                                            Console.Clear();
                                            continue;
                                        }
                                    }
                                    else if (Choice == "3")
                                    {
                                        Console.WriteLine("Är du säker på att du vill radera användaren? JA/NEJ");
                                        var deletechoice = Console.ReadLine()!.ToUpper();
                                        if (deletechoice == "JA")
                                        {
                                            User.DeleteUser(existingUsername!);
                                            Console.WriteLine("Användaren är borttagen, Tråkigt att se dig lämna oss :´(");
                                            Console.WriteLine("Du blir automatiskt tagen till huvudmenyn.");
                                            Console.ReadKey();
                                            Console.Clear();
                                            editProfile = false;
                                            insidemenu2running = false;
                                            insidemenurunning = false;
                                            break;
                                        }
                                        else if (deletechoice == "NEJ")
                                        {
                                            Console.WriteLine("Vad glada vi blir att du valt att stanna hos oss <3");
                                            Console.ReadKey();
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                                            Console.WriteLine("\n\t\t\tFelaktigt val, Försök igen.");
                                            Console.ResetColor();
                                            Console.ReadKey();
                                            Console.Clear();
                                            break;
                                        }
                                        break;
                                    }
                                    else if (Choice == "9")
                                    {
                                        editProfile = false;
                                        Console.Clear();
                                        break;
                                    }
                                    else if (Choice == "0")
                                    {
                                        Console.WriteLine("Välkommen åter");
                                        editProfile = false;
                                        insidemenu2running = false;
                                        insidemenurunning = false;
                                        running = false;
                                        Console.ReadKey();
                                        Console.Clear();
                                        break;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        Console.WriteLine("\n\t\t\tFelaktigt val, Försök igen.");
                                        Console.ResetColor();
                                        Console.ReadKey();
                                        Console.Clear();
                                        continue;
                                    }   
                                }
                                break;
                            case "9":
                                Console.WriteLine("\n\t\t\tUtloggad");
                                insidemenurunning = false;
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case "0":
                                Console.WriteLine("\n\t\t\tVälkommen åter");
                                insidemenurunning = false;
                                running = false;
                                Console.ReadKey();
                                Console.Clear();
                                break;
                        }
                    }
                }
            }
            break;
        case "0":
            Console.WriteLine("\n\t\t\tVälkommen åter");
            running = false;
            Console.ReadKey();
            Console.Clear();
            break;
        default:
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\t\t\tFelaktigt val, Försök igen.");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
            break;
    }   
}

