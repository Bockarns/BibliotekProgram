
using IndividuelltProjekt;
using IndividuelltProjekt.Data;
using IndividuelltProjekt.Models;
using System;

Console.Title = "JGB Bibliotek";

//Programs main bool for running continuesly until user quits
bool running = true;
//using arrays for usernames and password temperary, so i can continue with the menus

//start of program
while (running)
{
//Bools for nested menus, and to check for existing usernames
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
                Console.WriteLine("Ska användaren vara Admin?");
                Console.Write("Ja/Nej: ");
                var admincheck = Console.ReadLine().ToUpper();
                if (admincheck == "JA")
                {
                    bool checker = Admin.CheckUser(newUsername!);
                    if (checker == false)
                    {
                        Admin.AddUser(newUsername!, newPassword!);
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
                    if (checker == false)
                    {
                        User.AddUser(newUsername!, newPassword!);
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
            Console.Clear();
            Console.WriteLine("\nLogga in användare\n");
            Console.WriteLine("Fyll i ditt användarnamn:");
            var existingUsername = Console.ReadLine();
            Console.WriteLine("Fyll i ditt lösenord:");
            var existingPassword = Console.ReadLine();
            Console.WriteLine("Är du admin? ");
            Console.Write("Ja/Nej: ");
            var adminCheck = Console.ReadLine().ToUpper();
            if (adminCheck == "JA")
            {
                var user = Admin.GetUser(existingUsername!);
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
                                Console.WriteLine("\nSkriv in ISBN (13 siffror): ");
                                long newIsbn = long.Parse(Console.ReadLine()!);
                                if (newIsbn.ToString().Length != 13)
                                {
                                    Console.WriteLine("ISBN Måste bestå av 13 siffror!");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("Skriv in Författare (Separera för- och efternamn med mellanrum): ");
                                    var newAuther = Console.ReadLine();
                                    Console.WriteLine("Skriv in titel på boken: ");
                                    var newTitle = Console.ReadLine();
                                    bool isAvaliable = true;
                                    Book.AddBook(newIsbn, newAuther!, newTitle!, isAvaliable);
                                    Console.WriteLine("Ny bok tillagd i biblioteket");
                                    var book = Book.GetBookByISBN(newIsbn);
                                    if (book != null)
                                        Console.Write($"{book.Title}, {book.Auther}, {book.ISBN} ");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                break;
                            case "2":
                                //Tre olika lösningar. antingen så ska man söka på ISBN nummer för samtliga
                                //Ändringar som nedan men med ISBN nummer på alla.
                                //Eller så kommer ett prompt att man ska skriva in ISBN för den boken man 
                                //Vill göra ändringar för. sen när boken är hittat så ska man välja vad man
                                //Vill uppdatera.
                                Console.WriteLine("\n Vad vill du uppdatera?");
                                Console.WriteLine("1. ISBN ");
                                Console.WriteLine("2. Författare ");
                                Console.WriteLine("3. Titel ");
                                Console.WriteLine("4. Tillgänglighet ");
                                Choice = Console.ReadLine();
                                switch (Choice)
                                {
                                    case "1":
                                        Console.WriteLine("Skriv in ISBN nummer (13 siffror):");
                                        break;
                                    case "2":
                                        Console.WriteLine("Skriv in Författar:");
                                        break;
                                    case "3":
                                        Console.WriteLine("Skriv in titel");
                                        break;
                                    case "4":
                                        Console.WriteLine("Skriv in ISBN nummer (13 siffror):");
                                        break;
                                    case "0":
                                        break;
                                }
                                break;
                            case "3":
                                Console.WriteLine("Radera bok");
                                break;
                            case "4":
                                Console.WriteLine("Lista böcker");
                                break;
                            case "5":
                                Console.Clear();
                                var editProfile = true;
                                while (editProfile)
                                {
                                    Menus.EditProfile();
                                    Choice = Console.ReadLine();
                                    if (Choice == "1")
                                    {
                                        Console.WriteLine("Skriv in nytt önskat användarnamn: ");
                                        var newUsername = Console.ReadLine();
                                        bool checker = Admin.CheckUser(newUsername!);
                                        if (checker == false)
                                        {
                                           user = Admin.UpdateUsername(existingUsername, newUsername);
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
                                            user = Admin.UpdatePassword(existingUsername!, existingPassword, newPassword!);
                                            Console.WriteLine($"Nytt lösenord registrerat för {existingUsername}");
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Lösenorden matchar inte! Försök igen.");
                                            continue;
                                        }

                                        break;
                                    }
                                    else if (Choice == "3")
                                    {
                                        Console.WriteLine("Är du säker på att du vill radera användaren? JA/NEJ");
                                        var deletechoice = Console.ReadLine()!.ToUpper();
                                        if (deletechoice == "JA")
                                        {
                                            Admin.DeleteUser(existingUsername!);
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
                else
                {
                    Console.WriteLine("Inloggning misslyckades");
                    Console.ReadKey();
                    continue;
                }
            }
            else if (adminCheck == "NEJ")
            {
                var user = User.GetUser(existingUsername!);

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
                                            var avaliable = false;
                                            //Kontroll om boken finns i databasen
                                            if (book != null)
                                            {
                                                avaliable = book.Avaliable;
                                                //Kontroll om boken finns tillgänglig för lån eller redan är utlånad.
                                                if (avaliable == true)
                                                {
                                                    Console.WriteLine($"\n***| Titel: {book.Title},\n***| Författare: {book.Auther},\n***| ISBN: {book.ISBN},\n***| Finns tillgänglig för lån!");
                                                    Console.WriteLine("\nVill du låna denna?");
                                                    Console.Write("JA/NEJ: ");
                                                    var checkingOutBook = Console.ReadLine().ToUpper();
                                                    if (checkingOutBook == "JA")
                                                    {
                                                        Console.WriteLine("Här vill jag skapa en inner join som då kopplar användare med denna bok, sen ändrar avaliable till false");
                                                    }
                                                    else
                                                        break;
                                                }
                                                else
                                                    Console.WriteLine($"\n***| Titel: {book.Title},\n***| Författare: {book.Auther},\n***| ISBN: {book.ISBN},\n***| Finns EJ tillgänglig för lån!");
                                            }
                                            Console.ReadKey();
                                            break;
                                        case "2":
                                            Console.WriteLine("\t\tSök på titel på boken:");
                                            Console.Write("\t\tTitel: ");
                                            var inputTitle = Console.ReadLine();
                                            book = Book.GetBookByTitle(inputTitle!);
                                            avaliable = false;
                                            if (book != null)
                                            {
                                                avaliable = book.Avaliable;
                                                if (avaliable == true)
                                                {
                                                    Console.WriteLine($"\n***| Titel: {book.Title},\n***| Författare: {book.Auther},\n***| ISBN: {book.ISBN},\n***| Finns tillgänglig för lån!");
                                                }
                                                else
                                                    Console.WriteLine($"\n***| Titel: {book.Title},\n***| Författare: {book.Auther},\n***| ISBN: {book.ISBN},\n***| Finns EJ tillgänglig för lån!");
                                            }
                                            Console.ReadKey();
                                            break;
                                        case "3":
                                            Console.WriteLine("\t\tSök på författarens för- och efternamn:");
                                            Console.Write("\t\tFörfattare: ");
                                            var inputAuther = Console.ReadLine();
                                            book = Book.GetBookByAuther(inputAuther!);
                                            avaliable = false;
                                            if (book != null)
                                            {
                                                avaliable = book.Avaliable;
                                                if (avaliable == true)
                                                {
                                                    Console.WriteLine($"\n***| Titel: {book.Title},\n***| Författare: {book.Auther},\n***| ISBN: {book.ISBN},\n***| Finns tillgänglig för lån!");
                                                }
                                                else
                                                    Console.WriteLine($"\n***| Titel: {book.Title},\n***| Författare: {book.Auther},\n***| ISBN: {book.ISBN},\n***| Finns EJ tillgänglig för lån!");
                                            }
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
                                Console.ReadKey();
                                break;
                            case "4":
                                Console.WriteLine("\t\tLista alla dina lånade böcker");
                                Console.ReadKey();
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
                                            user = User.UpdateUsername(existingUsername, newUsername);
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
                                            user = User.UpdatePassword(existingUsername!, existingPassword, newPassword!);
                                            Console.WriteLine($"Nytt lösenord registrerat för {existingUsername}");
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Lösenorden matchar inte! Försök igen.");
                                            continue;
                                        }

                                            break;
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
                else
                {
                    Console.WriteLine("Inloggning misslyckades");
                    continue;
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
            break;
    }   
}

