using DPA_Musicsheets.Commands;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DPA_Musicsheets.Handlers
{
     class ExplorerHandler: MainHandler
    {
        SaveCommand saveHandler = new SaveCommand();
        SavePDFCommand PDFHandler = new SavePDFCommand();
        OpenCommand openHandler = new OpenCommand();

        TextBox open = new TextBox(); 
        string stuffToSave;

        public void setStuffToSave(String text)
        {
            stuffToSave = text;
        }
        public void setTextbox(TextBox textbox)
        {
            open = textbox;
        }

        public override bool HandleRequest(List<System.Windows.Input.Key> request)
        {

            if (request.Contains(System.Windows.Input.Key.S) && request.Contains(System.Windows.Input.Key.P) && request.Contains(System.Windows.Input.Key.LeftCtrl))
            {
                Console.WriteLine("Time to save things as PDF");//todo

                PDFHandler.setContent(stuffToSave);
                PDFHandler.execute();
                return true;
            }
            else if (request.Contains(System.Windows.Input.Key.S)&& request.Contains(System.Windows.Input.Key.LeftCtrl))
            {
                Console.WriteLine("Time to save things as lillypond");

                saveHandler.setContent(stuffToSave);
                saveHandler.execute();
                return true;
            }
            else if (request.Contains(System.Windows.Input.Key.O) && request.Contains(System.Windows.Input.Key.LeftCtrl))
            {
                Console.WriteLine("Time to open some files!");
                openHandler.settextBox(open);
                openHandler.execute();
                return true;
            }
            else if (successor != null)
            {
                return successor.HandleRequest(request);//succesor is MusicHandler
            }

            return false;
        }

    }
}
