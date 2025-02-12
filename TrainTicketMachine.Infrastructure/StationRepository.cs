using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TrainTicketMachine.Domain.Interfaces.Infrastructure;

namespace TrainTicketMachine.Infrastructure
{
    public class StationRepository : IStationRepository
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly string _dataUrl;

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
            var stationData = JsonSerializer.Deserialize<List<StationDTO>>(response);
            return stationData.Select(s => s.stationName).ToList();
        }

        private class StationDTO
        {
            public string stationName { get; set; } = string.Empty;
            public string stationCode { get; set; } = string.Empty;

        }
    }
}
