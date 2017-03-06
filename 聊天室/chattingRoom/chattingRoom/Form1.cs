using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Collections;

namespace chattingRoom
{
    public partial class FormChatServer : Form
    {
       
        public FormChatServer()
        {
            InitializeComponent();
        }


        //初始化监听客户端请求的线程和socket
        private Thread threadWatch = null;
        private Socket socketWatch = null;
        //建立一个可以存储客户端和套接字的字典
        private Dictionary<string, Socket> dict = new Dictionary<string, Socket>();
        MemoryStream ms = new MemoryStream();
        ///<summary>
        ///将信息添加到chatBox
        /// </summary>
        private delegate void changeText(string msg);
        void showMsg(string msg)
        {
            if (this.InvokeRequired)
            {
                //保证控件安全，如果是true 则说明不是一个线程，就切换到这个线程
                this.BeginInvoke(new changeText(showMsg), msg);
            }
            else
            {
                chatBox.AppendText(msg + "\r\n");
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormChatServer_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //创建一个Udp协议的套接字
            //初始化Socket 类使用指定的地址族、 套接字类型和协议。
            if (socketWatch == null)
            {
                ///<remarks>
                ///支持数据报，即为固定 （通常很小） 的最大长度的无连接的、 不可靠的消息。
                ///消息可能会丢失或重复，并且可能不按顺序抵达。
                ///一个 Socket 类型的 Dgram 不需要任何连接之前发送和接收数据，并且可以与多个对等方通信。
                ///Dgram 使用数据报协议 (Udp) 和 InterNetworkAddressFamily。
                ///</remarks>
                socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            else return;
            //取IP地址,将Ipbox中的内容转化成ip地址
            IPAddress ipAdd = IPAddress.Parse(this.IPBox.Text.Trim());
            //创建一个网络端点
            IPEndPoint ipPort = new IPEndPoint(ipAdd, int.Parse(this.PortBox.Text.Trim()));
            //将监听套接字绑定到唯一ip和端口
            socketWatch.Bind(ipPort);
            //设置监听队列的长度
            socketWatch.Listen(10);
            threadWatch = new Thread(watchConnection);
            threadWatch.IsBackground = true;//设置为后台
            threadWatch.Start();//启动线程
            showMsg("\t***服务器启动成功***");
        }
            void watchConnection()
            {
                while (true)
                {
                //创建监听的连接套接字
                Socket socketConnection = socketWatch.Accept();
                //将常见的对象添加到字典
                dict.Add(socketConnection.RemoteEndPoint.ToString(), socketConnection);
                //绑定UI控件
                BindListBox();
                //为客户端套接字添加新线程用于接受客户端的数据
                if (socketConnection != null && socketConnection.Connected)
                {
                    Thread t = new Thread(RecMsg);
                    //设置后台线程
                    t.IsBackground = true;
                    //为客户端开启线程
                    t.Start(socketConnection);
                    showMsg("***" + socketConnection.RemoteEndPoint.ToString() + "客户端进入***");
                }

                }
            }
        /// <summary>
        /// 向客户端发送信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string strMsg = this.sendBox.Text.ToString();
            //将sendBox的信息文本转化为Utf-8
            byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes(strMsg);
            //取得连接的套接字
            string selectKey = null;
            if(onlineBox.SelectedItem!=null)
            {
                selectKey = onlineBox.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Please chose a client:)");
                return;
            }
            Socket socketSend = dict[selectKey];
            //发送信息
            socketSend.Send(arrMsg);
            showMsg("Send Data:" + strMsg);
        }
        private delegate void changeListBox();
        /// <summary>
        /// 绑定ListBox数据
        /// </summary>
        void BindListBox()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new changeListBox(BindListBox));
            }
            else
            {
                onlineBox.Items.Clear();
                foreach (var item in dict)
                {
                    onlineBox.Items.Add(item.Key);
                }

            }
        }
      
        /// <summary>
        /// 接收客户端信息的线程执行代码
        /// </summary>
        /// <param name="c"></param>
        void RecMsg(object c)
        {
            Socket socketClient = c as Socket;
            while ((socketClient != null || socketClient.Connected))
            {
                try
                {
                    //创建一个字节数组接收数据
                    byte[] arrMsgRec = new byte[1024 * 1024 * 2];//mark
                    //接收数据
                    
                 
                    int length = socketClient.Receive(arrMsgRec);
                    //将字节数组转换成字符串,此时是将所有的元素都转成字符串了，而真正接收到的只有服务端发来的几个字符。
                    string strMsgRec = System.Text.Encoding.UTF8.GetString(arrMsgRec, 0, length);
                    //显示字符串
                   // Synchronize(socketClient.RemoteEndPoint.ToString() + "：" + strMsgRec);
                    showMsg(socketClient.RemoteEndPoint.ToString() + "：" + strMsgRec);
                }
                catch (Exception e)
                {
              
                   
                    if (socketClient != null &&socketClient.Connected)
                    {
                        showMsg(socketClient.RemoteEndPoint.ToString() + "已经离开：（ ***");
                        //移除dict中的套接字
                        dict.Remove(socketClient.RemoteEndPoint.ToString());
                        //重新绑定界面
                        BindListBox();
                        //关闭套接字
                        socketClient.Close();
                        //结束当前线程
                        Thread.CurrentThread.Abort();
                    }
                }
            }
        }

        private void chatBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //取得连接的套接字
            string selectKey = null;
            try
            {
                if (onlineBox.SelectedItem != null)
                {
                    selectKey = onlineBox.SelectedItem.ToString();
                }
                else
                {
                    MessageBox.Show("Please chose a client:)");
                    return;
                }
                Socket socketClient = dict[selectKey];
               
                //移除dict中的套接字
                dict.Remove(socketClient.RemoteEndPoint.ToString());
                socketClient.Shutdown(SocketShutdown.Both);
                System.Threading.Thread.Sleep(10);            
                //关闭套接字
                socketClient.Close();
                //结束当前线程
                Thread.CurrentThread.Abort();
                //重新绑定界面
                BindListBox();

            }
            catch(Exception c)
            {
               
            }
                
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            string strSendMsg = this.sendBox.Text.Trim();
            //将要发送的字符串转成UTF8对应的字节数组
            byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes(strSendMsg);
            //sockConnection.Send(arrMsg);
            //取得连接的套接字
            string selectkey = null;
            if (onlineBox.SelectedItem != null)
            {
                selectkey = onlineBox.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("请选择要发送的客户端");
                return;
            }
            Socket socketSend = dict[selectkey];
            //发送信息
            socketSend.Send(arrMsg);
            showMsg("发送数据：" + strSendMsg);
        }

    }
 }

