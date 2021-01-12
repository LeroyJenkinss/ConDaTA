using Sequential;
using System;
using System.IO;
using System.IO.Pipes;
using System;
using System.Net;
using System.Net.Sockets;


//todo [Assignment]: add required namespaces

namespace Concurrent
{
    public class ConcurrentServer : SequentialServer
    {
        // todo [Assignment]: implement required attributes specific for concurrent server

        public ConcurrentServer(Setting settings) : base(settings)
        {

            // todo [Assignment]: implement required code

        }
        public override void prepareServer()
        {
            Console.WriteLine("[Server] is ready to start ...");
            try
            {
                localEndPoint = new IPEndPoint(this.ipAddress, settings.serverPortNumber);
                listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(localEndPoint);
                listener.Listen(settings.serverListeningQueue);

                while (true)
                {
                    Console.WriteLine("Waiting for incoming connections ... ");
                    Socket connection = listener.Accept();
                    this.numOfClients++;
                    this.handleClient(connection);
                }
            }
            catch (Exception e) { Console.Out.WriteLine("[Server] Preparation: {0}", e.Message); }
        }
        public override string processMessage(String msg)
        {
            // todo [Assignment]: implement required code
            return "";
        }
    }
}