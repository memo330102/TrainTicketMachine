using Microsoft.Extensions.Configuration;
using TrainTicketMachine.Application.Contracts;
using TrainTicketMachine.Domain.Entities;
using TrainTicketMachine.Domain.ValueObjects;
using TrainTicketMachine.Infrastructure.Contracts;
using TrainTicketMachine.Infrastructure.Models.Statıon;

namespace TrainTicketMachine.Application.Services
{
    public class StationService : IStationService
    {
        private readonly IStationRepository _repository;
        private readonly IStationCacheService _cache;
        private readonly string _stationListCacheKey;
        public StationService(IStationRepository repository, IStationCacheService cache, IConfiguration configuration)
        {
            _repository = repository;
            _cache = cache;
            _stationListCacheKey = configuration["StationData:StationListCacheKey"] ?? "";
        }

        public async Task<List<StationAggregate>> SearchStationsAsync(string query)
        {
            List<RemoteStationResponse> stations = new List<RemoteStationResponse>();
            var cachedData = await _cache.GetCacheValueAsync<List<RemoteStationResponse>>(_stationListCacheKey);
            if (cachedData != null)
            {
                stations = cachedData;
            }
            else
            {
                stations = await _repository.SearchStationsAsync(query);

                await _cache.SetCacheValueAsync(_stationListCacheKey, stations, TimeSpan.FromHours(1));
            }

            var stationnames = stations
                      .Where(n => n.stationName.StartsWith(query, StringComparison.OrdinalIgnoreCase))
                      .Select(n => new StationAggregate(n.stationName))
                      .ToList();

            var nextChars = await GetNextCharactersAsync(query, stationnames);

            return stationnames;
        }

        public async Task<List<char>> GetNextCharactersAsync(string query , List<StationAggregate> stationNames)
        {
            var nextChars = stationNames
                              .SelectMany(s => s.Name.Value.Length > query.Length
                                              ? new[] { s.Name.Value[query.Length] }
                                              : Array.Empty<char>())
                              .Distinct()
                              .OrderBy(c => c)
                              .ToList();

            return nextChars;
        }
    }
}
