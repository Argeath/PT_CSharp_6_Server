using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PT_CSharp_6;

namespace PT_CSharp_6_Server
{
    class ConnectionListener
    {
        private static int port = 11000;
        public static bool done = false;

        public void loop()
        {
            while (true)
            {
                UdpClient listener = new UdpClient(port);
                IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, port);
                
                string receivedCommand;
                byte[] receivedBytes;
                try
                {
                    while (!done)
                    {
                        receivedBytes = listener.Receive(ref groupEP);
                        Console.WriteLine("Received a broadcast from {0}", groupEP.ToString());

                        receivedCommand = Encoding.ASCII.GetString(receivedBytes, 0, receivedBytes.Length);
                        Console.WriteLine("COMMAND: \n{0}\n\n", receivedCommand);

                        if (receivedCommand == "connect")
                        {
                            Client client = new Client
                            {
                                id = Client.nextId,
                                ip = groupEP
                            };

                            if (Client.clients.TryAdd(Client.nextId, client))
                            {
                                Console.WriteLine("{0} connected. ID: {1}", groupEP, Client.nextId);
                                Client.nextId++;
                                client.sendString("11001");
                            }
                        }
                        else if (receivedCommand == "disconnect")
                        {
                            Client.removeClientByIP(groupEP);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                listener.Close();
            }
        }

        public static void init()
        {
            ConnectionListener listener = new ConnectionListener();
            Thread t = new Thread(listener.loop);
            t.Start();
        }
    }
}
