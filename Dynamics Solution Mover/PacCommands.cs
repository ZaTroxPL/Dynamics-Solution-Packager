
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
        internal static async Task<string> CheckIfPacAsync()
        {            
            return await PacRunner.RunPacCommandAsync("");
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

        internal static async Task<string> PacAuthListAsync()
        {
            return await PacRunner.RunPacCommandAsync("auth list");
        }

        internal static async Task<string> PacAuthCreateAsync()
        {
            return await PacRunner.RunPacCommandAsync("auth create");
        }

        internal static async Task<string> PacAuthSelectAsync(int index)
        {
            return await PacRunner.RunPacCommandAsync($"auth select -i {index}");
        }

        internal static async Task<string> PacAuthWhoAsync()
        {
            return await PacRunner.RunPacCommandAsync($"auth who");
        }
    }

    internal static class PacRunner
    {
        internal static async Task<string> RunPacCommandAsync(string arguments)
        {
            ProcessStartInfo command = new()
            {
                FileName = "pac",
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = Process.Start(command);
            ArgumentNullException.ThrowIfNull(process);

            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();

            process.WaitForExit();

            if (process.ExitCode != 0)
                throw new Exception($"PAC command failed: {error}");

            return output;
        }
    }
}
