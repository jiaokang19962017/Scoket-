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
using System.Threading;
using System.Net.Sockets;

namespace ServerClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //关闭对文本框非法线程的操作
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }
        Thread threadWatch = null;//负责监听客户端线程
        Socket socketWatch = null;//负责监听客户端的套接字
        private void btnServerConn_Click(object sender, EventArgs e)
        {
            //定义一个套接字用于监听客户端发来的信息,包含3个参数(ipv4寻址协议,流式协议,tcp协议)
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //服务端发送信息 需要一个ip地址和端口号
            IPAddress ipaddress = IPAddress.Parse(txtIP.Text.Trim());//获取文本框输入的ip地址
            //将ip地址和端口号绑定到网络节点endpoint上
            IPEndPoint endpoint = new IPEndPoint(ipaddress, int.Parse(txtPORT.Text.Trim()));//获取文本框输入的端口号
            //监听绑定的网络节点
            socketWatch.Bind(endpoint);
            //将套接字的舰艇队列长度限制为20
            socketWatch.Listen(20);
            //创建一个监听线程
            threadWatch = new Thread(WatchConnecting);
            //将窗体线程设置为与后台同步
            threadWatch.IsBackground = true;
            //启动线程
            threadWatch.Start();
            //启动线程后在txtmsg显示
            txtMsg.AppendText("开始监听客户端传来的消息"+"\r\n");
        }
        //创建一个负责和客户端通信的套接字
        Socket socConnection = null;

        /// <summary>
        /// 监听客户发来的请求
        /// </summary>
        private void WatchConnecting()
        {
            while (true)//持续不断的监听客户发来的请求
            {
                socConnection = socketWatch.Accept();
                txtMsg.AppendText("客户端连接成功!"+"\r\n");
                //创建一个通信线程
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ServerRecMsg);
                Thread thr = new Thread(pts);
                thr.IsBackground = true;
                //启动线程
                thr.Start();
            }
        }
        /// <summary>
        /// 发送信息到客户端的方法
        /// </summary>
        private void ServerSendMsg(string sendMsg)
        {
            //将发送的字符串转换为机器可以识别的字节数组
            byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendMsg);
            //向客户端发送字节数组信息
            socConnection.Send(arrSendMsg);
            //将发送的字符串信息附件到文本框上
            txtMsg.AppendText("So-flash:"+GetCurrenTime()+"\r\n"+sendMsg+"\r\n");
        }
        /// <summary>
        /// 接收客户端发来的信息
        /// </summary>
        /// <param name="socketClientPara"></param>
        private void ServerRecMsg(object socketClientPara)
        {
            Socket socketServer = socketClientPara as Socket;
            while (true)
            {
                //创建一个内存缓冲区,大小为1024*1024
                byte[] arrServerRecMsg = new byte[1024 * 1024];
                //将接收到的信息存入内存缓冲区,并返回自长度
                int length = socketServer.Receive(arrServerRecMsg);
                //将机器接收到的字节数组转化成字符串
                string strSRecMsg = Encoding.UTF8.GetString(arrServerRecMsg, 0, length);
                //将发送的文本显示到文本框上
                txtMsg.AppendText("天之涯:"+ GetCurrenTime()+"\r\n"+strSRecMsg+"\r\n");

            }
        }
        //发送信息到客户端
        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            ServerSendMsg(txtSendMsg.Text.Trim());
        }

        private DateTime GetCurrenTime()
        {
            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;
            return currentTime;
        }
    }
}
