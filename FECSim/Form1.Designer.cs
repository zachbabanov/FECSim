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
            label2 = new Label();
            chooseSendButton = new Button();
            chooseRecieveButton = new Button();
            fileSystemWatcher1 = new FileSystemWatcher();
            chooseFileTextBox = new TextBox();
            chooseDirectoryTextBox = new TextBox();
            label1 = new Label();
            recieversAmountTextBox = new TextBox();
            sendFileButton = new Button();
            packetSizeComboBox = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(50, 87);
            label2.Name = "label2";
            label2.Size = new Size(80, 20);
            label2.TabIndex = 4;
            label2.Text = "Packet size";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // chooseSendButton
            // 
            chooseSendButton.Location = new Point(165, 10);
            chooseSendButton.Margin = new Padding(3, 4, 3, 4);
            chooseSendButton.Name = "chooseSendButton";
            chooseSendButton.Size = new Size(140, 31);
            chooseSendButton.TabIndex = 5;
            chooseSendButton.Text = "Choose file";
            chooseSendButton.UseVisualStyleBackColor = true;
            chooseSendButton.Click += button3_Click;
            // 
            // chooseRecieveButton
            // 
            chooseRecieveButton.Location = new Point(165, 49);
            chooseRecieveButton.Margin = new Padding(3, 4, 3, 4);
            chooseRecieveButton.Name = "chooseRecieveButton";
            chooseRecieveButton.Size = new Size(140, 31);
            chooseRecieveButton.TabIndex = 6;
            chooseRecieveButton.Text = "Choose directory";
            chooseRecieveButton.UseVisualStyleBackColor = true;
            chooseRecieveButton.Click += button4_Click;
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.SynchronizingObject = this;
            // 
            // chooseFileTextBox
            // 
            chooseFileTextBox.Location = new Point(14, 12);
            chooseFileTextBox.Name = "chooseFileTextBox";
            chooseFileTextBox.ReadOnly = true;
            chooseFileTextBox.Size = new Size(145, 27);
            chooseFileTextBox.TabIndex = 7;
            chooseFileTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // chooseDirectoryTextBox
            // 
            chooseDirectoryTextBox.Location = new Point(14, 48);
            chooseDirectoryTextBox.Name = "chooseDirectoryTextBox";
            chooseDirectoryTextBox.ReadOnly = true;
            chooseDirectoryTextBox.Size = new Size(145, 27);
            chooseDirectoryTextBox.TabIndex = 8;
            chooseDirectoryTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 121);
            label1.Name = "label1";
            label1.Size = new Size(85, 40);
            label1.TabIndex = 10;
            label1.Text = "Amount \r\nof recievers";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // recieversAmountTextBox
            // 
            recieversAmountTextBox.Location = new Point(165, 128);
            recieversAmountTextBox.Name = "recieversAmountTextBox";
            recieversAmountTextBox.Size = new Size(140, 27);
            recieversAmountTextBox.TabIndex = 11;
            recieversAmountTextBox.TextAlign = HorizontalAlignment.Right;
            recieversAmountTextBox.TextChanged += recieversAmountTextBox_TextChanged;
            // 
            // sendFileButton
            // 
            sendFileButton.Location = new Point(81, 180);
            sendFileButton.Name = "sendFileButton";
            sendFileButton.Size = new Size(140, 29);
            sendFileButton.TabIndex = 13;
            sendFileButton.Text = "Send";
            sendFileButton.UseVisualStyleBackColor = true;
            sendFileButton.Click += sendFileButton_Click;
            // 
            // packetSizeComboBox
            // 
            packetSizeComboBox.FormattingEnabled = true;
            packetSizeComboBox.Items.AddRange(new object[] { "64B", "128B", "256B", "512B", "1KB", "2KB", "4KB", "8KB", "16KB", "32KB", "64KB" });
            packetSizeComboBox.Location = new Point(165, 87);
            packetSizeComboBox.Name = "packetSizeComboBox";
            packetSizeComboBox.Size = new Size(140, 28);
            packetSizeComboBox.TabIndex = 15;
            packetSizeComboBox.SelectedIndexChanged += packetSizeComboBox_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(317, 221);
            Controls.Add(packetSizeComboBox);
            Controls.Add(sendFileButton);
            Controls.Add(recieversAmountTextBox);
            Controls.Add(label1);
            Controls.Add(chooseDirectoryTextBox);
            Controls.Add(chooseFileTextBox);
            Controls.Add(chooseRecieveButton);
            Controls.Add(chooseSendButton);
            Controls.Add(label2);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "FECSIM";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private Button chooseSendButton;
        private Button chooseRecieveButton;
        private FileSystemWatcher fileSystemWatcher1;
        private TextBox chooseDirectoryTextBox;
        private TextBox chooseFileTextBox;
        private Button sendFileButton;
        private TextBox recieversAmountTextBox;
        private Label label1;
        private ComboBox packetSizeComboBox;
    }
}