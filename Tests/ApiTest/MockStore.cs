using System;
using System.Collections.Generic;
using System.Linq;

using HelloWorld.Interface;

namespace HelloWorld.Test.ApiTest
{
    public class MockStore : IStore
    {
        private Dictionary<Type, List<IEntity>> _cache =
            new Dictionary<Type, List<IEntity>>();

        public string ConnectionString => string.Empty;

        public MockStore(string connString) { }

        public void Delete<T>(int id) where T : IEntity
        {
            if (_cache.TryGetValue(typeof(T), out var collection ))
            {
                collection.RemoveAll(t => t.Id == id);
            }
        }

        public IEnumerable<T> Get<T>()
        {
            if(_cache.TryGetValue(typeof(T), out var collection))
            {
                return collection.Cast<T>();
            }
            return Array.Empty<T>();
        }

        public T Get<T>(int id) where T : IEntity
        {
            if (_cache.TryGetValue(typeof(T), out var collection))
            {
                return (T)collection.FirstOrDefault(i => i.Id == id);
            }
            return default(T);
        }

        public void Save<T>(params T[] items) where T : IEntity
        {
            if (!_cache.ContainsKey(typeof(T)))
            {
                _cache.Add(typeof(T), new List<IEntity>());
            }

            if(_cache.TryGetValue(typeof(T), out var collection))
            {
                collection.RemoveAll(c => items.Any(i => i.Id == c.Id));
                collection.AddRange(items.Select(i => i as IEntity));
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _cache?.Clear();
                    _cache = null;
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
