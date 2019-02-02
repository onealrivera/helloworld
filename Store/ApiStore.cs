using System;
using System.Collections.Generic;
using System.Net.Http;

using HelloWorld.Interface;

using Newtonsoft.Json;

namespace HelloWorld.Store
{
    public class ApiStore : IStore
    {
        private HttpClient _api;

        /// <summary>
        /// Connection string
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Api store constructor
        /// </summary>
        /// <param name="connString">Connection string</param>
        public ApiStore(string connString)
        {
            ConnectionString = connString;
            _api = new HttpClient() { BaseAddress = new Uri(connString) };
        }

        /// <summary>
        /// Deletes a specific instance of type T
        /// </summary>
        /// <typeparam name="T">Instance type</typeparam>
        /// <param name="id">Type id</param>
        public void Delete<T>(int id) where T : IEntity
        {
            _api.DeleteAsync(GetSafeUri<T>(id.ToString()));
        }

        ///// <summary>
        ///// Dispose this data store
        ///// </summary>
        //public void Dispose()
        //{
        //    _api?.Dispose();
        //    _api = null;
        //}

        /// <summary>
        /// Get a collection of type T
        /// </summary>
        /// <typeparam name="T">Instance type</typeparam>
        /// <returns>Collection of type T</returns>
        public IEnumerable<T> Get<T>()
        {
            var result = _api.GetAsync(GetSafeUri<T>()).Result;
            return JsonConvert.DeserializeObject<IEnumerable<T>>(
                result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Gets a specific instance of type T
        /// </summary>
        /// <typeparam name="T">Instance type</typeparam>
        /// <param name="id">Type id</param>
        /// <returns>Instance of type t</returns>
        public T Get<T>(int id) where T : IEntity
        {
            var result = _api.GetAsync(GetSafeUri<T>(id.ToString())).Result;
            return JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Saves a collection of type T
        /// </summary>
        /// <typeparam name="T">Instance type</typeparam>
        /// <param name="items">Instances to save</param>
        public void Save<T>(params T[] items) where T : IEntity
        {
            _api.PostAsync(GetSafeUri<T>(), new StringContent(
                JsonConvert.SerializeObject(items)));
        }

        private string GetSafeUri<T>(string id = null)
            => Uri.EscapeUriString(
                $"{ConnectionString}/{typeof(T).Name}/{id ?? string.Empty}");

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _api?.Dispose();
                    _api = null;
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
