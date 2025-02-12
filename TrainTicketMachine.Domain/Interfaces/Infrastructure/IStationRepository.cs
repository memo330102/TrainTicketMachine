using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketMachine.Domain.Interfaces.Infrastructure
{
    public interface IStationRepository
    {
        Task<List<string>> SearchStationsAsync(string query);
        Task<List<char>> GetNextCharactersAsync(string query);
    }
}
