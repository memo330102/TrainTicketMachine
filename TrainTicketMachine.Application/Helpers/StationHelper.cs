using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicketMachine.Application.Contracts;
using TrainTicketMachine.Infrastructure.Models.Statıon;

namespace TrainTicketMachine.Application.Helpers
{
    public class StationHelper : IStationHelper
    {
        public List<char> FindNextCharacters(string query, List<string> stationNames)
        {
            return stationNames
                .Where(name => name.Length > query.Length)
                .Select(name => name[query.Length])
                .Distinct()
                .OrderBy(c => c)
                .ToList();
        }

        public List<string> FindStationsBySearch(string query, HashSet<StationDataSourceResponse> stations)
        {
            return stations
                .Where(n => n.stationName.StartsWith(query, StringComparison.OrdinalIgnoreCase))
                .Select(n => n.stationName)
                .ToList();
        }
    }
}
