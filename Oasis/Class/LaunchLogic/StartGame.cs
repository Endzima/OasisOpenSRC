using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using Oasis.Class.LaunchLogic;

namespace Oasis.Class.LaunchLogic
{
    // Also written by me, to whoever decomp'd this launcher, fuck you and also this is written by me, ain't Eon.
    public class StartGame
    {
        public static void Launch(string fortnitePath, string ExeArgs, string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Logger.Log("Invalid launch arguments, this shouldn't be possible, contact owner. Error Code: WR-1005");
                MessageBox.Show("Invalid launch arguments, this shouldn't be possible, contact owner. Error Code: WR-1005");
                return;
            }

            if (string.IsNullOrEmpty(fortnitePath))
            {
                throw new ArgumentException("Fortnite executable path is not set.");
            }

            string calderaToken = "eyJhbGciOiJFUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2NvdW50X2lkIjoiYmU5ZGE1YzJmYmVhNDQwN2IyZjQwZWJhYWQ4NTlhZDQiLCJnZW5lcmF0ZWQiOjE2Mzg3MTcyNzgsImNhbGRlcmFHdWlkIjoiMzgxMGI4NjMtMmE2NS00NDU3LTliNTgtNGRhYjNiNDgyYTg2IiwiYWNQcm92aWRlciI6IkVhc3lBbnRpQ2hlYXQiLCJub3RlcyI6IiIsImZhbGxiYWNrIjpmYWxzZX0.VAWQB67RTxhiWOxx7DBjnzDnXyyEnX7OljJm-j2d88G_WgwQ9wrE6lwMEHZHjBd1ISJdUO1UVUqkfLdU5nofBQ";
            string exePath = Path.Combine(fortnitePath, "FortniteGame\\Binaries\\Win64", "FortniteClient-Win64-Shipping.exe");

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = $"-AUTH_LOGIN={username} -AUTH_PASSWORD={password} -AUTH_TYPE=epic {ExeArgs}",
                UseShellExecute = true,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                CreateNoWindow = false
            };

            try
            {
                Process fortniteProcess = Process.Start(startInfo);
                Debug.WriteLine("Fortnite is launching...");

                if (fortniteProcess != null)
                {
                    fortniteProcess.WaitForInputIdle();

                    int processId = fortniteProcess.Id;
                    string dllPath = Path.Combine(fortnitePath, "Engine\\Binaries\\ThirdParty\\NVIDIA\\NVaftermath\\Win64", "Redirect.dll");

                    Task.Delay(20000);

                    InjectRedirect injectRedirect = new InjectRedirect();
                    bool injectionSuccess = injectRedirect.InjectDLL(processId, dllPath);

                    if (injectionSuccess)
                    {
                        Logger.Log($"DLL injected successfully into process ID: {processId}");
                    }
                    else
                    {
                        Logger.Log($"DLL injection failed for process ID: {processId}");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error launching Fortnite: " + ex.Message);
            }
        }

    }
}
