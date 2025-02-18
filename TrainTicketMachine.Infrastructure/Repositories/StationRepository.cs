using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TrainTicketMachine.Infrastructure.Contracts;
using TrainTicketMachine.Infrastructure.Models.Statıon;

namespace TrainTicketMachine.Infrastructure.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly IStationDataSource _dataSource;
        private static List<RemoteStationResponse> _cachedStations = new List<RemoteStationResponse>();
        private static readonly SemaphoreSlim _cacheLock = new SemaphoreSlim(1, 1);
        public StationRepository(IStationDataSource remoteStationProvider)
        {
            _dataSource = remoteStationProvider;
        }

        public async Task<List<string>> SearchStationsAsync(string query)
        {
            var stations = await _dataSource.LoadStationsAsync();
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
