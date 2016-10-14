using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DPA_Musicsheets
{
    public static class LilypondReader
    {
        internal static void OpenLilypond(string filePath, LilypondHandler handler)
        {
            String rawFile = System.IO.File.ReadAllText(filePath);
            String[] LilypondParts = cleanFile(rawFile);

            readLilypond(LilypondParts, handler);
           }

        private static string[] cleanFile(string rawFile)
        {
            //First of all remove unnecessary characters
            String newFile = rawFile
                                .Replace("\r\n", "")
                                .Replace("|", "");
            
            //Befor splitting, make sure the brackets have spaces around them
            newFile = Regex.Replace(newFile, @"((?<!\s)({|}))", " $1");
            newFile = Regex.Replace(newFile, @"(({|})(?!\s))", "$1 ");

            //After that, return an array of strings and delete its empty values        
            return newFile
                                .Split(' ')
                                .Where(x => !string.IsNullOrEmpty(x))
                                .ToArray();
        }

        internal static void readLilypond(String[] LilypondParts, LilypondHandler handler)
        {
            //Initialize default values for pitch and octave
            char currentPitch = 'C';
            int currentOctave = 3;

            for (int i = 0; i < LilypondParts.Length; i++)
            {
                String current = LilypondParts[i];
                switch (current)
                {
                    case "\\relative":
                        String next = LilypondParts[++i];
                        currentOctave = currentOctave + handler.readRelativeString(next);
                        currentPitch = Char.ToUpper(next[0]);
                        break;
                    case "\\clef":
                        //stel de sleutel in, gebruiken we nog niet
                        break;
                    case "\\time":
                        handler.setTimeSignature(LilypondParts[++i]);
                        break;
                    case "\\tempo":
                        handler.setTempo(LilypondParts[++i]);
                        break;
                    case "\\alternative":
                        //stel een alternatief in voor herhaling
                        break;
                    case "\\repeat":
                        //begin een herhaling
                        break;
                    case "{":
                        //open een nieuwe laag
                        break;
                    case "}":
                        //sluit een nieuwe laag
                        break;
                    default:
                        //Run a regex to split all values
                        String pattern = @"(?<pitch>[a-z]{1,3})(?<sign>\W?)(?<duration>\d{1,2})(?<point>\W?)";
                        String noteString = LilypondParts[i];
                        String pitch;
                        String sign;
                        int duration;
                        int point;

                        if (Regex.IsMatch(noteString, pattern))
                        {
                            Regex r = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
                            pitch = r.Match(noteString).Result("${pitch}").ToUpper();
                            sign = r.Match(noteString).Result("${sign}");
                            duration = Int32.Parse(r.Match(noteString).Result("${duration}"));
                            point = (r.Match(noteString).Result("${point}").Count());

                            if (sign != null)
                            {
                                if (sign.Equals("'"))
                                {
                                    currentOctave++;
                                }
                                if (sign.Equals(","))
                                {
                                    currentOctave--;
                                }
                            }

                            //Create new Note
                            DeezNuts note = handler.createNote(currentPitch, currentOctave, pitch, duration, point);
                            handler.song.notes.Add(note);

                            currentOctave = note.octave;

                            if (!pitch.Equals("R"))
                            {
                                currentPitch = Char.ToUpper(pitch[0]);
                            }

                            break;
                        }

                        else
                            break;
                }
            }
        }
    }
}
