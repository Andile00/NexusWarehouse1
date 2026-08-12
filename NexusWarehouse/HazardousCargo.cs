using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusWarehouse
{
    // Derived Class 2: Hazardous Cargo
    public class HazardousCargo : Cargo
    {
        public bool HasSafetyClearance { get; set; }

        public HazardousCargo(string id, double weight, string zone, bool hasClearance)
            : base(id, weight, zone)
        {
            HasSafetyClearance = hasClearance;
        }

        public override bool CanDispatch()
        {
            return HasSafetyClearance && !IsDispatched;
        }

        public override void Dispatch()
        {
            if (!CanDispatch())
            {
                throw new InvalidCargoOperationException($"Cargo {Id} cannot be dispatched without safety clearance.");
            }
            IsDispatched = true;
        }
    }
}
