using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusWarehouse
{
    // Bonus Feature 1: Cargo Factory (Generates random cargo entities)
    public static class CargoFactory
    {
        private static Random random = new Random();

        public static Cargo CreateRandomCargo()
        {
            string id = "C-" + random.Next(100, 999);
            double weight = Math.Round(random.NextDouble() * 300 + 20, 1);
            string zone = "Zone-" + (char)('A' + random.Next(0, 4));

            if (random.Next(0, 2) == 0)
            {
                double temp = Math.Round(random.NextDouble() * 12 - 2, 1);
                return new PerishableCargo(id, weight, zone, temp);
            }
            else
            {
                bool clearance = random.Next(0, 2) == 1;
                return new HazardousCargo(id, weight, zone, clearance);
            }
        }
    }

    // Bonus Feature 2: File Logging
    public static class FileLogger
    {
        private static string logFile = "warehouse_log.txt";

        public static void Log(string message)
        {
            try
            {
                string entry = $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}";
                System.IO.File.AppendAllText(logFile, entry);
            }
            catch
            {
                // Prevent file locking issues from crashing the main app
            }
        }
    }
}
   
