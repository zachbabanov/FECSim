using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms.VisualStyles;

namespace FECSim
{
    internal class Sink
    {
        Socket sinkSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        IPEndPoint ipEndPoint;
        public byte[] recievedData = new byte[byte.MaxValue];

        public Sink(int port)
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[3];
            this.ipEndPoint = new IPEndPoint(ipAddr, port);
        }

        public void Start()
        {
            try
            {
                sinkSocket.Bind(ipEndPoint);
                sinkSocket.Shutdown(SocketShutdown.Send);
                sinkSocket.Accept();
                sinkSocket.Listen();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void RecieveByteArray()
        {
            try
            {
                sinkSocket.Receive(recievedData);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Stop() 
        { 
            sinkSocket?.Close();
        }
    }
}
