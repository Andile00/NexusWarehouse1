using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusWarehouse
{
    // Custom exception thrown when cargo weight exceeds storage limits
    public class CapacityExceededException : Exception
    {
        public CapacityExceededException(string message) : base(message)
        {
        }
    }

    // Custom exception thrown when invalid cargo operations are attempted
    public class InvalidCargoOperationException : Exception
    {
        public InvalidCargoOperationException(string message) : base(message)
        {
        }
    }
}
