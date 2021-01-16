using Sequential;
using System;
using System.IO;
using System.IO.Pipes;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;


//todo [Assignment]: add required namespaces

namespace Concurrent
{
    public class ConcurrentServer : SequentialServer
    {
        // todo [Assignment]: implement required attributes specific for concurrent server

        List<string> cmdList = new List<string>();
        string lastClientId;

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
                    if (this.numOfClients == settings.experimentNumberOfClients )
                    {
                        break;
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

            Console.WriteLine(cmdList);

            return "Your vote has been processed";
        }

        public void determineCommandWithMostVotesAndExecute()
        {
            string cmdToExecute = "";
            int cmdWithHighestVotes = 0;
            foreach (var vote in settings.votingList.Split(settings.commands_sep))
            {
                var countVoteOfCommand = cmdList.Where(x => x == vote).Select(x => x).ToList().Count();
                if (countVoteOfCommand > cmdWithHighestVotes)
                {
                    cmdToExecute = vote;
                    cmdWithHighestVotes = countVoteOfCommand;
                }
            }

            executeCommand(cmdToExecute);
        }

        private void executeCommand(string command)
        {
            //String bashCmd = "";
            //if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            //{

            //}
            /* ProcessStartInfo processInfo;
             Process process;

             processInfo = new ProcessStartInfo("/bin/bash", command);
             processInfo.CreateNoWindow = true;
             processInfo.UseShellExecute = true;

             process = Process.Start(processInfo);*/
            ProcessStartInfo procStartInfo = new ProcessStartInfo("/bin/bash", command);
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;

            Process proc = new Process();
            proc.StartInfo = procStartInfo;
            proc.Start();

            String result = proc.StandardOutput.ReadToEnd();
            System.Console.WriteLine(result);

        }
    }
}