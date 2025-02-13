using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicketMachine.Domain.Entities;

namespace TrainTicketMachine.Application.Contracts
{
    public interface IStationService
    {
        public Task<List<StationAggregate>> SearchStationsAsync(string query);
        public Task<List<char>> GetNextCharactersAsync(string query);
    }
}
