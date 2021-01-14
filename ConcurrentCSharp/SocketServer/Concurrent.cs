using Sequential;
using System;
using System.IO;
using System.IO.Pipes;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;


//todo [Assignment]: add required namespaces

namespace Concurrent
{
    public class ConcurrentServer : SequentialServer
    {
        // todo [Assignment]: implement required attributes specific for concurrent server

        List<string> cmdList = new List<string>();
        string lastClientId;
        int cmdWithHighestVotes = 0;
        string cmdToExecute;

        public ConcurrentServer(Setting settings) : base(settings)
        {

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
                    if (true)
                    {

                    }
                }
            }
            catch (Exception e) { Console.Out.WriteLine("[Server] Preparation: {0}", e.Message); }
        }
        public override string processMessage(String msg)
        {
            // ClientId=1>ls -all

            //lastClientId = String.Parse(msg.Split(settings.command_msg_sep)[0].Split("=")[1]);
            var cmd = msg.Split(settings.command_msg_sep)[1];

            cmdList.Add(cmd);

            foreach (var vote in settings.votingList.Split(settings.commands_sep))
            {
                var countVoteOfCommand = cmdList.Where(x => x == vote).Select(x => x).ToList().Count();
                if (countVoteOfCommand > cmdWithHighestVotes)
                {
                    cmdToExecute = vote;
                    cmdWithHighestVotes = countVoteOfCommand;
                }
            }


            Console.WriteLine(cmdList);

            return "Your vote has been processed";
        }
    }
}