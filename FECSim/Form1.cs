namespace FECSim
{
    public partial class Form1 : Form
    {
        Sink? sink;
        Source? source;
        private string ?choosedFilePath;
        private string ?choosedFileName;

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

            sink.Start();
            source.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (sink != null)
                sink.Stop();
            if (source != null)
                source.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                choosedFilePath = openFileDialog1.FileName;
                choosedFileName = openFileDialog1.SafeFileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string directoryName;
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
                directoryName = folderBrowserDialog.SelectedPath;
            else directoryName = folderBrowserDialog.InitialDirectory;
            var sendFile = File.Create(directoryName + "\\" + choosedFileName);
            if (source != null && choosedFilePath != null)
                source.SendByteArray(System.IO.File.ReadAllBytes(choosedFilePath));
            if (sink != null)
            {
                sink.RecieveByteArray();
                sendFile.Write(sink.recievedData);
            }
            sendFile.Close();
        }
    }
}