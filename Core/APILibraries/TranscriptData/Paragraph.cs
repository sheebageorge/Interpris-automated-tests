using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Automation.UI.Core.APILibraries.TranscriptData
{
    public class Paragraph
    {
        public const string SPECIAL_CHAR_LIST = ",!?:.;%`";
        public const string SPECIAL_CHAR_LIST_NO_SPACE_PRE = ",!?:.;";
        public const string SPECIAL_CHAR_LIST_WITH_SPACE_PRE = "%`";

        public string SpeakerTag { get; set; }
        public double TimeStamp { get; set; }
        public double Duration { get; set; }
        public string Text { get; set; }

        public Paragraph(string speakerTag, double timeStamp, double duration, string text)
        {
            SpeakerTag = speakerTag;
            TimeStamp = timeStamp;
            Duration = duration;
            Text = text;
        }

        /// <summary>
        /// Convert the JSON object to paragraphs
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <param name="pageIndex"></param>
        /// <returns>List of paragraphs</returns>
        public static IList<Paragraph> GetParagraphsFromJSONObject(TranscriptJSONFile jsonObject, int pageIndex)
        {
            IList<Paragraph> paragraphList = new List<Paragraph>();

            // get the current page object
            Page currentPage = jsonObject.Pages[pageIndex];

            // get speaker object list
            List<Speaker> currentSpeakers = jsonObject.Speakers;

            // get the speaker tag - time stamp list
            List<double> speakerTimeStamp = new List<double>();
            foreach (Speaker speaker in currentSpeakers)
            {
                foreach (TimeSpan timeSpan in speaker.TimeSpans)
                {
                    speakerTimeStamp.Add(timeSpan.StartTime);
                }
            }

            speakerTimeStamp = speakerTimeStamp.OrderBy(o => o).ToList();

            // loop over speaker list to create the paragraphs
            foreach (Speaker speaker in currentSpeakers)
            {
                List<Snippet> currentSnippets = currentPage.Snippets;

                foreach (TimeSpan timeSpan in speaker.TimeSpans)
                {
                    try
                    {
                        // get next timestamp to stop the paragraph building
                        double nextTimeStampVal;
                        try
                        {
                            int nextTimeStampIdx = speakerTimeStamp.FindIndex(o => o == timeSpan.StartTime);

                            if (nextTimeStampIdx < speakerTimeStamp.Count - 1)
                            {
                                nextTimeStampVal = speakerTimeStamp[nextTimeStampIdx + 1];
                            }
                            else
                            {
                                nextTimeStampVal = double.MaxValue; // jsonObject.TranscriptionAudioDuration;
                            }
                        }
                        catch (ArgumentNullException)
                        {
                            nextTimeStampVal = double.MaxValue; // jsonObject.TranscriptionAudioDuration;
                        }

                        // building the paragraph
                        StringBuilder paragraphText = new StringBuilder();

                        int findWordIdx = currentSnippets.FindIndex(o => o.StartTime == timeSpan.StartTime);
                        double currentDuration = 0;

                        // add the first word
                        paragraphText.Append(currentSnippets[findWordIdx].Text);
                        currentDuration += currentSnippets[findWordIdx].Duration;

                        for (int i = findWordIdx + 1; i < currentSnippets.Count; i++)
                        {
                            if (currentSnippets[i].StartTime < nextTimeStampVal)
                            {
                                string currentText = currentSnippets[i].Text;

                                // if the word is special char, not add space to head
                                if (currentText.Length == 1 && SPECIAL_CHAR_LIST_NO_SPACE_PRE.Contains(currentText))
                                {
                                    paragraphText.Append(currentText);
                                }
                                else
                                {
                                    paragraphText.Append(' ');
                                    paragraphText.Append(currentText);
                                }

                                currentDuration += currentSnippets[i].Duration;
                            }
                            else
                            {
                                break;
                            }
                        }

                        paragraphList.Add(new Paragraph(speaker.Name, timeSpan.StartTime,
                            timeSpan.Duration, paragraphText.ToString()));
                    }
                    catch (ArgumentNullException) { }
                }
            }

            return paragraphList.OrderBy(o => o.TimeStamp).ToList();
        }

        public override string ToString()
        {
            return string.Format("SpeakerTag:{0}\nTimeStamp:{1}\nParagraph:{2}\n", SpeakerTag, TimeStamp, Text);
        }
    }
}
