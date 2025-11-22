using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividuelltProjekt
{
    static class Menus
    {
        //Mainmenu printout
        public static void MainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t*********************************************************************************");
            Console.WriteLine("\t*\t\t\t\t\t\t\t\t\t\t*");
            Console.Write($"\t*\t\t\t***");
            Console.ResetColor();
            Console.Write(" Välkommen till biblioteket! ");
            Console.ForegroundColor= ConsoleColor.Green;
            Console.Write("***\t\t\t*\n");
            Console.WriteLine("\t*\t\t\t\t\t\t\t\t\t\t*");
            Console.WriteLine("\t*********************************************************************************\n");
            Console.ResetColor();
            Console.WriteLine("\t\tVälj ett av följande alternativ:");
            Console.WriteLine("\t\t1. Användare");
            Console.WriteLine("\t\t2. Administratör");
            Console.WriteLine("\t\t0. Avsluta");
            Console.Write("\t\tSkriv in ditt val här: ");
        }
        //Submenu printout
        public static void SubMainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t*********************************************************************************");
            Console.WriteLine("\t*\t\t\t\t\t\t\t\t\t\t*");
            Console.Write($"\t*\t\t***");
            Console.ResetColor();
            Console.Write(" Välkommen, välj något av följande val: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("***\t\t\t*\n");
            Console.WriteLine("\t*\t\t\t\t\t\t\t\t\t\t*");
            Console.WriteLine("\t*********************************************************************************\n");
            Console.ResetColor();
            Console.WriteLine("\t\t1. Skapa nytt konto");
            Console.WriteLine("\t\t2. Logga in");
            Console.WriteLine("\t\t3. Återgå till huvudmenyn");
            Console.WriteLine("\t\t0. Avsluta");
            Console.Write("\t\tSkriv in ditt val här: ");
        }
        //Adminmenu printout
        public static void AdminMenu(string adminUserName)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t*********************************************************************************");
            Console.WriteLine("\t*\t\t\t\t\t\t\t\t\t\t*");
            //check lenght of username to get the outline to match
            if (adminUserName.Length > 15)
            {
                Console.Write($"\t*\t\t\t***");
                Console.ResetColor();
                Console.Write($" Välkommen {adminUserName} ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("***\t\t\t*\n");
            }
            else if (adminUserName.Length < 15)
            {
                Console.Write($"\t*\t\t\t\t***");
                Console.ResetColor();
                Console.Write($" Välkommen {adminUserName} ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("***\t\t\t\t*\n");
            }
            Console.WriteLine("\t*\t\t\t\t\t\t\t\t\t\t*");
            Console.WriteLine("\t*********************************************************************************\n");
            Console.ResetColor();
            Console.WriteLine("\t\t1.Lägg till ny bok");
            Console.WriteLine("\t\t2.Redigera befintlig bok");
            Console.WriteLine("\t\t3.Ta bort bok");
            Console.WriteLine("\t\t4.Lista alla böcker");
            Console.WriteLine("\t\t9.Logga ut och återgå till huvudmenyn");
            Console.WriteLine("\t\t0.Avsluta");
            Console.Write("\t\tSkriv in ditt val här: ");
        }
        //Usermenu printout
        public static void UserMenu(string userName)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t*********************************************************************************");
            Console.WriteLine("\t*\t\t\t\t\t\t\t\t\t\t*");
            Console.ResetColor();
            //check lenght of username to get the outline to match
            if (userName.Length > 15)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\t*\t\t\t***");
                Console.ResetColor();
                Console.Write($" Välkommen {userName} ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("***\t\t\t*\n");
            }
            else if (userName.Length < 15)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\t*\t\t\t\t***");
                Console.ResetColor();
                Console.Write($" Välkommen {userName }");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("***\t\t\t\t*");
            }
            Console.WriteLine("\n\t*\t\t\t\t\t\t\t\t\t\t*");
            Console.Write("\t*\t");
            Console.ResetColor();
            Console.Write("Här i menyn nedan kan du söka efter böcker, låna böcker,");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\t\t*");
            Console.Write("\n\t*\t");
            Console.ResetColor();
            Console.Write("lämna tillbaka lånade böcker men också se alla dina lånade böcker." );
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
            Console.WriteLine("\t\t9.Logga ut och återgå till huvudmenyn");
            Console.WriteLine("\t\t0.Avsluta");
            Console.Write("\t\tSkriv in ditt val här: ");
        }
    }
}
