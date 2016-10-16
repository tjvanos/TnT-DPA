using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DPA_Musicsheets.Commands
{
    public class TimeCommand : BaseCommand
    {
        private TextBox box;
        string time = "4/4";

        public void setBox(TextBox text)
        {
            this.box = text;
        }
        public void setTime(string time)
        {
            this.time = time;
        }

        public void execute()
        {
            //add the tempo
            box.Text = box.Text.Insert(box.SelectionStart, " \n   \\time " + time);
        }
    }
}
