﻿using TrainTicketMachine.Application.Contracts;
using TrainTicketMachine.Domain.Entities;
using TrainTicketMachine.Infrastructure.Contracts;

namespace TrainTicketMachine.Application.Services
{
    public class StationService : IStationService
    {
        private readonly IStationRepository _repository;

        public StationService(IStationRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<StationAggregate>> SearchStationsAsync(string query)
        {
            var stations = await _repository.SearchStationsAsync(query);
            return stations.Select(s => new StationAggregate(s)).ToList();
        }

        public async Task<List<char>> GetNextCharactersAsync(string query)
        {
            var stations = await _repository.SearchStationsAsync(query);
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
