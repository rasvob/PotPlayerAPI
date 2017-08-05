using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using PotPlayerApiLib;
using PotPlayerApiLib.Service;
using WinApiRemoteLib;

namespace PotPlayerAPI.BusinessLogic
{
    public class PotPlayerServiceClient: IDisposable, IPotPlayerApiService
    {
        private readonly IPotPlayerApiService _channel;

        public PotPlayerServiceClient()
        {
            string address = "net.pipe://localhost/potwcf";

            NetNamedPipeBinding binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            EndpointAddress ep = new EndpointAddress(address);
            _channel = ChannelFactory<IPotPlayerApiService>.CreateChannel(binding, ep);
        }

        public void Dispose()
        {
            try
            {
                (_channel as IDisposable)?.Dispose();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public void InvokeRemoteCommand(IntPtr handle, PotPlayerAction action)
        {
            _channel.InvokeRemoteCommand(handle, action);
        }

        public IEnumerable<ProcessWindow> GetProcessWindows()
        {
            return _channel.GetProcessWindows();
        }

        public void StartNewInstance(string path)
        {
            _channel.StartNewInstance(path);
        }
    }
}