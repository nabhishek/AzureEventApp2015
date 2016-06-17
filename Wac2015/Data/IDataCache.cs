using System;
using System.Threading.Tasks;

namespace Wac2015.Data
{
    public interface IDataCache
    {
        Task<T> TryGetAsync<T>(Guid id) where T : class;
        Task<T> PutAsync<T>(Guid id, T state) where T : class;
        Task RemoveAsync<T>(Guid id) where T : class;
    }
}