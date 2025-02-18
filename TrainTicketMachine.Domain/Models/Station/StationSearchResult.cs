using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicketMachine.Domain.Entities;

namespace TrainTicketMachine.Domain.Models.Station
{
    public class StationSearchResult
    {
        public List<string> StationNames { get; set; } 
        public List<char> NextCharacters { get; set; } 
    }
}
