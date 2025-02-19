using Microsoft.Extensions.Configuration;
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

        private readonly string _stationListCacheKey;
        public StationService(IStationRepository repository, IStationCacheService cache, IStationHelper helper, IConfiguration configuration)
        {
            _repository = repository;
            _cache = cache;
            _helper = helper;
            _stationListCacheKey = configuration["StationData:StationListCacheKey"] ?? "";
        }

        public async Task<StationSearchResult> SearchStationsAsync(string query)
        {
            List<StationDataSourceResponse> stations = new List<StationDataSourceResponse>();
            var cachedData = await _cache.GetCacheValueAsync<List<StationDataSourceResponse>>(_stationListCacheKey);
            if (cachedData != null)
            {
                stations = cachedData;
            }
            else
            {
                stations = await _repository.SearchStationsAsync();

                await _cache.SetCacheValueAsync(_stationListCacheKey, stations, TimeSpan.FromHours(1));
            }

            var stationNames = _helper.FindStationsBySearch(query, stations);

            var nextChars = _helper.FindNextCharacters(query, stationNames);

            return new StationSearchResult
            {
                StationNames = stationNames,
                NextCharacters = nextChars
            };
        }
    }
}
