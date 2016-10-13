using Microsoft.Win32;
using PSAMControlLibrary;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DPA_Musicsheets
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MidiPlayer _player;
        public ObservableCollection<MidiTrack> MidiTracks
        {
            get; private set;
        }
        public MidiHandler midiHandler = new MidiHandler();
        public LilypondHandler lilypondHandler = new LilypondHandler();

        // De OutputDevice is een midi device of het midikanaal van je PC.
        // Hierop gaan we audio streamen.
        // DeviceID 0 is je audio van je PC zelf.
        private OutputDevice _outputDevice = new OutputDevice(0);

        public MainWindow()
        {
            this.MidiTracks = new ObservableCollection<MidiTrack>();
            InitializeComponent();
            DataContext = MidiTracks;
            FillPSAMViewer();
            //notenbalk.LoadFromXmlFile("Resources/example.xml");
        }

        private void FillPSAMViewer()
        {
            staff.ClearMusicalIncipit();

            // Clef = sleutel
            staff.AddMusicalSymbol(new Clef(ClefType.GClef, 2));
            staff.AddMusicalSymbol(new TimeSignature(TimeSignatureType.Numbers, 4, 4));
            /* 
                The first argument of Note constructor is a string representing one of the following names of steps: A, B, C, D, E, F, G. 
                The second argument is number of sharps (positive number) or flats (negative number) where 0 means no alteration. 
                The third argument is the number of an octave. 
                The next arguments are: duration of the note, stem direction and type of tie (NoteTieType.None if the note is not tied). 
                The last argument is a list of beams. If the note doesn't have any beams, it must still have that list with just one 
                    element NoteBeamType.Single (even if duration of the note is greater than eighth). 
                    To make it clear how beamlists work, let's try to add a group of two beamed sixteenths and eighth:
                        Note s1 = new Note("A", 0, 4, MusicalSymbolDuration.Sixteenth, NoteStemDirection.Down, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Start, NoteBeamType.Start});
                        Note s2 = new Note("C", 1, 5, MusicalSymbolDuration.Sixteenth, NoteStemDirection.Down, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Continue, NoteBeamType.End });
                        Note e = new Note("D", 0, 5, MusicalSymbolDuration.Eighth, NoteStemDirection.Down, NoteTieType.None,new List<NoteBeamType>() { NoteBeamType.End });
                        viewer.AddMusicalSymbol(s1);
                        viewer.AddMusicalSymbol(s2);
                        viewer.AddMusicalSymbol(e); 
            */

            staff.AddMusicalSymbol(new Note("A", 0, 4, MusicalSymbolDuration.Sixteenth, NoteStemDirection.Down, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Start, NoteBeamType.Start }));
            staff.AddMusicalSymbol(new Note("C", 1, 5, MusicalSymbolDuration.Sixteenth, NoteStemDirection.Down, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Continue, NoteBeamType.End }));
            staff.AddMusicalSymbol(new Note("D", 0, 5, MusicalSymbolDuration.Eighth, NoteStemDirection.Down, NoteTieType.Start, new List<NoteBeamType>() { NoteBeamType.End }));
            staff.AddMusicalSymbol(new Barline());

            staff.AddMusicalSymbol(new Note("D", 0, 5, MusicalSymbolDuration.Whole, NoteStemDirection.Down, NoteTieType.Stop, new List<NoteBeamType>() { NoteBeamType.Single }));
            staff.AddMusicalSymbol(new Note("E", 0, 4, MusicalSymbolDuration.Quarter, NoteStemDirection.Up, NoteTieType.Start, new List<NoteBeamType>() { NoteBeamType.Single }) { NumberOfDots = 1 });
            staff.AddMusicalSymbol(new Barline());

            staff.AddMusicalSymbol(new Note("C", 0, 4, MusicalSymbolDuration.Half, NoteStemDirection.Up, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Single }));
            staff.AddMusicalSymbol(
                new Note("E", 0, 4, MusicalSymbolDuration.Half, NoteStemDirection.Up, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Single })
                {
                    IsChordElement = true
                });
            staff.AddMusicalSymbol(
                new Note("G", 0, 4, MusicalSymbolDuration.Half, NoteStemDirection.Up, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Single })
                {
                    IsChordElement = true
                });
            staff.AddMusicalSymbol(new Barline());
        }

        private void FillPSAMViewer(Song song)
        {
            staff.ClearMusicalIncipit();

            // Clef = sleutel
            staff.AddMusicalSymbol(new Clef(ClefType.GClef, 2));//g
            staff.AddMusicalSymbol(new TimeSignature(TimeSignatureType.Numbers, (uint)song.TimeSignature[0], (uint)song.TimeSignature[1]));

            //tellen in een maat song.TimeSignature[0]
            double max = (1.0 / song.TimeSignature[1]) * song.TimeSignature[0];
            double counter = 0.0;

            for (int i = 0; i < song.notes.Count(); i++)
            {
                DeezNuts note = song.notes[i];
                if (counter >= max)
                {
                    counter = 0.0;
                    staff.AddMusicalSymbol(new Barline());
                }

                counter += 1.0/note.duration;

                if (i == 0)
                {
                    staff.AddMusicalSymbol(new Note(note.pitch.ToString(), note.type, note.octave - 1, getSymbol(note.duration), NoteStemDirection.Up, NoteTieType.None,/* getConnections(null, note)*/new List<NoteBeamType>() { NoteBeamType.Single }) { NumberOfDots = note.point });
                    Console.WriteLine("" + getConnections(null, note).ToString());
                }
                else
                {
                    staff.AddMusicalSymbol(new Note(note.pitch.ToString(), note.type, note.octave - 1, getSymbol(note.duration), NoteStemDirection.Up, NoteTieType.None, /*getConnections(song.notes[i-1], note)*/ new List<NoteBeamType>() { NoteBeamType.Single }) { NumberOfDots = note.point });
                    Console.WriteLine("" + getConnections(song.notes[i - 1], note)[0]);
                }
            }

        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (_player != null)
            {
                _player.Dispose();
            }

            _player = new MidiPlayer(_outputDevice);
            _player.Play(txt_FilePath.Text);
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Midi Files(.mid)|*.mid|Lilypond Files(.ly)|*.ly" };
            if (openFileDialog.ShowDialog() == true)
            {
                txt_FilePath.Text = openFileDialog.FileName;
            }
        }

        private void btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            if (_player != null)
                _player.Dispose();
        }

        private void btn_ShowContent_Click(object sender, RoutedEventArgs e)
        {
            String ext = System.IO.Path.GetExtension(txt_FilePath.Text).ToLower();
            //test
            if (ext == ".mid")
            {
                ShowMidiTracks(MidiReader.ReadMidi(txt_FilePath.Text, midiHandler));
                FillPSAMViewer(midiHandler.song);
            }
            else if (ext == ".ly")
            {
                LilypondReader.OpenLilypond(txt_FilePath.Text, lilypondHandler);
                FillPSAMViewer(lilypondHandler.song);
            }

            
        }

        private void ShowMidiTracks(IEnumerable<MidiTrack> midiTracks)
        {
            MidiTracks.Clear();
            foreach (var midiTrack in midiTracks)
            {
                MidiTracks.Add(midiTrack);
            }

            tabCtrl_MidiContent.SelectedIndex = 0;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _outputDevice.Close();
            if (_player != null)
            {
                _player.Dispose();
            }
        }

        private MusicalSymbolDuration getSymbol(int length)
        {
            switch (length)
            {
                case 16:
                    return MusicalSymbolDuration.Sixteenth;
                case 8:
                    return MusicalSymbolDuration.Eighth;
                case 4:
                    return MusicalSymbolDuration.Quarter;
                case 2:
                    return MusicalSymbolDuration.Half;
                case 1:
                    return MusicalSymbolDuration.Whole;
                default:
                    return MusicalSymbolDuration.Whole;
            }


        }


        private List<NoteBeamType> getConnections(DeezNuts prev, DeezNuts current)
        {
            if (prev == null)
            {
                switch (current.duration)
                {
                    case 32:
                        return new List<NoteBeamType>() { NoteBeamType.Start, NoteBeamType.Start, NoteBeamType.Start };
                    case 16:
                        return new List<NoteBeamType>() { NoteBeamType.Start, NoteBeamType.Start };
                    case 8:
                        return new List<NoteBeamType>() { NoteBeamType.Start };
                    default:
                        return new List<NoteBeamType>() { NoteBeamType.Single };
                }
            }
            else
            {
                switch (current.duration)
                {
                    case 32:
                        switch (prev.duration)
                        {
                            case 32:
                                return new List<NoteBeamType>() { NoteBeamType.Continue, NoteBeamType.Continue, NoteBeamType.Continue };
                            case 16:
                                return new List<NoteBeamType>() { NoteBeamType.Continue, NoteBeamType.Continue, NoteBeamType.Start };
                            case 8:
                                return new List<NoteBeamType>() { NoteBeamType.Continue, NoteBeamType.Start, NoteBeamType.Start };
                            default:
                                return new List<NoteBeamType>() { NoteBeamType.Start, NoteBeamType.Start, NoteBeamType.Start };
                        }
                    case 16:
                        switch (prev.duration)
                        {
                            case 32:
                                return new List<NoteBeamType>() { NoteBeamType.Continue, NoteBeamType.Continue, NoteBeamType.End };
                            case 16:
                                return new List<NoteBeamType>() { NoteBeamType.Continue, NoteBeamType.Continue };
                            case 8:
                                return new List<NoteBeamType>() { NoteBeamType.Continue, NoteBeamType.Start };
                            default:
                                return new List<NoteBeamType>() { NoteBeamType.Start, NoteBeamType.Start };
                        }
                    case 8:
                        switch (prev.duration)
                        {
                            case 32:
                                return new List<NoteBeamType>() { NoteBeamType.Continue, NoteBeamType.End, NoteBeamType.End };
                            case 16:
                                return new List<NoteBeamType>() { NoteBeamType.Continue, NoteBeamType.ForwardHook };
                            case 8:
                                return new List<NoteBeamType>() { NoteBeamType.Continue };
                            default:
                                return new List<NoteBeamType>() { NoteBeamType.Start };
                        }
                    default:
                        switch (prev.duration)
                        {
                            case 32:
                                return new List<NoteBeamType>() { NoteBeamType.End, NoteBeamType.End, NoteBeamType.End };
                            case 16:
                                return new List<NoteBeamType>() { NoteBeamType.End, NoteBeamType.End };
                            case 8:
                                return new List<NoteBeamType>() { NoteBeamType.End };
                            default:
                                return new List<NoteBeamType>() { NoteBeamType.Single };
                        }
                }
            }
        }
    }
}
