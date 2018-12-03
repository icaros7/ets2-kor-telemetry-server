using System;
using System.Configuration;
using System.Reflection;
using System.Windows.Forms;
using Funbit.Ets.Telemetry.Server.Helpers;

namespace Funbit.Ets.Telemetry.Server.Setup
{
    public class FirewallSetup : ISetup
    {
        static readonly log4net.ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        static readonly string FirewallRuleName = StringLib.Firewall_RuleName + $"{ConfigurationManager.AppSettings["Port"]})";

        SetupStatus _status;

        public FirewallSetup()
        {
            try
            {
                if (Settings.Instance.FirewallSetupHadErrors)
                {
                    _status = SetupStatus.Installed;
                }
                else
                {
                    string port = ConfigurationManager.AppSettings["Port"];
                    const string arguments = "advfirewall firewall show rule dir=in name=all";
                    Log.Info(StringLib.Firewall_CheckRule);
                    string output = ProcessHelper.RunNetShell(arguments, StringLib.Firewall_FailedCheckRule);
                    // this check is kind of lame, but it works in any locale...
                    _status = output.Contains(port) && output.Contains(FirewallRuleName)
                        ? SetupStatus.Installed : SetupStatus.Uninstalled;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                _status = SetupStatus.Failed;
            }
        }

        public SetupStatus Status => _status;

        public SetupStatus Install(IWin32Window owner)
        {
            if (_status == SetupStatus.Installed)
                return _status;

            try
            {
                string port = ConfigurationManager.AppSettings["Port"];
                string arguments = $"advfirewall firewall add rule name=\"{FirewallRuleName}\" " +
                                   $"dir=in action=allow protocol=TCP localport={port} remoteip=localsubnet";
                Log.Info(StringLib.Firewall_AddRule);
                ProcessHelper.RunNetShell(arguments, StringLib.Firewall_FailedAddRule);
                _status = SetupStatus.Installed;
            }
            catch (Exception ex)
            {
                _status = SetupStatus.Failed;
                Log.Error(ex);
                Settings.Instance.FirewallSetupHadErrors = true;
                Settings.Instance.Save();
                throw new Exception(StringLib.Firewall_WFMissing1 + Environment.NewLine +
                                    StringLib.Firewall_WFMissing2 +
                                    ConfigurationManager.AppSettings["Port"] + StringLib.Firewall_WFMissing3, ex);
            }

            return _status;
        }

        public SetupStatus Uninstall(IWin32Window owner)
        {
            if (_status == SetupStatus.Uninstalled)
                return _status;

            SetupStatus status;
            try
            {
                string arguments = $"advfirewall firewall delete rule name=\"{FirewallRuleName}\"";
                Log.Info(StringLib.Firewall_RmRule);
                ProcessHelper.RunNetShell(arguments, StringLib.Firewall_FailedRmRule);
                status = SetupStatus.Uninstalled;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                _status = SetupStatus.Failed;
                throw new Exception(StringLib.Firewall_WFMissing1 + Environment.NewLine +
                                    StringLib.Firewall_WFMissing2 +
                                    ConfigurationManager.AppSettings["Port"] + StringLib.Firewall_WFMissing3_1, ex);
            }
            return status;
        }
    }
}