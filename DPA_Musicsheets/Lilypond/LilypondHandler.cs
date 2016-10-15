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

        public void setTimeSignature(String signatureString)
        {
            var numbers = signatureString.Split('/');
            song.TimeSignature = new int[] { Int32.Parse(numbers[0]), Int32.Parse(numbers[1]) };
        }

        internal void setClef(String clefString)
        {
            throw new NotImplementedException();
        }

        internal void setTempo(String tempoString)
        {
            var numbers = tempoString.Split('=');
            song.tempo = Int32.Parse(numbers[1]);
        }

        internal int readRelativeString(String relativeString)
        {
            if ((relativeString.Split('\'').Length - 1) == 1)
            {
                return 1;
            }
            else if ((relativeString.Split(',').Length - 1) == 1)
            {
                return -1;
            }

            else
                return 0;
        }

        public void reset()
        {
            song = new Song();
        }

        internal DeezNuts createNote(char currentPitch, int currentOctave, string pitch, int duration, int point)
        {
            //Create a new note and set some values
            DeezNuts note = new DeezNuts();

            Char mainPitch = Char.ToUpper(pitch[0]);
            note.point = point;
            note.duration = duration;

            //Check if the note is a rest note
            if (mainPitch != 'R')
            {
                //Check if the note has a special type IS or ES
                if (pitch.Length > 1)
                {
                    String type = pitch.Substring(1, 2);
                    if (type.Equals("IS"))
                        note.type = 1;
                    if (type.Equals("ES"))
                        note.type = -1;
                }
                else
                    note.type = 0;

                Char lowest = 'C';
                Char highest = 'B';

                //Check how many steps are needed to find the closest pitch on the left side
                int leftSteps = 0;
                bool changeOctaveLeft = false;

                for (char C = currentPitch; C != mainPitch; )
                {
                    leftSteps++;

                    if (C == lowest)
                    {
                        changeOctaveLeft = true;
                        C = highest;
                    }
                    else if (C == 'A')
                    {
                        C = 'G';
                    }
                    else
                        C--;
                }

                //Check how many steps are needed to find the closest pitch on the right side
                int rightSteps = 0;
                bool changeOctaveRight = false;

                for (char C = currentPitch; C != mainPitch; )
                {
                    rightSteps++;

                    if (C == highest)
                    {
                        changeOctaveRight = true;
                        C = lowest;
                    }
                    else if (C == 'G')
                    {
                        C = 'A';
                    }
                    else
                        C++;
                }

                //Determine if either right or left way is shorter
                if (leftSteps < rightSteps && changeOctaveLeft)
                    note.octave = currentOctave - 1;

                else if (rightSteps < leftSteps && changeOctaveRight)
                    note.octave = currentOctave + 1;
               
                else
                    note.octave = currentOctave;

                //Check in which direction the stem should point
                if ((note.octave > 4) || (note.octave == 4 && mainPitch == 'B'))
                {
                    note.directionUp = false;
                }
                else
                    note.directionUp = true;
                
            }

            //Else, note is a rest note
            else
            {
                note.rest = true;
                note.octave = currentOctave;
            }

            note.pitch = mainPitch;



            return note;
        }
    }
}
