using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividuelltProjekt
{
    static class Menus
    {
        public static void MainMenu()
        {
            Console.WriteLine("Välkommen till biblioteket!");
            Console.WriteLine("Välj ett av följande alternativ:");
            Console.WriteLine("1. Användare");
            Console.WriteLine("2. Administratör");
            Console.WriteLine("0. Avsluta");
        }

        public static void SubMainMenu()
        {
            Console.WriteLine("Välkommen, välj något av följande val:");
            Console.WriteLine("1. Skapa nytt konto");
            Console.WriteLine("2. Logga in");
            Console.WriteLine("3. Återgå till huvudmenyn");
            Console.WriteLine("0. Avsluta");
        }

        public static void AdminMenu(string adminUserName)
        {
            Console.WriteLine($"\nVälkommen {adminUserName}\n");
            Console.WriteLine("1.Lägg till ny bok");
            Console.WriteLine("2.Redigera befintlig bok");
            Console.WriteLine("3.Ta bort bok");
            Console.WriteLine("4.Lista alla böcker");
            Console.WriteLine("9.Logga ut och återgå till huvudmenyn");
            Console.WriteLine("0.Avsluta");
        }

        public static void UserMenu(string userName)
        {
            Console.WriteLine($"\nVälkommen {userName}\n");
            Console.WriteLine("1.Sök bok");
            Console.WriteLine("2.Låna bok");
            Console.WriteLine("3.Lämna tillbaka bok");
            Console.WriteLine("4.Lista alla dina lånade böcker");
            Console.WriteLine("9.Logga ut och återgå till huvudmenyn");
            Console.WriteLine("0.Avsluta");
        }
    }
}
