using System;
using System.Collections.Generic;
using System.Text;

namespace Automation.UI.Core.APILibraries.TranscriptData
{
    public class TranscriptJSONFile
    {
        public string TranscriptionId { get; set; }
        public string TranscriptionAudioName { get; set; }
        public double TranscriptionAudioDuration { get; set; }
        public List<Page> Pages { get; set; }
        public List<Speaker> Speakers { get; set; }
        public string ResponseId { get; set; }

        public override string ToString()
        {
            StringBuilder myStringBuilder = new StringBuilder();

            myStringBuilder.AppendLine("===== TranscriptionId:");
            myStringBuilder.AppendLine(TranscriptionId);
            myStringBuilder.AppendLine("===== TranscriptionAudioName:");
            myStringBuilder.AppendLine(TranscriptionAudioName);
            myStringBuilder.AppendLine("===== TranscriptionAudioDuration:");
            myStringBuilder.AppendLine(TranscriptionAudioDuration.ToString());
            myStringBuilder.AppendLine("===== Pages:");
            myStringBuilder.AppendLine(String.Join("\n", Pages));
            myStringBuilder.AppendLine("===== Speakers:");
            myStringBuilder.AppendLine(String.Join("\n", Speakers));
            myStringBuilder.AppendLine("===== ResponseId:");
            myStringBuilder.AppendLine(ResponseId);

            return myStringBuilder.ToString();
        }
    }
}
