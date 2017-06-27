using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace TCP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("服务开启中");
            IPAddress ip = IPAddress.Parse("127.0.0.1");//获取ip地址
            TcpListener listener = new TcpListener(ip, 8500);

            //开始监听
            listener.Start();
            Console.WriteLine("开始监听");
            Console.WriteLine("\n\n 输入 \"q\"键退出");
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Q);
        }
    }
}
