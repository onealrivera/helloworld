using System;
using System.Net.WebSockets;

using HelloWorld.Entity;
using HelloWorld.Interface;
using HelloWorld.Store;

namespace HelloWorld.Cli
{
    class Program
    {
        private static IStore _storage = Factory.GetStore();

        static void Main(string[] args)
        {
            foreach(var msg in _storage.Get<Message>())
            {
                Console.WriteLine($"{msg.Data}");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit ... ");
            Console.ReadKey();
        }
     }
}
