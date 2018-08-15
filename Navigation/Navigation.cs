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
            OpenSession();
            StartSession();
            SelectAll();
            ExportTrack();
        }

        public void BounceAllRemainingTracks()
        {
            for (int i = 1; i <= 100; i++)
            {
                RenameFiles();

                OpenSessionShortcut();
                SelectNextSession(i);

                autoIt.Sleep(5000);

                windowName = autoIt.WinGetTitle("[ACTIVE]");
                autoIt.WinActivate(windowName);

                string fileCheck = @"C:\Users\ciara\OneDrive\Desktop\ArdourTracks\" + windowName + "_session.wav";
                Console.WriteLine(fileCheck); // check to see full filepath

                if (File.Exists(fileCheck)) // if that file path exists, break the loop and close ardour, but currently stays within the for loop
                {
                    break;
                }
                else
                {
                    SelectAll();
                    ExportTrack();
                }
            }
        }

        public void HighlightSession()
        {
            autoIt.MouseMove(500, 375);
            Thread.Sleep(2000);
            autoIt.MouseClick("primary"); // Highlights the first session only
        }

        public void OpenSession()
        {
            autoIt.MouseMove(800, 560);
            autoIt.MouseClick("primary");
        }

        public void StartSession()
        {
            autoIt.MouseClick("primary", 800, 235, 2);
        }

        public void SelectAll()
        {
            autoIt.Sleep(4000);
            //windowName = autoIt.WinGetTitle("[ACTIVE]");
            //autoIt.WinActivate(windowName);

            autoIt.Sleep(2000);

            autoIt.Send("{g}");

            autoIt.Send("{CTRLDOWN}");
            autoIt.Send("{a}");
            autoIt.Send("{CTRLUP}");
        }

        public void ExportTrack()
        {
            autoIt.Send("{ALTDOWN}"); // opens export window
            autoIt.Send("{e}");
            autoIt.Send("{ALTUP}");

            autoIt.MouseClick("primary", 1100, 85); // Opens export presets
            autoIt.MouseClick("primary", 1100, 95); // Selects export template (AutomatedExport)

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

        public void OpenSessionShortcut()
        {
            autoIt.Send("{CTRLDOWN}"); // shortcut for opening session's from already within Ardour
            autoIt.Send("{o}");
            autoIt.Send("{CTRLUP}");

            autoIt.Sleep(1000);
        }

        public void SelectNextSession(int count)
        {
            autoIt.Send("{DOWN " + count.ToString() + "}"); // arrow down to next track, key press increments by one each time
            autoIt.Send("{ENTER}"); // opens session folder

            autoIt.Sleep(1000);

            autoIt.Send("{END}"); // navigates to session file
            autoIt.Sleep(1000);
            autoIt.Send("{ENTER}"); // opens track

            autoIt.Sleep(1000);
        }

        public void RenameFiles()
        {
            foreach (string fileName in Directory.GetFiles(@"C:\Users\ciara\OneDrive\Desktop\ArdourTracks\", "*.wav")) // doesn't rename for some reason
            {
                File.AppendAllText(fileName, Environment.NewLine + " - Ardour");
            }
        }
    }
}
