using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Win32.TaskScheduler;
using AutoItX3Lib;

namespace ArdourUploader
{
    class Program
    {
        static void Main(string[] args)
        {
            Navigation nav = new Navigation();

            //TaskService ts = new TaskService();
            //TaskDefinition td = ts.NewTask();

            //td.Principal.RunLevel = TaskRunLevel.Highest;
            //td.Triggers.AddNew(TaskTriggerType.Logon);
            //string program_path = @"C:\Program Files\Ardour5\bin\Ardour.exe"; 

            //td.Actions.Add(new ExecAction(program_path, null));
            //ts.RootFolder.RegisterTaskDefinition("bootArdour", td);

            //Opens the application without adminstrative rights
            Process ardour = new Process();

            ardour.StartInfo.FileName = @"C:\Program Files\Ardour5\bin\Ardour.exe";
            ardour.Start();

            nav.HighlightSession();
            nav.OpenSession();
            nav.StartSession();
            nav.SelectAll();
            nav.ExportTrack();
        }
    }
}
