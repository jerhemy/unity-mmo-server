using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using GameServer.GameServer;
using GameServer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GameServer
{
    class Program
    {
        private static ConnectionManager _connectionManager;

        private static List<Entity> entities = new List<Entity>();
        
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
 
            var config = builder.Build();
 
            var appConfig = config.Get<ServerConfig>();
            
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            //configure console logging
            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            
            logger.LogDebug("Starting application");

            logger.LogInformation("Starting application");
                
            Random rnd = new Random();
            for (int x = 0; x < 1; x++)
            {
                var id = x + 2;
                entities.Add(new Entity
                {
                    id = id , 
                    name = @"Text" + x, 
                    loc = new SimpleVector3(0, 0, 0), 
                    orientation = 0,
                    waypoints = new []
                    {
                        new SimpleVector3(-5f, 0, 5f),
                        new SimpleVector3(-5f, 0, 35f),
                        new SimpleVector3(-25f, 0, 35f),
                        new SimpleVector3(-35f, 0, 5f)
                    }
                });
            }
            //entityRepository = new EntityRepository();
            
            _connectionManager = new ConnectionManager();
//            Console.WriteLine($"Max Clients : {appConfig.maxClients}");
//            Console.WriteLine($"Public Address : {appConfig.publicAddress}");
//            Console.WriteLine($"Port : {appConfig.port}");
//            Console.WriteLine($"ProtocolID : {appConfig.protocolID}");
//            Console.WriteLine($"PrivateKey : {appConfig.privateKey}");

            _connectionManager.StartServer();
            Task.Run(() => {
                GameLoop();
            });
            
            Console.ReadLine();
            
            _connectionManager.StopServer();
        }

        private static void GameLoop()
        {
            while (true)
            {             
                entities.ForEach(e => { _connectionManager.SendAll(e, MessageType.Entity); });
                Thread.Sleep(600);
            }
        }
    }
}