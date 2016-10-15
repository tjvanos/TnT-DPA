using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Commands

{
    public class SavePDFCommand : BaseCommand
    {

        private string filename;
        private string text;

    public void setContent(String text)
    {
        this.text = text;
    }

    public void execute()
        {//school gegeven code:
         //Dit is een C# class die gebruik maakt van een nieuw proces. Hierbij start lilypond op en slaat je bestand als PDF op.
         //Daarnaast kopieert hij dan nog de file naar de gewenste locatie.
            string lilypondLocation = @"C:\Program Files (x86)\LilyPond\usr\bin\lilypond.exe";
            string sourceFolder = @"c:\temp\";
            string sourceFileName = "Twee-emmertjes-water-halen";
            string targetFolder = @"c:\users\mmaaschu\desktop\";
            string targetFileName = "Test";

            var process = new Process
            {
                StartInfo =
                {
                    WorkingDirectory = sourceFolder,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Arguments = String.Format("--pdf \"{0}{1}\"", sourceFolder, sourceFileName + ".ly"),
                    FileName = lilypondLocation
                }
            };

            process.Start();
            while (!process.HasExited)
            { /* Wait for exit */
            }

            File.Copy(sourceFolder + sourceFileName + ".pdf", targetFolder + targetFileName + ".pdf", true);
        }
    }
}
