using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using WinApiRemoteLib;

namespace PotPlayerApiLib
{
    public class PotPlayerRemote: CommandRemoteBase
    {
        public static readonly string AppName = "PotPlayerMini64";

        public PotPlayerRemote(ProcessWindow window): base(window)
        {

        }

        public void Pause()
        {
            Command(Key.Space);
        }

        public void Fullscreen()
        {
            Command(Key.F);
        }

        public void NextFile()
        {
            Command(Key.PageDown);
        }

        public void PreviousFile()
        {
            Command(Key.PageUp);
        }

        public void Rewind()
        {
            Command(Key.Left);    
        }

        public void Forward()
        {
            Command(Key.Right);
        }

        public void VolumeUp()
        {
            CommandSequence("{UP}");
        }

        public void VolumeDown()
        {
            CommandSequence("{DOWN}");
        }

        public void Mute()
        {
            Command(Key.M);
        }

        public static ProcessWindow GetProcessWindowForApp()
        {
            Process process = Process.GetProcesses().FirstOrDefault(t => t.ProcessName.Equals(AppName, StringComparison.CurrentCultureIgnoreCase)) ?? throw new ArgumentNullException();
            return new ProcessWindow(process);
        }

        public static IEnumerable<ProcessWindow> GetProcessWindowsForApp()
        {
            return Process.GetProcesses()
                .Where(t => t.ProcessName.Equals(AppName, StringComparison.CurrentCultureIgnoreCase))
                .Select(t => new ProcessWindow(t));
        }

        public void DoAction(PotPlayerAction action)
        {
            switch (action)
            {
                case PotPlayerAction.Pause:
                    Pause();
                    break;
                case PotPlayerAction.Fullscreen:
                    Fullscreen();
                    break;
                case PotPlayerAction.NextFile:
                    NextFile();
                    break;
                case PotPlayerAction.PreviousFile:
                    PreviousFile();
                    break;
                case PotPlayerAction.Rewind:
                    Rewind();
                    break;
                case PotPlayerAction.Forward:
                    Forward();
                    break;
                case PotPlayerAction.VolumeUp:
                    VolumeUp();
                    break;
                case PotPlayerAction.VolumeDown:
                    VolumeDown();
                    break;
                case PotPlayerAction.Mute:
                    Mute();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }
    }
}