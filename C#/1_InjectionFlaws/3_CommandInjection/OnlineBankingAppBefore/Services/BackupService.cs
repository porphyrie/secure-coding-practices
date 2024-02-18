using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OnlineBankingApp.Services
{
    public class BackupService
    {
        public async Task BackupDB(string backupName)
        {
            using (Process p = new Process())
            {
                string source = Path.Combine(Environment.CurrentDirectory, "OnlineBank.db");
                string backupsDir = Path.Combine(Environment.CurrentDirectory, "backups");
                string destination = Path.Combine(backupsDir, backupName);

                if (!Directory.Exists(backupsDir))
                {
                    Directory.CreateDirectory(backupsDir);
                }

                p.StartInfo.Arguments = "/c copy \"" + source + "\" \"" + destination + "\"";
                p.StartInfo.FileName = "cmd";
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;

                p.Start();
                string output = await p.StandardOutput.ReadToEndAsync();
                string error = await p.StandardError.ReadToEndAsync();

                await p.WaitForExitAsync();

                if (p.ExitCode != 0)
                {
                    throw new InvalidOperationException("Backup failed. Output: " + output + " Error: " + error);
                }
            }
        }
    }
}