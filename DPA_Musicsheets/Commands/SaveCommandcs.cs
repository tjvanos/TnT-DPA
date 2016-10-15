using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Commands
{
    public class SaveCommand : BaseCommand
    {
    private string text;

    public void setContent(String text)
    {
        this.text = text;
    }

    public void execute()
    {
            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "MyLilypond"; // Default file name
            dlg.DefaultExt = ".ly"; // Default file extension
            dlg.Filter = "Text documents (.ly)|*.ly"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                File.WriteAllText(filename, text);
            }
        }
}
}
