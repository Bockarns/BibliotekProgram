
using IndividuelltProjekt;

//Programs main bool for running continuesly until user quits
bool running = true;

//start of program
while (running)
{
bool insidemenurunning = true;
bool createAccount = true;
bool userNameExists = false;
bool userNamefound = false;
string[] adminUserName = new string[10];
string[] adminPassword = new string[10];
string[] userName = new string[1];
string[] userPassword = new string[1];
int userCount = 0;
int adminUserCount = 0;

    Menus.MainMenu();
    var mainMenuChoice = Console.ReadLine();
    switch (mainMenuChoice)
    {
        case "1":
            while (insidemenurunning)
            {
                Menus.SubMainMenu();
                var userMainMenuChoice = Console.ReadLine();
                switch (userMainMenuChoice)
                {
                    case "1":
                        if (userCount >= userName.Length)
                        {
                            Console.WriteLine("Max antal konton uppnått! Kan inte skapa fler.");
                            Console.ReadKey();
                            break;
                        }
                        while (createAccount)
                        {
                            Console.WriteLine("Fyll i önskat användarnamn:");
                            string newUserName = Console.ReadLine().ToUpper();

                            userNameExists = false;
                            for (int i = 0; i < userCount; i++)
                            {
                                if (userName[i] == newUserName)
                                {
                                    userNameExists = true;
                                    break;
                                }
                            }

                            if (userNameExists)
                            {
                                Console.WriteLine("Användarnamnet är redan taget. Försök med ett annat.");
                                Console.ReadKey();
                                continue;
                            }

                            Console.WriteLine("Fyll i önskat lösenord");
                            string newUserPassword = Console.ReadLine();
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
                        Console.WriteLine("Fyll i ditt användarnamn:");
                        string existingUserName = Console.ReadLine().ToUpper();
                        Console.WriteLine("Fyll i ditt lösenord:");
                        string existingUserPassword = Console.ReadLine();

                        userNamefound = false;
                        for(int i = 0; i < userCount; i++)
                        {
                            if (userName[i] == existingUserName && userPassword[i] == existingUserPassword)
                            {
                                Console.WriteLine($"Inloggning lyckades för användare: {userName[i]}");
                                userNamefound = true;
                                break;
                            }
                        }
                        if (!userNamefound)
                        {
                            Console.WriteLine("Felaktigt användarnamn eller lösenord. Försök igen.");
                            continue;
                        }
                        Console.Clear();
                        Menus.UserMenu(existingUserName);
                        userMainMenuChoice = Console.ReadLine();
                        switch (userMainMenuChoice)
                        {
                            case "1":
                                Console.WriteLine("Hej Hej");
                                Console.ReadKey();
                            break;
                        }
                        break;

                    case "3":
                        insidemenurunning = false;
                        break;

                    case "4":
                        Console.WriteLine("Välkommen åter");
                        insidemenurunning = false;
                        running = false;
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
                Menus.SubMainMenu();
                var adminMainMenuChoice = Console.ReadLine();
                switch (adminMainMenuChoice)
                {
                    case "1":
                        if (userCount >= adminUserName.Length)
                        {
                            Console.WriteLine("Max antal konton uppnått! Kan inte skapa fler.");
                            Console.ReadKey();
                            break;
                        }
                        while (createAccount)
                        {
                            Console.WriteLine("Fyll i önskat användarnamn:");
                            string newAdminUserName = Console.ReadLine().ToUpper();

                            userNameExists = false;
                            for (int i = 0; i < userCount; i++)
                            {
                                if (userName[i] == newAdminUserName)
                                {
                                    userNameExists = true;
                                    break;
                                }
                            }

                            if (userNameExists)
                            {
                                Console.WriteLine("Användarnamnet är redan taget. Försök med ett annat.");
                                Console.ReadKey();
                                continue;
                            }

                            Console.WriteLine("Fyll i önskat lösenord");
                            string newAdminPassword = Console.ReadLine();
                            adminUserName[userCount] = newAdminUserName;
                            adminPassword[userCount] = newAdminPassword;
                            userCount++;

                            Console.WriteLine($"Nytt konto skapat för användare: {newAdminUserName}.");
                            Console.ReadKey();
                            createAccount = false;
                            Console.Clear();
                        }
                        break;

                    case "2":
                        Console.WriteLine("Fyll i ditt användarnamn:");
                        string existingAdminUserName = Console.ReadLine().ToUpper();
                        Console.WriteLine("Fyll i ditt lösenord:");
                        string existingAdminPassword = Console.ReadLine();

                        userNamefound = false;
                        for (int i = 0; i < userCount; i++)
                        {
                            if (adminUserName[i] == existingAdminUserName && adminPassword[i] == existingAdminPassword)
                            {
                                Console.WriteLine($"Inloggning lyckades för användare: {adminUserName[i]}");
                                userNamefound = true;
                                break;
                            }
                        }
                        if (!userNamefound)
                        {
                            Console.WriteLine("Felaktigt användarnamn eller lösenord. Försök igen.");
                        }
                        Console.Clear();
                        Menus.AdminMenu(existingAdminUserName);
                        adminMainMenuChoice = Console.ReadLine();
                        switch (adminMainMenuChoice)
                        {
                            case "1":
                                Console.WriteLine("Hej Hej");
                                Console.ReadKey();
                                break;
                        }
                        break;

                    case "3":
                        insidemenurunning = false;
                        break;

                    case "0":
                        Console.WriteLine("Välkommen åter");
                        insidemenurunning = false;
                        running = false;
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
            break;

        default:
            Console.WriteLine("Felaktigt val, Försök igen.");
            break;
    }   
}

