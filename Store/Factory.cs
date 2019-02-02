using System;
using System.Configuration;

using HelloWorld.Interface;

namespace HelloWorld.Store
{
    /// <summary>
    /// Factory store
    /// </summary>
    public static class Factory
    {
        private const string DefaultConnection = "Default";

        /// <summary>
        /// Gets the store instance based on the connection string defined
        /// </summary>
        /// <param name="connectionName">Connection name to reference</param>
        /// <returns>IStore implementation</returns>
        public static IStore GetStore(string connectionName = DefaultConnection)
        {
            var conn = ConfigurationManager.ConnectionStrings[connectionName];
            var store = Activator.CreateInstance(Type.GetType(conn.ProviderName), conn.ConnectionString) as IStore;
            
            // automatically dispose when application shuts down
            AppDomain.CurrentDomain.ProcessExit += (s, e) => store?.Dispose();
            return store;
        }
    }
}
