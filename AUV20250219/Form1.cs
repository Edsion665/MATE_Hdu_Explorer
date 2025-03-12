using STTech.BytesIO.Tcp;
using System.Windows.Forms;
using System.IO.Ports;

using System;//������ϵͳ��
using System.Threading;
/*
using System.Net;
using System.Net.Sockets;
*/
using SharpDX.XInput;//
using SharpDX;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using static System.Windows.Forms.LinkLabel;
/*
using System.Security.Cryptography.Xml;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
*/
namespace AUV2025
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private Controller controller;//xbox���ƶ���
        private Gamepad currentGamepadState;//xbox״̬

        int[] a = new int[30];//�ֱ�������Ϣ20������
        int[] b = new int[30];//�洢ǰһʱ���ֱ���Ϣ
        char[] nodeState = { 'C', 'C', 'C', 'C', 'C', 'C' };//��ݮ�ɽڵ�״̬ 
        char[] sensorState = { 'C', 'C', 'C', 'C', 'C', 'C', 'C' };//�������ڵ�״̬ 
        string[] lines = { "" };//ʹ������ı�����
        int curLine = 0;//ʹ�����ȡ�ĵ�ǰ����

        public Form1()
        {
            InitializeComponent();//��ʼ���������
            client = new TcpClient() { Host = "192.168.66.2", Port = 55555}; //��ʼ������ͻ���
            controller = new Controller(UserIndex.One);//��ʼ��xbox������
            timer1.Start();
            timer2.Start();

            //            CheckForIllegalCrossThreadCalls = false;

            propertyGrid1.SelectedObject = client;
            client.OnDataReceived += Client_OnDataReceived;
            client.OnConnectedSuccessfully += Client_OnConnectedSuccessfully;
            client.OnConnectionFailed += Client_OnConnectionFailed;
            client.OnDisconnected += Client_OnDisconnected;
        }

        #region code for dataCommunication
        private void Print(string msg)
        {
            richTextBox1.AppendText($"[{DateTime.Now}] {msg}\r\n");
        }
        private void Client_OnDisconnected(object? sender, STTech.BytesIO.Core.DisconnectedEventArgs e)
        {
            Print($"Disconnected!{e.ReasonCode}");
        }
        private void Client_OnConnectionFailed(object? sender, STTech.BytesIO.Core.ConnectionFailedEventArgs e)
        {
            Print("Connect failed!");
        }
        private void Client_OnConnectedSuccessfully(object? sender, STTech.BytesIO.Core.ConnectedSuccessfullyEventArgs e)
        {
            Print("Connect successfully!");
        }
        private void Client_OnDataReceived(object? sender, STTech.BytesIO.Core.DataReceivedEventArgs e)
        {
            richTextBox4.AppendText($"[{DateTime.Now}] {e.Data.EncodeToString()}\r\n");
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            client.Connect();
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            client.Disconnect();
        }
        private void OpenButton_Click(object sender, EventArgs e)
        {
            client.Connect();
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            client.Disconnect();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            char[] char_datatime = DateTime.Now.ToString().ToCharArray();
            for (int i = 0; i < char_datatime.Length; i++)
            {
                if (char_datatime[i] == ':' | char_datatime[i] == ' ' | char_datatime[i] == '/')
                    char_datatime[i] = ',';
            }
            string str_datatime = new string(char_datatime);
            string command_TimeSyn = "@ST," + str_datatime + ",$";
            client.Send(command_TimeSyn.GetBytes());
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            string command_DataAcquire = "@DU$";
            client.Send(command_DataAcquire.GetBytes());
        }
        #endregion

        #region code for nodeSetup
        private void AUTOButton_Click(object sender, EventArgs e)
        {
            if (nodeState[0] == 'C')
            {
                AUTOStatus.Text = "Opened";
                AUTOButton.UseVisualStyleBackColor = true;
                nodeState[0] = 'O';
            }
            else
            {
                AUTOStatus.Text = "Closed";
                AUTOButton.UseVisualStyleBackColor = false;
                nodeState[0] = 'C';
            }
        }
        private void CAMERAButton_Click(object sender, EventArgs e)
        {
            if (nodeState[1] == 'C')
            {
                CAMERAStatus.Text = "Opened";
                CAMERAButton.UseVisualStyleBackColor = true;
                nodeState[1] = 'O';
            }
            else
            {
                CAMERAStatus.Text = "Closed";
                CAMERAButton.UseVisualStyleBackColor = false;
                nodeState[1] = 'C';
            }
        }
        private void UACMButton_Click(object sender, EventArgs e)
        {
            if (nodeState[2] == 'C')
            {
                UACMStatus.Text = "Opened";
                UACMButton.UseVisualStyleBackColor = true;
                nodeState[2] = 'O';
            }
            else
            {
                UACMStatus.Text = "Closed";
                UACMButton.UseVisualStyleBackColor = false;
                nodeState[2] = 'C';
            }
        }
        private void BDGPSButton_Click(object sender, EventArgs e)
        {
            if (nodeState[3] == 'C')
            {
                BDGPSStatus.Text = "Opened";
                BDGPSButton.UseVisualStyleBackColor = true;
                nodeState[3] = 'O';
            }
            else
            {
                BDGPSStatus.Text = "Closed";
                BDGPSButton.UseVisualStyleBackColor = false;
                nodeState[3] = 'C';
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (nodeState[4] == 'C')
            {
                STM32Status.Text = "Opened";
                STM32Button.UseVisualStyleBackColor = true;
                nodeState[4] = 'O';
            }
            else
            {
                STM32Status.Text = "Closed";
                STM32Button.UseVisualStyleBackColor = false;
                nodeState[4] = 'C';
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (nodeState[5] == 'C')
            {
                PiStatus.Text = "Opened";
                PiButton.UseVisualStyleBackColor = true;
                nodeState[5] = 'O';
            }
            else
            {
                PiStatus.Text = "Closed";
                PiButton.UseVisualStyleBackColor = false;
                nodeState[5] = 'C';
            }
        }
        private void SetupButton_Click(object sender, EventArgs e)
        {
            string strNodeState = new string(nodeState);
            string command_NodeSetup = $"@NS" + strNodeState + "$";
            client.Send(command_NodeSetup.GetBytes());
        }
        private void button7_Click(object sender, EventArgs e)
        {
            string command_NodeStatus = "@NI$";
            client.Send(command_NodeStatus.GetBytes());
        }
        #endregion

        #region code for securityParaSetup
        private void button6_Click(object sender, EventArgs e)
        {
            string command_SecurityParaSetup = "@SP," + textBox1.Text.ToString() + "," + textBox2.Text.ToString() + "," + textBox3.Text.ToString() + "," + textBox4.Text.ToString() + ",$";
            client.Send(command_SecurityParaSetup.GetBytes());
        }
        #endregion

        #region code for missionTransmit
        private void button4_Click(object sender, EventArgs e)
        {
            string command_MissionStart = "@AA$";
            client.Send(command_MissionStart.GetBytes());
        }
        private void button9_Click(object sender, EventArgs e)
        {
            richTextBox2.ReadOnly = true;
            richTextBox2.BackColor = Color.Gray;
            File.WriteAllText("mission.txt", richTextBox2.Text);
            lines = File.ReadAllLines("mission.txt");
            button1.Enabled = true;
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            richTextBox2.ReadOnly = false;
            richTextBox2.BackColor = Color.White;
            richTextBox2.Text = "";
            curLine = 0;
            string command_MissionClear = "@AC$";
            client.Send(command_MissionClear.GetBytes());
            button1.Enabled = false;
            button4.Enabled = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            int liner = lines.Length;
            if (liner > curLine)
            {
                string str_liner = liner.ToString();
                if (str_liner.Length == 1)
                    str_liner = "0" + str_liner;
                string str_lineindex = (curLine + 1).ToString();
                if (str_lineindex.Length == 1)
                    str_lineindex = "0" + str_lineindex;
                string sendtext = "@AW" + str_liner + str_lineindex + lines[curLine] + "$";
                client.Send(sendtext.GetBytes());
            }
            else if (liner == curLine)
                button4.Enabled = true;
            curLine++;
        }
        #endregion

        #region code for controller
        private bool isConnected = true;

        private bool IsButtonPressed(GamepadButtonFlags button)//�ж�ĳ����ť�Ƿ��£������Ƿ�
        {
            return (currentGamepadState.Buttons & button) == button;//&��ʾ��λand
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!controller.IsConnected)
            {
                Print("Xbox������δ���ӡ�");
            }
            else
                Print("Xbox�����������ӡ�");
        }
        //        private DateTime time1; // ��¼��ʱ������ʱ��1
        //        private DateTime time2; // ��¼��ʱ������ʱ��2
        //        int cdcs = 0;
        /*        public void changecolor()
                {
                    if (isConnected)
                    {
                        // �� isConnected ��Ϊ True ʱ���ز�����ָ����ͼƬ
                        //                pictureBox2.Image = Properties.Resources.�������̵�; // �滻Ϊ����ͼƬ·��

                    }
                    else
                    {
                        //                pictureBox2.Image = Properties.Resources.�����ĺ��; // �滻Ϊ����ͼƬ·��
                        //pictureBox2.Image = System.Drawing.Image.FromFile("Resources\\�����ĺ��.png"); // �滻Ϊ����ͼƬ·��
                        // �� isConnected Ϊ False ʱ����ִ�������������������ͼ��

                    }
                }*/
        private void UpdateControllerState()
        {
            // ��ȡ�ֱ�״̬�����͵�������
            SharpDX.XInput.State state = controller.GetState();
            currentGamepadState = state.Gamepad;
            // string controllerInfo = label3.Text;
            // SendMessage(controllerInfo);
        }
        /// <summary>��ȡ�ֱ�״̬��Ϣ��time1_Tick���滹�м���ֱ������try-catch�߼�
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>���������������������ȡ����Ͱ�ť
        private double GetLeftTriggerValue()
        {
            // ��ȡ�������󴥷������İ��³̶�
            float L = currentGamepadState.LeftTrigger / 255f;
            return (double)Math.Round(L, 1) * 100;
        }
        private double GetRightTriggerValue()
        {
            // ��ȡ�Ұ�����Ҵ��������İ��³̶�
            float R = currentGamepadState.RightTrigger / 255f;
            return (double)Math.Round(R, 1) * 100;
        }
        //������һ���ؼ��ķ���timer1_Tick,�������ʱ��ķ�����Form1.Designer.csԤ���ˣ�
        //ÿ��һ��ʱ�䣨��ʱ�������ֱ�״�������֣����ǽ���ȥ�����ڵ��ֱ���Ϣ�������б�b��a��
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            //UpdateControllerState();
            /*
            if (isControllerConnected)
            {
                UpdateControllerState();
                // �����ֱ���ص��߼�...                                         
            }
            */

            try
            {
                UpdateControllerState();
                isConnected = true;
                Console.WriteLine("���������ֱ��ɹ�");
                isConnected = true;
            }
            catch (SharpDXException ex)
            {
                isConnected = false;
                Console.WriteLine("�����ֱ����ִ������!");
                isConnected = false;
            }

            //            changecolor();


            ////////////////////////////////////////
            ////////��ȡ��ҡ������ҡ������ֵ////////
            ////////////////////////////////////////
            //Ŀǰ����Ʒ����Ƿ��հ����Ƿ������һ��͵��ӽǸ�Ϊ��̬����
            //10.5���ڵ���Ʒ����ָ��ˣ���ȭ��


            //��ҡ������ҡ�˵ĽǶ�ֵ����
            //angle1 = yjnangle(angle1,x1,y1);//�õ���Ҧ���ϵķ���yjnangle���Ƿ��ϱ���贸�ʽ��
            //angle1 = sffw(angle1, x1, y1);//������Ҫ����ķַ�λ
            //angle2 = yjnangle(angle2,x2,y2);//�õ���Ҧ���ϵķ���yjnangle���Ƿ��ϱ���贸�ʽ��
            //angle2 = sffw(angle2, x2, y2);//������Ҫ����ķַ�λ


            ////////////////////////////////////
            ////////��ȡ�ֱ�ȫ��״̬��Ϣ////////
            ////////////////////////////////////
            // ʹ�� switch ���������ť��״̬
            GamepadButtonFlags pressedButtons = currentGamepadState.Buttons;

            // �ж�A��ť�Ƿ���
            /*if ((pressedButtons & GamepadButtonFlags.A) != 0)
            {
                label1.Text = "Press"; // A��ť����
            }
            else
            {
                label1.Text = "Release"; // A��ťδ����
            }*/
            //�ұ���ĸ����
            a[0] = (int)(pressedButtons & GamepadButtonFlags.A);
            a[1] = (int)(pressedButtons & GamepadButtonFlags.B);
            a[2] = (int)(pressedButtons & GamepadButtonFlags.X);
            a[3] = (int)(pressedButtons & GamepadButtonFlags.Y);
            //���ʮ������
            a[4] = (int)(pressedButtons & GamepadButtonFlags.DPadDown);
            a[5] = (int)(pressedButtons & GamepadButtonFlags.DPadRight);
            a[6] = (int)(pressedButtons & GamepadButtonFlags.DPadLeft);
            a[7] = (int)(pressedButtons & GamepadButtonFlags.DPadUp);
            //�м�˵���
            a[8] = (int)(pressedButtons & GamepadButtonFlags.Start);
            a[9] = (int)(pressedButtons & GamepadButtonFlags.Back);
            //������׼��
            a[10] = (int)(pressedButtons & GamepadButtonFlags.RightShoulder);
            a[11] = (int)(pressedButtons & GamepadButtonFlags.LeftShoulder);
            //����ҡ�˰��¼�
            a[12] = (int)(pressedButtons & GamepadButtonFlags.LeftThumb);
            a[13] = (int)(pressedButtons & GamepadButtonFlags.RightThumb);
            //��ҡ��X��Y����ƫ����
            a[14] = currentGamepadState.LeftThumbX;
            a[15] = currentGamepadState.LeftThumbY;
            //��ҡ��X��Y����ƫ����
            a[16] = currentGamepadState.RightThumbX;
            a[17] = currentGamepadState.RightThumbY;
            //���Ұ������
            a[18] = (int)(GetLeftTriggerValue());
            a[19] = (int)(GetRightTriggerValue());


            try
            {
                //�ƽ�������ģʽ��@MT{�ƽ������}{�ƽ���ת�ٵ�λ}$
                if (a[18] != 0)
                {
                    string sendtext = $"@ MT{comboBox4.SelectedIndex + 1} {-a[18] / 10} $";
                    client.Send(sendtext.GetBytes());
                }
                if (a[19] != 0)
                {
                    string sendtext = $"@ MT{comboBox4.SelectedIndex + 1} {a[19] / 10} $";
                    client.Send(sendtext.GetBytes());
                }

                //ģʽ����
                if (a[8] != b[8])
                {
                    if (a[8] != 0)
                    {
                        string sendtext = $"@MDP{comboBox1.SelectedIndex + 1} 1$";
                        client.Send(sendtext.GetBytes());
                    }
                    else
                    {
                        //string sendtext = $"@MDP{comboBox1.SelectedIndex + 1} 0$";
                        //client.Send(sendtext.GetBytes());
                    }
                    b[8] = a[8];
                }
                if (a[9] != b[9])
                {
                    if (a[9] != 0)
                    {
                        string sendtext = $"@MDM{comboBox1.SelectedIndex + 1} 1$";
                        client.Send(sendtext.GetBytes());
                    }
                    else
                    {
                        //string sendtext = $"@MDM{comboBox1.SelectedIndex + 1} 0$";
                        //client.Send(sendtext.GetBytes());
                    }
                    b[9] = a[9];
                }

                //ƽ��ģʽ��@MM"ƽ����������"{ƽ���ٶȲ���}$
                if (a[7] != 0)
                {
                    string sendtext = $"@MMF{comboBox2.SelectedIndex + 1}$";
                    client.Send(sendtext.GetBytes());
                }
                if (a[4] != 0)
                {
                    string sendtext = $"@MMB{comboBox2.SelectedIndex + 1}$";
                    client.Send(sendtext.GetBytes());
                }
                if (a[10] != 0)
                {
                    string sendtext = $"@MMU{comboBox2.SelectedIndex + 1}$";
                    client.Send(sendtext.GetBytes());
                }
                if (a[11] != 0)
                {
                    string sendtext = $"@MMD{comboBox2.SelectedIndex + 1}$";
                    client.Send(sendtext.GetBytes());
                }

                //ת��ģʽ��@MR"ת����������"{ת���ٶȲ���}$
                if (a[6] != 0)
                {
                    string sendtext = $"@MRL{comboBox3.SelectedIndex + 1}$";
                    client.Send(sendtext.GetBytes());
                }
                if (a[5] != 0)
                {
                    string sendtext = $"@MRR{comboBox3.SelectedIndex + 1}$";
                    client.Send(sendtext.GetBytes());
                }
                if (a[3] != 0)
                {
                    string sendtext = $"@MRB{comboBox3.SelectedIndex + 1} $";
                    client.Send(sendtext.GetBytes());
                }
                if (a[0] != 0)
                {
                    string sendtext = $"@MRF{comboBox3.SelectedIndex + 1} $";
                    client.Send(sendtext.GetBytes());
                }
                if (a[2] != 0)
                {
                    string sendtext = $"@MRU{comboBox3.SelectedIndex + 1} $";
                    client.Send(sendtext.GetBytes());
                }
                if (a[1] != 0)
                {
                    string sendtext = $"@MRD{comboBox3.SelectedIndex + 1} $";
                    client.Send(sendtext.GetBytes());
                }

                //ҡ�˺��п���ģʽ��@MS{�ٶȲ���}{�ǶȲ���}$
                if (a[14] != 0 || a[15] != 0 || (a[14] != b[14] || a[15] != b[15]))
                {
                    int NormofLeftThumb, AngleofLeftThumb, bNormofLeftThumb, bAngleofLeftThumb;
                    (NormofLeftThumb, AngleofLeftThumb) = EightAngle(a[14], a[15]);
                    (bNormofLeftThumb, bAngleofLeftThumb) = EightAngle(b[14], b[15]);
                    //(NormofRightThumb, AngleofRightThumb) = getNormAndAngle(a[16], a[17]);
                    //string sendtext = $"@MP{NormofRightThumb},{AngleofRightThumb}$";
                    string sendtext = $"@ MS {AngleofLeftThumb} 1 $";
                    string bsendtext = $"@ MS {bAngleofLeftThumb} 0 $";
                    if (a[14] != 0 || a[15] != 0)
                        client.Send(sendtext.GetBytes());
                    else
                    {
                        client.Send(bsendtext.GetBytes());
                    }
                    b[14] = a[14];
                    b[15] = a[15];
                }
                if (a[12] != 0)
                {
                    string sendtext = $"@MAS$";
                    client.Send(sendtext.GetBytes());
                }

                //ҡ����̬����ģʽ��@MP{���Ȳ���}{��λ����}$
                if (a[16] != 0 || a[17] != 0 || (a[16] != b[16] || a[17] != b[17]))
                {
                    int NormofRightThumb, AngleofRightThumb, bNormofRightThumb, bAngleofRightThumb;
                    (NormofRightThumb, AngleofRightThumb) = ForthAngle(a[16], a[17]);
                    (bNormofRightThumb, bAngleofRightThumb) = ForthAngle(b[16], b[17]);
                    //(NormofRightThumb, AngleofRightThumb) = getNormAndAngle(a[16], a[17]);
                    //string sendtext = $"@MP{NormofRightThumb},{AngleofRightThumb}$";
                    string sendtext = $"@ MP {AngleofRightThumb} 1 $";
                    string bsendtext = $"@ MP {bAngleofRightThumb} 0 $";
                    if (a[16] != 0 || a[17] != 0)
                        client.Send(sendtext.GetBytes());
                    else
                    {
                        client.Send(bsendtext.GetBytes());
                    }
                    b[16] = a[16];
                    b[17] = a[17];
                }
                    
         


                if (a[13] != 0)
                {
                    string sendtext = $"@MAP$";
                    client.Send(sendtext.GetBytes());
                    b[16] = a[16];
                    b[17] = a[17];
                }
            }

            catch (Exception ex)
            {
                // �����쳣�������������ﴦ���쳣���߼�¼��־
                Print("�ֱ����ݷ����쳣!");
                //MessageBox.Show("��򿪴���");
            }

            // �������������ť...

            //// ���߿���ʹ�� switch ������������ť
            //switch (currentGamepadState.Buttons)
            //{
            //    case GamepadButtonFlags.A:
            //        // A ��ť����
            //        // ִ����Ӧ����
            //        break;
            //    case GamepadButtonFlags.B:
            //        // B ��ť����
            //        // ִ����Ӧ����
            //        break;
            //        // �������������ť...
            //}
        }

        /*�����Ǽ���ҡ��ģֵ��Ƕȵĳ����㷨*/
        public (int, int) getNormAndAngle(int x, int y)
        {
            if (Math.Abs(x) <= 1600)
                x = 0;
            if (Math.Abs(y) <= 1600)
                y = 0;
            double norm = Math.Sqrt(x * x + y * y) / 410;
            double angle = Math.Round(Math.Atan2(y, x), 4) * 180 / 3.1415;//+ 180
            return ((int)norm, (int)angle);
        }

        public (int, int) EightAngle(int x, int y)   //8������  MATElyr
        {
            if (Math.Abs(x) <= 1600)
                x = 0;
            if (Math.Abs(y) <= 1600)
                y = 0;
            double norm = Math.Sqrt(x * x + y * y) / 410;

            // ʹ�� Math.Atan2 ����Ƕȣ����ȣ�
            double angleRad = Math.Atan2(y, x);

            // ���Ƕȴӻ���ת��Ϊ����
            double angleDeg = angleRad * (180 / Math.PI);

            // ���Ƕ�ת��Ϊ 0 �� 360 �ȷ�Χ
            if (angleDeg < 0)
                angleDeg += 360;

            // ���ݽǶȽ��� 8 ����
            int partition = (int)((angleDeg + 22.5)/ 45) % 8;

            return ((int)norm, partition);
        }

        public (int, int) ForthAngle(int x, int y)   //4������   MATElyr
        {
            if (Math.Abs(x) <= 1600)
                x = 0;
            if (Math.Abs(y) <= 1600)
                y = 0;
            double norm = Math.Sqrt(x * x + y * y) / 410;

            // ʹ�� Math.Atan2 ����Ƕȣ����ȣ�
            double angleRad = Math.Atan2(y, x);

            // ���Ƕȴӻ���ת��Ϊ����
            double angleDeg = angleRad * (180 / Math.PI);

            // ���Ƕ�ת��Ϊ 0 �� 360 �ȷ�Χ
            if (angleDeg < 0)
                angleDeg += 360;

            // ���ݽǶȽ��� 8 ����
            int partition = (int)((angleDeg + 22.5) / 90) % 4;

            return ((int)norm, partition);
        }

        /*�������������ķַ�λ�ķ�������*/
        public double sffw(double angle0, double a1, double b1)
        {
            String f = "Z";
            if (a1 == b1 & a1 == 0)
            {
                angle0 = 999;
            }
            else
            {
                if (angle0 <= 135 && angle0 >= 45)
                {
                    angle0 = 0;
                    f = "N";
                }
                else if (angle0 >= -135 && angle0 <= -45)
                {
                    angle0 = 180;
                    f = "S";
                }
                else if (angle0 <= 45 && angle0 >= -45)
                {
                    angle0 = 90;
                    f = "E";
                }
                else
                {
                    angle0 = -90;
                    f = "W";
                }
            }
            return angle0;
        }
        /*������Ҧ���ϼ��㷽λ�ǵķ�������*/
        public double yjnangle(double angle0, double a1, double b1)
        {
            if ((a1 > 0 && b1 > 0) || (a1 == 0 && b1 > 0))
            {
                angle0 = 90 - angle0;
            }
            else if ((a1 < 0 && b1 > 0) || (a1 < 0 && b1 == 0))
            {
                angle0 = -angle0 + 90;
            }
            else if (a1 < 0 && b1 < 0)
            {
                angle0 = -270 - angle0;
            }
            else if ((a1 > 0 && b1 < 0) || (a1 > 0 && b1 == 0) || (a1 == 0 && b1 < 0))
            {
                angle0 = 90 - angle0;
            }
            return angle0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (sensorState[0] == 'C')
            {
                sensorLabel1.Text = "Opened";
                sensorButton1.UseVisualStyleBackColor = true;
                sensorState[0] = 'O';
            }
            else
            {
                sensorLabel1.Text = "Closed";
                sensorButton1.UseVisualStyleBackColor = false;
                sensorState[0] = 'C';
            }
        }

        private void sensorButton2_Click(object sender, EventArgs e)
        {
            if (sensorState[1] == 'C')
            {
                sensorLabel2.Text = "Opened";
                sensorButton2.UseVisualStyleBackColor = true;
                sensorState[1] = 'O';
            }
            else
            {
                sensorLabel2.Text = "Closed";
                sensorButton2.UseVisualStyleBackColor = false;
                sensorState[1] = 'C';
            }
        }

        private void sensorButton3_Click(object sender, EventArgs e)
        {
            if (sensorState[2] == 'C')
            {
                sensorLabel3.Text = "Opened";
                sensorButton3.UseVisualStyleBackColor = true;
                sensorState[2] = 'O';
            }
            else
            {
                sensorLabel3.Text = "Closed";
                sensorButton3.UseVisualStyleBackColor = false;
                sensorState[2] = 'C';
            }
        }

        private void sensorButton4_Click(object sender, EventArgs e)
        {
            if (sensorState[3] == 'C')
            {
                sensorLabel4.Text = "Opened";
                sensorButton4.UseVisualStyleBackColor = true;
                sensorState[3] = 'O';
            }
            else
            {
                sensorLabel4.Text = "Closed";
                sensorButton4.UseVisualStyleBackColor = false;
                sensorState[3] = 'C';
            }
        }

        private void sensorButton5_Click(object sender, EventArgs e)
        {
            if (sensorState[4] == 'C')
            {
                sensorLabel5.Text = "Opened";
                sensorButton5.UseVisualStyleBackColor = true;
                sensorState[4] = 'O';
            }
            else
            {
                sensorLabel5.Text = "Closed";
                sensorButton5.UseVisualStyleBackColor = false;
                sensorState[4] = 'C';
            }
        }

        private void sensorButton6_Click(object sender, EventArgs e)
        {
            if (sensorState[5] == 'C')
            {
                sensorLabel6.Text = "Opened";
                sensorButton6.UseVisualStyleBackColor = true;
                sensorState[5] = 'O';
            }
            else
            {
                sensorLabel6.Text = "Closed";
                sensorButton6.UseVisualStyleBackColor = false;
                sensorState[5] = 'C';
            }
        }

        private void sensorButton7_Click(object sender, EventArgs e)
        {
            if (sensorState[6] == 'C')
            {
                sensorLabel7.Text = "Opened";
                sensorButton7.UseVisualStyleBackColor = true;
                sensorState[6] = 'O';
            }
            else
            {
                sensorLabel7.Text = "Closed";
                sensorButton7.UseVisualStyleBackColor = false;
                sensorState[6] = 'C';
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string strSensorState = new string(sensorState);
            string command_NodeSetup = $"@RS" + strSensorState + "$";
            client.Send(command_NodeSetup.GetBytes());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string command_SensorStatus = "@RI$";
            client.Send(command_SensorStatus.GetBytes());
        }
        #endregion

        #region code for serialPort
        //      private Timer sendTimer = new Timer();//�����򴮿��Զ������ֱ���Ϣ

        /*
                private SerialPort serialPort = new SerialPort();

                private void RefreshAvailablePorts()
                {
                    // ������еĴ����б�
                    comboBox1.Items.Clear();

                    // ��ȡ������Ͽ��õĴ����б�
                    string[] availablePorts = SerialPort.GetPortNames();

                    // �����ô�����ӵ� ComboBox
                    comboBox1.Items.AddRange(availablePorts);

                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0; // Ĭ��ѡ���һ������
                    }
                }

                private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
                {
                    if (serialPort.IsOpen)
                    {
                        try
                        {
                            string receivedData = serialPort.ReadExisting();

                            // ʹ�� Invoke ���ȵ����߳��ϸ��� UI
                            this.Invoke(new Action(() =>
                            {
                                // ���ÿһ�����ݣ������ַ����䵽��ͬ�� TextBox
                                foreach (var line in receivedData.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    if (line.StartsWith("M"))
                                    {
                                        richTextBox5.AppendText(line + Environment.NewLine); // ���� receive_rtb
                                                                                            //                                ScrollToBottom(receive_rtb); // �Զ��������ײ�
                                    }
                                    else if (line.StartsWith("A"))
                                    {
                                        richTextBox6.AppendText(line + Environment.NewLine); // ���� receive_rtb1
                                                                                             //                                ScrollToBottom(receive_rtb1); // �Զ��������ײ�
                                    }
                                }
                            }));
                        }
                        catch (Exception ex)
                        {
                            // �����쳣������־��¼
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                    }
                }
        */
        private void Form1_Load(object sender, EventArgs e)
        {
            /*
                        RefreshAvailablePorts();
                        serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
                        if (comboBox1.Items.Count > 0)
                        {
                            comboBox1.SelectedIndex = 0; // Ĭ��ѡ���һ������
                        }
                        changecolor();
            */
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        /*
private void button8_Click_1(object sender, EventArgs e)
{
MessageBox.Show($"���ڲ�����{serialPort.PortName}, �����ʣ�{serialPort.BaudRate}, ����λ��{serialPort.DataBits}, У��λ��{serialPort.Parity}, ֹͣλ��{serialPort.StopBits}");

if (!serialPort.IsOpen)
{
// ���ô��ڲ���
serialPort.PortName = comboBox1.SelectedItem.ToString();
serialPort.BaudRate = 9600; // ���ò�����
serialPort.DataBits = 8;    // ��������λ
serialPort.Parity = Parity.None; // ����У��λ
serialPort.StopBits = StopBits.One; // ����ֹͣλ

try
{
 serialPort.Open(); // �򿪴���
 button8.Text = "�رմ���";
}
catch (Exception ex)
{
 MessageBox.Show("�޷��򿪴��ڣ�" + ex.Message);
}
}
else
{
serialPort.Close(); // �رմ���
button8.Text = "�򿪴���";
}
}
*/
        #endregion

    }
}