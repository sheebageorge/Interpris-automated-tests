using System;
using System.Collections.Generic;
using System.Text;

namespace Automation.UI.Core.APILibraries.TranscriptData
{
    public class Speaker
    {
        public string Name { get; set; }
        public List<TimeSpan> TimeSpans { get; set; }
        public string ID { get; set; }

        public override string ToString()
        {
            StringBuilder myStringBuilder = new StringBuilder();

            myStringBuilder.AppendLine("===== Name:");
            myStringBuilder.AppendLine(Name);
            myStringBuilder.AppendLine("===== TimeSpans:");
            myStringBuilder.AppendLine(String.Join("\n", TimeSpans));
            myStringBuilder.AppendLine("===== ID:");
            myStringBuilder.AppendLine(ID);

            return myStringBuilder.ToString();
        }
    }
}
