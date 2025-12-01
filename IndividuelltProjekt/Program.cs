
using IndividuelltProjekt;
using IndividuelltProjekt.Data;
using IndividuelltProjekt.Models;
using System;
using System.Diagnostics.Eventing.Reader;

//Jag har valt att lägga menyerna i en egen klass som void, för annars blev koden så plottrig med alla extra rader.

Console.Title = "JGB Bibliotek";

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
                Console.Clear();
                Console.WriteLine("\nSkapa nytt konto\n");
                Console.WriteLine("Fyll i önskat användarnamn:");
                var newUsername = Console.ReadLine();
                Console.WriteLine("Fyll i önskat lösenord");
                var newPassword = Console.ReadLine();
                Console.WriteLine("Upprepa lösenordet");
                var newPassword2 = Console.ReadLine();
                if (newPassword != newPassword2)
                {
                    Console.WriteLine("Lösenorden matchar inte...");
                    break;
                }
                Console.WriteLine("Ska användaren vara Admin?");
                Console.Write("Ja/Nej: ");
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
                        Console.WriteLine($"Konto skapat för användare: {newUsername}");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Tryck på valfri tangent för att återgå till menyn...");
                        Console.ResetColor();
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                        Console.WriteLine($"Användarnamnet {newUsername} används redan");
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
                        Console.WriteLine($"Konto skapat för användare: {newUsername}");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Tryck på valfri tangent för att återgå till menyn...");
                        Console.ResetColor();
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                        Console.WriteLine($"Användarnamnet {newUsername} används redan");
                }
            }
            catch (Exception MyExcep)
            {
                Console.WriteLine(MyExcep.ToString());
            }
            break;
        case "2":
            //Logga in, denna kollar om användaren existerar i DB och om den är admin eller vanlig användare
            Console.Clear();
            Console.WriteLine("\nLogga in användare\n");
            Console.WriteLine("Fyll i ditt användarnamn:");
            var existingUsername = Console.ReadLine();
            Console.WriteLine("Fyll i ditt lösenord:");
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
                    Console.WriteLine($"Inloggning lyckades för användare {existingUsername}");
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
                                Console.WriteLine("\nSkriv in ISBN (13 siffror): ");
                                long newIsbn = long.Parse(Console.ReadLine()!);
                                if (newIsbn.ToString().Length != 13)
                                {
                                    Console.WriteLine("ISBN Måste bestå av 13 siffror!");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("Skriv in Författare (Separera för- och efternamn med mellanrum): ");
                                    var newAuthor = Console.ReadLine();
                                    Console.WriteLine("Skriv in titel på boken: ");
                                    var newTitle = Console.ReadLine();
                                    bool isAvaliable = true;
                                    Book.AddBook(newIsbn, newAuthor!, newTitle!, isAvaliable);
                                    Console.WriteLine("Ny bok tillagd i biblioteket");
                                    var book = Book.GetBookByISBN(newIsbn);
                                    if (book != null)
                                        Console.Write($"{book.Title}, {book.Author}, {book.Id} ");
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
                                        Console.WriteLine("Vill du söka via ISBN eller via författare och titel?");
                                        Console.Write("1 = ISBN eller 2 = Författare och titel: ");
                                        var searchChoice = Console.ReadLine();
                                        if (searchChoice == "1")
                                        {
                                            Console.WriteLine("Skriv in ISBN nummer (13 siffror):");
                                            var ISBN = long.Parse(Console.ReadLine()!);
                                            if (ISBN.ToString().Length != 13)
                                            {
                                                Console.WriteLine("ISBN Måste bestå av 13 siffror!");
                                                Console.ReadKey();
                                                Console.Clear();
                                            }
                                            else
                                            {
                                                var book = Book.GetBookByISBN(ISBN);
                                                Console.WriteLine("Stämmer det att du vill ändra författare för denna bok?");
                                                Console.WriteLine($"\n***| Titel: {book.Title},\n***| Författare: {book.Author},\n***| ISBN: {book.Id}");
                                                Console.Write("JA/NEJ: ");
                                                var confirm = Console.ReadLine()!.ToUpper();
                                                if (confirm == "JA")
                                                {
                                                    Console.WriteLine("Skriv in ny författare:");
                                                    var newAuthor = Console.ReadLine();
                                                    Book.UpdateAuthor(book.Author!, newAuthor!);
                                                    book = Book.GetBookByISBN(ISBN);
                                                    Console.WriteLine($"Boken: {book.Title}, Skriven av: {book.Author} har uppdaterats.");
                                                    Console.ReadKey();
                                                    Console.Clear();
                                                }
                                                else
                                                    break;
                                            } 
                                            break;
                                        }
                                        else if (searchChoice == "2")
                                        {
                                            Console.WriteLine("Skriv in författarens förnamn och efternamn: ");
                                            var author = Console.ReadLine();
                                            Console.WriteLine("Skriv in titel på boken: ");
                                            var title = Console.ReadLine();
                                            var book = Book.GetBookByAuthorAndTitle(author!, title!);
                                            if (book == null)
                                            {
                                                Console.WriteLine("Boken hittades inte, kontrollera stavning och försök igen.");
                                                Console.ReadKey();
                                                Console.Clear();
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Stämmer det att du vill ändra författare för denna bok?");
                                                Console.WriteLine($"\n***| Titel: {book.Title},\n***| Författare: {book.Author},\n***| ISBN: {book.Id}");
                                                Console.Write("JA/NEJ: ");
                                                var confirm = Console.ReadLine()!.ToUpper();
                                                if (confirm == "JA")
                                                {
                                                    Console.WriteLine("Skriv in ny författare:");
                                                    var newAuthor = Console.ReadLine();
                                                    Book.UpdateAuthor(book.Author!, newAuthor!);
                                                    book = Book.GetBookByAuthorAndTitle(author!, title!);
                                                    Console.WriteLine($"Boken: {book.Title}, Skriven av: {book.Author} har uppdaterats.");
                                                    Console.ReadKey();
                                                    Console.Clear();
                                                }
                                                else
                                                    break;
                                            }
                                                
                                        }
                                        else
                                            break;
                                        break;
                                    case "2":
                                        Console.WriteLine("Vill du söka via ISBN eller via författare och titel?");
                                        Console.Write("1 = ISBN eller 2 = Författare och titel: ");
                                        searchChoice = Console.ReadLine();
                                        if (searchChoice == "1")
                                        {
                                            Console.WriteLine("Skriv in ISBN nummer (13 siffror):");
                                            var ISBN = long.Parse(Console.ReadLine()!);
                                            if (ISBN.ToString().Length != 13)
                                            {
                                                Console.WriteLine("ISBN Måste bestå av 13 siffror!");
                                                Console.ReadKey();
                                                Console.Clear();
                                            }
                                            else
                                            {
                                                var book = Book.GetBookByISBN(ISBN);
                                                Console.WriteLine("Stämmer det att du vill ändra författare för denna bok?");
                                                Console.WriteLine($"\n***| Titel: {book.Title},\n***| Författare: {book.Author},\n***| ISBN: {book.Id}");
                                                Console.Write("JA/NEJ: ");
                                                var confirm = Console.ReadLine()!.ToUpper();
                                                if (confirm == "JA")
                                                {
                                                    Console.WriteLine("Skriv in ny titel:");
                                                    var newTitle = Console.ReadLine();
                                                    Book.UpdateTitle(book.Title!, newTitle!);
                                                    book = Book.GetBookByISBN(ISBN);
                                                    Console.WriteLine($"Boken: {book.Title}, Skriven av: {book.Author} har uppdaterats.");
                                                    Console.ReadKey();
                                                    Console.Clear();
                                                }
                                                else
                                                    break;
                                            }
                                            break;
                                        }
                                        else if (searchChoice == "2")
                                        {
                                            Console.WriteLine("Skriv in författarens förnamn och efternamn: ");
                                            var author = Console.ReadLine();
                                            Console.WriteLine("Skriv in titel på boken: ");
                                            var title = Console.ReadLine();
                                            var book = Book.GetBookByAuthorAndTitle(author!, title!);
                                            if (book == null)
                                            {
                                                Console.WriteLine("Boken hittades inte, kontrollera stavning och försök igen.");
                                                Console.ReadKey();
                                                Console.Clear();
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Stämmer det att du vill ändra författare för denna bok?");
                                                Console.WriteLine($"\n***| Titel: {book.Title},\n***| Författare: {book.Author},\n***| ISBN: {book.Id}");
                                                Console.Write("JA/NEJ: ");
                                                var confirm = Console.ReadLine()!.ToUpper();
                                                if (confirm == "JA")
                                                {
                                                    Console.WriteLine("Skriv in ny titel:");
                                                    var newTitle = Console.ReadLine();
                                                    Book.UpdateTitle(book.Title!, newTitle!);
                                                    book = Book.GetBookByAuthorAndTitle(author!, title!);
                                                    Console.WriteLine($"Boken: {book.Title}, Skriven av: {book.Author} har uppdaterats.");
                                                    Console.ReadKey();
                                                    Console.Clear();
                                                }
                                                else
                                                    break;
                                            }
                                        }
                                        else
                                            break;
                                        break;
                                    case "3":
                                        Console.WriteLine("Vill du söka via ISBN eller via författare och titel?");
                                        Console.Write("1 = ISBN eller 2 = Författare och titel: ");
                                        searchChoice = Console.ReadLine();
                                        if (searchChoice == "1")
                                        {
                                            Console.WriteLine("Skriv in ISBN nummer (13 siffror):");
                                            var ISBN = long.Parse(Console.ReadLine()!);
                                            if (ISBN.ToString().Length != 13)
                                            {
                                                Console.WriteLine("ISBN Måste bestå av 13 siffror!");
                                                Console.ReadKey();
                                                Console.Clear();
                                            }
                                            else
                                            {
                                                var book = Book.GetBookByISBN(ISBN);
                                                Console.WriteLine("Stämmer det att du vill ändra författare för denna bok?");
                                                Console.WriteLine($"\n***| Titel: {book.Title},\n***| Författare: {book.Author},\n***| ISBN: {book.Id}, \n***| Tillgänglighet: {book.Available}: ");
                                                Console.Write("JA/NEJ: ");
                                                var confirm = Console.ReadLine()!.ToUpper();
                                                if (confirm == "JA")
                                                {
                                                    Book.UpdateAvailable(ISBN, book.Available);
                                                    book = Book.GetBookByISBN(ISBN);
                                                    Console.WriteLine($"Boken: {book.Title}, Tillgänglighet har uppdaterats till: {book.Available}");
                                                    Console.ReadKey();
                                                    Console.Clear();
                                                }
                                            }
                                            break;
                                        }
                                        else if (searchChoice == "2")
                                        {
                                            Console.WriteLine("Skriv in författarens förnamn och efternamn: ");
                                            var author = Console.ReadLine();
                                            Console.WriteLine("Skriv in titel på boken: ");
                                            var title = Console.ReadLine();
                                            var book = Book.GetBookByAuthorAndTitle(author!, title!);
                                            if (book == null)
                                            {
                                                Console.WriteLine("Boken hittades inte, kontrollera stavning och försök igen.");
                                                Console.ReadKey();
                                                Console.Clear();
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Stämmer det att du vill ändra författare för denna bok?");
                                                Console.WriteLine($"\n***| Titel: {book.Title},\n***| Författare: {book.Author},\n***| ISBN: {book.Id}");
                                                Console.Write("JA/NEJ: ");
                                                var confirm = Console.ReadLine()!.ToUpper();
                                                if (confirm == "JA")
                                                {
                                                    Book.UpdateAvailable(book.Id, book.Available);
                                                    book = Book.GetBookByISBN(book.Id);
                                                    Console.WriteLine($"Boken: {book.Title}, Tillgänglighet har uppdaterats till: {book.Available}");
                                                    Console.ReadKey();
                                                    Console.Clear();
                                                }
                                                else
                                                    break;
                                            }
                                        }
                                        else
                                            break;
                                        break;
                                    case "9":
                                        Console.WriteLine("Återgår till föregående meny");
                                        break;
                                    case "0":
                                        Console.WriteLine("Välkommen åter");
                                        insidemenu2running = false;
                                        insidemenurunning = false;
                                        running = false;
                                        Console.ReadKey();
                                        Console.Clear();
                                        break;
                                }
                                break;
                            case "3":
                                Console.WriteLine("Vill du söka up boken via ISBN eller via författare och titel?");
                                Console.Write("1 = ISBN eller 2 = Författare och titel ");
                                var deleteSearchChoice = Console.ReadLine();
                                if (deleteSearchChoice == "1")
                                {
                                    Console.WriteLine("Skriv in ISBN nummer (13 siffror):");
                                    var ISBN = long.Parse(Console.ReadLine()!);
                                    if (ISBN.ToString().Length != 13)
                                    {
                                        Console.WriteLine("ISBN Måste bestå av 13 siffror!");
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        var book = Book.GetBookByISBN(ISBN);
                                        Console.WriteLine("Stämmer det att du vill radera denna bok?");
                                        Console.WriteLine($"\n***| Titel: {book.Title},\n***| Författare: {book.Author},\n***| ISBN: {book.Id}");
                                        Console.Write("JA/NEJ: ");
                                        var confirm = Console.ReadLine()!.ToUpper();
                                        if (confirm == "JA")
                                        {
                                            Book.DeleteBook(ISBN);
                                            Console.WriteLine("Boken är raderad från biblioteket.");
                                            Console.ReadKey();
                                            Console.Clear();
                                        }
                                        else
                                            break;
                                    }
                                    break;
                                }
                                else if (deleteSearchChoice == "2")
                                {
                                    Console.WriteLine("Skriv in författarens förnamn och efternamn: ");
                                    var author = Console.ReadLine();
                                    Console.WriteLine("Skriv in titel på boken: ");
                                    var title = Console.ReadLine();
                                    var book = Book.GetBookByAuthorAndTitle(author!, title!);
                                    if (book == null)
                                    {
                                        Console.WriteLine("Boken hittades inte, kontrollera stavning och försök igen.");
                                        Console.ReadKey();
                                        Console.Clear();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Stämmer det att du vill radera denna bok?");
                                        Console.WriteLine($"\n***| Titel: {book.Title},\n***| Författare: {book.Author},\n***| ISBN: {book.Id}");
                                        Console.Write("JA/NEJ: ");
                                        var confirm = Console.ReadLine()!.ToUpper();
                                        if (confirm == "JA")
                                        {
                                            Book.DeleteBook(book.Id);
                                            Console.WriteLine("Boken är raderad från biblioteket.");
                                            Console.ReadKey();
                                            Console.Clear();
                                        }
                                        else
                                            break;
                                    }
                                    break;
                                }
                                else
                                    break;
                            case "4":
                                        //Listar alla böcker och visar om de är lånade eller ej
                                        Console.WriteLine("Lista böcker");
                                        Book.ListAllBooks();
                                        Console.ReadKey();
                                        Console.Clear();
                                        break;
                                    case "5":
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
                                                    Console.WriteLine("Lösenorden matchar inte! Försök igen.");
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
                                                Console.WriteLine("Felaktigt val, försök igen");
                                                continue;
                                            }
                                        }
                                        break;
                                    case "9":
                                        Console.WriteLine("Utloggad");
                                        insidemenu2running = false;
                                        insidemenurunning = false;
                                        Console.ReadKey();
                                        Console.Clear();
                                        break;
                                    case "0":
                                        Console.WriteLine("Välkommen åter");
                                        insidemenu2running = false;
                                        insidemenurunning = false;
                                        running = false;
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
                    Console.WriteLine($"Inloggning lyckades för användare {existingUsername}");
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
                                            //Kontroll om boken finns i databasen
                                            if (book != null)
                                            {
                                                available = book.Available;
                                                //Kontroll om boken finns tillgänglig för lån eller redan är utlånad.
                                                if (available == true)
                                                {
                                                    Console.WriteLine($"\n***| Titel: {book.Title},\n***| Författare: {book.Author},\n***| ISBN: {book.Id},\n***| Finns tillgänglig för lån!");
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
                                                    Console.WriteLine($"\n***| Titel: {book.Title},\n***| Författare: {book.Author},\n***| ISBN: {book.Id},\n***| Finns EJ tillgänglig för lån!");
                                            }
                                            Console.ReadKey();
                                            break;
                                        case "2":
                                            Console.WriteLine("\t\tSök på titel på boken:");
                                            Console.Write("\t\tTitel: ");
                                            var inputTitle = Console.ReadLine();
                                            Book.GetBookByTitle(inputTitle!);
                                            Console.ReadKey();
                                            break;
                                        case "3":
                                            Console.WriteLine("\t\tSök på författarens för- och efternamn:");
                                            Console.Write("\t\tFörfattare: ");
                                            var inputAuthor = Console.ReadLine();
                                            Book.SearchBookByAuthor(inputAuthor!);
                                            available = false;
                                            Console.ReadKey();
                                            break;
                                        case "4":
                                            Console.WriteLine("\t\tSök med nyckelord (i titel eller författare):");
                                            Console.Write("\t\tNyckelord: ");
                                            var inputKeyword = Console.ReadLine();
                                            Book.SearchBooksByKeyword(inputKeyword!);
                                            Console.ReadKey();
                                            break;
                                        case "5":
                                            //Listar alla tillgängliga böcker
                                            Book.ListAllAvailableBooks();
                                            Console.ReadKey();
                                            break;
                                        case "6":
                                            Book.ListAllBooks();
                                            Console.ReadKey();
                                            break;
                                        case "9":
                                            Console.WriteLine("\n\t\tÅter till föregående sida!");
                                            Console.ReadKey();
                                            insidemenu2running = false;
                                            break;
                                        case "0":
                                            Console.WriteLine("\n\t\tVälkommen åter!");
                                            insidemenu2running = false;
                                            insidemenurunning = false;
                                            running = false;
                                            Console.ReadKey();
                                            break;
                                        default:
                                            Console.WriteLine("\t\tFelaktigt val, Försök igen");
                                            break;
                                    }
                                }
                                break;
                            case "2":
                                Console.WriteLine("\t\t\n Låna en bok");
                                Console.ReadKey();
                                break;
                            case "3":
                                Console.WriteLine("\t\tLämna tillbaka en bok");
                                Console.WriteLine("\t\tSkriv in ISBN nummer på den bok du vill lämna tillbaka");
                                Console.Write("\t\t13 siffror: ");
                                long isbnReturn = long.Parse(Console.ReadLine()!);
                                Loan.ReturnBook(isbnReturn);
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case "4":
                                var bookLoanList = Loan.GetLoanList(userId);
                                if (bookLoanList != null)
                                {   
                                    Console.WriteLine("\t\tLista alla dina lånade böcker");
                                    foreach(var book in bookLoanList)
                                        Console.WriteLine($"\n***| Titel: {book.Title}\n***| Författare: {book.Author}\n***| ISBN: {book.Id}");
                                }
                                else
                                    Console.WriteLine("\n\t\tDu har inga lånade böcker just nu!");
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case "5":
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
                                            Console.WriteLine($"Användarnamnet {newUsername} används redan");
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
                                            Console.WriteLine("Lösenorden matchar inte! Försök igen.");
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
                                        Console.WriteLine("Felaktigt val, försök igen");
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
            Console.WriteLine("\n\t\t\tFelaktigt val, Försök igen.");
            Console.ReadKey();
            Console.Clear();
            break;
    }   
}

