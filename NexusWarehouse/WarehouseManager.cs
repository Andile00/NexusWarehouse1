using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NexusWarehouse
{
    public class WarehouseManager
    {
        private List<Cargo> inventory = new List<Cargo>();
        private double maxCapacity = 1000.0; // Maximum warehouse capacity in kg
        public WarehouseEvents Events { get; } = new WarehouseEvents();

        public WarehouseManager()
        {
            // Automatically log event notifications to file
            Events.OnCapacityAlert += (msg) => FileLogger.Log("CAPACITY WARNING: " + msg);
            Events.OnTemperatureAlert += (msg) => FileLogger.Log("TEMP WARNING: " + msg);
        }

        public void AddCargo(Cargo item)
        {
            double currentWeight = GetTotalWeight();
            if (currentWeight + item.Weight > maxCapacity)
            {
                string alertMsg = $"Cannot add item {item.Id}. Total weight ({currentWeight + item.Weight}kg) exceeds capacity ({maxCapacity}kg).";
                Events.RaiseCapacityAlert(alertMsg);
                throw new CapacityExceededException(alertMsg);
            }

            inventory.Add(item);
            FileLogger.Log($"Added cargo {item.Id} ({item.Weight}kg) to {item.Zone}");
        }

        public bool RemoveCargo(string id)
        {
            Cargo item = FindCargo(id);
            if (item != null)
            {
                inventory.Remove(item);
                FileLogger.Log($"Removed cargo {id}");
                return true;
            }
            return false;
        }

        public Cargo FindCargo(string id)
        {
            return inventory.Find(c => c.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        }

        public List<Cargo> GetActiveCargo()
        {
            return inventory.FindAll(c => !c.IsDispatched);
        }

        public double GetTotalWeight()
        {
            double total = 0;
            foreach (var c in inventory)
            {
                if (!c.IsDispatched) total += c.Weight;
            }
            return total;
        }

        // Multithreading Requirement: Background process runs independently without blocking user input
        public void StartBackgroundMonitoring(bool keepRunning)
        {
            Task.Run(() =>
            {
                while (keepRunning)
                {
                    lock (inventory)
                    {
                        foreach (var item in inventory)
                        {
                            if (item is PerishableCargo perishable && !perishable.IsDispatched)
                            {
                                if (perishable.Temperature > 5.0)
                                {
                                    Events.RaiseTemperatureAlert($"Cargo {perishable.Id} temperature is unsafe ({perishable.Temperature}°C)!");
                                }
                            }
                        }
                    }
                    Thread.Sleep(6000); // Runs every 6 seconds in the background
                }
            });
        }
    }
}
