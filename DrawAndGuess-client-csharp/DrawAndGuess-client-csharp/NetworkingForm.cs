using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawAndGuess_client_csharp
{
    public abstract class NetworkingForm : Form
    {
        public NetworkingForm() 
        {
            Program.RegisterMessageHandler(this);
        }

        public abstract void HandleMessage(string message);

        protected override void Dispose(bool disposing)
        {
            Program.UnregisterMessageHandler(this);
            base.Dispose(disposing);
        }
    }
}
