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
        private int room;

        public WaitDlg(int room, string[] nicks, bool isMaster)
        {
            InitializeComponent();
            Program.RegisterMessageHandler(this, this);

            btnStart.Enabled = isMaster;
            btnStart.Text = isMaster ? "Start Game" : "Waiting...";

            this.room = room;
            labelRoomNum.Text = "Room ID: " + room.ToString();
            listBox1.Items.AddRange(nicks);
        }

        ~WaitDlg()
        {
            Program.UnregisterMessageHandler(this);
        }

        public void HandleMessage(string message) 
        {
            JObject obj = JObject.Parse(message);
            if (obj["method"] == null || (string) obj["method"] == "")
            { // 服务器主动发送的消息
                string _event = (string) obj["event"];
                // 处理服务器消息
                if (_event == "user_join")// 用户加入
                {
                    string nick = (string) obj["nick"];
                    listBox1.Items.Add(nick);
                }
                else if (_event == "user_exit")// 用户退出
                {
                    string nick = (string) obj["nick"];
                    listBox1.Items.Remove(nick);
                }
                else if (_event == "room_expire")// 房间解散
                {
                    MessageBox.Show("Room manager has disconnected. Game will now end.");
                    Close();
                    Dispose();
                }
                else if (_event == "game_start")// 房间解散
                {
                    int count = listBox1.Items.Count;
                    string[] members = (from str in obj["players"] select (string)str).ToArray();
                    new DrawDlg(room, members).ShowDialog();
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Program.SendMessage("{\"method\": \"start_game\"}");
        }
    }
}
