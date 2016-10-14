using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Handlers
{
    class MusicHandler : MainHandler
    {
        public override bool HandleRequest(List<System.Windows.Input.Key> request)
        {
            if (request.Contains(System.Windows.Input.Key.J) && request.Contains(System.Windows.Input.Key.LeftCtrl))
            {
                Console.WriteLine("J spotted!");
                return true;
            }
            else if (successor != null)
            {
                return successor.HandleRequest(request);
            }

            return false;
        }

    }
}
