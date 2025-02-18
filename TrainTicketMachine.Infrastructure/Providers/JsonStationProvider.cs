using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using TrainTicketMachine.Infrastructure.Contracts;
using TrainTicketMachine.Infrastructure.Models.Statıon;

namespace TrainTicketMachine.Infrastructure.Providers
{
    public class JsonStationProvider : IStationDataSource
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly IMemoryCache _cache;
        private readonly string _dataUrl;
        private readonly string _stationListCacheKey;

        public JsonStationProvider(IConfiguration configuration, IMemoryCache cache)
        {
            _dataUrl = configuration["StationData:Url"];
            _stationListCacheKey = configuration["StationData:StationListCacheKey"];
            _cache = cache;
        }

        public async Task<List<RemoteStationResponse>> LoadStationsAsync()
        {
            if (_cache.TryGetValue(_stationListCacheKey, out List<RemoteStationResponse> cachedData))
            {
                return cachedData;
            }

            var response = await _httpClient.GetStringAsync(_dataUrl);
            var stationData = JsonSerializer.Deserialize<List<RemoteStationResponse>>(response);

            _cache.Set(_stationListCacheKey, stationData, TimeSpan.FromHours(1));
            return stationData;
        }
    }
}