using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PotPlayerApiLib;
using WinApiRemoteLib;

namespace PotPlayerAPI.ViewModels.Pot
{
    public class PotRemoteViewModel
    {
        public IEnumerable<ProcessWindow> Windows { get; set; }

        public IEnumerable<PotPlayerAction> PotPlayerActions { get; set; } = new[]
        {
            PotPlayerAction.Pause, PotPlayerAction.Fullscreen, PotPlayerAction.VolumeDown, PotPlayerAction.Mute,
            PotPlayerAction.VolumeUp, PotPlayerAction.PreviousFile, PotPlayerAction.NextFile, PotPlayerAction.Rewind,
            PotPlayerAction.Forward
        };

        public PotRemotePostViewModel PotRemotePost { get; set; }
    }

    public class PotRemotePostViewModel
    {
        [Required]
        public PotPlayerAction PotPlayerAction { get; set; }

        [Required]
        public int Handle { get; set; }
    }
}