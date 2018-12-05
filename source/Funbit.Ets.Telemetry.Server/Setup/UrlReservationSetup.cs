using System;
using System.Configuration;
using System.Reflection;
using System.Windows.Forms;
using Funbit.Ets.Telemetry.Server.Helpers;

namespace Funbit.Ets.Telemetry.Server.Setup
{
    public class UrlReservationSetup : ISetup
    {
        static readonly log4net.ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        SetupStatus _status;
        
        public UrlReservationSetup()
        {
            try
            {
                if (Settings.Instance.UrlReservationSetupHadErrors)
                {
                    _status = SetupStatus.Installed;
                }
                else
                {
                    string port = ConfigurationManager.AppSettings["Port"];
                    string arguments = $@"http show urlacl url=http://+:{port}/";
                    Log.Info(StringLib.UrlReservation_CheckRule);
                    string output = ProcessHelper.RunNetShell(arguments, StringLib.UrlReservation_FailedCheckRule);
                    _status = output.Contains(port) ? SetupStatus.Installed : SetupStatus.Uninstalled;    
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
                // get Everyone token for the current locale
                string everyone = new System.Security.Principal.SecurityIdentifier(
                    "S-1-1-0").Translate(typeof(System.Security.Principal.NTAccount)).ToString();
                string port = ConfigurationManager.AppSettings["Port"];
                string arguments = string.Format("http add urlacl url=http://+:{0}/ user=\"\\{1}\"", port, everyone);
                Log.Info(StringLib.UrlReservation_AddRule);
                ProcessHelper.RunNetShell(arguments, StringLib.UrlReservation_FailedAddRule);
                _status = SetupStatus.Installed;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                _status = SetupStatus.Failed;
                Settings.Instance.UrlReservationSetupHadErrors = true;
                Settings.Instance.Save();
                throw;
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
                string port = ConfigurationManager.AppSettings["Port"];
                string arguments = $@"http delete urlacl url=http://+:{port}/";
                Log.Info(StringLib.UrlReservation_RmRule);
                ProcessHelper.RunNetShell(arguments, StringLib.UrlReservation_FailedRmRule);
                status = SetupStatus.Uninstalled;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                status = SetupStatus.Failed;
            }
            return status;
        }
    }
}