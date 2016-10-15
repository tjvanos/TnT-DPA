using DPA_Musicsheets.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DPA_Musicsheets.Handlers
{
    class MusicHandler : MainHandler
    {
        ClefCommand clefHandler = new ClefCommand();
        TempoCommand tempoHandler = new TempoCommand();
        TextBox currentText; //is de textbox, currentText.Text is de inhoud

        public void setBox(TextBox box)
        {
            currentText = box;
        }

        public override bool HandleRequest(List<System.Windows.Input.Key> request)
        {

            if (request.Contains(System.Windows.Input.Key.C) && request.Contains(System.Windows.Input.Key.LeftAlt))
            {
                Console.WriteLine("Time to add a clef treble");

                clefHandler.setBox(currentText);//textbox doorgeven
                clefHandler.execute();//logica uitvoeren

                return true;
            }
            else if (request.Contains(System.Windows.Input.Key.S) && request.Contains(System.Windows.Input.Key.LeftAlt))
            {
                Console.WriteLine("...met ALT + S een tempo (speed) 4=120 invoegen op de huidige plek");

                tempoHandler.setBox(currentText);
                tempoHandler.execute();

                return true;
            }
            //etc.
            else if (successor != null)
            {
                return successor.HandleRequest(request);
            }

            return false;
        }

    }
}
