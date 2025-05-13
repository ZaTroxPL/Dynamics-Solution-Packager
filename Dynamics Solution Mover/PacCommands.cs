
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics_Solution_Mover
{
    internal static class PacCommands
    {
        internal static string CheckIfPac()
        {
            ProcessStartInfo checkIfPacExists = new()
            {
                FileName = "pac",
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };

            var proc = Process.Start(checkIfPacExists);
            ArgumentNullException.ThrowIfNull(proc);
            string output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();

            return output;
        }

        internal static async Task<string> CheckIfPacAsync()
        {
            return await Task.Run(() => CheckIfPac());
        }

        internal static string InstallPac()
        {
            ProcessStartInfo startInfo = new()
            {
                FileName = "dotnet",
                Arguments = "tool install --global Microsoft.PowerApps.CLI.Tool",
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };

            var proc = Process.Start(startInfo);
            ArgumentNullException.ThrowIfNull(proc);
            string output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();

            return output;
        }

        internal static async Task<string> InstallPacAsync()
        {
            return await Task.Run(() => InstallPac());
        }

        internal static string PacAuthList()
        {
            ProcessStartInfo authList = new()
            {
                FileName = "pac",
                Arguments = "auth list",
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };

            var proc = Process.Start(authList);
            ArgumentNullException.ThrowIfNull(proc);
            string output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();

            return output;
        }

        internal static async Task<string> PacAuthListAsync()
        {
            return await Task.Run(() => PacAuthList());
        }


        internal static string PacAuthCreate()
        {
            ProcessStartInfo authCreate = new()
            {
                FileName = "pac",
                Arguments = "auth create",
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };

            var proc = Process.Start(authCreate);
            ArgumentNullException.ThrowIfNull(proc);
            string output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();

            return output;
        }

        internal static async Task<string> PacAuthCreateAsync()
        {
            return await Task.Run(() => PacAuthCreate());
        }

        internal static string PacAuthSelect(int index)
        {
            ProcessStartInfo authSelect = new()
            {
                FileName = "pac",
                Arguments = $"auth select -i {index}",
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };

            var proc = Process.Start(authSelect);
            ArgumentNullException.ThrowIfNull(proc);
            string output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();

            return output;
        }

        internal static async Task<string> PacAuthSelectAsync(int index)
        {
            return await Task.Run(() => PacAuthSelect(index));
        }

        internal static string PacAuthWho()
        {
            ProcessStartInfo authWho = new()
            {
                FileName = "pac",
                Arguments = $"auth who",
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };

            var proc = Process.Start(authWho);
            ArgumentNullException.ThrowIfNull(proc);
            string output = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();

            return output;
        }

        internal static async Task<string> PacAuthWhoAsync()
        {
            return await Task.Run(() => PacAuthWho());
        }
    }
}
