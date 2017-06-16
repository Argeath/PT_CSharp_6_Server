using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PT_CSharp_6_Server
{
    class Program
    {
        public static int port = 11000;
        static void Main(string[] args)
        {
            Socket sending_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress send_to_address = IPAddress.Parse("192.168.2.255");
            IPEndPoint sending_end_point = new IPEndPoint(send_to_address, 11000);
            
            ConnectionListener.init();
        }
    }
}
