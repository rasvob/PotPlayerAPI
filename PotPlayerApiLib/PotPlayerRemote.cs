using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using WinApiRemoteLib;

namespace PotPlayerApiLib
{
    public class PotPlayerRemote: AbstractCommandRemote
    {
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
    }
}