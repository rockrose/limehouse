using Microsoft.Extensions.Caching.Memory;

namespace LimehouseStudios.Services.Caching
{
    internal class Cache<T> where T : class
    {
        private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        public void Insert(string key, T item)
        {
            _cache.Set(key, item);
        }

        public T? Get(string key)
        {
            if (_cache.TryGetValue(key, out T item))
            {
                return item;
            }
            return default;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
