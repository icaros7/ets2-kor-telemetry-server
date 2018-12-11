using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Funbit.Ets.Telemetry.Server.Controllers;
using Funbit.Ets.Telemetry.Server.Data;
using Funbit.Ets.Telemetry.Server.Helpers;
using Funbit.Ets.Telemetry.Server.Setup;
using IWshRuntimeLibrary;
using Microsoft.Owin.Hosting;
using System.Globalization;
using System.Threading;

namespace Funbit.Ets.Telemetry.Server
{
    public partial class MainForm : Form
    {
        IDisposable _server;
        static readonly log4net.ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        readonly HttpClient _broadcastHttpClient = new HttpClient();
        static readonly Encoding Utf8 = new UTF8Encoding(false);
        static readonly string BroadcastUrl = ConfigurationManager.AppSettings["BroadcastUrl"];
        static readonly string BroadcastUserId = Convert.ToBase64String(
            Utf8.GetBytes(ConfigurationManager.AppSettings["BroadcastUserId"] ?? ""));
        static readonly string BroadcastUserPassword = Convert.ToBase64String(
            Utf8.GetBytes(ConfigurationManager.AppSettings["BroadcastUserPassword"] ?? ""));
        static readonly int BroadcastRateInSeconds = Math.Min(Math.Max(1, 
            Convert.ToInt32(ConfigurationManager.AppSettings["BroadcastRate"])), 86400);
        static readonly bool UseTestTelemetryData = Convert.ToBoolean(
            ConfigurationManager.AppSettings["UseEts2TestTelemetryData"]);

        public MainForm()
        {
            InitializeComponent();
        }

        private void FormInitialize()
        {
            // Locale Apply

            // Main Form Title
            Text = StringLib.Title + @" " + AssemblyHelper.Version;
            // ToolStripMenu_Sever
            serverToolStripMenu.Text = StringLib.Menu_Server;
            AddShortCutToolStripMenuItem.Text = StringLib.Menu_Server_ShortCut;
            uninstallToolStripMenuItem.Text = StringLib.Menu_Server_Uninstall;

            // ToolStripMenu_Help
            helpToolStripMenuItem.Text = StringLib.Menu_Help;
            helpToolStripMenu.Text = StringLib.Menu_Help;
            donateToolStripMenuItem.Text = StringLib.Menu_Help_Donation;
            aboutToolStripMenuItem.Text = StringLib.Menu_Help_About;
            TranslateToolStripMenuItem.Text = StringLib.Menu_Help_Translate;

            groupBox1.Text = StringLib.groupBox1;
            statusTitleLabel.Text = StringLib.statusTitleLabel + @" :";
            networkInterfaceTitleLabel.Text = StringLib.networkInterfaceTitleLabel + @" :";
            serverIpTitleLabel.Text = StringLib.serverIpTitleLabel + @" :";
            appUrlTitleLabel.Text = StringLib.appUrlTitleLabel + @" :";
            apiEndpointUrlTitleLabel.Text = StringLib.apiEndpointUrlTitleLabel + @" :";

            // status label
            statusLabel.Text = StringLib.Checking;

            // Context Menu
            closeToolStripMenuItem.Text = StringLib.Context_Close;
        }

        static string IpToEndpointUrl(string host)
        {
            return $"http://{host}:{ConfigurationManager.AppSettings["Port"]}";
        }

        void Setup()
        {
            try
            {
                if (Program.UninstallMode && SetupManager.Steps.All(s => s.Status == SetupStatus.Uninstalled))
                {
                    MessageBox.Show(this, StringLib.Uninstall_NotInstall, StringLib.Done,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Environment.Exit(0);
                }

                if (Program.UninstallMode || SetupManager.Steps.Any(s => s.Status != SetupStatus.Installed))
                {
                    // we wait here until setup is complete
                    var result = new SetupForm().ShowDialog(this);
                    if (result == DialogResult.Abort)
                        Environment.Exit(0);
                }

                // raise priority to make server more responsive (it does not eat CPU though!)
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.AboveNormal;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                ex.ShowAsMessageBox(this, StringLib.Error_Install);
            }
        }

        void Start()
        {
            try
            {
                // load list of available network interfaces
                var networkInterfaces = NetworkHelper.GetAllActiveNetworkInterfaces();
                interfacesDropDown.Items.Clear();
                foreach (var networkInterface in networkInterfaces)
                    interfacesDropDown.Items.Add(networkInterface);
                // select remembered interface or default
                var rememberedInterface = networkInterfaces.FirstOrDefault(
                    i => i.Id == Settings.Instance.DefaultNetworkInterfaceId);
                if (rememberedInterface != null)
                    interfacesDropDown.SelectedItem = rememberedInterface;
                else
                    interfacesDropDown.SelectedIndex = 0; // select default interface

                // bind to all available interfaces
                _server = WebApp.Start<Startup>(IpToEndpointUrl("+"));

                // start ETS2 process watchdog timer
                statusUpdateTimer.Enabled = true;

                // turn on broadcasting if set
                if (!string.IsNullOrEmpty(BroadcastUrl))
                {
                    _broadcastHttpClient.DefaultRequestHeaders.Add("X-UserId", BroadcastUserId);
                    _broadcastHttpClient.DefaultRequestHeaders.Add("X-UserPassword", BroadcastUserPassword);
                    broadcastTimer.Interval = BroadcastRateInSeconds * 1000;
                    broadcastTimer.Enabled = true;
                }

                // show tray icon
                trayIcon.Visible = true;
                
                // make sure that form is visible
                Activate();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                ex.ShowAsMessageBox(this, StringLib.Error_Network, MessageBoxIcon.Exclamation);
            }
        }
        
        void MainForm_Load(object sender, EventArgs e)
        {
            // Check Last Settings
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Instance.LastLang);
            // Apply Locale
            FormInitialize();

            // log current version for debugging
            Log.InfoFormat("{0} ({1}) {2}", Environment.OSVersion, 
                Environment.Is64BitOperatingSystem ? "64" + StringLib.CurrentVersion_Bit : "32" + StringLib.CurrentVersion_Bit,
                Program.UninstallMode ? "[" + StringLib.CurrentVersion_UninstallMode + "]" : "" + StringLib.CurrentVersion_Running);
            

            // install or uninstall server if needed
            Setup();

            Start();
        }

        void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _server?.Dispose();
            trayIcon.Visible = false;
        }
    
        void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        void trayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        void statusUpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (UseTestTelemetryData)
                {
                    statusLabel.Text = @"Ets2TestTelemetry.json" + StringLib.statusLabel_ConnectedAt;
                    statusLabel.ForeColor = Color.DarkGreen;
                } 
                else if (Ets2ProcessHelper.IsEts2Running && Ets2TelemetryDataReader.Instance.IsConnected)
                {
                    statusLabel.Text = $"{StringLib.statusLabel_ConnectedSim} ({Ets2ProcessHelper.LastRunningGameName})";
                    statusLabel.ForeColor = Color.DarkGreen;
                }
                else if (Ets2ProcessHelper.IsEts2Running)
                {
                    statusLabel.Text = $"{StringLib.statusLabel_RunningSim} ({Ets2ProcessHelper.LastRunningGameName})";
                    statusLabel.ForeColor = Color.Teal;
                }
                else
                {
                    statusLabel.Text = StringLib.statusLabel_NotRunningSim;
                    statusLabel.ForeColor = Color.FromArgb(240, 55, 30);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                ex.ShowAsMessageBox(this, StringLib.Error_Process);
                statusUpdateTimer.Enabled = false;
            }
        }

        void apiUrlLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessHelper.OpenUrl(((LinkLabel)sender).Text);
        }

        void appUrlLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessHelper.OpenUrl(((LinkLabel)sender).Text);
        }
        
        void MainForm_Resize(object sender, EventArgs e)
        {
            ShowInTaskbar = WindowState != FormWindowState.Minimized;
            if (!ShowInTaskbar && trayIcon.Tag == null)
            {
                trayIcon.ShowBalloonTip(1000, StringLib.Title, StringLib.trayIcon_DoubleClick, ToolTipIcon.Info);
                trayIcon.Tag = StringLib.trayIcon_Tag;
            }
        }

        void interfaceDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedInterface = (NetworkInterfaceInfo) interfacesDropDown.SelectedItem;
            appUrlLabel.Text = IpToEndpointUrl(selectedInterface.Ip) + Ets2AppController.TelemetryAppUriPath;
            apiUrlLabel.Text = IpToEndpointUrl(selectedInterface.Ip) + Ets2TelemetryController.TelemetryApiUriPath;
            ipAddressLabel.Text = selectedInterface.Ip;
            Settings.Instance.DefaultNetworkInterfaceId = selectedInterface.Id;
            Settings.Instance.Save();
        }

        async void broadcastTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                broadcastTimer.Enabled = false;
                await _broadcastHttpClient.PostAsJsonAsync(BroadcastUrl, Ets2TelemetryDataReader.Instance.Read());
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            broadcastTimer.Enabled = true;
        }
        
        void uninstallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string exeFileName = Process.GetCurrentProcess().MainModule.FileName;
            var startInfo = new ProcessStartInfo
            {
                Arguments = $"/C ping 127.0.0.1 -n 2 && \"{exeFileName}\" -uninstall",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            };
            Process.Start(startInfo);
            Application.Exit();
        }

        void donateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessHelper.OpenUrl("http://funbit.info/ets2/donate.htm");
        }

        void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessHelper.OpenUrl("https://github.com/icaros7/ets2-kor-telemetry-server");
        }

        void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: implement later
        }

        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void AddShortCutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var shell = new WshShell();
            string shortCutLinkFilePath = startupFolderPath + @"\" + StringLib.AddShortCut_Name  + @".lnk";
            var windowsApplicationShortcut = (IWshShortcut)shell.CreateShortcut(shortCutLinkFilePath);
            windowsApplicationShortcut.Description = StringLib.AddShortCut_Desciption;
            windowsApplicationShortcut.WorkingDirectory = Application.StartupPath;
            windowsApplicationShortcut.TargetPath = Application.ExecutablePath;
            windowsApplicationShortcut.Save();
            MessageBox.Show(StringLib.AddShortCut_Done, StringLib.Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TranslateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, @"한국어 : hominlab@minnote.net", StringLib.Menu_Help_Translate, MessageBoxButtons.OK ,MessageBoxIcon.Information);
        }

        private void Lang_ko_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLang("ko", Lang_ko_ToolStripMenuItem.Text);
        }

        private void Lang_en_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLang("en", Lang_en_ToolStripMenuItem.Text);
        }

        private void ChangeLang(string lang, string lang_button)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            Settings.Instance.LastLang = lang;
            Settings.Instance.Save();
            MessageBox.Show(StringLib.Settings_Lang_LogInfo, StringLib.Information, MessageBoxButtons.OK ,MessageBoxIcon.Information);
            Log.InfoFormat(StringLib.Settings_Lang_1 + lang_button + StringLib.Settings_Lang_2);
            FormInitialize();
        }
    }
}