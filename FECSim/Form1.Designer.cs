namespace FECSim
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            textBox1 = new TextBox();
            startButton = new Button();
            stopButton = new Button();
            label2 = new Label();
            chooseButton = new Button();
            sendButton = new Button();
            fileSystemWatcher1 = new FileSystemWatcher();
            label3 = new Label();
            comboBox1 = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(29, 15);
            label1.TabIndex = 0;
            label1.Text = "Port";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(72, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 1;
            // 
            // startButton
            // 
            startButton.Location = new Point(12, 41);
            startButton.Name = "startButton";
            startButton.Size = new Size(75, 23);
            startButton.TabIndex = 2;
            startButton.Text = "Start";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += button1_Click;
            // 
            // stopButton
            // 
            stopButton.Location = new Point(97, 41);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(75, 23);
            stopButton.TabIndex = 3;
            stopButton.Text = "Stop";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 78);
            label2.Name = "label2";
            label2.Size = new Size(25, 15);
            label2.TabIndex = 4;
            label2.Text = "File";
            // 
            // chooseButton
            // 
            chooseButton.Location = new Point(43, 74);
            chooseButton.Name = "chooseButton";
            chooseButton.Size = new Size(75, 23);
            chooseButton.TabIndex = 5;
            chooseButton.Text = "Choose";
            chooseButton.UseVisualStyleBackColor = true;
            chooseButton.Click += button3_Click;
            // 
            // sendButton
            // 
            sendButton.Location = new Point(124, 74);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(75, 23);
            sendButton.TabIndex = 6;
            sendButton.Text = "Send";
            sendButton.UseVisualStyleBackColor = true;
            sendButton.Click += button4_Click;
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.SynchronizingObject = this;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(178, 20);
            label3.Name = "label3";
            label3.Size = new Size(65, 15);
            label3.TabIndex = 7;
            label3.Text = "Packet Size";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "128B", "256B", "512B", "1KB", "2KB", "4KB", "8KB", "16KB", "32KB", "64KB" });
            comboBox1.Location = new Point(178, 41);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 8;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(310, 109);
            Controls.Add(comboBox1);
            Controls.Add(label3);
            Controls.Add(sendButton);
            Controls.Add(chooseButton);
            Controls.Add(label2);
            Controls.Add(stopButton);
            Controls.Add(startButton);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "Form1";
            Text = "FECSIM";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Button startButton;
        private Button stopButton;
        private Label label2;
        private Button chooseButton;
        private Button sendButton;
        private FileSystemWatcher fileSystemWatcher1;
        private ComboBox comboBox1;
        private Label label3;
    }
}