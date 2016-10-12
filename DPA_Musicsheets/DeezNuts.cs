using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets
{
    public class DeezNuts
    {
        public char pitch { get; set; }
        public int octave { get; set; }
        public int type { get; set; } // 0 normal / 1 bis / -1 bes
        public int duration { get; set; }//1 hoeveelste noot bijv 4 = 1/4 noot is een kwart noot
        public bool rest { get; set; }
        public bool point { get; set; }
        public bool directionUp { get; set; }//staaf naar boven of onder optioneel


        public void setPitch(int pitch, String[] pitches)
        {
            String pitchTemp = pitches[pitch % 12];
            char pitchToAdd = pitchTemp[0];

            if (pitchTemp.Length == 2)
            {
                this.pitch = pitchToAdd;
                this.type = 1;
            }
            else
            {
                this.pitch = pitchToAdd;
                this.type = 0;
            }
        }

        public void setOctave(int pitch)
        {
            double temp = pitch/12;
            this.octave = (int)Math.Floor(temp);
        }

        public void setDuration(int previousTicks,int currentTicks, int division, int timesignature)
        {
            double deltaTicks = currentTicks - previousTicks;
            double percentageOfBeatNote = deltaTicks / division;
            double percentageOfWholeNote = (1.0 / timesignature) * percentageOfBeatNote;

            for (int noteLength = 32; noteLength >= 1; noteLength /= 2)
            {
                double absoluteNoteLength = (1.0 / noteLength);

                if (percentageOfWholeNote <= absoluteNoteLength)//noot maat zonder punt
                {
                    this.duration = noteLength;
                    this.point = false;
                    break;
                }
                if ((percentageOfWholeNote*1.5) <= absoluteNoteLength) //een punt achter een noot is anderhalf keer zijn lengte
                {
                    this.duration = noteLength;
                    this.point = true;
                    break;
                }

            }
        }

        internal void setRest(bool v)
        {
            this.rest = v;
        }
    }
}
