using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicketMachine.Infrastructure.Models.Statıon;

namespace TrainTicketMachine.Application.Contracts
{
    public interface IStationHelper
    {
        List<string> FindStationsBySearch(string query, HashSet<StationDataSourceResponse> stations);
        List<char> FindNextCharacters(string query, List<string> stationNames);
    }
}
