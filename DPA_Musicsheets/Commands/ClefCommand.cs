using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DPA_Musicsheets.Commands
{
    public class ClefCommand : BaseCommand
    {
        private TextBox box;

        public void setBox(TextBox text)
        {
            this.box = text;
        }

        public void execute()
        {
            //add the clef treble
            box.Text = box.Text.Insert(box.SelectionStart, " \n  \\clef treble");
 // \tempo 4 = 120
        }
    }
}
