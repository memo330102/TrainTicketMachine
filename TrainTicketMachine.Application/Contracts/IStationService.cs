using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicketMachine.Domain.Entities;
using TrainTicketMachine.Domain.Models.Station;

namespace TrainTicketMachine.Application.Contracts
{
    public interface IStationService
    {
        public Task<StationSearchResult> SearchStationsAsync(string query);
    }
}
