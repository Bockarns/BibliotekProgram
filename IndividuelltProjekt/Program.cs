
bool running = true;

while (running)
{
    Console.WriteLine("Välkommen till biblioteket!");
    Console.WriteLine("Välj ett av följande alternativ:");
    Console.WriteLine("1. Användare");
    Console.WriteLine("2. Administratör");
    Console.WriteLine("0. Avsluta");
    int mainMenuChoice = int.Parse(Console.ReadLine());
    bool insidemenurunning = true;
    switch (mainMenuChoice)
    {
        case 1:
            while (insidemenurunning)
            {
                Console.WriteLine("Välkommen, välj något av följande val:");
                Console.WriteLine("1. Skapa nytt konto");
                Console.WriteLine("2. Logga in");
                Console.WriteLine("3. Återgå till huvudmenyn");
                Console.WriteLine("0. Avsluta");
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
                Console.WriteLine("Välkommen, välj något av följande val:");
                Console.WriteLine("1. Skapa nytt konto");
                Console.WriteLine("2. Logga in");
                Console.WriteLine("3. Återgå till huvudmenyn");
                Console.WriteLine("0. Avsluta");
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

