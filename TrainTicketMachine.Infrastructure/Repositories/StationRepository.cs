using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TrainTicketMachine.Infrastructure.Contracts;
using TrainTicketMachine.Infrastructure.Models.Statıon;

namespace TrainTicketMachine.Infrastructure.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly IRemoteStationProvider _remoteStationProvider;
        public StationRepository(IRemoteStationProvider remoteStationProvider)
        {
            _remoteStationProvider = remoteStationProvider;
        }
        public async Task<List<string>> SearchStationsAsync(string query)
        {
            var stations = await _remoteStationProvider.GetStationsFromRemoteAsync();
            var stationnames = stations.Select(s => s.stationName);
            return stationnames.Where(s => s.StartsWith(query, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public async Task<List<char>> GetNextCharactersAsync(string query)
        {
            var stations = await SearchStationsAsync(query);
            var nextChars = new HashSet<char>();
            foreach (var station in stations)
            {
                if (station.Length > query.Length)
                {
                    nextChars.Add(station[query.Length]);
                }
            }
            return nextChars.OrderBy(c => c).ToList();
        }
    }
}
