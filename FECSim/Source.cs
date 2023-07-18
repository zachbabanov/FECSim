using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FECSim
{
    internal class Source
    {
        Socket sinkSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        IPEndPoint ipEndPoint;

        public Source(int port)
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[3];
            this.ipEndPoint = new IPEndPoint(ipAddr, port);
        }

        public void Start()
        {
            try
            {
                sinkSocket.Shutdown(SocketShutdown.Receive);
                sinkSocket.Connect(ipEndPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SendByteArray(byte[] byteArray)
        {
            try
            {
                sinkSocket.Send(byteArray);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SendFile(string fileName)
        {
            try 
            { 
                sinkSocket.SendFile(fileName);
            } 
            catch (Exception ex) 
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
