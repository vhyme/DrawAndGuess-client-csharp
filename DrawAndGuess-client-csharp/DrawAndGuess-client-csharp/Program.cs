using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleTCP;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;
using System.Diagnostics;

namespace DrawAndGuess_client_csharp
{
    static class Program
    {
        public delegate void UIHandler();

        public static SimpleTcpClient client;

        private static List<MessageHandler> handlers = new List<MessageHandler>();

        private static List<System.EventHandler<SimpleTCP.Message>> lambdas =
            new List<System.EventHandler<SimpleTCP.Message>>();

        [DllImport("user32.dll")]
        private static extern void SetProcessDPIAware(); 

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        public static void Main()
        {
            SetProcessDPIAware();
            try
            {
                client = new SimpleTcpClient().Connect("139.129.4.219", 8082);
                client.StringEncoder = System.Text.UnicodeEncoding.UTF8;
                client.Delimiter = System.Convert.ToByte('\n');
                client.DelimiterDataReceived += (sender, msg) =>
                {
                    Console.WriteLine(msg.MessageString);
                    if (msg.MessageString.Contains("无法解析的命令"))
                    {
                        MessageBox.Show("Command can't be parsed!");
                    }

                    JObject obj = JObject.Parse(msg.MessageString);
                    if ((string)obj["event"] == "ip_duplicate")
                    {
                        MessageBox.Show("相同IP已有人登录，你被迫下线，请更换网络后重试");
                        System.Environment.Exit(0);
                    }
                    else if (obj["success"] != null && !(bool)obj["success"])
                    {
                        MessageBox.Show(obj.Property("reason").Value.ToString());
                    }
                };

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new BeginDialog());
                client.Disconnect();
            }
            catch (SocketException)
            {
                MessageBox.Show("连接服务器失败，请重试");
            }
        }

        public static void SendMessage(string message)
        {
            client.WriteLine(message + '\n');
        }

        public static void RegisterMessageHandler(Control control, MessageHandler handler)
        {
            handlers.Add(handler);

            System.EventHandler<SimpleTCP.Message> lambda
                = (sender, msg) =>
                {
                    try
                    {
                        control.BeginInvoke(
                            new UIHandler(() => handler.HandleMessage(msg.MessageString))
                        );
                    }
                    catch
                    { 
                        // do nothing
                    }
                };

            lambdas.Add(lambda);

            Program.client.DelimiterDataReceived += lambda;
        }

        public static void UnregisterMessageHandler(MessageHandler handler)
        {
            int index = handlers.IndexOf(handler);
            handlers.RemoveAt(index);

            System.EventHandler<SimpleTCP.Message> lambda = lambdas[index];
            lambdas.RemoveAt(index);

            Program.client.DelimiterDataReceived -= lambda;
        }
    }
}
