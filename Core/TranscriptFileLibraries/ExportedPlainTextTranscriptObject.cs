using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.UI.Core.TranscriptFileLibraries
{
    public class ExportedPlainTextTranscriptObject
    {
        public string TimeStamp { get; set; }
        public string SpeakerTag { get; set; }
        public string Text { get; set; }

        public ExportedPlainTextTranscriptObject(string timeStamp, string speakerTag, string text)
        {
            TimeStamp = timeStamp;
            SpeakerTag = speakerTag;
            Text = text;
        }
    }
}
