using System;
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
    class DrawingListener
    {
        private static int port = 11001;
        public static bool done = false;
        public void loop()
        {
            while (true)
            {
                UdpClient listener = new UdpClient(port);
                IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, port);
                
                DrawingPacket receivedPacket;
                byte[] receivedBytes;
                try
                {
                    while (!done)
                    {
                        receivedBytes = listener.Receive(ref groupEP);
                        Console.WriteLine("Received a broadcast from {0}", groupEP.ToString());
                        receivedPacket = DrawingPacket.FromBytes(receivedBytes);

                        Console.WriteLine("X: {0} Y: {1}\n\n", receivedPacket.posX, receivedPacket.posY);

                        // TODO: Send to all
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
            DrawingListener listener = new DrawingListener();
            Thread t = new Thread(listener.loop);
            t.Start();
        }
    }
}
