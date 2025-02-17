using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketMachine.Infrastructure.Contracts
{
    public interface IStationCacheService
    {
        Task<T> GetCacheValueAsync<T>(string key);
        Task SetCacheValueAsync<T>(string key, T value, TimeSpan expiration);
        Task RemoveCacheValueAsync(string key);
    }
}
