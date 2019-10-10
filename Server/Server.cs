using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Library;
using Newtonsoft.Json;

namespace OblServer
{
    internal class Server
    {
        private int port;

        private static List<Bog> bøger = new List<Bog>()
        {
            new Bog("Den lille hvide kanin", "Anonym", 178, "1234567890123"),
            new Bog("Den lille sorte kanin", "Også Anonym", 187, "2345678901234"),
            new Bog("Programmering for de tanketomme", "Anders Levinski", 999, "3456789012345"),
            new Bog("Hansi Hinterseer is a Freaky alien genotype", "Naj Neslien", 123, "4567890123456"),
            new Bog("Kaniner under kniven: 101 opskrifter", "Hanne Stegemor", 631, "5678901234567")
        };

        public Server(int port)
        {
            this.port = port;
        }

        public void Start()
        {
            TcpListener server = new TcpListener(IPAddress.Any, port);
            server.Start();

            while (true)
            {
                TcpClient clientSocket = server.AcceptTcpClient();
                Task.Run(() =>
                {
                    TcpClient socket = clientSocket;
                    DoClient(socket);
                });
            }
        }

        private void DoClient(TcpClient socket)
        {
            using (NetworkStream stream = socket.GetStream())
            using (StreamReader fromClient = new StreamReader(stream))
            using (StreamWriter toClient = new StreamWriter(stream))
            {
                //toClient.AutoFlush = true;
                //string Fuckit = "Kaniner under kniven: 101 opskrifter", "Hanne Stegemor", 631, "5678901234567";
                //toClient.WriteLine(Fuckit);
                string strFromClient = fromClient.ReadLine();
                string data = fromClient.ReadLine();
                if (strFromClient != null)
                {
                    strFromClient = strFromClient.ToLower();
                }
                //string response = "";

                if (String.IsNullOrWhiteSpace(strFromClient))
                {
                    //error
                }

                if (strFromClient == "hentalle")
                {
                    DoHentAlle(toClient);
                    ////string jsonstr = JsonConvert.SerializeObject(bøger);
                    ////toClient.WriteLine(jsonstr);
                    ////Console.WriteLine(jsonstr);
                    //foreach (Bog i in bøger)
                    //{
                    //    string jsonstr = JsonConvert.SerializeObject(i);
                    //    toClient.WriteLine();
                    //    toClient.WriteLine(jsonstr);
                    //    Console.WriteLine(jsonstr);
                    //}
                }

                else if (strFromClient == "hent")
                {
                    DoHentEn(data, toClient);
                    //string Jsonstr = JsonConvert.SerializeObject(bøger.Find(i => i.Isbn13 == data));
                    //toClient.WriteLine(Jsonstr);
                    //Console.WriteLine(Jsonstr);
                }

                else if (strFromClient == "gem")
                {
                    DoGem(data, toClient);
                    //bøger.Add(JsonConvert.DeserializeObject<Bog>(data));
                }

                else
                {
                    //error
                }
            }
        }

        private void DoHentAlle(StreamWriter toClient)
        {
            foreach (Bog i in bøger)
            {
                string jsonstr = JsonConvert.SerializeObject(i);
                toClient.WriteLine();
                toClient.WriteLine(jsonstr);
                Console.WriteLine(jsonstr);
                toClient.Flush();
            }
        }

        private void DoHentEn(string data, StreamWriter toClient)
        {
            string Jsonstr = JsonConvert.SerializeObject(bøger.Find(i => i.Isbn13 == data));
            toClient.WriteLine(Jsonstr);
            Console.WriteLine(Jsonstr);
            toClient.Flush();
        }

        private void DoGem(string nyBog, StreamWriter toClient)
        {
            bøger.Add(JsonConvert.DeserializeObject<Bog>(nyBog));
        }
    }
}