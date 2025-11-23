
using IndividuelltProjekt;

//Programs main bool for running continuesly until user quits
bool running = true;
//using arrays for usernames and password temperary, so i can continue with the menus
string[] adminUserName = new string[10];
string[] adminPassword = new string[10];
string[] userName = new string[10];
string[] userPassword = new string[10];
int userCount = 1;
int adminUserCount = 1;

userName[0] = "1";
userPassword[0] = "1";
adminUserName[0] = "2";
adminPassword[0] = "2";

//start of program
while (running)
{
//Bools for nested menus, and to check for existing usernames
bool insidemenurunning = true;
bool insidemenu2running = true;
bool userNameExists = false;
bool userNamefound = false;

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
                bool createAccount = true;
                Menus.SubMainMenu();
                var userMainMenuChoice = Console.ReadLine();
                switch (userMainMenuChoice)
                {
                    case "1":
                        Console.Clear();
                        //Check array is not full (Will not stay after sql implementation)
                        if (userCount >= userName.Length)
                        {
                            Console.WriteLine("Max antal konton uppnått! Kan inte skapa fler.");
                            Console.ReadKey();
                            break;
                        }
                        //Create account with loop
                        while (createAccount)
                        {
                            Console.WriteLine("\nSkapa konto för ny användare.\n");
                            Console.WriteLine("Fyll i önskat användarnamn:");
                            string newUserName = Console.ReadLine().ToUpper();

                            //Check if username is avalible
                            userNameExists = false;
                            for (int i = 0; i < userCount; i++)
                            {
                                if (userName[i] == newUserName)
                                {
                                    userNameExists = true;
                                    break;
                                }
                            }
                            //If username is occupied this will force the user to pick a new username
                            if (userNameExists)
                            {
                                Console.WriteLine("Användarnamnet är redan taget. Försök med ett annat.");
                                Console.ReadKey();
                                continue;
                            }
                            //Continue after check
                            Console.WriteLine("Fyll i önskat lösenord");
                            string newUserPassword = Console.ReadLine();
                            //Put the username and password into the arrays
                            userName[userCount] = newUserName;
                            userPassword[userCount] = newUserPassword;
                            userCount++;
                            Console.WriteLine($"Nytt konto skapat för användare: {newUserName}.");
                            Console.ReadKey();
                            createAccount = false;
                            Console.Clear();
                        }
                        break;
                    case "2":
                        //Log in
                        Console.Clear();
                        Console.WriteLine("\nLogga in användare\n");
                        Console.WriteLine("Fyll i ditt användarnamn:");
                        string existingUserName = Console.ReadLine().ToUpper();
                        Console.WriteLine("Fyll i ditt lösenord:");
                        string existingUserPassword = Console.ReadLine();

                        //Look in arrays to find if username and password exists in them allso in the same element
                        userNamefound = false;
                        for(int i = 0; i < userCount; i++)
                        {
                            if (userName[i] == existingUserName && userPassword[i] == existingUserPassword)
                            {
                                Console.WriteLine($"Inloggning lyckades för användare: {userName[i]}");
                                userNamefound = true;
                                Console.Clear();
                                break;
                            }
                        }
                        if (!userNamefound)
                        {
                            Console.WriteLine("Felaktigt användarnamn eller lösenord. Försök igen.");
                            Console.ReadKey();
                            continue;
                        }
                        while(insidemenurunning)
                        {
                            Console.Clear();
                            Menus.UserMainMenu(existingUserName);
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
                                                var inputISBN = int.Parse( Console.ReadLine() );
                                                Console.ReadKey();
                                                break;

                                            case "2": 
                                                Console.WriteLine("\tSök på titel på boken:");
                                                Console.Write("\tTitel: ");
                                                var inputTitle = Console.ReadLine().ToUpper();
                                                Console.ReadKey();
                                                break;
                                            case "3":
                                                Console.WriteLine("\tSök på författarens för- och efternamn:");
                                                Console.Write("\tFörfattare: ");
                                                var inputAuther = Console.ReadLine().ToUpper();
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
                                        createAccount = false;
                                        insidemenurunning = false;
                                        Console.ReadKey();
                                        Console.Clear();
                                        break;
                                    case "0":
                                        Console.WriteLine("Välkommen åter");
                                        createAccount = false;
                                        insidemenurunning = false;
                                        running = false;
                                        Console.Clear();
                                        break;
                                }
                        }
                        break;
                    case "3":
                        Console.Clear();
                        insidemenurunning = false;
                        break;
                    case "0":
                        Console.WriteLine("Välkommen åter");
                        insidemenurunning = false;
                        running = false;
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
                bool createAccount = true;
                Menus.SubMainMenu();
                var adminMainMenuChoice = Console.ReadLine();
                switch (adminMainMenuChoice)
                {
                    case "1":
                        //Check array is not full (Will not stay after sql implementation)
                        Console.Clear();
                        if (adminUserCount >= adminUserName.Length)
                        {
                            Console.WriteLine("Max antal konton uppnått! Kan inte skapa fler.");
                            Console.ReadKey();
                            break;
                        }
                        //Create account with loop
                        while (createAccount)
                        {   
                            Console.WriteLine("\nSkapa konto för ny Admin.\n");
                            Console.WriteLine("Fyll i önskat användarnamn:");
                            string newAdminUserName = Console.ReadLine().ToUpper();
                            //Check if username is avalible
                            userNameExists = false;
                            for (int i = 0; i < adminUserCount; i++)
                            {
                                if (adminUserName[i] == newAdminUserName)
                                {
                                    userNameExists = true;
                                    break;
                                }
                            }
                            //If username is occupied this will force the user to pick a new username
                            if (userNameExists)
                            {
                                Console.WriteLine("Användarnamnet är redan taget. Försök med ett annat.");
                                Console.ReadKey();
                                continue;
                            }
                            Console.WriteLine("Fyll i önskat lösenord");
                            string newAdminPassword = Console.ReadLine();
                            //Put the username and password into the arrays
                            adminUserName[adminUserCount] = newAdminUserName;
                            adminPassword[adminUserCount] = newAdminPassword;
                            adminUserCount++;
                            Console.WriteLine($"Nytt konto skapat för användare: {newAdminUserName}.");
                            Console.ReadKey();
                            createAccount = false;
                            Console.Clear();
                        }
                        break;
                    case "2":
                        //Log in
                        Console.Clear();
                        Console.WriteLine("\nLogga in Admin\n");
                        Console.WriteLine("Fyll i ditt användarnamn:");
                        string existingAdminUserName = Console.ReadLine().ToUpper();
                        Console.WriteLine("Fyll i ditt lösenord:");
                        string existingAdminPassword = Console.ReadLine();
                        //Look in arrays to find if username and password exists in them allso in the same element
                        userNamefound = false;
                        for (int i = 0; i < adminUserCount; i++)
                        {
                            if (adminUserName[i] == existingAdminUserName && adminPassword[i] == existingAdminPassword)
                            {
                                Console.WriteLine($"Inloggning lyckades för användare: {adminUserName[i]}");
                                userNamefound = true;
                                Console.Clear();
                                break;
                            }
                        }
                        if (!userNamefound)
                        {
                            Console.WriteLine("Felaktigt användarnamn eller lösenord. Försök igen.");
                            Console.ReadKey();
                            continue;
                        }
                        Console.Clear();
                        Menus.AdminMainMenu(existingAdminUserName);
                        adminMainMenuChoice = Console.ReadLine();
                        switch (adminMainMenuChoice)
                        {
                            case "1":
                                Console.WriteLine("Hej Hej");
                                Console.ReadKey();
                                break;
                            case "9":
                                Console.WriteLine("Utloggad");
                                createAccount = false;
                                insidemenurunning = false;
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case "0":
                                Console.WriteLine("Välkommen åter");
                                createAccount = false;
                                insidemenurunning = false;
                                running = false;
                                Console.Clear();
                                break;
                        }
                        break;
                    case "3":
                        Console.Clear();
                        insidemenurunning = false;
                        break;
                    case "0":
                        Console.WriteLine("Välkommen åter");
                        insidemenurunning = false;
                        running = false;
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
            Console.Clear();
            break;
        default:
            Console.WriteLine("Felaktigt val, Försök igen.");
            break;
    }   
}

