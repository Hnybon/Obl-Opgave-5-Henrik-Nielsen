using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Library;

namespace Obl_Opgave_5_Henrik_Nielsen
{
    internal class Client
    {
        private int port;

        public Client(int port)
        {
            this.port = port;
        }

        public void Start()
        {
            using (TcpClient cSocket = new TcpClient("localhost", port))
            using (Stream stream = cSocket.GetStream())
            using (StreamWriter toServer = new StreamWriter(stream))
            using (StreamReader fromServer = new StreamReader(stream))
            {
                Console.WriteLine();

                //String jsonStr = JsonConvert.SerializeObject(car);
                //Console.WriteLine($"Client json string: {jsonStr} and size:: {jsonStr.Length}");

                //toServer.WriteLine(jsonStr);
                toServer.Flush();
            }

        }
    }
}