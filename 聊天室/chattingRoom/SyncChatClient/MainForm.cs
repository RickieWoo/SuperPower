using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SyncChatClient
{
#region
    public class JFPackage
    {
        //结构体序列化
        [System.Serializable]
        //4字节对齐 iphone 和 android上可以1字节对齐
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct WorldPackage
        {
            public byte mEquipID;
            public byte mAnimationID;
            public byte mHP;
            public short mPosx;
            public short mPosy;
            public short mPosz;
            public short mRosx;
            public short mRosy;
            public short mRosz;

            public WorldPackage(short posx, short posy, short posz, short rosx, short rosy, short rosz, byte equipID, byte animationID, byte hp)
            {
                mPosx = posx;
                mPosy = posy;
                mPosz = posz;
                mRosx = rosx;
                mRosy = rosy;
                mRosz = rosz;
                mEquipID = equipID;
                mAnimationID = animationID;
                mHP = hp;
            }

        };

    }
#endregion
    public partial class MainForm : Form
    {
        private string ServerIP; //IP
        private int port;   //端口
        private bool isExit = false;
        private TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;
        /// <summary>
        /// 用于UDP发送的网络服务类
        /// </summary>
        private UdpClient udpcSend;
        private List<Socket> Lst_SokServer = new List<Socket>(); //套接字集合
        /// <summary>
        /// 用于UDP接收的网络服务类
        /// </summary>
        private UdpClient udpcRecv;
        #region
        //创建Socket对象， 这里的连接类型是TCP
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        
        //JFPackage.WorldPackage是封装的结构体，
        //在与服务器交互的时候会传递这个结构体
        //当客户端接到到服务器返回的数据包时，结构体add存在链表中。
        public List<JFPackage.WorldPackage> worldpackage;
        //private UdpClient sendUdpClient;
#endregion
        public MainForm()
        {
            InitializeComponent();
            Random r = new Random((int)DateTime.Now.Ticks);
            txt_UserName.Text = "user" + r.Next(100, 999);
            lst_OnlineUser.HorizontalScrollbar = true;
            SetServerIPAndPort();
        }

        /// <summary>
        /// 根据当前程序目录的文本文件‘ServerIPAndPort.txt’内容来设定IP和端口
        /// 格式：127.0.0.1:8885
        /// </summary>
        private void SetServerIPAndPort()
        {
            try
            {
                FileStream fs = new FileStream("ServerIPAndPort.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string IPAndPort = sr.ReadLine();
                ServerIP = IPAndPort.Split(':')[0]; //设定IP
                port = int.Parse(IPAndPort.Split(':')[1]); //设定端口
                sr.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("配置IP与端口失败，错误原因：" + ex.Message);
                Application.Exit();
            }
        }

        /// <summary>
        /// 【登陆】按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Login_Click(object sender, EventArgs e)
        {
            btn_Login.Enabled = false;
            try
            {
                
                //IPAddress ipAd = IPAddress.Parse("182.150.193.7");
                client = new TcpClient();
                client.Connect(IPAddress.Parse(ServerIP), port);
              
                IPEndPoint local = new IPEndPoint(IPAddress.Parse(ServerIP), port);

            //    receiveUdpClient = new UdpClient(local);
                AddTalkMessage("连接成功");
            }
            catch(Exception ex)
            {
                AddTalkMessage("连接失败，原因：" + ex.Message);
                btn_Login.Enabled = true;
                return;
            }
            //获取网络流
            NetworkStream networkStream = client.GetStream();
            //将网络流作为二进制读写对象
            br = new BinaryReader(networkStream);
            bw = new BinaryWriter(networkStream);
            //把用户名和登录信息发送给服务器
            SendMessage("Login," + txt_UserName.Text);
            //接收服务器消息线程
            Thread threadReceive = new Thread(new ThreadStart(ReceiveData));
            threadReceive.IsBackground = true;
            threadReceive.Start();
        }
        #region 根据定义一个头表示长度来检测UDP差错

        private void ReceiveSocket()
        {
            //在这个线程中接受服务器返回的数据
            while (true)
            {

                if (!clientSocket.Connected)
                {
                    //与服务器断开连接跳出循环
                    AddTalkMessage("Failed to clientSocket server.");
                    clientSocket.Close();
                    break;
                }
                try
                {
                    //接受数据保存至bytes当中
                    byte[] bytes = new byte[4096];
                    //Receive方法中会一直等待服务端回发消息
                    //如果没有回发会一直在这里等着。
                    int i = clientSocket.Receive(bytes);
                    if (i <= 0)
                    {
                        clientSocket.Close();
                        break;
                    }

                    //这里条件可根据你的情况来判断。
                    //因为我目前的项目先要监测包头长度，
                    //我的包头长度是2，所以我这里有一个判断
                    if (bytes.Length > 2)
                    {
                        SplitPackage(bytes, 0);
                    }
                    else
                    {
                        AddTalkMessage("length is not  >  2");
                    }

                }
                catch (Exception e)
                {
                    AddTalkMessage("Failed to clientSocket error." + e);
                    clientSocket.Close();
                    break;
                }
            }
        }

        private void SplitPackage(byte[] bytes, int index)
        {
            //在这里进行拆包，因为一次返回的数据包的数量是不定的
            //所以需要给数据包进行查分。
            while (true)
            {
                //包头是2个字节
                byte[] head = new byte[2];
                int headLengthIndex = index + 2;
                //把数据包的前两个字节拷贝出来
                Array.Copy(bytes, index, head, 0, 2);
                //计算包头的长度
                short length = BitConverter.ToInt16(head, 0);
                //当包头的长度大于0 那么需要依次把相同长度的byte数组拷贝出来
                if (length > 0)
                {
                    byte[] data = new byte[length];
                    //拷贝出这个包的全部字节数
                    Array.Copy(bytes, headLengthIndex, data, 0, length);
                    //把数据包中的字节数组强制转换成数据包的结构体
                    //BytesToStruct()方法就是用来转换的
                    //这里需要和你们的服务端程序商量，
                    JFPackage.WorldPackage wp = new JFPackage.WorldPackage();
                    wp = (JFPackage.WorldPackage)BytesToStruct(data, wp.GetType());
                    //把每个包的结构体对象添加至链表中。
                    worldpackage.Add(wp);
                    //将索引指向下一个包的包头
                    index = headLengthIndex + length;

                }
                else
                {
                    //如果包头为0表示没有包了，那么跳出循环
                    break;
                }
            }
        }
        //向服务端发送数据包，也就是一个结构体对象
        public void SendMessage(object obj)
        {

            if (!clientSocket.Connected)
            {
                clientSocket.Close();
                return;
            }
            try
            {
                //先得到数据包的长度
                short size = (short)Marshal.SizeOf(obj);
                //把数据包的长度写入byte数组中
                byte[] head = BitConverter.GetBytes(size);
                //把结构体对象转换成数据包，也就是字节数组
                byte[] data = StructToBytes(obj);

                //此时就有了两个字节数组，一个是标记数据包的长度字节数组， 一个是数据包字节数组，
                //同时把这两个字节数组合并成一个字节数组

                byte[] newByte = new byte[head.Length + data.Length];
                Array.Copy(head, 0, newByte, 0, head.Length);
                Array.Copy(data, 0, newByte, head.Length, data.Length);

                //计算出新的字节数组的长度
                int length = Marshal.SizeOf(size) + Marshal.SizeOf(obj);

                //向服务端异步发送这个字节数组
                IAsyncResult asyncSend = clientSocket.BeginSend(newByte, 0, length, SocketFlags.None, new AsyncCallback(sendCallback), clientSocket);
                //监测超时
                bool success = asyncSend.AsyncWaitHandle.WaitOne(5000, true);
                if (!success)
                {
                    clientSocket.Close();
                    AddTalkMessage("Time Out !");
                }

            }
            catch (Exception e)
            {
                AddTalkMessage("send message error: " + e);
            }
        }
        private void sendCallback(IAsyncResult asyncSend)
        {

        }
        //结构体转字节数组
        public byte[] StructToBytes(object structObj)
        {

            int size = Marshal.SizeOf(structObj);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(structObj, buffer, false);
                byte[] bytes = new byte[size];
                Marshal.Copy(buffer, bytes, 0, size);
                return bytes;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
        //字节数组转结构体
        public object BytesToStruct(byte[] bytes, Type strcutType)
        {
            int size = Marshal.SizeOf(strcutType);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(bytes, 0, buffer, size);
                return Marshal.PtrToStructure(buffer, strcutType);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }

        }
#endregion 
        /// <summary>
        /// 处理服务器信息
        /// </summary>
        private void ReceiveData()
        {
            string receiveString = null;
            while (isExit == false)
            {
                try
                {
                    //从网络流中读出字符串
                    //此方法会自动判断字符串长度前缀，并根据长度前缀读出字符串
                    receiveString = br.ReadString();
                }
                catch
                {
                    if (isExit == false)
                    {
                        MessageBox.Show("与服务器失去连接");
                    }
                    break;
                }
                string[] splitString = receiveString.Split(',');
                string command = splitString[0].ToLower();
                switch (command)
                {
                    case "login":   //格式： login,用户名
                        AddOnline(splitString[1]);
                        break;
                    case "logout":  //格式： logout,用户名
                        RemoveUserName(splitString[1]);
                        break;
                    case "talk":    //格式： talk,用户名,对话信息
                        AddTalkMessage(splitString[1] + "：\r\n");
                        AddTalkMessage(receiveString.Substring(splitString[0].Length + splitString[1].Length+2));
                        break;
                    default:
                        AddTalkMessage("什么意思啊：" + receiveString);
                        break;
                }
            }
            Application.Exit();
        }

        /// <summary>
        /// 向服务端发送消息
        /// </summary>
        /// <param name="message"></param>
        private void SendMessage(string message)
        {
            try
            {
                //将字符串写入网络流，此方法会自动附加字符串长度前缀
                bw.Write(message);
                bw.Flush();
            }
            catch
            {
                AddTalkMessage("发送失败");
            }
        }
       

        private delegate void AddTalkMessageDelegate(string message);
        /// <summary>
        /// 在聊天对话框（txt_Message）中追加聊天信息
        /// </summary>
        /// <param name="message"></param>
        private void AddTalkMessage(string message)
        {
            if (txt_Message.InvokeRequired)
            {
                AddTalkMessageDelegate d = new AddTalkMessageDelegate(AddTalkMessage);
                txt_Message.Invoke(d, new object[] { message });
            }
            else
            {
                txt_Message.AppendText(message + Environment.NewLine);
                txt_Message.ScrollToCaret();
            }
        }

        private delegate void AddOnlineDelegate(string message);
        /// <summary>
        /// 在线框（lst_Online)中添加其他客户端信息
        /// </summary>
        /// <param name="userName"></param>
        private void AddOnline(string userName)
        {
            if (lst_OnlineUser.InvokeRequired)
            {
                AddOnlineDelegate d = new AddOnlineDelegate(AddOnline);
                lst_OnlineUser.Invoke(d, new object[] { userName });
            }
            else
            {
                lst_OnlineUser.Items.Add(userName);
                lst_OnlineUser.SelectedIndex = lst_OnlineUser.Items.Count - 1;
                lst_OnlineUser.ClearSelected();
            }
        }

        private delegate void RemoveUserNameDelegate(string userName);
        /// <summary>
        /// 在在线框(lst_Online)中移除不在线的客户端信息
        /// </summary>
        /// <param name="userName"></param>
        private void RemoveUserName(string userName)
        {
            if (lst_OnlineUser.InvokeRequired)
            {
                RemoveUserNameDelegate d = new RemoveUserNameDelegate(RemoveUserName);
                lst_OnlineUser.Invoke(d, userName);
            }
            else
            {
                lst_OnlineUser.Items.Remove(userName);
                lst_OnlineUser.SelectedIndex = lst_OnlineUser.Items.Count - 1;
                lst_OnlineUser.ClearSelected();
            }
        }

        /// <summary>
        /// 【发送】按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Send_Click(object sender, EventArgs e)
        {
            if (lst_OnlineUser.SelectedIndex != -1)
            {
                SendMessage("Talk," + lst_OnlineUser.SelectedItem + "," + txt_SendText.Text + "\r\n");
                txt_SendText.Clear();
            }
            else
            {
                MessageBox.Show("请先在【当前在线】中选择一个对话着");
            }
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //未与服务器连接前 client 为 null
            if (client != null)
            {
                try
                {
                    SendMessage("Logout," + txt_UserName.Text);
                    isExit = true;
                    br.Close();
                    bw.Close();
                    client.Close();
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 在发送信息的文本框中按下【Enter】键触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_SendText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                //触发【发送】按钮的单击事件
                btn_Send.PerformClick();
            }
        }

        private void btn_LoadOnlineUser_Click(object sender, EventArgs e)
        {

        }

        private void lst_OnlineUser_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_Message_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
