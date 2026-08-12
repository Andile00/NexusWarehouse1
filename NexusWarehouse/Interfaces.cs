using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusWarehouse
{
    // Interface 1: Contract for tracking inventory items
    public interface IInventoryItem
    {
        string GetDetails();
        void Relocate(string newZone);
    }

    // Interface 2: Contract for items that can be dispatched out of the warehouse
    public interface IDispatchable
    {
        bool CanDispatch();
        void Dispatch();
    }
}
