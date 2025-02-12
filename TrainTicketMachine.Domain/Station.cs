using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketMachine.Domain
{
    public class Station
    {
        public string Name { get; set; } = string.Empty;
        public List<char> NextCharacters { get; set; } = new List<char>();
    }
}
