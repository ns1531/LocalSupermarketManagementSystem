using System;

namespace SupermarketManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();

                Console.WriteLine("LOCAL SUPERMRKET MANAGEMENT SYSTEM");
                Console.WriteLine("===================================");
                Console.WriteLine("1. Product Catalogue");
                Console.WriteLine("2. Supplier Records");
                Console.WriteLine("3. Product Search");
                Console.WriteLine("4. Stock Control");
                Console.WriteLine("5. Checkout & Sales Records");
                Console.WriteLine("6. Shop Reports");
                Console.WriteLine("7. Exit");
                Console.WriteLine();
                Console.Write("Choose an option (1 - 7) : ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        ShowSectionPlaceholder("Product Catalogue");
                        break;

                    case "2":
                        ShowSectionPlaceholder("Supplier Records");
                        break;

                    case "3":
                        ShowSectionPlaceholder("Product Search");
                        break;

                    case "4":
                        ShowSectionPlaceholder("Stock Control");
                        break;

                    case "5":
                        ShowSectionPlaceholder("Checkout & Sales Records");
                        break;

                    case "6":
                        ShowSectionPlaceholder("Shop Reports");
                        break;

                    case "7":
                        running = false;
                        Console.WriteLine("Exiting application.");
                        break;

                    default:
                        Console.WriteLine("Invalid input! Please choose a number from 1 to 7.");
                        Pause();
                        break;
                }
            }
        }

        static void ShowSectionPlaceholder(string sectionName)
        {
            Console.Clear();
            Console.WriteLine(sectionName.ToUpper());
            Console.WriteLine("==============================");
            Console.WriteLine("Test 123");
            Pause();
        }

        static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
        }
    }
}