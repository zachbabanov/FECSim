using System.Net;
using System.Net.Sockets;

/// <summary>
/// Set of sockets to send data which was encoded
/// with local endoder
/// </summary>
public class Source
{
    /// <summary>
    /// Buffer to store delivery confirm flag
    /// false("0") is  
    /// true("1") is 
    /// </summary>
    private readonly byte[] tcpDeliveryConfirmBuffer = new byte[1];

    public IPAddress sourceIpAddress;

    /// <summary>
    /// Tcp Socket for geting end to end delivery confimation 
    /// after you get enough sequances to decode full data
    /// </summary>
    private Socket sinkConfirmSocket;
    private readonly IPEndPoint sinkConfirmEndPoint;

    private readonly EndPoint sourceEndPoint;
    private readonly Socket sourceSocket;

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
        return sinkConfirmEndPoint;
    }

    public EndPoint GetUdpEndPoint()
    {
        return sourceEndPoint;
    }

    public Source()
    {
        sourceIpAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1];
        sourceSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        sourceEndPoint = new IPEndPoint(sourceIpAddress, NextFreePort());

        sinkConfirmSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        sinkConfirmEndPoint = new IPEndPoint(sourceIpAddress, NextFreePort());
    }

    public void StartUdp(Sink sink)
    {
        sinkConfirmSocket.Bind(sink.GetTcpEndPoint());
        sinkConfirmSocket.Listen();

        sourceSocket.Bind(sourceEndPoint);
    }

    public void AcceptTcpConnection()
    {
        sinkConfirmSocket = sinkConfirmSocket.Accept();
    }

    public int GetDeliveryConfirm()
    {
        sinkConfirmSocket.Receive(tcpDeliveryConfirmBuffer);

        return Convert.ToInt32(tcpDeliveryConfirmBuffer[0]);
    }

    /// <summary>
    /// Send a number of sequences and their lenght to a paired sink tcp socket
    /// </summary>
    /// <param name="numberOfSeq">Number of encoded sequences in single array</param>
    /// <param name="seqLen">Lenght of each single sequence</param>
    public void SendPacketInformation(int numberOfSeq, int seqLen)
    {
        byte[] tcpPacketInfoBuffer = new byte[2];

        tcpPacketInfoBuffer[0] = Convert.ToByte(numberOfSeq);
        tcpPacketInfoBuffer[1] = Convert.ToByte(seqLen);

        sinkConfirmSocket.Send(tcpPacketInfoBuffer);
    }

    /// <summary>
    /// Send data to specified sink object
    /// </summary>
    /// <param name="buffer">Byte array of data, without specified number of sequences and sequence lenght</param>
    /// <param name="sink">Existing sink object with paired tcp socket</param>
    public void SendTo(byte[] buffer, Sink sink)
    {
        sourceSocket.SendTo(buffer, sink.GetUdpEndPoint());
    }

    /// <summary>
    /// Send data to specified sink object with udp packet information.
    /// Is equivalent to consistent call of SendPacketInformation and SendTo functions
    /// </summary>
    /// <param name="buffer">Byte array of data. Contain more than one encoded sequence</param>
    /// <param name="numberOfSeq">Number of encoded sequences in buffer</param>
    /// <param name="seqLen">Lenght of each single sequence</param>
    /// <param name="sink">Existing sink object with paired tcp socket</param>
    public void SendTo(byte[] buffer, int numberOfSeq, int seqLen, Sink sink)
    {
        SendPacketInformation(numberOfSeq, seqLen);
        sourceSocket.SendTo(buffer, sink.GetUdpEndPoint());
    }

    public void Stop()
    {
        sinkConfirmSocket.Close();
        sinkConfirmSocket.Dispose();

        sourceSocket.Close();
        sourceSocket.Dispose();
    }
}
