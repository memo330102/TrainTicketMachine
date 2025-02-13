using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TrainTicketMachine.Infrastructure.Contracts;
using TrainTicketMachine.Infrastructure.Models.Statıon;

namespace TrainTicketMachine.Infrastructure.Repositories
{
    public class StationRepository : IStationRepository
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly string _dataUrl = string.Empty;

        public StationRepository(IConfiguration configuration)
        {
            _dataUrl = configuration["StationData:Url"];
        }
        public async Task<List<string>> SearchStationsAsync(string query)
        {
            var stations = await GetStationsFromRemoteAsync();
            return stations.Where(s => s.StartsWith(query, StringComparison.OrdinalIgnoreCase)).ToList();
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

        private async Task<List<string>> GetStationsFromRemoteAsync()
        {
            var response = await _httpClient.GetStringAsync(_dataUrl);
            var stationData = JsonSerializer.Deserialize<List<RemoteStationResponse>>(response);
            return stationData.Select(s => s.stationName).ToList();
        }
    }
}
