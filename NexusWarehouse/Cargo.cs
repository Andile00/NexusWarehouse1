using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusWarehouse
{
    // Abstract Base Class
    public abstract class Cargo : IInventoryItem, IDispatchable
    {
        public string Id { get;  set; }
        public double Weight { get; set; }
        public string Zone { get;  set; }
        public bool IsDispatched { get; set; }

        public Cargo(string id, double weight, string zone)
        {
            Id = id;
            Weight = weight;
            Zone = zone;
            IsDispatched = false;
        }

        public string GetDetails()
        {
            string status = IsDispatched ? "Dispatched" : "In Warehouse";
            return $"[ID: {Id}] Type: {GetType().Name} | Weight: {Weight}kg | Zone: {Zone} | Status: {status}";
        }

        public void Relocate(string newZone)
        {
            if (IsDispatched)
            {
                throw new InvalidCargoOperationException("Cannot relocate cargo that has already been dispatched.");
            }
            Zone = newZone;
        }

        // Abstract methods to be overridden by child classes
        public abstract bool CanDispatch();
        public abstract void Dispatch();
    }
}
