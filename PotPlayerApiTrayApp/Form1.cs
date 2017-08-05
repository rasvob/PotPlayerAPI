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

        public Form1()
        {
            InitializeComponent();
            WindowState = FormWindowState.Minimized;
            BuildNotifyIconMenu();
            Hide();
            StartService();
        }

        private void StartService()
        {
            _serviceHost = new ServiceHost(typeof(PotPlayerApiService));
            NetNamedPipeBinding binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            WebHttpBinding webHttpBinding = new WebHttpBinding() {CrossDomainScriptAccessEnabled = true};
            _serviceHost.AddServiceEndpoint(typeof(IPotPlayerApiService), binding, _serviceAdressPipe);
            ServiceEndpoint endpoint = _serviceHost.AddServiceEndpoint(typeof(IPotPlayerApiService), webHttpBinding, "http://192.168.2.7:56112/potservice/");
            endpoint.Behaviors.Add(new WebHttpBehavior());
            _serviceHost.Open();
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
                new MenuItem("Exit", (sender, args) => Application.Exit()),
            });
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

        private void TestButton_Click(object sender, EventArgs e)
        {
            
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
    }
}
