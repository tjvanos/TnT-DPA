using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets
{
    public class MidiHandler
    {

        public Song song = new Song();
        DeezNuts note = new DeezNuts();
        int tempAbsoluteTicks;
        
        static string[] pitches = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

        public  void addNote(int pitch, int absoluteTicks, int toneHieight, int division,int deltaTicks, string command)
        {
            if (toneHieight == 0 || command=="NoteOff")//einde van een noot geen geluid meer
            {
                note.setDuration(tempAbsoluteTicks, absoluteTicks, division, song.TimeSignature[1]);

                song.notes.Add(note);
            }
            else//start note, geluid van een noot begint
            {
                note = new DeezNuts();
                tempAbsoluteTicks = absoluteTicks;
                note.setOctave(pitch);
                note.setPitch(pitch, pitches);

                if (deltaTicks > 0) //voor een rust
                {
                    note.setRest(true);
                }
                //basis logica uit eerste deel van note

                //Check in which direction the stem should point
                if ((note.octave > 4) || (note.octave == 4 && note.pitch == 'B'))
                {
                    note.directionUp = false;
                }
                else
                    note.directionUp = true;

            }

        }

        public void addTimeSignature(string timesign)
        {
            string[] stringSeparators = new string[] { "(", "/", ")" };
            string[] result;
            result = timesign.Split(stringSeparators, StringSplitOptions.None);
            song.TimeSignature = new int[2] { Int32.Parse(result[1].Trim()), Int32.Parse(result[2].Trim()) };
        }

        public void addName(string name)
        {
            song.name = name;
        }


    }
}
