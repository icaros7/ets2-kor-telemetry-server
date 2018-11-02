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
                pluginStatusLabel.Text = @"ETS2/ATS telemetry 플러그인 DLL을 지웁니다.";
                firewallStatusLabel.Text = $@"{port}번 포트에 대한 방화벽 허용 규칙을 지웁니다.";
                urlReservationStatusLabel.Text = $@"http://+:{port}/에 대한 ACL 규칙을 지웁니다.";
                okButton.Text = @"설치 제거";
            }
            else
            {
                pluginStatusLabel.Text = @"ETS2/ATS telemetry 플로그인 DLL을 설치합니다.";
                firewallStatusLabel.Text = $@"{port}번 포트에 대한 방화벽 허용 규칙을 추가합니다.";
                urlReservationStatusLabel.Text = $@"http://+:{port}/에 대한 ACL 허용 규칙을 추가합니다.";
                okButton.Text = @"설치";
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
            Text += @" " + AssemblyHelper.Version + @" - 설정";

            // make sure that game is not running
            if (Ets2ProcessHelper.IsEts2Running)
            {
                MessageBox.Show(this,
                    @"설정 과정중엔 ETS2 혹은 ATS가 실행중이면 안됩니다." + Environment.NewLine +
                    @"게임을 우선 종료하시고, 다시시도 해주세요.", @"경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    ex.ShowAsMessageBox(this, @"설정 에러");
                }
            }

            string message;
            if (_setupFinished)
            {
                message = Program.UninstallMode
                              ? @"서버가 성공적으로 제거되었습니다. " + Environment.NewLine +
                                @"확인을 누르면 종료됩니다."
                              : @"서버가 성공적으로 설치되었습니다. " + Environment.NewLine +
                                "확인을 누르시면 서버가 시작됩니다.";
            }
            else
            {
                message = Program.UninstallMode
                              ? @"서버가 제거되었지만 오류가 있습니다." + Environment.NewLine +
                                @"확인을 누르시면 종료됩니다."
                              : @"서버가 설치되었지만 오류가 있습니다." + Environment.NewLine +
                                "확인을 누르시면 종료됩니다.";
            }

            if (Program.UninstallMode)
                Helpers.Settings.Clear();

            MessageBox.Show(this, message, @"완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
