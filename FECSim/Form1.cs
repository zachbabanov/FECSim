using System.Diagnostics;
using System.Net.Sockets;

namespace FECSim
{
    public partial class Form1 : Form
    {
        Task SendAndRecieve(int index, byte[] packet)
        {
            Debug.WriteLine("Task {0} started", index);
            byte[] packetRecieved = new byte[packet.Length];

            Source source = new();
            Sink sink = new(source);

            sink.StartUdp();
            source.StartUdp(sink);
            Debug.WriteLine("Source and Sink {0} started", index);

            sink.StartTcp();

            source.AcceptTcpConnection();

            source.SendTo(packet, sink);
            Debug.WriteLine("Packet {0} sent", index);
            sink.RecieveFrom(packetRecieved, source);
            Debug.WriteLineIf(packetRecieved.Length != 0, String.Format("Packet {0} recieved", index));

            Task.Factory.StartNew(() => 
            {
                try 
                { 
                    packetsRecieved.Add(index, packetRecieved); 
                }
                catch (ArgumentException)
                { 
                    packetsRecieved.Remove(index); packetsRecieved.Add(index, packetRecieved); 
                };
            }).Wait();
            Debug.WriteLine("Packet {0} added to list", index);

            sink.Stop();
            source.Stop();

            return Task.CompletedTask;
        }

        private readonly Dictionary<int, byte[]> packetsRecieved = new();
        private EncodedPacket? encodedPacket;
        private readonly Encoder encoder;

        private string? choosedFilePath;
        private string? choosedFileName;
        private string? directoryName;

        private int packetSize;
        private readonly int[] packetSizeComboBoxCollection = { 64, 128, 256, 512, 1024, 2048, 4096,
                                                       8192, 16384, 32798, 65576};

        private int amountOfRecievers = 8;

        public Form1()
        {
            Debug.WriteLine("Form initialazed");
            encoder = new(8);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            packetSizeComboBox.SelectedIndex = 0;
            Debug.WriteLine("packetSizeComboBox value is set to default(0)");
            packetSize = packetSizeComboBoxCollection[packetSizeComboBox.SelectedIndex];
            recieversAmountTextBox.Text = "8";
            Debug.WriteLine("packetSizeComboBox value is set to default(8)");
            sendFileButton.Enabled = false;
            Debug.WriteLine("sendFileButton is set to default(disabled)");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new();
            DialogResult result = openFileDialog1.ShowDialog();
            Debug.WriteLine("Choosing file to send dialog opened");
            if (result == DialogResult.OK)
            {
                choosedFilePath = openFileDialog1.FileName;
                Debug.WriteLine("choosedFilePath value is changed to {0}", choosedFilePath);
                choosedFileName = openFileDialog1.SafeFileName;
                Debug.WriteLine("choosedFileName value is changed to {0}", choosedFileName);
                chooseFileTextBox.Text = choosedFileName;
            }

            if (directoryName != null && choosedFilePath != null)
            {
                sendFileButton.Enabled = true;
                Debug.WriteLine("sendFileButton enabled");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                directoryName = folderBrowserDialog.SelectedPath;
                Debug.WriteLine("directoryName value is changed to {0}", directoryName);
                chooseDirectoryTextBox.Text = directoryName;
                Debug.WriteLine("chooseDirectoryTextBox.Text value is changed to {0}", directoryName);
            }
            else
            {
                directoryName = folderBrowserDialog.InitialDirectory;
                Debug.WriteLine("directoryName value is changed to {0}", directoryName);
                chooseDirectoryTextBox.Text = directoryName;
                Debug.WriteLine("chooseDirectoryTextBox.Text value is changed to {0}", directoryName);
            }

            if (directoryName != null && choosedFilePath != null)
            {
                sendFileButton.Enabled = true;
                Debug.WriteLine("sendFileButton enabled");
            }
        }

        private void packetSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            packetSize = packetSizeComboBoxCollection[packetSizeComboBox.SelectedIndex];
            Debug.WriteLine("packetSize value is changed to {0}", packetSizeComboBoxCollection[packetSizeComboBox.SelectedIndex]);
        }

        private void recieversAmountTextBox_TextChanged(object sender, EventArgs e)
        {
            amountOfRecievers = Convert.ToInt32(recieversAmountTextBox.Text);
            Debug.WriteLine("amountOfRecievers value is changed to {0}", amountOfRecievers);
        }
        
        private void sendFileButton_Click(object sender, EventArgs e)
        {
            if (choosedFilePath == null)
                return;

            int[] offsetsByOrder = Enumerable.Range(0, amountOfRecievers).ToArray();

            encoder.Read(choosedFilePath);
            Debug.WriteLine("enoder object readed bytes from {0}", choosedFilePath);
            encoder.Encode();
            Debug.WriteLine("encoder object encoded all readed bytes");

            encodedPacket = new(encoder.encodedSequence.Length / encoder.encodedSequence.GetLength(1), packetSize);
            encodedPacket.SplitSequenceToPackets(encoder.encodedSequence);
            Debug.WriteLine("Encoded data splitted to packets in encodedPacket object");

            Parallel.ForEach(offsetsByOrder, x => SendAndRecieve(x, encodedPacket.GetPacket(x)).Wait());
            Debug.WriteLine("All packets sent and recieved. Sources and sinks are disposed");

            packetsRecieved.OrderBy(x => x.Key);
            Debug.WriteLine("packetsRecieved object sorted");
        }
    }
}