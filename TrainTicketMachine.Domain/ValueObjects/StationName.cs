using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketMachine.Domain.ValueObjects
{
    public class StationName
    {
        public string Value { get; private set; }

        public StationName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Station name cannot be empty.");
            Value = value;
        }
    }
}
