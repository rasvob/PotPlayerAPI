using System;
using System.Collections.Generic;
using System.ServiceModel;
using WinApiRemoteLib;

namespace PotPlayerApiLib.Service
{
    [ServiceContract]
    public interface IPotPlayerApiService
    {
        [OperationContract]
        void InvokeRemoteCommand(IntPtr handle, PotPlayerAction action);

        [OperationContract]
        IEnumerable<ProcessWindow> GetProcessWindows();

        [OperationContract]
        void StartNewInstance(string path);
    }
}