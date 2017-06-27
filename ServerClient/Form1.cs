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

        }
    }
}
