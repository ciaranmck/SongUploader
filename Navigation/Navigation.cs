using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoItX3Lib;

namespace ArdourUploader
{
    public class Navigation
    {
        private AutoItX3 autoIt = new AutoItX3();
        private string windowName;

        public void NavigateArdour()
        {
            HighlightSession();
            OpenSession();
            StartSession();

            autoIt.Sleep(2000);

            FileCheckAndExport();
        }

        public void BounceAllRemainingTracks()
        {
            for (int i = 1; i <= 50; i++)
            {
                OpenSessionShortcut();
                SelectNextSession(i); // failing repeatedly due to port assignment issue within Ardour. Does not appear to be symptomatic of the code

                autoIt.Sleep(2000);

                windowName = autoIt.WinGetTitle("[ACTIVE]");

                string fileCheck = @"C:\Users\ciara\OneDrive\Desktop\ArdourTracks\" + windowName + "_session.wav";

                if (File.Exists(fileCheck))
                {
                    Console.WriteLine("This filepath exists already");
                    break;
                }
                else
                {
                    SelectAll();
                    ExportTrack();
                }
            }            
        }

        public void RenameFiles() // Unused
        {
            foreach (string fileName in Directory.GetFiles(@"C:\Users\ciara\OneDrive\Desktop\ArdourTracks", "*.wav"))
            {
                File.Move(fileName, "1"); // filepath is correct, but adds Ardour to the very end after .wav, and creates a new file of wrong format
                Console.WriteLine(fileName);
            }
        }

        private bool FileCheckAndExport() // Unused
        {
            windowName = autoIt.WinGetTitle("[ACTIVE]");

            string fileCheck = @"C:\Users\ciara\OneDrive\Desktop\ArdourTracks\" + windowName + "_session.wav";

            if (File.Exists(fileCheck))
            {
                Console.WriteLine("This filepath exists already");
            }
            else
            {
                SelectAll();
                ExportTrack();
            }
            return File.Exists(fileCheck);
        }

        private void HighlightSession()
        {
            autoIt.MouseMove(500, 375);
            autoIt.Sleep(2000);
            autoIt.MouseClick("primary"); // Highlights the first session only
        }

        private void OpenSession()
        {
            autoIt.MouseMove(800, 560);
            autoIt.MouseClick("primary");
        }

        private void StartSession()
        {
            autoIt.MouseClick("primary", 800, 235, 2);
            autoIt.Sleep(2000);
        }

        private void SelectAll()
        {
            autoIt.Sleep(4000);

            autoIt.Send("{g}");

            autoIt.Send("{CTRLDOWN}");
            autoIt.Send("{a}");
            autoIt.Send("{CTRLUP}");
        }

        private void ExportTrack()
        {
            windowName = autoIt.WinGetTitle("[ACTIVE]");

            autoIt.Send("{ALTDOWN}"); // opens export window
            autoIt.Send("{e}");
            autoIt.Send("{ALTUP}");

            autoIt.MouseClick("primary", 1100, 85); // Opens export presets
            autoIt.MouseClick("primary", 1100, 95); // Selects export template (AutomatedExport)

            NameTrack(windowName);

            autoIt.MouseMove(850, 230); // highlights output path
            autoIt.MouseClick("primary");

            autoIt.Send("{CTRLDOWN}"); // set's file output path (Desktop)
            autoIt.Send("{a}");
            autoIt.Send("{CTRLUP}");
            autoIt.Send(@"C:\Users\ciara\OneDrive\Desktop\ArdourTracks\");
            autoIt.Send("{ENTER}");
            autoIt.Send("{ENTER}");

            autoIt.Sleep(2000);
        }

        private void NameTrack(string name)
        {
            autoIt.WinActivate(name);

            autoIt.MouseMove(682, 296);
            autoIt.MouseClick("primary");
            autoIt.Send(windowName);
        }

        private void OpenSessionShortcut() // shortcut for opening session's from already within Ardour
        {
            autoIt.Send("{CTRLDOWN}"); 
            autoIt.Send("{o}");
            autoIt.Send("{CTRLUP}");

            autoIt.Sleep(1000);
        }

        private void SelectNextSession(int count)
        {
            autoIt.Send("{DOWN " + count.ToString() + "}"); // arrow down to next track, key press increments by one each time
            autoIt.Send("{ENTER}"); // opens session folder

            autoIt.Sleep(1000);

            autoIt.Send("{END}"); // navigates to session file
            autoIt.Sleep(1000);
            autoIt.Send("{ENTER}"); // opens track

            autoIt.Sleep(1000);
        }

        private void CloseSession() // Unused
        {
            autoIt.MouseMove(60, 40);
            autoIt.MouseClick("primary");
            autoIt.MouseMove(60, 120);
            autoIt.MouseClick("primary");
        }
    }
}
