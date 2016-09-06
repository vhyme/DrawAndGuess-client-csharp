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

namespace DrawAndGuess_client_csharp
{
    public partial class BeginDialog : Form
    {
        public BeginDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new CreateDlg().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new JoinDlg().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
