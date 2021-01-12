using System;
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
            // todo [Assignment]: implement required code
            prepareClient();
            communicate();
            
        }
        public void run()
        {
            // todo [Assignment]: implement required code
            // send message to server
            
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
                for (int index = 1; index <= settings.experimentNumberOfClients; index++)
                {
                    clients[index] = new ConcurrentClient(index, settings);
                    clients[index].run();
                }
            }
            catch (Exception e)
            { Console.Out.WriteLine("[Concurrent Simulator] {0}", e.Message); }
        }
    }
}
