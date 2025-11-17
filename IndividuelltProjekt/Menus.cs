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

        public static void UserMainMenu()
        {
            Console.WriteLine("Välkommen, välj något av följande val:");
            Console.WriteLine("1. Skapa nytt konto");
            Console.WriteLine("2. Logga in");
            Console.WriteLine("3. Återgå till huvudmenyn");
            Console.WriteLine("0. Avsluta");
        }

        public static void AdminMainsMenu()
        {
            Console.WriteLine("Välkommen, välj något av följande val:");
            Console.WriteLine("1. Skapa nytt konto");
            Console.WriteLine("2. Logga in");
            Console.WriteLine("3. Återgå till huvudmenyn");
            Console.WriteLine("0. Avsluta");
        }
    }
}
