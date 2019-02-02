using System;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace HelloWorld.Api
{
    /// <summary>
    /// Application entry
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Program entry point
        /// </summary>
        /// <param name="args">String arguments</param>
        public static void Main(string[] args)
            => BuildWebHost(args).Run();

        /// <summary>
        /// Build web host
        /// </summary>
        /// <param name="args">String arguments</param>
        /// <returns>Webhost instance</returns>
        public static IWebHost BuildWebHost(string[] args) 
            => WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
