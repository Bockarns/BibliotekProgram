
using IndividuelltProjekt;
using IndividuelltProjekt.Models;
using IndividuelltProjekt.Data;

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

//Call for main menu printout
    Menus.MainMenu();
    var mainMenuChoice = Console.ReadLine();

//Switch for menu choich - equal throughout the program inside each menu
    switch (mainMenuChoice)
    {
        case "1":
            while (insidemenurunning)
            {
                Console.Clear();
                Menus.SubMainMenu();
                var userMainMenuChoice = Console.ReadLine();
                switch (userMainMenuChoice)
                {
                    case "1":
                        try
                        {
                            Console.Clear();
                            //Create account with loop
                            Console.WriteLine("\nSkapa konto för ny användare.\n");
                            Console.WriteLine("Fyll i önskat användarnamn:");
                            var newUsername = Console.ReadLine();
                            Console.WriteLine("Fyll i önskat lösenord");
                            var newPassword = Console.ReadLine();
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
                        catch (Exception MyExcep)
                        {
                            Console.WriteLine(MyExcep.ToString());
                        }
                        break;
                    case "2":
                        //Log in
                        Console.Clear();
                        Console.WriteLine("\nLogga in användare\n");
                        Console.WriteLine("Fyll i ditt användarnamn:");
                        var existingUsername = Console.ReadLine();
                        Console.WriteLine("Fyll i ditt lösenord:");
                        var existingPassword = Console.ReadLine();
                        var user = User.GetUser(existingUsername!);
                        if (user != null && user.Password == existingPassword)
                        {
                            Console.WriteLine($"Inloggning lyckades för användare {existingUsername}");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine("Inloggning misslyckades");
                            continue;
                        }
                        while (insidemenurunning)
                        {
                            Console.Clear();
                            Menus.UserMainMenu(existingUsername!);
                            userMainMenuChoice = Console.ReadLine();
                            switch (userMainMenuChoice)
                                {
                                    case "1":
                                    insidemenu2running = true;
                                    while (insidemenu2running)
                                    {
                                        Console.Clear();
                                        Menus.UserSearchMenu();
                                        var userSearchMenuChoice = Console.ReadLine();
                                        switch(userSearchMenuChoice)
                                        {
                                            case "1":
                                                Console.WriteLine("\tSök på ISBN nummer:" );
                                                Console.Write("\t13 siffror: ");
                                                var inputISBN = int.Parse(Console.ReadLine()!);
                                                Console.ReadKey();
                                                break;

                                            case "2": 
                                                Console.WriteLine("\tSök på titel på boken:");
                                                Console.Write("\tTitel: ");
                                                var inputTitle = Console.ReadLine();
                                                Console.ReadKey();
                                                break;
                                            case "3":
                                                Console.WriteLine("\tSök på författarens för- och efternamn:");
                                                Console.Write("\tFörfattare: ");
                                                var inputAuther = Console.ReadLine();
                                                Console.ReadKey();
                                                break;
                                            case "9":
                                                Console.WriteLine("\t\tÅter till föregående sida!");
                                                Console.ReadKey();
                                                insidemenu2running = false;
                                                break;
                                            case "0":
                                                Console.WriteLine("\t\tVälkommen åter!");
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
                                    Console.WriteLine("\t Låna en bok");
                                    break;
                                    case "3":
                                    Console.WriteLine("\tLämna tillbaka en bok");
                                    break;
                                    case "4":
                                    Console.WriteLine("\tLista alla dina lånade böcker");
                                    break;

                                    case "9":
                                        Console.WriteLine("Utloggad");
                                        insidemenurunning = false;
                                        Console.ReadKey();
                                        Console.Clear();
                                        break;
                                    case "0":
                                        Console.WriteLine("Välkommen åter");
                                        insidemenurunning = false;
                                        running = false;
                                        Console.ReadKey();
                                        Console.Clear();
                                        break;
                                }
                        }
                        break;
                    case "3":
                        Console.Clear();
                        insidemenurunning = false;
                        Console.ReadKey();
                        break;
                    case "0":
                        Console.WriteLine("Välkommen åter");
                        insidemenurunning = false;
                        running = false;
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Felaktigt val, Försök igen.");
                        break;
                }
            }
            break;
        case "2":
            while (insidemenurunning)
            {
                Console.Clear();
                Menus.SubMainMenu();
                var adminMainMenuChoice = Console.ReadLine();
                switch (adminMainMenuChoice)
                {
                    case "1":
                        try
                        {
                            Console.Clear();
                            //Create account with loop
                            Console.WriteLine("\nSkapa konto för ny användare.\n");
                            Console.WriteLine("Fyll i önskat användarnamn:");
                            var newUsername = Console.ReadLine();
                            Console.WriteLine("Fyll i önskat lösenord");
                            var newPassword = Console.ReadLine();
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
                        var user = User.GetUser(existingUsername!);
                        if (user != null && user.Password == existingPassword)
                        {
                            Console.WriteLine($"Inloggning lyckades för användare {existingUsername}");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine("Inloggning misslyckades");
                            Console.ReadKey();
                            continue;
                        }
                        Console.Clear();
                        
                        insidemenu2running = true;
                        while (insidemenu2running)
                        {
                            Menus.AdminMainMenu(existingUsername!);
                            adminMainMenuChoice = Console.ReadLine();

                            switch (adminMainMenuChoice)
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
                    case "3":
                        Console.ReadKey();
                        Console.Clear();
                        insidemenurunning = false;
                        break;
                    case "0":
                        Console.WriteLine("Välkommen åter");
                        insidemenurunning = false;
                        running = false;
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Felaktigt val, Försök igen.");
                        break;
                }
            }
            break;
        case "0":
            Console.WriteLine("Välkommen åter");
            running = false;
            Console.ReadKey();
            Console.Clear();
            break;
        default:
            Console.WriteLine("Felaktigt val, Försök igen.");
            Console.ReadKey();
            break;
    }   
}

