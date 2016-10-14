using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Handlers
{
    class EtcHandler : MainHandler
    {
        public override bool HandleRequest(List<System.Windows.Input.Key> request)
        {
            //eventuele overige handlers
                return false;
        }
    }
}
