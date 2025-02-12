using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketMachine.Domain.Interfaces.Application
{
    public interface IStationService
    {
        public Task<List<string>> SearchStationsAsync(string query);
        public Task<List<char>> GetNextCharactersAsync(string query);
    }
}
