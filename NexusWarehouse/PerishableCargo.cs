using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusWarehouse
{
    // Derived Class 1: Perishable Cargo
    public class PerishableCargo : Cargo
    {
        public double Temperature { get; set; }

        public PerishableCargo(string id, double weight, string zone, double temperature)
            : base(id, weight, zone)
        {
            Temperature = temperature;
        }

        public override bool CanDispatch()
        {
            return Temperature <= 5.0 && !IsDispatched;
        }

        public override void Dispatch()
        {
            if (!CanDispatch())
            {
                throw new InvalidCargoOperationException($"Cargo {Id} cannot be dispatched due to high temperature ({Temperature}°C).");
            }
            IsDispatched = true;
        }
    }
}
