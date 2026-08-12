using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusWarehouse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Nexus Warehouse Hub";
            WarehouseManager manager = new WarehouseManager();

            // Subscribe Console UI to System Events
            manager.Events.OnCapacityAlert += (msg) => Console.WriteLine($"\n[ALERT EVENT] {msg}");
            manager.Events.OnTemperatureAlert += (msg) => Console.WriteLine($"\n[CRITICAL EVENT] {msg}");

            // Start Background Task
            bool systemActive = true;
            manager.StartBackgroundMonitoring(systemActive);

            while (systemActive)
            {
                Console.WriteLine("\n=================================");
                Console.WriteLine("    WAREHOUSE MANAGEMENT HUB");
                Console.WriteLine("=================================");
                Console.WriteLine($"Total Stored Weight: {manager.GetTotalWeight()} / 1000 kg");
                Console.WriteLine("1. Add Random Cargo (Factory)");
                Console.WriteLine("2. View Active Cargo");
                Console.WriteLine("3. Relocate Cargo");
                Console.WriteLine("4. Dispatch Cargo");
                Console.WriteLine("5. Remove Cargo");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Cargo newCargo = CargoFactory.CreateRandomCargo();
                            manager.AddCargo(newCargo);
                            Console.WriteLine($"\nAdded successfully: {newCargo.GetDetails()}");
                            break;

                        case "2":
                            var list = manager.GetActiveCargo();
                            Console.WriteLine($"\n--- INVENTORY ({list.Count} items) ---");
                            if (list.Count == 0) Console.WriteLine("No active cargo stored.");
                            foreach (var item in list)
                            {
                                Console.WriteLine(item.GetDetails());
                            }
                            break;

                        case "3":
                            Console.Write("Enter Cargo ID: ");
                            string relId = Console.ReadLine();
                            Console.Write("Enter New Zone (e.g. Zone-B): ");
                            string zone = Console.ReadLine();

                            Cargo target = manager.FindCargo(relId);
                            if (target != null)
                            {
                                target.Relocate(zone);
                                Console.WriteLine("Relocation complete.");
                            }
                            else
                            {
                                Console.WriteLine("Cargo ID not found.");
                            }
                            break;

                        case "4":
                            Console.Write("Enter Cargo ID to Dispatch: ");
                            string dispId = Console.ReadLine();
                            Cargo dispCargo = manager.FindCargo(dispId);

                            if (dispCargo != null)
                            {
                                dispCargo.Dispatch();
                                Console.WriteLine($"Cargo {dispId} successfully dispatched!");
                            }
                            else
                            {
                                Console.WriteLine("Cargo ID not found.");
                            }
                            break;

                        case "5":
                            Console.Write("Enter Cargo ID to Remove: ");
                            string remId = Console.ReadLine();
                            if (manager.RemoveCargo(remId))
                            {
                                Console.WriteLine("Cargo removed.");
                            }
                            else
                            {
                                Console.WriteLine("Cargo ID not found.");
                            }
                            break;

                        case "6":
                            systemActive = false;
                            Console.WriteLine("Exiting application...");
                            break;

                        default:
                            Console.WriteLine("Invalid option. Try again.");
                            break;
                    }
                }
                catch (CapacityExceededException ex)
                {
                    Console.WriteLine($"\n[CAPACITY EXCEEDED] {ex.Message}");
                }
                catch (InvalidCargoOperationException ex)
                {
                    Console.WriteLine($"\n[LOGICAL ERROR] {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[SYSTEM ERROR] {ex.Message}");
                }
            }
        }
    }
}
