using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Windows.Forms;
using PotPlayerApiLib;
using PotPlayerApiLib.Service;
using WinApiRemoteLib;

namespace PotPlayerApiTrayApp
{
    public partial class Form1 : Form
    {
        private ServiceHost _serviceHost;
        private static readonly string _serviceAdressPipe = "net.pipe://localhost/potwcf";
        private static readonly string _httpAdress = "http://192.168.2.7:56112/potservice/";

        public Form1()
        {
            InitializeComponent();

            BuildNotifyIconMenu();
            WindowState = FormWindowState.Minimized;

            StartService();
        }

        private void StartService()
        {
            _serviceHost = new ServiceHost(typeof(PotPlayerApiService));
            NetNamedPipeBinding binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            WebHttpBinding webHttpBinding = new WebHttpBinding() {CrossDomainScriptAccessEnabled = true};
            _serviceHost.AddServiceEndpoint(typeof(IPotPlayerApiService), binding, _serviceAdressPipe);
            EventLog.WriteEntry("PotRemote", "Pipe binded", EventLogEntryType.Information);
            ServiceEndpoint endpoint = _serviceHost.AddServiceEndpoint(typeof(IPotPlayerApiService), webHttpBinding, _httpAdress);
            endpoint.Behaviors.Add(new WebHttpBehavior());
            EventLog.WriteEntry("PotRemote", "Adress binded", EventLogEntryType.Information);
            _serviceHost.Open();
            EventLog.WriteEntry("PotRemote", "_serviceHost.State", EventLogEntryType.Information);
            Debug.WriteLine(_serviceHost.State);
        }

        private void StopService()
        {
            _serviceHost?.Close();
        }

        private void BuildNotifyIconMenu()
        {
            NotifyIcon.ContextMenu = new ContextMenu(new MenuItem[]
            {
                new MenuItem("Restart service", OnClickRestartServiceTray),
                new MenuItem("Show state", OnClickGetStateTray),
                new MenuItem("Exit", OnClickExitTray)
            });
        }

        private void OnClickExitTray(object sender, EventArgs eventArgs)
        {
            StopService();
            Application.Exit();
        }

        private void OnClickGetStateTray(object o, EventArgs eventArgs)
        {
            ShowState();
        }

        private void ShowState()
        {
            NotifyIcon.BalloonTipText = $@"Service state: {_serviceHost?.State ?? CommunicationState.Closed}";
            NotifyIcon.ShowBalloonTip(1000);
        }

        private void OnClickRestartServiceTray(object o, EventArgs eventArgs)
        {
            StopService();
            StartService();
            ShowState();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                NotifyIcon.Visible = true;
                
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
            }
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(false);
        }
    }
}
