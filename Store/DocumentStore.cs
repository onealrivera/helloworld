using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using HelloWorld.Interface;

using Newtonsoft.Json;

namespace HelloWorld.Store
{
    /// <summary>
    /// Document storage
    /// </summary>
    public class DocumentStore : IStore
    {
        private string _connString;

        /// <summary>
        /// Connection string
        /// </summary>
        public string ConnectionString
        {
            get => _connString;
            private set
            {
                _connString = Path.IsPathRooted(value) ? value :
                    $@"{AppContext.BaseDirectory}\{value}";
                if (!Directory.Exists(_connString))
                {
                    Directory.CreateDirectory(_connString);
                }
            }
        }

        /// <summary>
        /// Document store constructor
        /// </summary>
        /// <param name="connString">Connection string</param>
        public DocumentStore(string connString)
            => ConnectionString = connString;

        /// <summary>
        /// Deletes a specific instance of type T
        /// </summary>
        /// <typeparam name="T">Instance type</typeparam>
        /// <param name="id">Type id</param>
        public void Delete<T>(int id) where T : IEntity
        {
            var data = new List<T>(Get<T>());

            var item = data.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                data.Remove(item);
                Save<T>(ConnectionString, data);
            }
        }

        /// <summary>
        /// Dispose this store instance
        /// </summary>
        public void Dispose()
        {
            // nothing to dispose at the moment
        }

        /// <summary>
        /// Get a collection of type T
        /// </summary>
        /// <typeparam name="T">Instance type</typeparam>
        /// <returns>Collection of type T</returns>
        public IEnumerable<T> Get<T>()
        {
            var fileName = FileSource<T>(ConnectionString);
            return File.Exists(fileName) ?  
                JsonConvert.DeserializeObject<IEnumerable<T>>(
                    File.ReadAllText(fileName))
                    : new T[] { };
        }

        /// <summary>
        /// Gets a specific instance of type T
        /// </summary>
        /// <typeparam name="T">Instance type</typeparam>
        /// <param name="id">Type id</param>
        /// <returns>Instance of type t</returns>
        public T Get<T>(int id) where T : IEntity
            => Get<T>().FirstOrDefault(i => i.Id == id);

        /// <summary>
        /// Saves a collection of type T
        /// </summary>
        /// <typeparam name="T">Instance type</typeparam>
        /// <param name="items">Instances to save</param>
        public void Save<T>(params T[] items) where T : IEntity
        {
            var data = new List<T>(Get<T>());
            data.RemoveAll(d => items.Any(i => i.Id == d.Id));
            data.AddRange(items);
            Save<T>(ConnectionString, data);
        }

        private static string FileSource<T>(string basePath)
            => $@"{basePath}\{typeof(T).Name}.json";

        private static void Save<T>(string basePath, IEnumerable<T> data)
            => File.WriteAllText(
                FileSource<T>(basePath), 
                JsonConvert.SerializeObject(data));
             
    }
}
