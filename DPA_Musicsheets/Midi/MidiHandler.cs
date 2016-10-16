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
        DeezNut note = new DeezNut();
        int tempAbsoluteTicks;
        
        static string[] pitches = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

        public  void addNote(int pitch, int absoluteTicks, int toneHieight, int division,int deltaTicks, string command)
        {
            if (toneHieight == 0 || command=="NoteOff")//einde van een noot geen geluid meer
            {
                setDuration(note, tempAbsoluteTicks, absoluteTicks, division, song.TimeSignature[1]);

                song.notes.Add(note);
            }
            else//start note, geluid van een noot begint
            {
                note = new DeezNut();
                tempAbsoluteTicks = absoluteTicks;
                setOctave(note, pitch);
                setPitch(note, pitch, pitches);

                if (deltaTicks > 0) //voor een rust
                {
                    setRest(note, true);
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

        private void setPitch(DeezNut note, int pitch, String[] pitches)
        {
            String pitchTemp = pitches[pitch % 12];
            char pitchToAdd = pitchTemp[0];

            if (pitchTemp.Length == 2)
            {
                note.pitch = pitchToAdd;
                note.type = 1;
            }
            else
            {
                note.pitch = pitchToAdd;
                note.type = 0;
            }
        }

        private void setOctave(DeezNut note, int pitch)
        {
            double temp = pitch / 12;
            note.octave = (int)Math.Floor(temp) - 1;
        }

        private void setDuration(DeezNut note, int previousTicks, int currentTicks, int division, int timesignature)
        {
            double deltaTicks = currentTicks - previousTicks;
            double percentageOfBeatNote = deltaTicks / division;
            double percentageOfWholeNote = (1.0 / timesignature) * percentageOfBeatNote;

            for (int noteLength = 32; noteLength >= 1; noteLength /= 2)
            {
                double absoluteNoteLength = (1.0 / noteLength);

                if (percentageOfWholeNote <= absoluteNoteLength)//noot maat zonder punt
                {
                    note.duration = noteLength;
                    note.point = 0;
                    break;
                }
                if (percentageOfWholeNote <= (absoluteNoteLength * 1.5)) //een punt achter een noot is anderhalf keer zijn lengte
                {
                    note.duration = noteLength;
                    note.point = 1;
                    break;
                }

            }
        }

        private void setRest(DeezNut note, bool v)
        {
            note.rest = v;
        }

    }
}
