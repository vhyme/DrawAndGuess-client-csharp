using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleTCP;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

namespace DrawAndGuess_client_csharp
{
    public partial class BeginDialog : Form, MessageHandler
    {
        private string nick;
        private int room;

        public BeginDialog()
        {
            InitializeComponent();
            Program.RegisterMessageHandler(this, this);
        }

        ~BeginDialog()
        {
            Program.UnregisterMessageHandler(this);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            pnlCreate.Visible = !pnlCreate.Visible;
            if (pnlCreate.Visible)
            {
                pnlJoin.Visible = false;
            }
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            pnlJoin.Visible = !pnlJoin.Visible;
            if (pnlJoin.Visible)
            {
                pnlCreate.Visible = false;
            }
        }

        public void HandleMessage(string message)
        {
            JObject obj = JObject.Parse(message);
            if (obj["method"] == null || (string)obj["method"] == "")
            {
            }
            else
            {
                string method = obj.Property("method").Value.ToString();
                if (method == "create_room")
                {
                    if (obj.Property("success").Value.ToString() == "True")
                    {
                        int room = int.Parse(obj.Property("room").Value.ToString());

                        DrawDialog dlg = new DrawDialog(room, nick, new string[] { nick }, true);
                        dlg.ShowDialog();
                    }
                }
                else if (method == "join_room")
                {
                    if (obj["success"] != null && (bool)obj["success"] && obj["players"] != null)
                    {
                        string[] nicks = (from str in obj["players"] select (string)str).ToArray();

                        DrawDialog dlg = new DrawDialog(room, nick, nicks, false);
                        dlg.ShowDialog();
                    }
                }
            }
        }

        private void btnCreateSubmit_Click(object sender, EventArgs e)
        {
            nick = tbxNickCreate.Text;
            if (nick.Length < 2)
            {
                MessageBox.Show("Too short nickname!");
            }
            else
            {
                Program.SendMessage("{\"method\": \"create_room\", \"nick\": \"" + nick + "\"}");
            }
        }

        private void btnJoinSubmit_Click(object sender, EventArgs e)
        {
            nick = tbxNickJoin.Text;
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

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void BeginDialog_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x0112, 0xF012, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void BeginDialog_Activated(object sender, EventArgs e)
        {
            try
            {
                Opacity = 1;
            }
            catch
            { 
            }
        }

        private void BeginDialog_Deactivate(object sender, EventArgs e)
        {
            try
            {
                Opacity = 0.75;
            }
            catch
            {
            }
        }

        private void tbxNickCreate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCreateSubmit_Click(sender, e);
            }
        }

        private void tbxRoomId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnJoinSubmit_Click(sender, e);
            }
        }

        private void tbxNickJoin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnJoinSubmit_Click(sender, e);
            }
        }
    }
}
