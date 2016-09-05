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
        public CreateDlg()
        {
            Program.RegisterMessageHandler(this);
            InitializeComponent();
        }

        ~CreateDlg() 
        {
            Program.UnregisterMessageHandler(this);
        }

        private void onSubmit(object sender, EventArgs e)
        {
            string nick = labelNick.Text;
            Program.SendMessage("{\"method\": \"create\", \"nick\": \"" + nick + "\"}");
        }

        public void HandleMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
