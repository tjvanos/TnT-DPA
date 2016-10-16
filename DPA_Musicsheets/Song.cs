using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets
{
    public class Song
    {
        public string name { get; set; }
        public int[] TimeSignature { get; set; }
        // = new int[2] bijv. 4/4
        public int tempo { get; set; }

        public List<DeezNut> notes = new List<DeezNut>() ;
    }
}
