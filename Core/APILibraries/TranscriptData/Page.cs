using System;
using System.Collections.Generic;
using System.Text;

namespace Automation.UI.Core.APILibraries.TranscriptData
{
    public class Page
    {
        public double StartTime { get; set; }
        public List<Snippet> Snippets { get; set; }
        public string ID { get; set; }

        public override string ToString()
        {
            StringBuilder myStringBuilder = new StringBuilder();

            myStringBuilder.AppendLine("===== StartTime:");
            myStringBuilder.AppendLine(StartTime.ToString());
            myStringBuilder.AppendLine("===== Snippets:");
            myStringBuilder.AppendLine(String.Join("\n", Snippets));
            myStringBuilder.AppendLine("===== ID:");
            myStringBuilder.AppendLine(ID);

            return myStringBuilder.ToString();
        }
    }
}
