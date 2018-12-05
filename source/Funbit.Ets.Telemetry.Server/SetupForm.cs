using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Funbit.Ets.Telemetry.Server.Helpers;
using Funbit.Ets.Telemetry.Server.Properties;
using Funbit.Ets.Telemetry.Server.Setup;

namespace Funbit.Ets.Telemetry.Server
{
    public partial class SetupForm : Form
    {
        static readonly log4net.ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        bool _setupFinished;

        readonly Dictionary<ISetup, PictureBox> _setupStatusImages = new Dictionary<ISetup, PictureBox>();

        public SetupForm()
        {
            InitializeComponent();

            DialogResult = DialogResult.OK;

            string port = ConfigurationManager.AppSettings["Port"];
            if (Program.UninstallMode)
            {
                pluginStatusLabel.Text = StringLib.Uninstall_Status_plugin;
                firewallStatusLabel.Text = $"{port}" + StringLib.Uninstall_Status_firewall;
                urlReservationStatusLabel.Text = $@"http://+:{port}/" + StringLib.Uninstall_Status_urlReservation;
                okButton.Text = StringLib.Uninstall_Status_okButton;
            }
            else
            {
                pluginStatusLabel.Text = StringLib.Install_Status_plugin;
                firewallStatusLabel.Text = $"{port}" + StringLib.Install_Status_firewall;
                urlReservationStatusLabel.Text = $@"http://+:{port}/" + StringLib.Install_Status_urlReservation;
                okButton.Text = StringLib.Install_Status_okButton;
            }
        }

        void SetStepStatus(ISetup step, SetupStatus status)
        {
            SetupStatus inverseStatus = status;
            if (Program.UninstallMode)
            {
                switch (status)
                {
                    case SetupStatus.Installed:
                        inverseStatus = SetupStatus.Uninstalled;
                        break;
                    case SetupStatus.Uninstalled:
                        inverseStatus = SetupStatus.Installed;
                        break;
                }
            }
            // convert status enumeration to images
            Bitmap statusImage = inverseStatus == SetupStatus.Uninstalled
                                     ? Resources.StatusIcon
                                     : (inverseStatus == SetupStatus.Installed
                                            ? Resources.SuccessStatusIcon
                                            : Resources.FailureStatusIcon);
            if (_setupStatusImages.ContainsKey(step))
                _setupStatusImages[step].Image = statusImage;
        }
        
        private void SetupForm_Load(object sender, EventArgs e)
        {
            // show application version 
            Text = StringLib.Title + @" " + AssemblyHelper.Version + @" - " + StringLib.Title_Setup;

            // make sure that game is not running
            if (Ets2ProcessHelper.IsEts2Running)
            {
                MessageBox.Show(this,
                    StringLib.IsEts2Running1 + Environment.NewLine +
                    StringLib.IsEts2Running2, StringLib.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Abort;
                return;
            }

            // make sure that we have Administrator rights
            if (!Uac.IsProcessElevated())
            {
                try
                {
                    // we have to restart the setup with Administrator privileges
                    Uac.RestartElevated();
                    DialogResult = DialogResult.Abort;
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
                finally
                {
                    // if succeeded or user declined elevation 
                    // then we just exit from the current process
                    Environment.Exit(0);
                }
            }
            
            // update UI 
            foreach (var step in SetupManager.Steps)
            {
                if (step is PluginSetup)
                    _setupStatusImages.Add(step, pluginStatusImage);
                else if (step is FirewallSetup)
                    _setupStatusImages.Add(step, firewallStatusImage);
                else if (step is UrlReservationSetup)
                    _setupStatusImages.Add(step, urlReservationStatusImage);
                SetStepStatus(step, step.Status);
            }

            // Locale Apply
            groupBox.Text = StringLib.groupBox;
            helpLabel.Text = StringLib.helpLabel;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            okButton.Enabled = false;
            _setupFinished = true;

            foreach (var step in SetupManager.Steps)
            {
                try
                {
                    SetStepStatus(step, Program.UninstallMode ? step.Uninstall(this) : step.Install(this));
                }
                catch (Exception ex)
                {
                    _setupFinished = false;
                    Log.Error(ex);
                    ex.ShowAsMessageBox(this, StringLib.Error_Setup);
                }
            }

            string message;
            if (_setupFinished)
            {
                message = Program.UninstallMode
                              ? StringLib.Uninstall_Mode_Fin1 + Environment.NewLine +
                                StringLib.Uninstall_Mode_Fin2
                              : StringLib.Install_Mode_Fin1 + Environment.NewLine +
                                StringLib.Install_Mode_Fin2;
            }
            else
            {
                message = Program.UninstallMode
                              ? StringLib.Uninstall_Mode_NotFin1 + Environment.NewLine +
                                StringLib.Uninstall_Mode_NotFin2
                              : StringLib.Install_Mode_NotFin1 + Environment.NewLine +
                                StringLib.Install_Mode_NotFin2;
            }

            if (Program.UninstallMode)
                Helpers.Settings.Clear();

            MessageBox.Show(this, message, StringLib.Done, MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void SetupForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing || Program.UninstallMode)
                DialogResult = DialogResult.Abort;
        }

        private void helpLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessHelper.OpenUrl("https://github.com/icaros7/ets2-kor-telemetry-server");
        }
    }
}
