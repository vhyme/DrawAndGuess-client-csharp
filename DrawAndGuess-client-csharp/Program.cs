using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleTCP;
using System.Runtime.InteropServices;

namespace DrawAndGuess_client_csharp
{
    static class Program
    {
        public static SimpleTcpClient client;

        private static List<MessageHandler> handlers = new List<MessageHandler>();

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

            client = new SimpleTcpClient().Connect("139.129.4.219", 8082);
            client.StringEncoder = System.Text.UnicodeEncoding.UTF8;
            client.Delimiter = System.Convert.ToByte('\n');
            client.DelimiterDataReceived += (sender, msg) =>
            {
                Console.WriteLine(msg.MessageString);
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BeginDialog());
        }

        public static void SendMessage(string message)
        {
            client.WriteLine(message + '\n');
        }

        public static void RegisterMessageHandler(MessageHandler handler) 
        {
            handlers.Add(handler);

            System.EventHandler<SimpleTCP.Message> lambda 
                = (sender, msg) => handler.HandleMessage(msg.MessageString);

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
