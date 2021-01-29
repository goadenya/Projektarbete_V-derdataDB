using Projektarbete_VäderdataDB.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Projektarbete_VäderdataDB
{
    class Application
    {
        static string Outdoors = "UTE";
        static string Indoors = "INNE";
        public static void Start()
        {
            Console.WriteLine("***********************************");
            Console.WriteLine("*       Nimbus Väderdatabas       *");
            Console.WriteLine("***********************************");
            Console.SetCursorPosition(0, 5);
            //Console.WriteLine("- Dataåtkomst i databasen NimbusWeatherDB. Innehåller data av \ntemperaturer som lästes mellan år 2016 - 2017.");
            //Console.SetCursorPosition(0, 10);
            Console.WriteLine("\n[Tryck på valfri knapp för att fortsätta.]");
            Console.SetCursorPosition(0, 15);
            Console.WriteLine("* 2021 - David Deriba *");

            
            Console.ReadKey();
            bool run = true;
            while (run)
            {
                Console.Clear();               
                Console.WriteLine("Tryck [Enter] för att gå till Meny.");
                Console.WriteLine("Tryck [Esc] för att avsluta.");
                ConsoleKeyInfo input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.Enter:
                        Menu();
                        break;
                    case ConsoleKey.Escape:
                        run = false;
                        break;
                    default:
                        break;
                }
            }
        }

        internal static void Menu()
        {
            Console.Clear();
            Console.WriteLine("***** Meny *****");
            Console.WriteLine();
            Console.WriteLine("Tryck [A] för göra en sökning utomhus");
            Console.WriteLine("Tryck [B] för göra en sökning inomhus");
            ConsoleKeyInfo input = Console.ReadKey();

            switch (input.Key)
            {
                case ConsoleKey.A:
                    OutdoorsQuery();
                    break;
                case ConsoleKey.B:
                    IndoorsQuery();
                    break;
                default:
                    break;
            }
        }
        internal static void IndoorsQuery()
        {
            Console.Clear();
            Console.WriteLine("******* Sökning Inomhus *******");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("A. Sök medeltemperatur för valt datum");
            Console.WriteLine("B. Sortering av varmaste till kallaste dagen enligt medeltemperatur per dag");
            Console.WriteLine("C. Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag");
            Console.WriteLine("D. Sortering av minst till störst risk för mögel");
            ConsoleKeyInfo input = Console.ReadKey();
            Console.Clear();
            switch (input.Key)
            {
                case ConsoleKey.A:
                    DisplaySearchDateAvgTemp(Indoors);
                    break;
                case ConsoleKey.B:
                    DisplayOrderedDateAvgTemps(Indoors);
                    break;
                case ConsoleKey.C:
                    DisplayOrderedDateAvgHumidity(Indoors);
                    break;
                case ConsoleKey.D:
                    DisplayMoldRiskAssesmentList(Indoors);
                    break;
                default:
                    break;
            }
        }

        internal static void OutdoorsQuery()
        {
            Console.Clear();
            Console.WriteLine("******* Sökning Utomhus *******");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("A. Sök medeltemperatur för valt datum");
            Console.WriteLine("B. Sortering av varmaste till kallaste dagen enligt medeltemperatur per dag");
            Console.WriteLine("C. Sortering av torrast till fuktigaste dagen enligt medelluftfuktighet per dag");
            Console.WriteLine("D. Sortering av minst till störst risk för mögel");
            Console.WriteLine("E. Sök datum för meteorologisk höst");
            Console.WriteLine("F. Sök datum för meteorologisk vinter");

            ConsoleKeyInfo input = Console.ReadKey();
            Console.Clear();
            
            switch (input.Key)
            {
                case ConsoleKey.A:
                    DisplaySearchDateAvgTemp(Outdoors);
                    break;

                case ConsoleKey.B:
                    DisplayOrderedDateAvgTemps(Outdoors);
                    break;

                case ConsoleKey.C:
                    DisplayOrderedDateAvgHumidity(Outdoors);
                    break;

                case ConsoleKey.D:
                    DisplayMoldRiskAssesmentList(Outdoors);
                    break;

                case ConsoleKey.E:
                    DisplayFirstDayOfFallSeason();
                    break;

                case ConsoleKey.F:
                    DisplayFirstDayOfWinterSeason();
                    break;

                default:
                    break;
            }
        }

        private static void DisplayFirstDayOfWinterSeason()
        {
            Console.WriteLine("***** Sök datum för meteorologisk vinter *****");
            Console.SetCursorPosition(0, 5);

            Console.WriteLine($"Första vinterdagen för säsongen: {Script.FirstDayOfWinterQuery()}");

            Console.WriteLine("\n[Tryck på valfri knapp för att fortsätta.]");
            Console.ReadKey();
        }

        private static void DisplayFirstDayOfFallSeason()
        {
            Console.WriteLine("***** Sök datum för meteorologisk höst *****");
            Console.SetCursorPosition(0, 5);

            Console.WriteLine($"Första höstdagen för säsongen: {Script.FirstDayOfFallQuery()}");

            Console.WriteLine("\n[Tryck på valfri knapp för att fortsätta.]");
            Console.ReadKey();
        }

        private static void DisplayMoldRiskAssesmentList(string place)
        {
            Console.WriteLine("***** Sortering av varmaste till kallaste dagen enligt medeltemperatur per dag *****");

            var moldRiskList = Script.MoldRiskAssessment(place);

            int row = 5;
            int count = 0;

            Utils.HeaderPlacer(3, "Nr", "Datum", "Medelfuktighet", "",$"Mögelrisk");
            foreach (var date in moldRiskList)
            {
                count++;
                Utils.InfoPlacer(row, count.ToString(), date.Date.ToShortDateString(), $"{date.AvgHumidity} %", "", $"{date.MoldRisk} %");
                row++;
            }           
            Console.WriteLine("\n[Tryck på valfri knapp för att fortsätta.]");
            Console.ReadKey();
        }

        private static void DisplayOrderedDateAvgHumidity(string place)
        {
            Console.WriteLine("***** Sortering av varmaste till kallaste dagen enligt medelluftfuktighet per dag *****");

            var dateAvgHumidityList = Script.ListAvgHumidityQuery(place);

            int row = 5;
            int count = 0;

            Utils.HeaderPlacer(3, "Nr", "Datum", $"Medeluftfuktighet");
            foreach (var date in dateAvgHumidityList)
            {
                count++;
                Utils.InfoPlacer(row, count.ToString(), date.Date.ToShortDateString(), $"{date.AvgHumidity} %");
                row++;
            }
            Console.WriteLine("\n[Tryck på valfri knapp för att fortsätta.]");
            Console.ReadKey();
        }

        private static void DisplaySearchDateAvgTemp(string place)
        {
            while (true)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("***** Sök medeltemperatur för valt datum *****");
                    Console.SetCursorPosition(0, 5);

                    Console.WriteLine("Visa medeltemperatur för ett angett datum mellan 2016-05-31 till 2017-01-10.");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.Write("Ange datum i format YYYY-MM-DD: ");
                    string queryDateInput = Console.ReadLine();

                    Console.SetCursorPosition(0, 10);
                    Console.WriteLine($"Medeltemperatur för datum {queryDateInput}: {Script.DateAvgTempQuery(queryDateInput, place)}°C");
                    break;
                }
                catch (Exception)
                {
                    Console.SetCursorPosition(0, 10);
                    Console.WriteLine("Ange ett giltigt datum!");
                    Thread.Sleep(3000);
                }
            }
            Console.WriteLine("\n[Tryck på valfri knapp för att fortsätta.]");
            Console.ReadKey();
        }

        private static void DisplayOrderedDateAvgTemps(string place)
        {
            Console.WriteLine("***** Sortering av varmaste till kallaste dagen enligt medeltemperatur per dag *****");
            var dateAvgTempList = Script.ListAvgTempQuery(place, true);
            
            int row = 5;
            int count = 0;
            Utils.HeaderPlacer(3, "Nr","Datum", $"Medeltemperatur ");
            foreach (var date in dateAvgTempList)
            {
                count++;
                Utils.InfoPlacer(row, count.ToString(),date.Date.ToShortDateString(), $"{date.AvgTemp} °C");
                row++;
            }
            Console.WriteLine();
            Console.WriteLine("\n[Tryck på valfri knapp för att fortsätta.]");
            Console.ReadKey();
        }    
    }
}
