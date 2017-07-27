using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using WinApiRemoteLib;

namespace AimpApiLib
{
    public class AimpRemote: CommandRemoteBase
    {
        public static readonly string AppName = "AIMP";

        public AimpRemote(ProcessWindow window) : base(window)
        {

        }

        public void Pause()
        {
            Command(Key.Space);
        }

        public void NextFile()
        {
            Command(Key.F2);
        }

        public void PreviousFile()
        {
            Command(Key.F1);
        }

        public void Rewind()
        {
            CommandSequence("^({LEFT})");
        }

        public void Forward()
        {
            CommandSequence("^({RIGHT})");
        }

        public void VolumeUp()
        {
            CommandSequence("^({UP})");
        }

        public void VolumeDown()
        {
            CommandSequence("^({DOWN})");
        }

        public void Mute()
        {
            Command(Key.V);
        }

        public static ProcessWindow GetProcessWindowForApp()
        {
            Process process = Process.GetProcesses().FirstOrDefault(t => t.ProcessName.Equals(AppName, StringComparison.CurrentCultureIgnoreCase)) ?? throw new ArgumentNullException();
            return new ProcessWindow(process);
        }

        public void DoAction(AimpActions action)
        {
            switch (action)
            {
                case AimpActions.Pause:
                    Pause();
                    break;
                case AimpActions.NextFile:
                    NextFile();
                    break;
                case AimpActions.PreviousFile:
                    PreviousFile();
                    break;
                case AimpActions.Rewind:
                    Rewind();
                    break;
                case AimpActions.Forward:
                    Forward();
                    break;
                case AimpActions.VolumeUp:
                    VolumeUp();
                    break;
                case AimpActions.VolumeDown:
                    VolumeDown();
                    break;
                case AimpActions.Mute:
                    Mute();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }
    }
}