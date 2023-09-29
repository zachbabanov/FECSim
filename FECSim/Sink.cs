using System.Net;
using System.Net.Sockets;

namespace FECSim
{

    /// <summary>
    /// Set of sockets for recieving udp/tcp packets
    /// of encoded data and decoding it until got full data
    /// </summary>
    public class Sink
    {
        /// <summary>
        /// Buffer to store udp packet info. 
        /// First byte is number of sequences in single packet,
        /// Second byte is sequence lenght
        /// </summary>
        private readonly byte[] tcpPacketInfoBuffer = new byte[2];
        private int numberOfSequences;
        private int sequenceLenght;

        public IPAddress sinkIpAddress;

        /// <summary>
        /// Tcp Socket for geting end to end delivery confimation 
        /// after you get enough sequences to decode full data
        /// </summary>
        private readonly Socket sourceConfirmSocket;
        private readonly IPEndPoint sourceConfirmEndPoint;

        private readonly Socket sinkSocket;
        private readonly IPEndPoint sinkEndPoint;

        private readonly TcpListener portListenerInstance = new(IPAddress.Loopback, 0);
        private int NextFreePort()
        {
            portListenerInstance.Start();
            int port = ((IPEndPoint)portListenerInstance.LocalEndpoint).Port;
            portListenerInstance.Stop();
            return port;
        }

        public IPEndPoint GetTcpEndPoint()
        {
            return sourceConfirmEndPoint;
        }

        public IPEndPoint GetUdpEndPoint()
        {
            return sinkEndPoint;
        }

        /// <summary>
        /// Creates a sink object for existing source object
        /// so tcp sockets of sink are paired to source one's
        /// </summary>
        /// <param name="source">Provide an existing source object to pair tcp sockets</param>
        public Sink(Source source)
        {
            sinkIpAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1];
            sinkSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sinkEndPoint = new IPEndPoint(sinkIpAddress, NextFreePort());

            sourceConfirmSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sourceConfirmEndPoint = source.GetTcpEndPoint();
        }

        public void StartUdp()
        {
            sinkSocket.Bind(sinkEndPoint);
        }

        public void StartTcp()
        {
            sourceConfirmSocket.Connect(sourceConfirmEndPoint);
        }

        public void RecievePacketInformation()
        {
            sourceConfirmSocket.Receive(tcpPacketInfoBuffer);

            numberOfSequences = Convert.ToInt32(tcpPacketInfoBuffer[0]);
            sequenceLenght = Convert.ToInt32(tcpPacketInfoBuffer[1]);
        }

        /// <summary>
        /// Recieve a specified sized array of bytes from existing source object
        /// </summary>
        /// <param name="array">byte array of specified size</param>
        /// <param name="source">Existing source object, which was paired on constructor</param>
        public void RecieveFrom(byte[] array, Source source)
        {
            EndPoint reference = source.GetUdpEndPoint();

            if (sinkSocket.ReceiveFrom(array, array.Length, SocketFlags.None, ref reference) != 0)
            {
                byte[] confirm = { 0 };
                sourceConfirmSocket.Send(confirm);
            }
            else
            {
                byte[] confirm = { 1 };
                sourceConfirmSocket.Send(confirm);
            }
        }

        public void Stop()
        {
            sourceConfirmSocket.Close();
            sourceConfirmSocket.Dispose();

            sinkSocket.Close();
            sinkSocket.Dispose();
        }
    }
}