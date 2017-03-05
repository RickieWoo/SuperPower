using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using System.Security.Cryptography;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace clientOfGod
{
  
    public partial class chatClient : Form
    {
        private int serverPort;
        private TcpClient clientSocket;
        private NetworkStream networkStream;
        private StreamReader sr;
        private Thread recThread = null;
        private string serverAddress;
        private string clientName;
        private bool connected = false;
        
        public chatClient()
        {
            InitializeComponent();
        }

        ////定义客户属性
        //public Client(string Name, EndPoint Host, Thread CLThread, Socket Sock)
        //{
        //    this.Name = Name;
        //    this.Host = Host;
        //    this.CLThread = CLThread;
        //    this.Sock = Sock;
        //}


        //public new string Name { get; set; }

        //public EndPoint Host { get; set; }

        //public Thread CLThread { get; set; }

        //public Socket Sock { get; set; }

        //定义并初始化客户接收数据的线程和套接字
        private Thread threadClient = null;
        private Socket socketClient = null;


        private void Client_Load(object sender, EventArgs e)
        {
            
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            if(nameBox.Text==""||IPBox.Text==""||portBox.Text=="")
            {
                MessageBox.Show("请输入完整信息", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                //将当前客户端属性赋值，并不可再更改
                clientName = nameBox.Text.Trim();
                serverAddress = IPBox.Text.Trim();
                serverPort = int.Parse(portBox.Text.Trim());
                nameBox.Enabled = false;
                IPBox.Enabled = false;
                portBox.Enabled = false;
            }
            ///<remarks>Trim:从当前字符串移除所有前导和尾随空白字符</remarks>
            try
            {
                IPAddress ipAdd = IPAddress.Parse(this.IPBox.Text.Trim());
                IPEndPoint port = new IPEndPoint(ipAdd, int.Parse(this.portBox.Text.Trim()));
                socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socketClient.Connect(port);
                //开启一个线程实例
                threadClient = new Thread(RecMsg);
                //设为后台线程
                threadClient.IsBackground = true;
                //启动线程
                threadClient.Start();
                showMsg("***Connection is OK***");
                //recOnlineList();
            }
            catch(Exception )
            {
                showMsg("***Sever is offline***");
            }
        }
       
        /// <summary>
        /// 接收服务器发来的当前在线ip
        /// </summary>
        //void recOnlineList()
        //{

        //}
        //private delegate void changeList(string list);
        /// <summary>
        /// 添加在线ip至列表
        /// </summary>
        /// <param name="list"></param>
      //void showList(object list)
      //  {
      //      if(this.InvokeRequired)
      //      {
      //          this.BeginInvoke(new changeList(showList), list);
      //      }
      //      else
      //      {
      //          foreach(var itme in list)
      //          onlineList.CreateObjRef(list );
      //      }
      //  }
        private delegate void changeText(string msg);
        /// <summary>
        /// 添加消息到文本框
        /// </summary>
        /// <param name="str">消息</param>
        void showMsg(string str)
        {
            if(this.InvokeRequired)
            {
                this.BeginInvoke(new changeText(showMsg),str); 
            }
            else
            {
                chatBox.AppendText(str  +"\t" + DateTime.Now.ToString()+"\r\n");
            }
        }
        void RecMsg()
        {
            while(true)
            {
                try
                {
                    //创建一个字节数组接收数据
                    byte[] arrMsgRec = new byte[1024 * 2014 * 2];
                    //接收数据
                    int length = socketClient.Receive(arrMsgRec);
                    //将接受的字符转成字符串
                    string strMsgRec = System.Text.Encoding.UTF8.GetString(arrMsgRec, 0, length);
            
                 //  if(!strMsgRec)
                    //将信息显示
                    showMsg("服务器：" + strMsgRec+"\t"+DateTime.Now.ToString());

                }
                catch(Exception e)
                {
                    showMsg("the Sever is offline");
                    //关闭当前套接字
                    socketClient.Close();
                    //结束当前线程
                    threadClient.Abort();//mark
                }
            }
        }

        //private void ReceiveChat()
        //{
        //    bool alive = true;
        //    while (alive)
        //    {
        //        try
        //        {
        //            Byte[] buffer = new Byte[2048000];
        //            networkStream.Read(buffer, 0, buffer.Length);
        //            string time = System.DateTime.Now.ToLongTimeString();
        //            string chatter = System.Text.Encoding.Default.GetString(buffer);

        //            string[] tokens = chatter.Split(new Char[] { '|' });

        //            if (tokens[0] == "CHAT")
        //            {
        //                msgBox.AppendText(tokens[1].Trim());
        //                msgBox.AppendText("  " + time + " \r\n");
        //                msgBox.AppendText(tokens[2].Trim());



        //            }
        //            if (tokens[0] == "PRIV")
        //            {

        //                msgBox.AppendText("悄悄话来自");
        //                msgBox.AppendText(tokens[1].Trim());
        //                msgBox.AppendText("  " + time + "\r\n");
        //                msgBox.AppendText(tokens[2] + "\r\n");//不同聊天模式token格式不一样 



        //            }
        //            if (tokens[0] == "JOIN")
        //            {
        //                msgBox.AppendText(time + "  ");
        //                msgBox.AppendText(tokens[1].Trim());
        //                msgBox.AppendText("加入聊天室" + "\r\n");
        //                string newcomer = tokens[1].Trim(new char[] { '\r', '\n' });
        //                //string newcomer = tokens[1].Trim();  
        //                //MessageBox.Show(newcomer);  
        //                onlineList.Items.Add(newcomer);
        //            }
        //            if (tokens[0] == "LEAVE")
        //            {
        //                msgBox.AppendText(time + "  ");
        //                msgBox.AppendText(tokens[1].Trim());
        //                msgBox.AppendText("退出了聊天室" + "\r\n");


        //                string leaver = tokens[1].Trim(new char[] { '\r', '\n' });


        //                for (int n = 0; n < onlineList.Items.Count; n++)
        //                {
        //                    if (onlineList.Items[n].ToString().CompareTo(leaver) == 0)
        //                    {
        //                        onlineList.Items.RemoveAt(n);
        //                    }
        //                }


        //            }
                   
        //        }
        //        catch (Exception) { }
        //    }
        //}
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            
            if(socketClient!=null)
            {
                if (groupBtn.Checked)//群聊模式
                {
                    string sendMsg = "CHAT|" + clientName + "| " + msgBox.Text + "\r\n";
                    //string sendMsg = this.msgBox.Text.Trim();
                    byte[] arrMsg = System.Text.Encoding.Default.GetBytes(sendMsg);
                    socketClient.Send(arrMsg);
                    showMsg(sendMsg + "\t" + DateTime.Now.ToString());
                    networkStream.Write(arrMsg, 0, arrMsg.Length);
                    msgBox.Text = "";
                }
                else//默认私聊模式
                {
                    if(onlineList.SelectedIndex==-1)
                    {
                        MessageBox.Show("Please choose a single client to chat:)", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    string destClient = onlineList.SelectedItem.ToString();
                    string sendMsg = "PRIV|" + clientName + "| " + msgBox.Text + "|"+destClient;
                    //string sendMsg = this.msgBox.Text.Trim();
                    byte[] arrMsg = System.Text.Encoding.Default.GetBytes(sendMsg);
                    socketClient.Send(arrMsg);
                    showMsg(sendMsg + "\t" + DateTime.Now.ToString());
                    networkStream.Write(arrMsg, 0, arrMsg.Length);
                    msgBox.Text = "";
                }
            }
            else
            {
                showMsg("The Sever is Offline, Please connect a sever");
                return;
            }
        }

        private void onlineList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
