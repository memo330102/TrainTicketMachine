using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicketMachine.Domain.Interfaces.Application;
using TrainTicketMachine.Domain.Interfaces.Infrastructure;

namespace TrainTicketMachine.Application
{
    public class StationService : IStationService
    {
        private readonly IStationRepository _repository;

        public StationService(IStationRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<string>> SearchStationsAsync(string query)
        {
            return await _repository.SearchStationsAsync(query);
        }

        public async Task<List<char>> GetNextCharactersAsync(string query)
        {
            return await _repository.GetNextCharactersAsync(query);
        }
    }
}
