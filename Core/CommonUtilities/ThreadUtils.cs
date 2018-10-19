using System.Threading;

namespace Automation.UI.Core.CommonUtilities
{
    /// <summary>
    /// This class provides functions of threading such as thread sleep
    /// </summary>
    public class ThreadUtils
    {
        private const int SHORT_SLEEP = 1;
        private const int MEDIUM_SLEEP = 5;
        private const int LONG_SLEEP = 10;

        /// <summary>
        /// Force main thread to sleep in a specific time
        /// </summary>
        /// <param name="sleepSeconds">Seconds to sleep</param>
        private static void SleepInSeconds(int sleepSeconds)
        {
            for(int i = 0; i < sleepSeconds; i++)
            {
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Force main thread to sleep in a short time (1 second)
        /// </summary>
        public static void SleepShortTime() => SleepInSeconds(SHORT_SLEEP);

        /// <summary>
        /// Force main thread to sleep in a medium time (5 seconds)
        /// </summary>
        public static void SleepMediumTime() => SleepInSeconds(MEDIUM_SLEEP);

        /// <summary>
        /// Force main thread to sleep in a long time (10 seconds)
        /// </summary>
        public static void SleepLongTime() => SleepInSeconds(LONG_SLEEP);

        /// <summary>
        /// Sleep in a very short time
        /// </summary>
        public static void SleepVeryShortTime() => Thread.Sleep(100);
    }
}
