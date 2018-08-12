using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoItX3Lib;

namespace ArdourUploader
{
    public class Navigation
    {
        AutoItX3 autoIt = new AutoItX3();
        private string windowName;

        public void NavigateArdour()
        {
            OpenSession();
            StartSession();
            SelectAll();
            ExportTrack();
        }

        public void HighlightSession()
        {
            autoIt.MouseMove(500, 375);
            Thread.Sleep(2000);
            autoIt.MouseClick("primary"); // Highlights the first session
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
            windowName = autoIt.WinGetTitle("[ACTIVE]");
            autoIt.WinActivate(windowName);

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
        }
    }
}
