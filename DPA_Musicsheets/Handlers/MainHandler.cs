using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Handlers
{
    abstract class MainHandler
    {

        protected MainHandler successor;

        public void SetSuccessor(MainHandler successor)
        {
            this.successor = successor;
        }

        public abstract bool HandleRequest(List<System.Windows.Input.Key> request);

    }
}
