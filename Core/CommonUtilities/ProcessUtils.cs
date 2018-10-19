using System.Diagnostics;

namespace Automation.UI.Core.CommonUtilities
{
    public class ProcessUtils
    {
        /// <summary>
        /// Start a process and wait for it to complete
        /// </summary>
        /// <param name="cmdStr">command to run</param>
        public static void RunProcessAndWaitFinish(string cmdStr)
        {
            var procStIfo = new ProcessStartInfo("cmd", "/c " + cmdStr)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var proc = new Process
            {
                StartInfo = procStIfo
            };

            proc.Start();

            // wait for process finishes
            proc.WaitForExit();
        }
    }
}
