using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mouseR
{
    internal class MouseReset
    {
        private static string command_1 = "/C %SystemRoot%\\Sysnative\\pnputil /enum-devices /connected /class mouse";
        private static string command_2 = "/C %SystemRoot%\\Sysnative\\pnputil /remove-device ";
        private static string command_3 = "/C %SystemRoot%\\Sysnative\\pnputil /scan-devices";

        public static void main()
        {
            var proc_1 = new ProcessStartInfo
            {
                FileName = @"C:\Windows\System32\cmd.exe",
                Arguments = command_1,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            var end_proc = Process.Start(proc_1);
            string read = "";
            while (true)
            {
                read = end_proc.StandardOutput.ReadLine();
                if (read.Contains("Instance ID:"))
                {
                    break;
                }
                else if (read == null)
                {
                    return;
                }
            }
            int point = 0;
            for (int i = read.Length - 1; i > 0; i--)
            {
                if (read[i] == ' ')
                {
                    point = i + 1;
                    break;
                }
            }

            string cmd_extra = read.Substring(point);

            System.Threading.Thread.Sleep(100);

            var proc_2 = new ProcessStartInfo
            {
                Verb = "runas",
                FileName = @"C:\Windows\System32\cmd.exe",
                Arguments = command_2 + "\"" + cmd_extra + "\"",
                UseShellExecute = true,
                RedirectStandardOutput = false,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(proc_2);

            System.Threading.Thread.Sleep(1000);

            var proc_3 = new ProcessStartInfo
            {
                FileName = @"C:\Windows\System32\cmd.exe",
                Arguments = command_3,
                UseShellExecute = true,
                Verb = "runas",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = false,
                RedirectStandardOutput = false
            };
            Process.Start(proc_3);
            return;

        }
    }
}
