namespace Automation.UI.Core.APILibraries.TranscriptData
{
    public class TimeSpan
    {
        public double StartTime { get; set; }
        public double Duration { get; set; }

        public override string ToString()
        {
            return string.Format("StartTime:{0}\nDuration:{1}\n",
                StartTime, Duration);
        }
    }
}
