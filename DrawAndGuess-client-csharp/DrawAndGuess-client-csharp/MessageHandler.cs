using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawAndGuess_client_csharp
{
    interface MessageHandler
    {
        void HandleMessage(string message);
    }
}
