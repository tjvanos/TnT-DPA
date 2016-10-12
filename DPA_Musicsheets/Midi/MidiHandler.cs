using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets
{
    public class MidiHandler
    {

        //internal static void getStuff(IEnumerable<MidiTrack> enumerable)
        //{
        //    string[] pitches = {"C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

        //    //convert into song
        //    Song song = new Song();

        //    List<MidiTrack> tabs = enumerable.ToList();
        //    MidiTrack info = tabs[0];
        //    MidiTrack music = tabs[1];

        //    var timesignexct = info.Messages[3];
        //    string[] stringSeparators = new string[] { "(", "/", ")" };
        //    string[] result;
        //    result = timesignexct.Split(stringSeparators, StringSplitOptions.None);


        //    song.name = info.Messages[0].ToString().Split(':')[1].Trim();
        //    song.TimeSignature = new int[2] { Int32.Parse(result[1].Trim()), Int32.Parse(result[2].Trim()) };
        //    song.tempo = Int32.Parse(info.Messages[4].ToString().Split(':')[1]);

        //    for (int i = 1; i < music.Messages.Count()-1; i ++)//loop reading the notes, in pairs of 2 --> note on - note off | first and last are no notes
        //    {
        //        DeezNuts note = new DeezNuts();
        //        /*note.setOctave(67);*/               // delen door 12 naar beneden afronden is octaaf
        //                                        // modulo 12 --- 77 %12
        //        // list maken van noten
        //        song.notes.Add(note);


        //    }

        //}

        static Song song = new Song();
        static DeezNuts note = new DeezNuts();
        static int tempAbsoluteTicks;
        
        static string[] pitches = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

        public static void addNote(int pitch, int absoluteTicks, int toneHieight, int division,int deltaTicks)
        {
            song.TimeSignature = new int[] { 4, 4 };
            if (toneHieight > 0)//start note, geluid van een noot begint
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
            }
            else//einde van een noot geen geluid meer
            {
                note.setDuration(tempAbsoluteTicks,absoluteTicks,division,song.TimeSignature[1]);
                
                song.notes.Add(note);
            }
            
        }

    }
}
