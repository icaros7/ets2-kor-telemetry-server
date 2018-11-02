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
        
        static readonly string FirewallRuleName = $"ETS2 TELEMETRY 서버 (포트 {ConfigurationManager.AppSettings["Port"]})";

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
                    Log.Info("방화벽 규칙 확인중...");
                    string output = ProcessHelper.RunNetShell(arguments, "방화벽 규칙 상태 확인 실패");
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
                Log.Info("방화벽 규칙 추가중...");
                ProcessHelper.RunNetShell(arguments, "방화벽 규칙 추가 실패");
                _status = SetupStatus.Installed;
            }
            catch (Exception ex)
            {
                _status = SetupStatus.Failed;
                Log.Error(ex);
                Settings.Instance.FirewallSetupHadErrors = true;
                Settings.Instance.Save();
                throw new Exception("윈도우 방화벽에 연결 할 수 없습니다." + Environment.NewLine +
                                    "만약 제3자 소프트웨어 백신 (V3, 카스퍼스키 등)을 사용중 이라면,  " +
                                    ConfigurationManager.AppSettings["Port"] + "번 TCP포트를 직접 열어주세요!", ex);
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
                Log.Info("방화벽 규칙 제거중...");
                ProcessHelper.RunNetShell(arguments, "방화벽 규칙 제거 실패");
                status = SetupStatus.Uninstalled;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                _status = SetupStatus.Failed;
                throw new Exception("윈도우 방화벽에 연결 할 수 없습니다." + Environment.NewLine +
                                    "만약 제3자 소프트웨어 백신 (V3, 카스퍼스키 등)을 사용중 이라면,  " +
                                    ConfigurationManager.AppSettings["Port"] + "번 TCP포트를 직접 닫아주세요!", ex);
            }
            return status;
        }
    }
}