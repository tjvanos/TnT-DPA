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
        public int type { get; set; } // 0 normal / 1 bis / -1 bes
        public int duration { get; set; }// enum heel half kwart etc.
        public bool rest { get; set; }
        public bool point { get; set; }
        public bool directionUp { get; set; }//staaf naar boven of onder

        public void setPitch(int pitch)
        {
            throw new NotImplementedException();
        }

        public void setOctave(int pitch)
        {
            double temp = pitch/12;
            this.octave = (int)Math.Floor(temp);
        }


        // eventueel boogje



    }
}
