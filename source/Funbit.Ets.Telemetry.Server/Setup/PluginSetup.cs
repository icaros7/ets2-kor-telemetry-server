using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows.Forms;
using Funbit.Ets.Telemetry.Server.Helpers;
using Microsoft.Win32;

namespace Funbit.Ets.Telemetry.Server.Setup
{
    public class PluginSetup : ISetup
    {
        static readonly log4net.ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        const string Ets2 = "ETS2";
        const string Ats = "ATS";
        SetupStatus _status;
        
        public PluginSetup()
        {
            try
            {
                Log.Info("플러그인 DLL 파일 확인중...");
                
                var ets2State = new GameState(Ets2, Settings.Instance.Ets2GamePath);
                var atsState = new GameState(Ats, Settings.Instance.AtsGamePath);

                if (ets2State.IsPluginValid() && atsState.IsPluginValid())
                {
                    _status = SetupStatus.Installed;
                }
                else
                {
                    _status = SetupStatus.Uninstalled;
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
                var ets2State = new GameState(Ets2, Settings.Instance.Ets2GamePath);
                var atsState = new GameState(Ats, Settings.Instance.AtsGamePath);

                if (!ets2State.IsPluginValid())
                {
                    ets2State.DetectPathNum();
                    for (; ets2State.try_num < ets2State.max_vdf; ets2State.try_num++)
                    {
                        ets2State.DetectPath2(ets2State.try_num);
                        if (!ets2State.IsPathValid())
                            continue;
                        else if (ets2State.IsPathValid())
                        {
                            ets2State.InstallPlugin();
                            break;
                        }
                    }
                    ets2State.BrowserForValidPath(owner);
                }

                if (!atsState.IsPluginValid())
                {
                    atsState.DetectPathNum();
                    for (; atsState.try_num < atsState.max_vdf; atsState.try_num++)
                    {
                        atsState.DetectPath2(atsState.try_num);
                        if (!atsState.IsPathValid())
                            continue;
                        else if (atsState.IsPathValid())
                        {
                            atsState.InstallPlugin();
                            break;
                        }
                    }
                    atsState.BrowserForValidPath(owner);
                }
                
                Settings.Instance.Ets2GamePath = ets2State.GamePath;
                Settings.Instance.AtsGamePath = atsState.GamePath;
                Settings.Instance.Save();
                
                _status = SetupStatus.Installed;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                _status = SetupStatus.Failed;
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
                var ets2State = new GameState(Ets2, Settings.Instance.Ets2GamePath);
                var atsState = new GameState(Ats, Settings.Instance.AtsGamePath);
                ets2State.UninstallPlugin();
                atsState.UninstallPlugin();
                status = SetupStatus.Uninstalled;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                status = SetupStatus.Failed;
            }
            return status;
        }
        
        class GameState
        {
            const string InstallationSkippedPath = "N/A";
            const string TelemetryDllName = "ets2-telemetry-server.dll";
            const string TelemetryX64DllMd5 = "1e41e885881eda2886a30f06573fb20e";
            const string TelemetryX86DllMd5 = "f600b370e66f39546fcfb4977b95c933";
            public int path_num = 0;
            public int max_vdf = 0;
            public int try_num = 0;

            readonly string _gameName;

            public GameState(string gameName, string gamePath)
            {
                _gameName = gameName;
                GamePath = gamePath;
            }

            string GameDirectoryName
            {
                get
                {
                    string fullName = "Euro Truck Simulator 2";
                    if (_gameName == Ats)
                        fullName = "American Truck Simulator";
                    return fullName;
                }
            }

            public string GamePath { get; private set; }

            public bool IsPathValid()
            {
                if (GamePath == InstallationSkippedPath)
                    return true;

                if (string.IsNullOrEmpty(GamePath))
                    return false;

                var baseScsPath = Path.Combine(GamePath, "base.scs");
                var binPath = Path.Combine(GamePath, "bin");
                bool validated = File.Exists(baseScsPath) && Directory.Exists(binPath);
                Log.InfoFormat("{2} 경로 확인중 : '{0}' ... {1}", GamePath, validated ? "확인" : "실패", _gameName);
                return validated;
            }

            public bool IsPluginValid()
            {
                if (GamePath == InstallationSkippedPath)
                    return true;

                if (!IsPathValid())
                    return false;

                return Md5(GetTelemetryPluginDllFileName(GamePath, x64: true)) == TelemetryX64DllMd5 &&
                    Md5(GetTelemetryPluginDllFileName(GamePath, x64: false)) == TelemetryX86DllMd5;
            }

            public void InstallPlugin()
            {
                if (GamePath == InstallationSkippedPath)
                    return;

                string x64DllFileName = GetTelemetryPluginDllFileName(GamePath, x64: true);
                string x86DllFileName = GetTelemetryPluginDllFileName(GamePath, x64: false);

                Log.InfoFormat("{1} x86 플러그인 DLL 파일을 복사중 : {0}", x86DllFileName, _gameName);
                File.Copy(LocalEts2X86TelemetryPluginDllFileName, x86DllFileName, true);

                Log.InfoFormat("Copying {1} x64 플러그인 DLL 파일을 복사중 : {0}", x64DllFileName, _gameName);
                File.Copy(LocalEts2X64TelemetryPluginDllFileName, x64DllFileName, true);
            }

            public void UninstallPlugin()
            {
                if (GamePath == InstallationSkippedPath)
                    return;

                Log.InfoFormat("{0}의 플러그인 DLL 파일을 백업중...", _gameName);
                string x64DllFileName = GetTelemetryPluginDllFileName(GamePath, x64: true);
                string x86DllFileName = GetTelemetryPluginDllFileName(GamePath, x64: false);
                string x86BakFileName = Path.ChangeExtension(x86DllFileName, ".bak");
                string x64BakFileName = Path.ChangeExtension(x64DllFileName, ".bak");
                if (File.Exists(x86BakFileName))
                    File.Delete(x86BakFileName);
                if (File.Exists(x64BakFileName))
                    File.Delete(x64BakFileName);
                File.Move(x86DllFileName, x86BakFileName);
                File.Move(x64DllFileName, x64BakFileName);
            }

            static string GetDefaultSteamPath()
            {
                var steamKey = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam");
                return steamKey?.GetValue("SteamPath") as string;
            }

            static string LocalEts2X86TelemetryPluginDllFileName => Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, @"Ets2Plugins\win_x86\plugins\", TelemetryDllName);

            static string LocalEts2X64TelemetryPluginDllFileName => Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, @"Ets2Plugins\win_x64\plugins", TelemetryDllName);
            
            static string GetPluginPath(string gamePath, bool x64)
            {
                return Path.Combine(gamePath, x64 ? @"bin\win_x64\plugins" : @"bin\win_x86\plugins");
            }

            static string GetTelemetryPluginDllFileName(string gamePath, bool x64)
            {
                string path = GetPluginPath(gamePath, x64);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return Path.Combine(path, TelemetryDllName);
            }
            
            static string Md5(string fileName)
            {
                if (!File.Exists(fileName))
                    return null;
                using (var provider = new MD5CryptoServiceProvider())
                {
                    var bytes = File.ReadAllBytes(fileName);
                    var hash = provider.ComputeHash(bytes);
                    var result = string.Concat(hash.Select(b => $"{b:x02}"));
                    return result;
                }
            }

            public void DetectPath()
            {
                GamePath = GetDefaultSteamPath();
                if (!string.IsNullOrEmpty(GamePath))
                    GamePath = Path.Combine(
                        GamePath.Replace('/', '\\'), @"SteamApps\common\" + GameDirectoryName);
            }

            public void DetectPath2(int try_n)
            {
                GamePath = GetDefaultSteamPath();
                GamePath = GamePath.Replace('/', '\\');
                if (try_num == 0)
                {
                    GamePath = Path.Combine(GamePath.Replace('/', '\\'), @"SteamApps\common\" + GameDirectoryName);
                    return;
                }
                string LibraryFolders = File.ReadAllText(Path.Combine(GamePath, @"SteamApps\libraryfolders.vdf"));
                string[] path = LibraryFolders.Split('"');
                string SteamLibrary = "SteamLibrary";
                if (System.Text.RegularExpressions.Regex.IsMatch(path[try_n], SteamLibrary, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    GamePath = Path.Combine(path[try_n].Replace(@"\\", @"\"), @"SteamApps\common\" + GameDirectoryName);
                }
            }

            public void DetectPathNum()
            {
                string SteamPath = GetDefaultSteamPath();
                SteamPath = SteamPath.Replace('/', '\\');
                if (File.Exists(Path.Combine(SteamPath, @"SteamApps\libraryfolders.vdf")))
                {
                    string LibraryFolders = File.ReadAllText(Path.Combine(SteamPath, @"SteamApps\libraryfolders.vdf"));
                    string[] multi_path = LibraryFolders.Split('"');
                    string SteamLibrary = "SteamLibrary";
                    max_vdf = multi_path.Length;
                    foreach (string s in multi_path)
                    {
                        if (System.Text.RegularExpressions.Regex.IsMatch(s, SteamLibrary, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                        {
                            path_num++;
                        }
                    }
                }
            }

            public void BrowserForValidPath(IWin32Window owner)
            {
                while (!IsPathValid())
                {
                    var result = MessageBox.Show(owner,
                        _gameName + @"의 경로를 탐지하지 못했습니다. " + Environment.NewLine +
                        @"만약 " + _gameName + @"를 설치하지 않았다면 [취소]를 눌러 넘깁니다." + Environment.NewLine +
                        @"아니라면 [확인]을 눌러 경로를 직접 설정합니다." + Environment.NewLine + Environment.NewLine +
                        @"예시 :" + Environment.NewLine + @"D:\STEAM\SteamApps\common\" + 
                        GameDirectoryName,
                        @"경고", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    if (result == DialogResult.Cancel)
                    {
                        GamePath = InstallationSkippedPath;
                        return;
                    }
                    var browser = new FolderBrowserDialog();
                    browser.Description = _gameName + @"의 경로가 선택됨";
                    browser.ShowNewFolderButton = false;
                    result = browser.ShowDialog(owner);
                    if (result == DialogResult.Cancel)
                        Environment.Exit(1);
                    GamePath = browser.SelectedPath;
                }
            }
        }
    }
}