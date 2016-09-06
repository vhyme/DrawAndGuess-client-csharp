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
    public partial class JoinDlg : Form, MessageHandler
    {
        private int room;

        public JoinDlg()
        {
            InitializeComponent();
            Program.RegisterMessageHandler(this, this);
        }

        ~JoinDlg()
        {
            Program.UnregisterMessageHandler(this);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string nick = tbxNick.Text;
            try
            {
                room = int.Parse(tbxRoomId.Text);
            }
            catch (Exception)
            {
                MessageBox.Show(tbxRoomId.Text + " is not a number!");
                return;
            }
            if (nick.Length < 2)
            {
                MessageBox.Show("Too short nickname!");
                return;
            }
            Program.SendMessage("{\"method\": \"join_room\", \"room\": " + room + ", \"nick\": \"" + nick + "\"}");
        }

        public void HandleMessage(string message)
        {
            JObject obj = JObject.Parse(message);
            if (obj["method"] == null)
            { // 服务器主动发送的消息
            }
            else
            {
                string method = (string) obj["method"];
                if (method == "join_room")
                {
                    if (obj["success"] != null && (bool) obj["success"])
                    {
                        string[] nicks = (string[]) from str in obj["players"].Children() select (string) str;

                        WaitDlg dlg = new WaitDlg(room, nicks, false);
                        dlg.ShowDialog();
                    }
                }
            }
        }
    }
}
