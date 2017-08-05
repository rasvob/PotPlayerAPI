using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using WinApiRemoteLib;

namespace PotPlayerApiLib.Service
{
    [ServiceContract]
    public interface IPotPlayerApiService
    {
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        [OperationContract]
        void InvokeRemoteCommand(int handle, PotPlayerAction action);

        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        [OperationContract]
        IEnumerable<ProcessWindow> GetProcessWindows();

        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        [OperationContract]
        void StartNewInstance(string path);

        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        [OperationContract]
        int GetNumber();
    }
}