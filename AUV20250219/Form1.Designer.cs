namespace AUV2025
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            fileSystemWatcher1 = new FileSystemWatcher();
            groupBox1 = new GroupBox();
            propertyGrid1 = new PropertyGrid();
            toolStrip1 = new ToolStrip();
            TCPConnectButton = new ToolStripDropDownButton();
            OpenButton = new ToolStripMenuItem();
            CloseButton = new ToolStripMenuItem();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            openToolStripMenuItem = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            groupBox2 = new GroupBox();
            richTextBox1 = new RichTextBox();
            groupBox3 = new GroupBox();
            button9 = new Button();
            button4 = new Button();
            button3 = new Button();
            button1 = new Button();
            richTextBox2 = new RichTextBox();
            richTextBox3 = new RichTextBox();
            AUTOButton = new Button();
            AUTOStatus = new Label();
            groupBox4 = new GroupBox();
            button5 = new Button();
            button2 = new Button();
            richTextBox4 = new RichTextBox();
            CAMERAStatus = new Label();
            CAMERAButton = new Button();
            UACMStatus = new Label();
            UACMButton = new Button();
            BDGPSStatus = new Label();
            BDGPSButton = new Button();
            STM32Status = new Label();
            STM32Button = new Button();
            PiStatus = new Label();
            PiButton = new Button();
            SetupButton = new Button();
            groupBox5 = new GroupBox();
            button7 = new Button();
            groupBox6 = new GroupBox();
            button6 = new Button();
            label3 = new Label();
            label4 = new Label();
            label2 = new Label();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            label1 = new Label();
            comboBox1 = new ComboBox();
            timer1 = new System.Windows.Forms.Timer(components);
            timer2 = new System.Windows.Forms.Timer(components);
            comboBox2 = new ComboBox();
            comboBox3 = new ComboBox();
            comboBox4 = new ComboBox();
            groupBox7 = new GroupBox();
            label6 = new Label();
            label5 = new Label();
            groupBox8 = new GroupBox();
            label7 = new Label();
            groupBox9 = new GroupBox();
            label8 = new Label();
            groupBox10 = new GroupBox();
            sensorLabel7 = new Label();
            sensorButton7 = new Button();
            button8 = new Button();
            sensorLabel2 = new Label();
            sensorLabel3 = new Label();
            sensorButton3 = new Button();
            button10 = new Button();
            sensorLabel6 = new Label();
            sensorLabel1 = new Label();
            sensorButton6 = new Button();
            sensorButton2 = new Button();
            sensorLabel5 = new Label();
            sensorButton5 = new Button();
            sensorLabel4 = new Label();
            sensorButton4 = new Button();
            sensorButton1 = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            groupBox1.SuspendLayout();
            toolStrip1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox9.SuspendLayout();
            groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.SynchronizingObject = this;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(propertyGrid1);
            groupBox1.Location = new Point(0, 28);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(141, 666);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "connect info";
            // 
            // propertyGrid1
            // 
            propertyGrid1.Dock = DockStyle.Fill;
            propertyGrid1.Location = new Point(3, 19);
            propertyGrid1.Name = "propertyGrid1";
            propertyGrid1.Size = new Size(135, 644);
            propertyGrid1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(32, 32);
            toolStrip1.Items.AddRange(new ToolStripItem[] { TCPConnectButton, toolStripDropDownButton1 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 2, 0);
            toolStrip1.Size = new Size(1669, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // TCPConnectButton
            // 
            TCPConnectButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            TCPConnectButton.DropDownItems.AddRange(new ToolStripItem[] { OpenButton, CloseButton });
            TCPConnectButton.Image = (Image)resources.GetObject("TCPConnectButton.Image");
            TCPConnectButton.ImageTransparentColor = Color.Magenta;
            TCPConnectButton.Name = "TCPConnectButton";
            TCPConnectButton.Size = new Size(94, 22);
            TCPConnectButton.Text = "TCP Connect";
            // 
            // OpenButton
            // 
            OpenButton.Name = "OpenButton";
            OpenButton.Size = new Size(108, 22);
            OpenButton.Text = "Open";
            OpenButton.Click += OpenButton_Click;
            // 
            // CloseButton
            // 
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(108, 22);
            CloseButton.Text = "Close";
            CloseButton.Click += CloseButton_Click;
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, closeToolStripMenuItem });
            toolStripDropDownButton1.Image = (Image)resources.GetObject("toolStripDropDownButton1.Image");
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(130, 22);
            toolStripDropDownButton1.Text = "Controller Connect";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(108, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(108, 22);
            closeToolStripMenuItem.Text = "Close";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(richTextBox1);
            groupBox2.Location = new Point(152, 28);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(598, 314);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "log info";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(3, 19);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(591, 289);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(button9);
            groupBox3.Controls.Add(button4);
            groupBox3.Controls.Add(button3);
            groupBox3.Controls.Add(button1);
            groupBox3.Controls.Add(richTextBox2);
            groupBox3.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox3.Location = new Point(772, 516);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(872, 178);
            groupBox3.TabIndex = 4;
            groupBox3.TabStop = false;
            groupBox3.Text = "mission transmit";
            // 
            // button9
            // 
            button9.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button9.Location = new Point(3, 134);
            button9.Name = "button9";
            button9.Size = new Size(213, 37);
            button9.TabIndex = 4;
            button9.Text = "Save";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // button4
            // 
            button4.Enabled = false;
            button4.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button4.Location = new Point(654, 134);
            button4.Name = "button4";
            button4.Size = new Size(213, 37);
            button4.TabIndex = 3;
            button4.Text = "Start";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button3.Location = new Point(220, 134);
            button3.Name = "button3";
            button3.Size = new Size(213, 37);
            button3.TabIndex = 2;
            button3.Text = "Clear";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click_1;
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button1.Location = new Point(437, 134);
            button1.Name = "button1";
            button1.Size = new Size(213, 37);
            button1.TabIndex = 1;
            button1.Text = "Send";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new Point(3, 22);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(864, 108);
            richTextBox2.TabIndex = 0;
            richTextBox2.Text = "";
            // 
            // richTextBox3
            // 
            richTextBox3.Location = new Point(6, 40);
            richTextBox3.Margin = new Padding(6, 5, 6, 5);
            richTextBox3.Name = "richTextBox3";
            richTextBox3.Size = new Size(734, 194);
            richTextBox3.TabIndex = 0;
            richTextBox3.Text = "";
            // 
            // AUTOButton
            // 
            AUTOButton.BackColor = SystemColors.InactiveCaption;
            AUTOButton.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            AUTOButton.Location = new Point(0, 29);
            AUTOButton.Name = "AUTOButton";
            AUTOButton.Size = new Size(119, 65);
            AUTOButton.TabIndex = 8;
            AUTOButton.Text = "Pi";
            AUTOButton.UseVisualStyleBackColor = false;
            AUTOButton.Click += AUTOButton_Click;
            // 
            // AUTOStatus
            // 
            AUTOStatus.AutoSize = true;
            AUTOStatus.BackColor = SystemColors.Info;
            AUTOStatus.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point);
            AUTOStatus.Location = new Point(3, 29);
            AUTOStatus.Name = "AUTOStatus";
            AUTOStatus.Size = new Size(40, 15);
            AUTOStatus.TabIndex = 9;
            AUTOStatus.Text = "Closed";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(button5);
            groupBox4.Controls.Add(button2);
            groupBox4.Controls.Add(richTextBox4);
            groupBox4.Location = new Point(152, 348);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(598, 346);
            groupBox4.TabIndex = 10;
            groupBox4.TabStop = false;
            groupBox4.Text = "received data";
            // 
            // button5
            // 
            button5.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button5.Location = new Point(3, 302);
            button5.Name = "button5";
            button5.Size = new Size(160, 37);
            button5.TabIndex = 3;
            button5.Text = "Time Synchronize";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button2.Location = new Point(432, 302);
            button2.Name = "button2";
            button2.Size = new Size(160, 37);
            button2.TabIndex = 2;
            button2.Text = "Data Acquire";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // richTextBox4
            // 
            richTextBox4.Location = new Point(3, 19);
            richTextBox4.Name = "richTextBox4";
            richTextBox4.Size = new Size(591, 277);
            richTextBox4.TabIndex = 0;
            richTextBox4.Text = "";
            // 
            // CAMERAStatus
            // 
            CAMERAStatus.AutoSize = true;
            CAMERAStatus.BackColor = SystemColors.Info;
            CAMERAStatus.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point);
            CAMERAStatus.Location = new Point(125, 31);
            CAMERAStatus.Name = "CAMERAStatus";
            CAMERAStatus.Size = new Size(40, 15);
            CAMERAStatus.TabIndex = 12;
            CAMERAStatus.Text = "Closed";
            // 
            // CAMERAButton
            // 
            CAMERAButton.BackColor = SystemColors.InactiveCaption;
            CAMERAButton.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            CAMERAButton.Location = new Point(125, 29);
            CAMERAButton.Name = "CAMERAButton";
            CAMERAButton.Size = new Size(119, 65);
            CAMERAButton.TabIndex = 11;
            CAMERAButton.Text = "STM32";
            CAMERAButton.UseVisualStyleBackColor = false;
            CAMERAButton.Click += CAMERAButton_Click;
            // 
            // UACMStatus
            // 
            UACMStatus.AutoSize = true;
            UACMStatus.BackColor = SystemColors.Info;
            UACMStatus.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point);
            UACMStatus.Location = new Point(250, 29);
            UACMStatus.Name = "UACMStatus";
            UACMStatus.Size = new Size(40, 15);
            UACMStatus.TabIndex = 14;
            UACMStatus.Text = "Closed";
            // 
            // UACMButton
            // 
            UACMButton.BackColor = SystemColors.InactiveCaption;
            UACMButton.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            UACMButton.Location = new Point(250, 29);
            UACMButton.Name = "UACMButton";
            UACMButton.Size = new Size(119, 65);
            UACMButton.TabIndex = 13;
            UACMButton.Text = "BDGPS";
            UACMButton.UseVisualStyleBackColor = false;
            UACMButton.Click += UACMButton_Click;
            // 
            // BDGPSStatus
            // 
            BDGPSStatus.AutoSize = true;
            BDGPSStatus.BackColor = SystemColors.Info;
            BDGPSStatus.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point);
            BDGPSStatus.Location = new Point(375, 29);
            BDGPSStatus.Name = "BDGPSStatus";
            BDGPSStatus.Size = new Size(40, 15);
            BDGPSStatus.TabIndex = 16;
            BDGPSStatus.Text = "Closed";
            // 
            // BDGPSButton
            // 
            BDGPSButton.BackColor = SystemColors.InactiveCaption;
            BDGPSButton.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BDGPSButton.Location = new Point(375, 29);
            BDGPSButton.Name = "BDGPSButton";
            BDGPSButton.Size = new Size(119, 65);
            BDGPSButton.TabIndex = 15;
            BDGPSButton.Text = "UACM";
            BDGPSButton.UseVisualStyleBackColor = false;
            BDGPSButton.Click += BDGPSButton_Click;
            // 
            // STM32Status
            // 
            STM32Status.AutoSize = true;
            STM32Status.BackColor = SystemColors.Info;
            STM32Status.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point);
            STM32Status.Location = new Point(500, 29);
            STM32Status.Name = "STM32Status";
            STM32Status.Size = new Size(40, 15);
            STM32Status.TabIndex = 18;
            STM32Status.Text = "Closed";
            // 
            // STM32Button
            // 
            STM32Button.BackColor = SystemColors.InactiveCaption;
            STM32Button.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            STM32Button.Location = new Point(500, 29);
            STM32Button.Name = "STM32Button";
            STM32Button.Size = new Size(119, 65);
            STM32Button.TabIndex = 17;
            STM32Button.Text = "CAMERA";
            STM32Button.UseVisualStyleBackColor = false;
            STM32Button.Click += button2_Click;
            // 
            // PiStatus
            // 
            PiStatus.AutoSize = true;
            PiStatus.BackColor = SystemColors.Info;
            PiStatus.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point);
            PiStatus.Location = new Point(628, 29);
            PiStatus.Name = "PiStatus";
            PiStatus.Size = new Size(40, 15);
            PiStatus.TabIndex = 20;
            PiStatus.Text = "Closed";
            // 
            // PiButton
            // 
            PiButton.BackColor = SystemColors.InactiveCaption;
            PiButton.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            PiButton.Location = new Point(625, 29);
            PiButton.Name = "PiButton";
            PiButton.Size = new Size(119, 65);
            PiButton.TabIndex = 19;
            PiButton.Text = "AUTO";
            PiButton.UseVisualStyleBackColor = false;
            PiButton.Click += button3_Click;
            // 
            // SetupButton
            // 
            SetupButton.BackColor = SystemColors.ControlLight;
            SetupButton.Font = new Font("Times New Roman", 15F, FontStyle.Bold, GraphicsUnit.Point);
            SetupButton.Location = new Point(375, 101);
            SetupButton.Name = "SetupButton";
            SetupButton.Size = new Size(369, 62);
            SetupButton.TabIndex = 21;
            SetupButton.Text = "Setup";
            SetupButton.UseVisualStyleBackColor = false;
            SetupButton.Click += SetupButton_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(button7);
            groupBox5.Controls.Add(CAMERAStatus);
            groupBox5.Controls.Add(UACMStatus);
            groupBox5.Controls.Add(UACMButton);
            groupBox5.Controls.Add(SetupButton);
            groupBox5.Controls.Add(PiStatus);
            groupBox5.Controls.Add(AUTOStatus);
            groupBox5.Controls.Add(PiButton);
            groupBox5.Controls.Add(CAMERAButton);
            groupBox5.Controls.Add(STM32Status);
            groupBox5.Controls.Add(STM32Button);
            groupBox5.Controls.Add(BDGPSStatus);
            groupBox5.Controls.Add(BDGPSButton);
            groupBox5.Controls.Add(AUTOButton);
            groupBox5.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox5.Location = new Point(3, 699);
            groupBox5.Margin = new Padding(2);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(2);
            groupBox5.Size = new Size(747, 172);
            groupBox5.TabIndex = 22;
            groupBox5.TabStop = false;
            groupBox5.Text = "node setup";
            // 
            // button7
            // 
            button7.BackColor = SystemColors.ControlLight;
            button7.Font = new Font("Times New Roman", 15F, FontStyle.Bold, GraphicsUnit.Point);
            button7.Location = new Point(0, 101);
            button7.Name = "button7";
            button7.Size = new Size(369, 62);
            button7.TabIndex = 22;
            button7.Text = "Status Refresh";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(button6);
            groupBox6.Controls.Add(label3);
            groupBox6.Controls.Add(label4);
            groupBox6.Controls.Add(label2);
            groupBox6.Controls.Add(textBox4);
            groupBox6.Controls.Add(textBox3);
            groupBox6.Controls.Add(textBox2);
            groupBox6.Controls.Add(textBox1);
            groupBox6.Controls.Add(label1);
            groupBox6.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox6.Location = new Point(772, 402);
            groupBox6.Margin = new Padding(2);
            groupBox6.Name = "groupBox6";
            groupBox6.Padding = new Padding(2);
            groupBox6.Size = new Size(872, 96);
            groupBox6.TabIndex = 23;
            groupBox6.TabStop = false;
            groupBox6.Text = "security para setup";
            // 
            // button6
            // 
            button6.BackColor = SystemColors.ControlLight;
            button6.Font = new Font("Times New Roman", 15F, FontStyle.Bold, GraphicsUnit.Point);
            button6.Location = new Point(625, 33);
            button6.Name = "button6";
            button6.Size = new Size(164, 52);
            button6.TabIndex = 22;
            button6.Text = "Setup";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(351, 63);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(83, 19);
            label3.TabIndex = 7;
            label3.Text = "Power Limit";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(353, 33);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(80, 19);
            label4.TabIndex = 6;
            label4.Text = "Depth Limit";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(109, 61);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(72, 19);
            label2.TabIndex = 5;
            label2.Text = "Time Limit";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(449, 59);
            textBox4.Margin = new Padding(2);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(124, 26);
            textBox4.TabIndex = 4;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(449, 29);
            textBox3.Margin = new Padding(2);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(124, 26);
            textBox3.TabIndex = 3;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(198, 59);
            textBox2.Margin = new Padding(2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(124, 26);
            textBox2.TabIndex = 2;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(198, 29);
            textBox1.Margin = new Padding(2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(124, 26);
            textBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(91, 33);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(93, 19);
            label1.TabIndex = 0;
            label1.Text = "Active Radius";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Left", "Right" });
            comboBox1.Location = new Point(126, 74);
            comboBox1.Margin = new Padding(2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(134, 25);
            comboBox1.TabIndex = 24;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick_1;
            // 
            // timer2
            // 
            timer2.Tick += timer2_Tick;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "0.5kn", "1kn", "1.5kn", "2kn", "2.5kn", "3kn", "3.5kn", "4kn", "4.5kn", "5kn", "5.5kn", "6kn" });
            comboBox2.Location = new Point(126, 36);
            comboBox2.Margin = new Padding(2);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(134, 25);
            comboBox2.TabIndex = 27;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Items.AddRange(new object[] { "ultra slow", "slow", "normal", "fast", "very fast" });
            comboBox3.Location = new Point(126, 39);
            comboBox3.Margin = new Padding(2);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(134, 25);
            comboBox3.TabIndex = 28;
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Items.AddRange(new object[] { "T1", "T2", "T3", "T4", "T5" });
            comboBox4.Location = new Point(126, 35);
            comboBox4.Margin = new Padding(2);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(134, 25);
            comboBox4.TabIndex = 29;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(label6);
            groupBox7.Controls.Add(label5);
            groupBox7.Controls.Add(comboBox4);
            groupBox7.Controls.Add(comboBox1);
            groupBox7.Location = new Point(772, 28);
            groupBox7.Margin = new Padding(2);
            groupBox7.Name = "groupBox7";
            groupBox7.Padding = new Padding(2);
            groupBox7.Size = new Size(266, 128);
            groupBox7.TabIndex = 30;
            groupBox7.TabStop = false;
            groupBox7.Text = "motors test";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(55, 74);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(55, 17);
            label6.TabIndex = 31;
            label6.Text = "steering";
            label6.Click += label6_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(55, 37);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(59, 17);
            label5.TabIndex = 30;
            label5.Text = "thrusters";
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(label7);
            groupBox8.Controls.Add(comboBox2);
            groupBox8.Location = new Point(772, 188);
            groupBox8.Margin = new Padding(2);
            groupBox8.Name = "groupBox8";
            groupBox8.Padding = new Padding(2);
            groupBox8.Size = new Size(266, 80);
            groupBox8.TabIndex = 31;
            groupBox8.TabStop = false;
            groupBox8.Text = "move control";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(60, 36);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(44, 17);
            label7.TabIndex = 32;
            label7.Text = "speed";
            label7.Click += label7_Click;
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(label8);
            groupBox9.Controls.Add(comboBox3);
            groupBox9.Location = new Point(772, 300);
            groupBox9.Margin = new Padding(2);
            groupBox9.Name = "groupBox9";
            groupBox9.Padding = new Padding(2);
            groupBox9.Size = new Size(266, 80);
            groupBox9.TabIndex = 32;
            groupBox9.TabStop = false;
            groupBox9.Text = "turn control";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(60, 41);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(42, 17);
            label8.TabIndex = 33;
            label8.Text = "agility";
            // 
            // groupBox10
            // 
            groupBox10.Controls.Add(sensorLabel7);
            groupBox10.Controls.Add(sensorButton7);
            groupBox10.Controls.Add(button8);
            groupBox10.Controls.Add(sensorLabel2);
            groupBox10.Controls.Add(sensorLabel3);
            groupBox10.Controls.Add(sensorButton3);
            groupBox10.Controls.Add(button10);
            groupBox10.Controls.Add(sensorLabel6);
            groupBox10.Controls.Add(sensorLabel1);
            groupBox10.Controls.Add(sensorButton6);
            groupBox10.Controls.Add(sensorButton2);
            groupBox10.Controls.Add(sensorLabel5);
            groupBox10.Controls.Add(sensorButton5);
            groupBox10.Controls.Add(sensorLabel4);
            groupBox10.Controls.Add(sensorButton4);
            groupBox10.Controls.Add(sensorButton1);
            groupBox10.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox10.Location = new Point(772, 699);
            groupBox10.Margin = new Padding(2);
            groupBox10.Name = "groupBox10";
            groupBox10.Padding = new Padding(2);
            groupBox10.Size = new Size(872, 172);
            groupBox10.TabIndex = 33;
            groupBox10.TabStop = false;
            groupBox10.Text = "sensor setup";
            // 
            // sensorLabel7
            // 
            sensorLabel7.AutoSize = true;
            sensorLabel7.BackColor = SystemColors.Info;
            sensorLabel7.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point);
            sensorLabel7.Location = new Point(750, 30);
            sensorLabel7.Name = "sensorLabel7";
            sensorLabel7.Size = new Size(40, 15);
            sensorLabel7.TabIndex = 24;
            sensorLabel7.Text = "Closed";
            // 
            // sensorButton7
            // 
            sensorButton7.BackColor = SystemColors.InactiveCaption;
            sensorButton7.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            sensorButton7.Location = new Point(750, 28);
            sensorButton7.Name = "sensorButton7";
            sensorButton7.Size = new Size(119, 65);
            sensorButton7.TabIndex = 23;
            sensorButton7.Text = "Light";
            sensorButton7.UseVisualStyleBackColor = false;
            sensorButton7.Click += sensorButton7_Click;
            // 
            // button8
            // 
            button8.BackColor = SystemColors.ControlLight;
            button8.Font = new Font("Times New Roman", 15F, FontStyle.Bold, GraphicsUnit.Point);
            button8.Location = new Point(0, 101);
            button8.Name = "button8";
            button8.Size = new Size(369, 62);
            button8.TabIndex = 22;
            button8.Text = "Status Refresh";
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click;
            // 
            // sensorLabel2
            // 
            sensorLabel2.AutoSize = true;
            sensorLabel2.BackColor = SystemColors.Info;
            sensorLabel2.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point);
            sensorLabel2.Location = new Point(124, 30);
            sensorLabel2.Name = "sensorLabel2";
            sensorLabel2.Size = new Size(40, 15);
            sensorLabel2.TabIndex = 12;
            sensorLabel2.Text = "Closed";
            // 
            // sensorLabel3
            // 
            sensorLabel3.AutoSize = true;
            sensorLabel3.BackColor = SystemColors.Info;
            sensorLabel3.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point);
            sensorLabel3.Location = new Point(249, 28);
            sensorLabel3.Name = "sensorLabel3";
            sensorLabel3.Size = new Size(40, 15);
            sensorLabel3.TabIndex = 14;
            sensorLabel3.Text = "Closed";
            // 
            // sensorButton3
            // 
            sensorButton3.BackColor = SystemColors.InactiveCaption;
            sensorButton3.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            sensorButton3.Location = new Point(250, 29);
            sensorButton3.Name = "sensorButton3";
            sensorButton3.Size = new Size(119, 65);
            sensorButton3.TabIndex = 13;
            sensorButton3.Text = "BDGPS";
            sensorButton3.UseVisualStyleBackColor = false;
            sensorButton3.Click += sensorButton3_Click;
            // 
            // button10
            // 
            button10.BackColor = SystemColors.ControlLight;
            button10.Font = new Font("Times New Roman", 15F, FontStyle.Bold, GraphicsUnit.Point);
            button10.Location = new Point(499, 101);
            button10.Name = "button10";
            button10.Size = new Size(369, 62);
            button10.TabIndex = 21;
            button10.Text = "Setup";
            button10.UseVisualStyleBackColor = false;
            button10.Click += button10_Click;
            // 
            // sensorLabel6
            // 
            sensorLabel6.AutoSize = true;
            sensorLabel6.BackColor = SystemColors.Info;
            sensorLabel6.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point);
            sensorLabel6.Location = new Point(627, 28);
            sensorLabel6.Name = "sensorLabel6";
            sensorLabel6.Size = new Size(40, 15);
            sensorLabel6.TabIndex = 20;
            sensorLabel6.Text = "Closed";
            // 
            // sensorLabel1
            // 
            sensorLabel1.AutoSize = true;
            sensorLabel1.BackColor = SystemColors.Info;
            sensorLabel1.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point);
            sensorLabel1.Location = new Point(2, 28);
            sensorLabel1.Name = "sensorLabel1";
            sensorLabel1.Size = new Size(40, 15);
            sensorLabel1.TabIndex = 9;
            sensorLabel1.Text = "Closed";
            // 
            // sensorButton6
            // 
            sensorButton6.BackColor = SystemColors.InactiveCaption;
            sensorButton6.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            sensorButton6.Location = new Point(625, 29);
            sensorButton6.Name = "sensorButton6";
            sensorButton6.Size = new Size(119, 65);
            sensorButton6.TabIndex = 19;
            sensorButton6.Text = "Magnetometer";
            sensorButton6.UseVisualStyleBackColor = false;
            sensorButton6.Click += sensorButton6_Click;
            // 
            // sensorButton2
            // 
            sensorButton2.BackColor = SystemColors.InactiveCaption;
            sensorButton2.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            sensorButton2.Location = new Point(125, 29);
            sensorButton2.Name = "sensorButton2";
            sensorButton2.Size = new Size(119, 65);
            sensorButton2.TabIndex = 11;
            sensorButton2.Text = "Altimeter";
            sensorButton2.UseVisualStyleBackColor = false;
            sensorButton2.Click += sensorButton2_Click;
            // 
            // sensorLabel5
            // 
            sensorLabel5.AutoSize = true;
            sensorLabel5.BackColor = SystemColors.Info;
            sensorLabel5.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point);
            sensorLabel5.Location = new Point(499, 28);
            sensorLabel5.Name = "sensorLabel5";
            sensorLabel5.Size = new Size(40, 15);
            sensorLabel5.TabIndex = 18;
            sensorLabel5.Text = "Closed";
            // 
            // sensorButton5
            // 
            sensorButton5.BackColor = SystemColors.InactiveCaption;
            sensorButton5.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            sensorButton5.Location = new Point(500, 29);
            sensorButton5.Name = "sensorButton5";
            sensorButton5.Size = new Size(119, 65);
            sensorButton5.TabIndex = 17;
            sensorButton5.Text = "Manipulator";
            sensorButton5.UseVisualStyleBackColor = false;
            sensorButton5.Click += sensorButton5_Click;
            // 
            // sensorLabel4
            // 
            sensorLabel4.AutoSize = true;
            sensorLabel4.BackColor = SystemColors.Info;
            sensorLabel4.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point);
            sensorLabel4.Location = new Point(374, 28);
            sensorLabel4.Name = "sensorLabel4";
            sensorLabel4.Size = new Size(40, 15);
            sensorLabel4.TabIndex = 16;
            sensorLabel4.Text = "Closed";
            // 
            // sensorButton4
            // 
            sensorButton4.BackColor = SystemColors.InactiveCaption;
            sensorButton4.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            sensorButton4.Location = new Point(375, 29);
            sensorButton4.Name = "sensorButton4";
            sensorButton4.Size = new Size(119, 65);
            sensorButton4.TabIndex = 15;
            sensorButton4.Text = "UACM";
            sensorButton4.UseVisualStyleBackColor = false;
            sensorButton4.Click += sensorButton4_Click;
            // 
            // sensorButton1
            // 
            sensorButton1.BackColor = SystemColors.InactiveCaption;
            sensorButton1.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            sensorButton1.Location = new Point(0, 29);
            sensorButton1.Name = "sensorButton1";
            sensorButton1.Size = new Size(119, 65);
            sensorButton1.TabIndex = 8;
            sensorButton1.Text = "TD";
            sensorButton1.UseVisualStyleBackColor = false;
            sensorButton1.Click += button15_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(1147, 28);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(497, 355);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 34;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1669, 882);
            Controls.Add(pictureBox1);
            Controls.Add(groupBox10);
            Controls.Add(groupBox9);
            Controls.Add(groupBox8);
            Controls.Add(groupBox7);
            Controls.Add(groupBox6);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(toolStrip1);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "AUV Test UI v0.6";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            groupBox1.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            groupBox10.ResumeLayout(false);
            groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FileSystemWatcher fileSystemWatcher1;
        private ToolStrip toolStrip1;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private RichTextBox richTextBox2;
        private RichTextBox richTextBox1;
        private Button button1;
        private ToolStripDropDownButton TCPConnectButton;
        private ToolStripMenuItem OpenButton;
        private ToolStripMenuItem CloseButton;
        private RichTextBox richTextBox3;
        private PropertyGrid propertyGrid1;
        private Button AUTOButton;
        private Label AUTOStatus;
        private GroupBox groupBox4;
        private RichTextBox richTextBox4;
        private Label CAMERAStatus;
        private Button CAMERAButton;
        private Label UACMStatus;
        private Button UACMButton;
        private Label BDGPSStatus;
        private Button BDGPSButton;
        private Label PiStatus;
        private Button PiButton;
        private Label STM32Status;
        private Button STM32Button;
        private Button SetupButton;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private Button button2;
        private Button button4;
        private Button button3;
        private Button button5;
        private TextBox textBox1;
        private Label label1;
        private Label label3;
        private Label label4;
        private Label label2;
        private TextBox textBox4;
        private TextBox textBox3;
        private TextBox textBox2;
        private Button button6;
        private Button button7;
        private ComboBox comboBox1;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private ComboBox comboBox4;
        private GroupBox groupBox7;
        private Label label6;
        private Label label5;
        private GroupBox groupBox8;
        private GroupBox groupBox9;
        private Label label7;
        private Label label8;
        private GroupBox groupBox10;
        private Button button8;
        private Label sensorLabel2;
        private Label sensorLabel3;
        private Button sensorButton3;
        private Button button10;
        private Label sensorLabel6;
        private Label sensorLabel1;
        private Button sensorButton6;
        private Button sensorButton2;
        private Label sensorLabel5;
        private Button sensorButton5;
        private Label sensorLabel4;
        private Button sensorButton4;
        private Button sensorButton1;
        private Label sensorLabel7;
        private Button sensorButton7;
        private PictureBox pictureBox1;
        private Button button9;
    }
}