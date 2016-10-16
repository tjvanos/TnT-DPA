using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets
{
    public class DeezNut
    {
        public char pitch { get; set; }
        public int octave { get; set; }
        public int type { get; set; } // 0 normal / 1 bis / -1 bes
        public int duration { get; set; }//1 hoeveelste noot bijv 4 = 1/4 noot is een kwart noot
        public bool rest { get; set; }
        public int point { get; set; }
        public bool directionUp { get; set; }//staaf naar boven of onder optioneel

    }
}
