using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DPA_Musicsheets.Commands
{
    public class OpenCommand : BaseCommand
    {
        private TextBox textBox;

        public void settextBox(TextBox text)
        {
            this.textBox = text;
        }

        public void execute()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Midi Files(.mid)|*.mid|Lilypond Files(.ly)|*.ly" };
            if (openFileDialog.ShowDialog() == true)
            {
                textBox.Text = openFileDialog.FileName;
            }
        }
    }
}
