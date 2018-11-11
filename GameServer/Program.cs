using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using NetcodeIO.NET;

namespace GameServer
{
    class Program
    {
        private static ConnectionManager _connectionManager;
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
 
            var config = builder.Build();
 
            var appConfig = config.Get<ServerConfig>();
            
            _connectionManager = new ConnectionManager();
//            Console.WriteLine($"Max Clients : {appConfig.maxClients}");
//            Console.WriteLine($"Public Address : {appConfig.publicAddress}");
//            Console.WriteLine($"Port : {appConfig.port}");
//            Console.WriteLine($"ProtocolID : {appConfig.protocolID}");
//            Console.WriteLine($"PrivateKey : {appConfig.privateKey}");

            _connectionManager.StartServer();
            
            
            Console.ReadLine();
            
            _connectionManager.StopServer();
        }


    }
}