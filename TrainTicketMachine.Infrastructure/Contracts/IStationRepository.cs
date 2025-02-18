using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicketMachine.Infrastructure.Models.Statıon;

namespace TrainTicketMachine.Infrastructure.Contracts
{
    public interface IStationRepository
    {
        Task<List<RemoteStationResponse>> SearchStationsAsync(string query);
    }
}
