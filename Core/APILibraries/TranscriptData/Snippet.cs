namespace Automation.UI.Core.APILibraries.TranscriptData
{
    public class Snippet
    {
        public double? Confidence { get; set; }
        public double StartTime { get; set; }
        public double Duration { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return string.Format("Confidence:{0}\nStartTime:{1}\nDuratione:{2}\nText:{3}\n",
                Confidence, StartTime, Duration, Text);
        }
    }
}
