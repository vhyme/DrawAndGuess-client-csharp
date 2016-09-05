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
    public partial class WaitDlg : Form
    {

        public WaitDlg(int room, string nick)
        {
            InitializeComponent();
            labelRoomNum.Text = "Room ID: " + room.ToString();
        }
    }
}
