namespace FECSim
{
    public partial class Form1 : Form
    {
        Sink ?sink;
        Source ?source;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sink = new Sink(Convert.ToInt32(textBox1.Text));
            source = new Source(Convert.ToInt32(textBox1.Text));

            sink.start();
            source.start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (sink != null)
                sink.stop();
            if (source != null)
                source.stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            /// Temporary solution for testing ///
            var sendFile = File.Create("test.txt");
            if (source != null)
                source.sendByteArray(System.IO.File.ReadAllBytes("D:\\test.txt"));
            if (sink != null)
            {
                sink.recieveByteArray();
                sendFile.Write(sink.recievedData);
            }
            sendFile.Close();
        }
    }
}