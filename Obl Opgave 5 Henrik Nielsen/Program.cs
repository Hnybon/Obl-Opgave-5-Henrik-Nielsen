using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obl_Opgave_5_Henrik_Nielsen
{
    class Program
    {
        private const int port = 4646;

        static void Main(string[] args)
        {
            Client client = new Client(port);
            client.Start();

            Console.ReadLine();
        }
    }
}