using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets
{
    public class LilypondHandler
    {
        public Song song = new Song();

        public void setTimeSignature(String signature)
        {
            var numbers = signature.Split('/');
            song.TimeSignature = new int[] { Int32.Parse(numbers[0]), Int32.Parse(numbers[1]) };
        }

        internal void setClef(String clef)
        {
            throw new NotImplementedException();
        }

        internal void setTempo(String tempo)
        {
            var numbers = tempo.Split('=');
            song.tempo = Int32.Parse(numbers[1]);
        }

        internal int readRelativeString(String relative)
        {
            if ((relative.Split('\'').Length - 1) == 1)
            {
                return 1;
            }
            else if ((relative.Split(',').Length - 1) == 1)
            {
                return -1;
            }

            else
                return 0;
        }

        internal void createNote(String note)
        {

        }
    }
}
