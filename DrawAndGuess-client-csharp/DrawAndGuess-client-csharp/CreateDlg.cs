using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawAndGuess_client_csharp
{
    public partial class CreateDlg : Form, MessageHandler
    {
        string nick = "";

        public CreateDlg()
        {
            InitializeComponent();
            Program.RegisterMessageHandler(this);
        }

        ~CreateDlg() 
        {
            Program.UnregisterMessageHandler(this);
        }

        private void onSubmit(object sender, EventArgs e)
        {
            nick = labelNick.Text;
            Program.SendMessage("{\"method\": \"create_room\", \"nick\": \"" + nick + "\"}");
        }

        public void HandleMessage(string message)
        {
            JObject obj = JObject.Parse(message);
            if (obj.Property("method") == null || obj.Property("method").ToString() == "")
            { // 服务器主动发送的消息
                string _event = obj.Property("event").Value.ToString();
                // 处理服务器消息
            }
            else
            {
                string method = obj.Property("method").Value.ToString();
                if (method == "create_room")
                {
                    if (obj.Property("success").Value.ToString() == "True")
                    {
                        int room = int.Parse(obj.Property("room").Value.ToString());
                        WaitDlg dlg = new WaitDlg(room, nick);

                        dlg.ShowDialog();
                    }
                }
            }
        }
    }
}
