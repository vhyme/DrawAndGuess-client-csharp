using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleTCP;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;

namespace DrawAndGuess_client_csharp
{
    static class Program
    {
        public delegate void UIHandler();

        public static SimpleTcpClient client;

        private static List<NetworkingForm> handlers = new List<NetworkingForm>();

        private static List<System.EventHandler<SimpleTCP.Message>> lambdas =
            new List<System.EventHandler<SimpleTCP.Message>>();

        //[DllImport("user32.dll")]
        //private static extern void SetProcessDPIAware(); 

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        public static void Main()
        {
            //SetProcessDPIAware();
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
                    if (obj.Property("method") == null || obj.Property("method").ToString() == "")
                    { // 服务器主动发送的消息

                    }
                    else
                    {
                        if (obj.Property("success") != null
                            && obj.Property("success").ToString() != ""
                            && obj.Property("success").Value.ToString() == "False")
                        {
                            MessageBox.Show(obj.Property("reason").Value.ToString());
                        }
                    }
                };

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new BeginDialog());
                client.Disconnect();
            }
            catch (SocketException)
            {
                MessageBox.Show("网络连接失败，请重试");
            }
        }

        public static void SendMessage(string message)
        {
            client.WriteLine(message + '\n');
        }

        public static void RegisterMessageHandler(NetworkingForm handler)
        {
            handlers.Add(handler);

            System.EventHandler<SimpleTCP.Message> lambda
                = (sender, msg) => handler.BeginInvoke(
                    new UIHandler(() => handler.HandleMessage(msg.MessageString))
                );

            lambdas.Add(lambda);

            Program.client.DelimiterDataReceived += lambda;
        }

        public static void UnregisterMessageHandler(NetworkingForm handler)
        {
            int index = handlers.IndexOf(handler);
            handlers.RemoveAt(index);

            System.EventHandler<SimpleTCP.Message> lambda = lambdas[index];
            lambdas.RemoveAt(index);

            Program.client.DelimiterDataReceived -= lambda;
        }
    }
    public class NetworkingForm : Form
    {
        public NetworkingForm()
        {
            Program.RegisterMessageHandler(this);
        }

        public void HandleMessage(string message)
        {
            // 此类不能设置抽象，否则会影响子类设计器的初始化，所以置空
        }

        protected override void Dispose(bool disposing)
        {
            Program.UnregisterMessageHandler(this);
            base.Dispose(disposing);
        }
    }
}
