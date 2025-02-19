using Microsoft.Extensions.Configuration;
using Serilog;
using TrainTicketMachine.Application.Contracts;
using TrainTicketMachine.Domain.Models.Station;
using TrainTicketMachine.Infrastructure.Contracts;
using TrainTicketMachine.Infrastructure.Models.Statıon;

namespace TrainTicketMachine.Application.Services
{
    public class StationService : IStationService
    {
        private readonly IStationRepository _repository;
        private readonly IStationHelper _helper;
        private readonly IStationCacheService _cache;
        private readonly ILogger _logger;
        private readonly string _stationListCacheKey;
        public StationService(IStationRepository repository, IStationCacheService cache, IStationHelper helper, IConfiguration configuration, ILogger logger)
        {
            _repository = repository;
            _cache = cache;
            _helper = helper;
            _stationListCacheKey = configuration["StationData:StationListCacheKey"] ?? "";
            _logger = logger;
        }

        public async Task<StationSearchResult> SearchStationsAsync(string query)
        {
            HashSet<StationDataSourceResponse> stations = new HashSet<StationDataSourceResponse>();

            try
            {
                var cachedData = await _cache.GetCacheValueAsync<HashSet<StationDataSourceResponse>>(_stationListCacheKey);
                if (cachedData != null)
                {
                    stations = cachedData;
                    _logger.Information($"Station List gets from cache.");
                }
                else
                {
                    stations = await _repository.SearchStationsAsync();
                    _logger.Information($"Station List gets from providers.");

                    await _cache.SetCacheValueAsync(_stationListCacheKey, stations, TimeSpan.FromHours(1));
                    _logger.Information($"Station List set to the cache.");
                }

                var stationNames = _helper.FindStationsBySearch(query, stations);

                var nextChars = _helper.FindNextCharacters(query, stationNames);

                return new StationSearchResult
                {
                    StationNames = stationNames,
                    NextCharacters = nextChars
                };
            }
            catch(Exception ex) 
            {
                _logger.Error($"Unexpected error while searching stations by {query} : {ex}");
                return new StationSearchResult
                {
                    StationNames = new List<string>(),
                    NextCharacters = new List<char>()
                };
            }
        }
    }
}
