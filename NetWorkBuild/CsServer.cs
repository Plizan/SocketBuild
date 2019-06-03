using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace NetWorkBuild
{
    class CsServer
    {
        TcpListener server;
        TcpClient client;
        StreamReader reader;
        StreamWriter writer;
        NetworkStream stream;
        Thread receive;
        bool isConnected;

        public CsServer()
        {
            Thread listen = new Thread(Listen);
            listen.Start();
        }

        private void Listen()
        {
            try
            {
                server = new TcpListener(IPAddress.Any, 9999);
                server.Start();
                Console.WriteLine("Server Start");

                client = server.AcceptTcpClient();
                isConnected = true;
                Console.WriteLine("Connect Success");

                stream = client.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);

                receive = new Thread(new ThreadStart(receive));
                receive.Start();
            }
            catch(SocketException se) { Console.WriteLine(se.Message); }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
