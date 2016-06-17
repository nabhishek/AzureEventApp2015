using System;
using System.Threading.Tasks;

namespace Wac2015.Data
{
    class MemoryDataCache : IDataCache
    {
        private readonly IDataCache _durableCache;
        private readonly ILogger _logger;
        private ImmutableDictionary<string, object> _cache;

        public MemoryDataCache(IDataCache durableCache, ILogger logger)
        {
            if (durableCache == null) throw new ArgumentNullException("durableCache");
            _durableCache = durableCache;
            _logger = logger.CreateLogger("MemoryDataCache");
            _cache = ImmutableDictionary.Create<string, object>();
        }

        public async Task<T> TryGetAsync<T>(Guid id) where T : class
        {
            var key = GetCacheKey<T>(id);
            object state;
            if (_cache.TryGetValue(key, out state))
            {
                //                _logger.Info(string.Format("HIT: mem:{0}/{1:n}", typeof(T).Name, id));
                return state as T;
            }

            //            _logger.Info(string.Format("MISS: mem:{0}/{1:n}", typeof(T).Name, id));

            bool remove = false;

            T foo = null;
            try
            {
                foo = await _durableCache.TryGetAsync<T>(id);
            }
            catch (Exception e)
            {
                remove = true;
                _logger.Warn(e.Message);
            }

            if (remove)
            {
                await _durableCache.RemoveAsync<T>(id);
            }

            return foo != null ? PutToMemory(id, foo) : null;
        }

        public Task<T> PutAsync<T>(Guid id, T state) where T : class
        {
            PutToMemory(id, state);
            try
            {
                return _durableCache.PutAsync(id, state);
            }
            catch (Exception e)
            {
                _logger.Warn(e);
                return Task.FromResult(state);
            }
        }

        private T PutToMemory<T>(Guid id, T state) where T : class
        {
            var key = GetCacheKey<T>(id);
            //            _logger.Info(string.Format("PUT: mem:{0}/{1:n}", typeof (T).Name, id));
            _cache = _cache.Remove(key);
            _cache = _cache.Add(key, state);
            return state;
        }

        public Task RemoveAsync<T>(Guid id) where T : class
        {
            //            _logger.Info(string.Format("REMOVE: mem:{0}/{1:n}", typeof(T).Name, id));
            _cache = _cache.Remove(GetCacheKey<T>(id));
            try
            {
                return _durableCache.RemoveAsync<T>(id);
            }
            catch (Exception e)
            {
                _logger.Warn(e);
                return Task.FromResult((object)null);
            }
        }
        private string GetCacheKey<T>(Guid id)
        {
            return string.Format("{0}{1:n}", typeof(T).Name, id);
        }

    }
}