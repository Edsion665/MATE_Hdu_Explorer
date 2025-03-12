using STTech.BytesIO.Tcp;
using System.Windows.Forms;
using System.IO.Ports;

using System;//基本的系统类
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
        private Controller controller;//xbox控制对象
        private Gamepad currentGamepadState;//xbox状态

        int[] a = new int[30];//手柄基本信息20个变量
        int[] b = new int[30];//存储前一时刻手柄信息
        char[] nodeState = { 'C', 'C', 'C', 'C', 'C', 'C' };//树莓派节点状态 
        char[] sensorState = { 'C', 'C', 'C', 'C', 'C', 'C', 'C' };//传感器节点状态 
        string[] lines = { "" };//使命框的文本内容
        int curLine = 0;//使命框读取的当前行数

        public Form1()
        {
            InitializeComponent();//初始化窗体组件
            client = new TcpClient() { Host = "192.168.66.2", Port = 55555}; //初始化网络客户端
            controller = new Controller(UserIndex.One);//初始化xbox控制器
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

        private bool IsButtonPressed(GamepadButtonFlags button)//判断某个按钮是否按下，返回是否
        {
            return (currentGamepadState.Buttons & button) == button;//&表示按位and
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!controller.IsConnected)
            {
                Print("Xbox控制器未连接。");
            }
            else
                Print("Xbox控制器已连接。");
        }
        //        private DateTime time1; // 记录计时器启动时间1
        //        private DateTime time2; // 记录计时器启动时间2
        //        int cdcs = 0;
        /*        public void changecolor()
                {
                    if (isConnected)
                    {
                        // 当 isConnected 变为 True 时加载并设置指定的图片
                        //                pictureBox2.Image = Properties.Resources.真正的绿灯; // 替换为您的图片路径

                    }
                    else
                    {
                        //                pictureBox2.Image = Properties.Resources.真正的红灯; // 替换为您的图片路径
                        //pictureBox2.Image = System.Drawing.Image.FromFile("Resources\\真正的红灯.png"); // 替换为您的图片路径
                        // 在 isConnected 为 False 时可以执行其他操作，或者清空图像

                    }
                }*/
        private void UpdateControllerState()
        {
            // 获取手柄状态并发送到服务器
            SharpDX.XInput.State state = controller.GetState();
            currentGamepadState = state.Gamepad;
            // string controllerInfo = label3.Text;
            // SendMessage(controllerInfo);
        }
        /// <summary>读取手柄状态信息，time1_Tick里面还有检查手柄情况的try-catch逻辑
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>下面先造出两个函数来读取扳机和按钮
        private double GetLeftTriggerValue()
        {
            // 获取左扳机（左触发器）的按下程度
            float L = currentGamepadState.LeftTrigger / 255f;
            return (double)Math.Round(L, 1) * 100;
        }
        private double GetRightTriggerValue()
        {
            // 获取右扳机（右触发器）的按下程度
            float R = currentGamepadState.RightTrigger / 255f;
            return (double)Math.Round(R, 1) * 100;
        }
        //下面是一个关键的方法timer1_Tick,这个处理时间的方法被Form1.Designer.cs预定了，
        //每隔一段时间（随时）监视手柄状况并呈现，我们将过去和现在的手柄信息储存在列表b和a中
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            //UpdateControllerState();
            /*
            if (isControllerConnected)
            {
                UpdateControllerState();
                // 其他手柄相关的逻辑...                                         
            }
            */

            try
            {
                UpdateControllerState();
                isConnected = true;
                Console.WriteLine("重新连接手柄成功");
                isConnected = true;
            }
            catch (SharpDXException ex)
            {
                isConnected = false;
                Console.WriteLine("连接手柄出现错误情况!");
                isConnected = false;
            }

            //            changecolor();


            ////////////////////////////////////////
            ////////获取左摇杆与右摇杆数据值////////
            ////////////////////////////////////////
            //目前的设计方案是仿照艾尔登法环，右滑竿的视角改为姿态调整
            //10.5现在的设计方案又改了，像拳皇


            //左摇杆与右摇杆的角度值计算
            //angle1 = yjnangle(angle1,x1,y1);//用到了姚江南的方法yjnangle，是符合鲍程璐格式的
            //angle1 = sffw(angle1, x1, y1);//马满宏要求的四分方位
            //angle2 = yjnangle(angle2,x2,y2);//用到了姚江南的方法yjnangle，是符合鲍程璐格式的
            //angle2 = sffw(angle2, x2, y2);//马满宏要求的四分方位


            ////////////////////////////////////
            ////////获取手柄全部状态信息////////
            ////////////////////////////////////
            // 使用 switch 语句检查多个按钮的状态
            GamepadButtonFlags pressedButtons = currentGamepadState.Buttons;

            // 判断A按钮是否按下
            /*if ((pressedButtons & GamepadButtonFlags.A) != 0)
            {
                label1.Text = "Press"; // A按钮按下
            }
            else
            {
                label1.Text = "Release"; // A按钮未按下
            }*/
            //右边字母区域
            a[0] = (int)(pressedButtons & GamepadButtonFlags.A);
            a[1] = (int)(pressedButtons & GamepadButtonFlags.B);
            a[2] = (int)(pressedButtons & GamepadButtonFlags.X);
            a[3] = (int)(pressedButtons & GamepadButtonFlags.Y);
            //左边十字区域
            a[4] = (int)(pressedButtons & GamepadButtonFlags.DPadDown);
            a[5] = (int)(pressedButtons & GamepadButtonFlags.DPadRight);
            a[6] = (int)(pressedButtons & GamepadButtonFlags.DPadLeft);
            a[7] = (int)(pressedButtons & GamepadButtonFlags.DPadUp);
            //中间菜单键
            a[8] = (int)(pressedButtons & GamepadButtonFlags.Start);
            a[9] = (int)(pressedButtons & GamepadButtonFlags.Back);
            //上面瞄准键
            a[10] = (int)(pressedButtons & GamepadButtonFlags.RightShoulder);
            a[11] = (int)(pressedButtons & GamepadButtonFlags.LeftShoulder);
            //左右摇杆按下键
            a[12] = (int)(pressedButtons & GamepadButtonFlags.LeftThumb);
            a[13] = (int)(pressedButtons & GamepadButtonFlags.RightThumb);
            //左摇杆X、Y方向偏移量
            a[14] = currentGamepadState.LeftThumbX;
            a[15] = currentGamepadState.LeftThumbY;
            //右摇杆X、Y方向偏移量
            a[16] = currentGamepadState.RightThumbX;
            a[17] = currentGamepadState.RightThumbY;
            //左右扳机力度
            a[18] = (int)(GetLeftTriggerValue());
            a[19] = (int)(GetRightTriggerValue());


            try
            {
                //推进器测试模式：@MT{推进器编号}{推进器转速挡位}$
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

                //模式更改
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

                //平动模式：@MM"平动控制命令"{平动速度参数}$
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

                //转动模式：@MR"转动控制命令"{转动速度参数}$
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

                //摇杆航行控制模式：@MS{速度参数}{角度参数}$
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

                //摇杆姿态控制模式：@MP{力度参数}{方位参数}$
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
                // 捕获异常，您可以在这里处理异常或者记录日志
                Print("手柄数据发生异常!");
                //MessageBox.Show("请打开串口");
            }

            // 继续检查其他按钮...

            //// 或者可以使用 switch 语句来检查多个按钮
            //switch (currentGamepadState.Buttons)
            //{
            //    case GamepadButtonFlags.A:
            //        // A 按钮按下
            //        // 执行相应操作
            //        break;
            //    case GamepadButtonFlags.B:
            //        // B 按钮按下
            //        // 执行相应操作
            //        break;
            //        // 继续检查其他按钮...
            //}
        }

        /*以下是计算摇杆模值与角度的常规算法*/
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

        public (int, int) EightAngle(int x, int y)   //8个分区  MATElyr
        {
            if (Math.Abs(x) <= 1600)
                x = 0;
            if (Math.Abs(y) <= 1600)
                y = 0;
            double norm = Math.Sqrt(x * x + y * y) / 410;

            // 使用 Math.Atan2 计算角度（弧度）
            double angleRad = Math.Atan2(y, x);

            // 将角度从弧度转换为度数
            double angleDeg = angleRad * (180 / Math.PI);

            // 将角度转换为 0 到 360 度范围
            if (angleDeg < 0)
                angleDeg += 360;

            // 根据角度进行 8 分区
            int partition = (int)((angleDeg + 22.5)/ 45) % 8;

            return ((int)norm, partition);
        }

        public (int, int) ForthAngle(int x, int y)   //4个分区   MATElyr
        {
            if (Math.Abs(x) <= 1600)
                x = 0;
            if (Math.Abs(y) <= 1600)
                y = 0;
            double norm = Math.Sqrt(x * x + y * y) / 410;

            // 使用 Math.Atan2 计算角度（弧度）
            double angleRad = Math.Atan2(y, x);

            // 将角度从弧度转换为度数
            double angleDeg = angleRad * (180 / Math.PI);

            // 将角度转换为 0 到 360 度范围
            if (angleDeg < 0)
                angleDeg += 360;

            // 根据角度进行 8 分区
            int partition = (int)((angleDeg + 22.5) / 90) % 4;

            return ((int)norm, partition);
        }

        /*以下是马满宏四分方位的方法定义*/
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
        /*以下是姚江南计算方位角的方法定义*/
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
        //      private Timer sendTimer = new Timer();//用来向串口自动发送手柄信息

        /*
                private SerialPort serialPort = new SerialPort();

                private void RefreshAvailablePorts()
                {
                    // 清空已有的串口列表
                    comboBox1.Items.Clear();

                    // 获取计算机上可用的串口列表
                    string[] availablePorts = SerialPort.GetPortNames();

                    // 将可用串口添加到 ComboBox
                    comboBox1.Items.AddRange(availablePorts);

                    if (comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0; // 默认选择第一个串口
                    }
                }

                private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
                {
                    if (serialPort.IsOpen)
                    {
                        try
                        {
                            string receivedData = serialPort.ReadExisting();

                            // 使用 Invoke 调度到主线程上更新 UI
                            this.Invoke(new Action(() =>
                            {
                                // 检查每一行数据，按首字符分配到不同的 TextBox
                                foreach (var line in receivedData.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    if (line.StartsWith("M"))
                                    {
                                        richTextBox5.AppendText(line + Environment.NewLine); // 放入 receive_rtb
                                                                                            //                                ScrollToBottom(receive_rtb); // 自动滚动到底部
                                    }
                                    else if (line.StartsWith("A"))
                                    {
                                        richTextBox6.AppendText(line + Environment.NewLine); // 放入 receive_rtb1
                                                                                             //                                ScrollToBottom(receive_rtb1); // 自动滚动到底部
                                    }
                                }
                            }));
                        }
                        catch (Exception ex)
                        {
                            // 处理异常，如日志记录
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
                            comboBox1.SelectedIndex = 0; // 默认选择第一个串口
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
MessageBox.Show($"串口参数：{serialPort.PortName}, 波特率：{serialPort.BaudRate}, 数据位：{serialPort.DataBits}, 校验位：{serialPort.Parity}, 停止位：{serialPort.StopBits}");

if (!serialPort.IsOpen)
{
// 配置串口参数
serialPort.PortName = comboBox1.SelectedItem.ToString();
serialPort.BaudRate = 9600; // 设置波特率
serialPort.DataBits = 8;    // 设置数据位
serialPort.Parity = Parity.None; // 设置校验位
serialPort.StopBits = StopBits.One; // 设置停止位

try
{
 serialPort.Open(); // 打开串口
 button8.Text = "关闭串口";
}
catch (Exception ex)
{
 MessageBox.Show("无法打开串口：" + ex.Message);
}
}
else
{
serialPort.Close(); // 关闭串口
button8.Text = "打开串口";
}
}
*/
        #endregion

    }
}