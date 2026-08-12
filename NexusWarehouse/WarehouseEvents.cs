using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusWarehouse
{
    // Custom delegate definition for event handlers
    public delegate void WarehouseAlertHandler(string message);

    public class WarehouseEvents
    {
        // Event 1: Fired when total weight approaches or breaches storage limits
        public event WarehouseAlertHandler OnCapacityAlert;

        // Event 2: Fired when perishable items reach unsafe temperature thresholds
        public event WarehouseAlertHandler OnTemperatureAlert;

        public void RaiseCapacityAlert(string message)
        {
            OnCapacityAlert?.Invoke(message);
        }

        public void RaiseTemperatureAlert(string message)
        {
            OnTemperatureAlert?.Invoke(message);
        }
    }
}
