using System;
using System.Collections.Generic;
using System.Threading;
using Sequential;

namespace Concurrent
{
    public class ConcurrentClient : SimpleClient
    {
        public Thread workerThread;
        public string cmd; 

        public ConcurrentClient(int id, Setting settings) : base(id, settings)
        {
            prepareClient();
        }
        public void run()
        {
            communicate();
            Thread.Sleep(settings.delayForTermination);
        }
    }
    public class ConcurrentClientsSimulator : SequentialClientsSimulator
    {
        private ConcurrentClient[] clients;

        public ConcurrentClientsSimulator() : base()
        {
            Console.Out.WriteLine("\n[ClientSimulator] Concurrent simulator is going to start with {0}", settings.experimentNumberOfClients);
            clients = new ConcurrentClient[settings.experimentNumberOfClients];
        }

        public void ConcurrentSimulation()
        {
            try
            {
                // todo [Assignment]: implement required code
                //{
                //workerThread.
                //clients[index] = new ConcurrentClient(index, settings);
                //clients[1].run();

                //Thread mainThread = new Thread();

                var listThread = new List<Thread>();


                for (int index = 0; index < settings.experimentNumberOfClients; index++)
                {
                    listThread.Add(new Thread(() => new ConcurrentClient(index, settings).run()));
                    listThread[index].Start();
                }

                foreach(var client in listThread)
                    client.Join();

                //ConcurrentClient c;

                //Thread printingThread = new Thread(() => { c = new ConcurrentClient(c, settings).run(settings.experimentNumberOfClients, settings.delayForTermination)} );
                //Thread clientThread = new Thread(() => { });
                // check: how we get return value of a thread

                
                //client.Start();
                //clientThread.Start();
                //}
            }
            catch (Exception e)
            { Console.Out.WriteLine("[Concurrent Simulator] {0}", e.Message); }
        }
    }
}
