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
    public partial class WaitDlg : Form, MessageHandler
    {

        public WaitDlg(int room, string[] nicks, bool isMaster)
        {
            InitializeComponent();
            btnStart.Enabled = isMaster;
            btnStart.Text = isMaster ? "Start Game" : "Waiting...";

            Program.RegisterMessageHandler(this, this);
            labelRoomNum.Text = "Room ID: " + room.ToString();
            listBox1.Items.AddRange(nicks);
        }

        public void HandleMessage(string message) 
        {
            JObject obj = JObject.Parse(message);
            if (obj.Property("method") == null || obj.Property("method").ToString() == "")
            { // 服务器主动发送的消息
                string _event = obj.Property("event").Value.ToString();
                // 处理服务器消息
                if (_event == "user_join")
                {
                    string nick = obj.Property("nick").Value.ToString();
                    listBox1.Items.Add(nick);
                }
                else if (_event == "user_exit")
                {
                    string nick = obj.Property("nick").Value.ToString();
                    listBox1.Items.Remove(nick);
                }
            }
            else
            {
                /*string method = obj.Property("method").Value.ToString();
                if (method == "create_room")
                {
                    if (obj.Property("success").Value.ToString() == "True")
                    {
                        int room = int.Parse(obj.Property("room").Value.ToString());
                        WaitDlg dlg = new WaitDlg(room, new string[] { nick });

                        dlg.ShowDialog();
                    }
                }*/
            }
        }

        ~WaitDlg()
        {
            Program.UnregisterMessageHandler(this);
        }
    }
}
