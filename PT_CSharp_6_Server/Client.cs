using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PT_CSharp_6_Server
{
    class Client
    {
        public byte id;
        public IPEndPoint ip;

        public static ConcurrentDictionary<byte, Client> clients;
        public static byte nextId = 0;

        public void sendBytes(byte[] data)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                socket.SendTo(data, ip);
            }
        }

        public void sendString(string str)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(str);
            sendBytes(buffer);
        }

        public static Client findClientByIP(IPEndPoint ip)
        {
            return clients.FirstOrDefault(c => Equals(c.Value.ip, ip)).Value;
        }

        public static void removeClientByIP(IPEndPoint ip)
        {
            Client c = findClientByIP(ip);
            clients.TryRemove(c.id, out c);
        }
    }
}
