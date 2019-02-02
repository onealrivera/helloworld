using System;
using System.Collections.Generic;

namespace HelloWorld.Interface
{
    /// <summary>
    /// Store interface
    /// </summary>
    public interface IStore : IDisposable
    {
        /// <summary>
        /// Connection string
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Deletes a specific instance of type T
        /// </summary>
        /// <typeparam name="T">Instance type</typeparam>
        /// <param name="id">Type id</param>
        void Delete<T>(int id) where T : IEntity;

        /// <summary>
        /// Get a collection of type T
        /// </summary>
        /// <typeparam name="T">Instance type</typeparam>
        /// <returns>Collection of type T</returns>
        IEnumerable<T> Get<T>();

        /// <summary>
        /// Gets a specific instance of type T
        /// </summary>
        /// <typeparam name="T">Instance type</typeparam>
        /// <param name="id">Type id</param>
        /// <returns>Instance of type t</returns>
        T Get<T>(int id) where T : IEntity;

        /// <summary>
        /// Saves a collection of type T
        /// </summary>
        /// <typeparam name="T">Instance type</typeparam>
        /// <param name="items">Instances to save</param>
        void Save<T>(params T[] items) where T : IEntity;
    }
}
