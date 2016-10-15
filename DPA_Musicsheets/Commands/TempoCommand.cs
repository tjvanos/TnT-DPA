using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DPA_Musicsheets.Commands
{
    public class TempoCommand : BaseCommand
    {
        private TextBox box;

        public void setBox(TextBox text)
        {
            this.box = text;
        }

        public void execute()
        {
            //add the tempo
            box.Text = box.Text.Insert(box.SelectionStart, " \n  \\tempo 4=120");
        }
    }
}
