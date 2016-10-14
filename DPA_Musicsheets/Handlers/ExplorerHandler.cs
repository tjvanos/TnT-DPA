using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Handlers
{
     class ExplorerHandler: MainHandler
    {
        public override bool HandleRequest(List<System.Windows.Input.Key> request)
        {

            if (request.Contains(System.Windows.Input.Key.S) && request.Contains(System.Windows.Input.Key.P) && request.Contains(System.Windows.Input.Key.LeftCtrl))
            {
                Console.WriteLine("Time to save things as PDF");
                return true;
            }
            else if (request.Contains(System.Windows.Input.Key.S)&& request.Contains(System.Windows.Input.Key.LeftCtrl))
            {
                Console.WriteLine("Time to save things as lillypond");
                return true;
            }
            else if (request.Contains(System.Windows.Input.Key.O) && request.Contains(System.Windows.Input.Key.LeftCtrl))
            {
                Console.WriteLine("Time to open some files!");
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
