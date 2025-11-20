
using IndividuelltProjekt;

bool running = true;

while (running)
{
    Menus.MainMenu();
    int mainMenuChoice = int.Parse(Console.ReadLine());
    bool insidemenurunning = true;
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
                        Console.WriteLine("Fyll i önskat användarnamn:");
                        string newUserName = Console.ReadLine();
                        Console.WriteLine("Fyll i önskat lösenord");
                        string newUserPassword = Console.ReadLine();
                        Console.WriteLine("Nytt konto skapat");
                        break;
                    case 2:
                        Console.WriteLine("Fyll i ditt användarnamn:");
                        string userName = Console.ReadLine();
                        Console.WriteLine("Fyll i ditt lösenord:");
                        string userPassword = Console.ReadLine();
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
                        string newUserName = Console.ReadLine();
                        Console.WriteLine("Fyll i önskat lösenord");
                        string newUserPassword = Console.ReadLine();
                        Console.WriteLine("Nytt konto skapat");
                        break;
                    case 2:
                        Console.WriteLine("Fyll i ditt användarnamn:");
                        string userName = Console.ReadLine();
                        Console.WriteLine("Fyll i ditt lösenord:");
                        string userPassword = Console.ReadLine();
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

