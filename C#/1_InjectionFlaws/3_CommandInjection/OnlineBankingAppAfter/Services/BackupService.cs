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
        //public async Task BackupDB(string backupName)
        //{
        //    using (Process p = new Process())
        //    {
        //        var regex = new Regex(@"^[a-zA-Z0-9-]+$");
        //        if (!regex.IsMatch(backupName))
        //        {
        //            throw new ArgumentException("Invalid backup name. Only alphanumeric characters and hyphens are allowed.");
        //        }

        //        string source = Path.Combine(Environment.CurrentDirectory, "OnlineBank.db");
        //        string backupsDir = Path.Combine(Environment.CurrentDirectory, "backups");
        //        string destination = Path.Combine(backupsDir, backupName);

        //        if (!Directory.Exists(backupsDir))
        //        {
        //            Directory.CreateDirectory(backupsDir);
        //        }

        //        p.StartInfo.Arguments = "/c copy \"" + source + "\" \"" + destination + "\"";
        //        p.StartInfo.FileName = "cmd";
        //        p.StartInfo.CreateNoWindow = true;
        //        p.StartInfo.UseShellExecute = false;
        //        p.StartInfo.RedirectStandardInput = true;
        //        p.StartInfo.RedirectStandardOutput = true;
        //        p.StartInfo.RedirectStandardError = true;

        //        p.Start();
        //        string output = await p.StandardOutput.ReadToEndAsync();
        //        string error = await p.StandardError.ReadToEndAsync();

        //        await p.WaitForExitAsync();

        //        if (p.ExitCode != 0)
        //        {
        //            throw new InvalidOperationException("Backup failed. Output: " + output + " Error: " + error);
        //        }
        //    }
        //}

        public async Task BackupDB(string backupName)
        {
            string source = Path.Combine(Environment.CurrentDirectory, "OnlineBank.db");
            string backupsDir = Path.Combine(Environment.CurrentDirectory, "backups");
            string destination = Path.Combine(backupsDir, backupName);

            await FileCopyAsync(source, destination);
        }

        public async Task FileCopyAsync(string sourceFileName, string destinationFileName,
            int maxRetries = 3, int delayBetweenRetries = 1000)
        {
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    using var sourceFile = new FileStream(sourceFileName, FileMode.Open,
                        FileAccess.Read, FileShare.ReadWrite);
                    using var destinationFile = new FileStream(destinationFileName, FileMode.Create,
                        FileAccess.Write, FileShare.None);

                    await sourceFile.CopyToAsync(destinationFile);
                    return;
                }
                catch (IOException)
                {
                    if (i == maxRetries - 1) throw;
                    await Task.Delay(delayBetweenRetries);
                }
            }
        }

    }
}