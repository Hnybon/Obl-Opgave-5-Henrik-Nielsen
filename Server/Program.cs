using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace OblServer
{
    class Program
    {
        
        private const int port = 4646;

        static void Main(string[] args)
        {
            Server server = new Server(port);
            server.Start();

            Console.ReadLine();
        }
    }
}