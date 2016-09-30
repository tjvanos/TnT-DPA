using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets
{
    class DeezNuts
    {
        public char pitch { get; set; }
        public int octave { get; set; }
        public int type { get; set; } // normal/bis/bes
        public int duration { get; set; }// enum heel half kwart etc.
        public bool rest { get; set; }
        public bool point { get; set; }
        public bool directionUp { get; set; }//staaf naar boven of onder


        // eventueel boogje



    }
}
