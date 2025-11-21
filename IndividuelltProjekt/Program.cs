
using IndividuelltProjekt;

bool running = true;
bool insidemenurunning = true;
bool createAccount = true;
bool userNameExists = true;
bool userNamefound = true;
string[] adminUserName = new string[10];
string[] adminPassword = new string[10];
string[] userName = new string[10];
string[] userPassword = new string[10];
int userCount = 0;
int adminUserCount = 0;


while (running)
{
    Menus.MainMenu();
    int mainMenuChoice = int.Parse(Console.ReadLine());
    switch (mainMenuChoice)
    {
        case 1:
            while (insidemenurunning)
            {
                Menus.SubMainMenu();
                int userMainMenuChoice = int.Parse(Console.ReadLine());
                switch (userMainMenuChoice)
                {
                    case 1:
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
                       
                    case 2:
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
                        }
                        Console.Clear();
                        Menus.UserMenu();
                        userMainMenuChoice = int.Parse(Console.ReadLine());
                        switch (userMainMenuChoice)
                        {
                            case 1:
                                Console.WriteLine("Hej Hej");
                                Console.ReadKey();
                            break;
                        }
                        break;

                    case 3:
                        insidemenurunning = false;
                        break;

                    case 0:
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

        case 2:
            while (insidemenurunning)
            {
                Menus.SubMainMenu();
                int adminMainMenuChoice = int.Parse(Console.ReadLine());
                switch (adminMainMenuChoice)
                {
                    case 1:
                        Console.WriteLine("Fyll i önskat användarnamn:");
                        string newAdminUserName = Console.ReadLine();
                        Console.WriteLine("Fyll i önskat lösenord");
                        string newAdminPassword = Console.ReadLine();
                        Console.WriteLine("Nytt konto skapat");
                        break;
                    case 2:
                        Console.WriteLine("Fyll i ditt användarnamn:");
                        string existingAdminUserName = Console.ReadLine();
                        Console.WriteLine("Fyll i ditt lösenord:");
                        string existingAdminPassword = Console.ReadLine();
                        break;
                    case 3:
                        insidemenurunning = false;
                        break;
                    case 0:
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

        case 0:
            Console.WriteLine("Välkommen åter");
            running = false;
            break;

        default:
            Console.WriteLine("Felaktigt val, Försök igen.");
            break;
    }   
}

