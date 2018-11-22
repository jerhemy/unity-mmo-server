using System;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Zone.Zone zone = new Zone.Zone("startzone");

            zone.Start();

            Console.ReadLine();

            zone.Stop();

        }

    }
}