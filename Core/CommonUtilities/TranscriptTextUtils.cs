using Automation.UI.Core.APILibraries.TranscriptData;
using Automation.UI.Core.Selenium.PageObjects.Interpris.Product;
using Automation.UI.Core.TranscriptFileLibraries;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Automation.UI.Core.CommonUtilities
{
    public class TranscriptTextUtils
    {
        public const char EXPORTED_TRANSCRIPT_FILE_COLUMN_DELIMETER = '\t';

        /// <summary>
        /// Read transcript file and return results
        /// </summary>
        /// <param name="fileFolder"></param>
        /// <param name="fileName"></param>
        /// <returns>List of paragraphs</returns>
        public static IList<ExportedPlainTextTranscriptObject> ReadExportedTranscriptFile(
            string fileFolder, string fileName)
        {
            IList<ExportedPlainTextTranscriptObject> fileContent = new List<ExportedPlainTextTranscriptObject>();
            string filePath = Path.Combine(fileFolder, fileName);
            string line;

            // Read the file and display it line by line.  
            StreamReader file = new StreamReader(filePath);

            try
            {
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Trim().Length > 1)
                    {
                        string[] lineArr = line.Split(EXPORTED_TRANSCRIPT_FILE_COLUMN_DELIMETER);

                        fileContent.Add(new ExportedPlainTextTranscriptObject(lineArr[0], lineArr[1], lineArr[2]));
                    }
                }
            }
            finally
            {
                file.Close();
            }

            return fileContent;
        }

        /// <summary>
        /// Calculate the number of word in a transcribed text
        /// </summary>
        /// <param name="transcribedText"></param>
        /// <param name="subStrLength"></param>
        /// <returns>The number of word</returns>
        public static int GetTranscribedTextWordCount(string transcribedText, int subStrLength = -1)
        {
            transcribedText = transcribedText.Trim();

            string strPreOfMergedWords = transcribedText;

            if (subStrLength > 0)
            {
                strPreOfMergedWords = strPreOfMergedWords.Substring(0, subStrLength);
            }

            return StringUtils.WordCount(strPreOfMergedWords) +
                strPreOfMergedWords.Count(o => Paragraph.SPECIAL_CHAR_LIST.Contains(o));
        }

        /// <summary>
        /// Get the index of the snippet of the target word
        /// </summary>
        /// <param name="transcriptJSONFile"></param>
        /// <param name="paragraphIndex"></param>
        /// <param name="wordParagraphIndex"></param>
        /// <returns>Snippet object</returns>
        public static int GetSnippetIndexOfAWord(TranscriptJSONFile transcriptJSONFile,
            int paragraphIndex, int wordParagraphIndex)
        {
            // calculate the new paragraphs
            IList<Paragraph> Paragraphs = Paragraph.GetParagraphsFromJSONObject(
                transcriptJSONFile, TranscribeEditorPage.DEFAULT_PAGE_INDEX);

            double paraTimeStamp = Paragraphs[paragraphIndex].TimeStamp;
            List<Snippet> Snippets = transcriptJSONFile.Pages[TranscribeEditorPage.DEFAULT_PAGE_INDEX].Snippets;

            for (int i = 0; i < Snippets.Count; i++)
            {
                if (Snippets[i].StartTime == paraTimeStamp)
                {
                    return i + wordParagraphIndex;
                }
            }

            return 0;
        }

        /// <summary>
        /// Get random index of a word from head or tail
        /// </summary>
        /// <param name="targetString"></param>
        /// <param name="fromHeadOfWord"></param>
        /// <returns>The random index the substring in the big string</returns>
        public static int LookUpPositionOfAWord(string targetString, bool fromHeadOfWord = true)
        {
            List<int> allIndexes = StringUtils.AllIndexesOf(targetString,
                TranscribeEditorPage.TRANSCRIPT_WORD_SPACE_STR);

            if (fromHeadOfWord)
            {
                if (allIndexes.Count <= 0)
                {
                    return 0;
                }

                return allIndexes.ElementAt(RandomUtils.GetIntNumberInRange(0, allIndexes.Count)) + 1;
            }
            else
            {
                if (allIndexes.Count <= 0)
                {
                    // if the left char of the space is special char, re-search another space
                    if (Paragraph.SPECIAL_CHAR_LIST.Contains(targetString[targetString.Length - 1]))
                    {
                        return targetString.Length - 1;
                    }

                    return targetString.Length;
                }

                int foundIdx = allIndexes.ElementAt(RandomUtils.GetIntNumberInRange(0, allIndexes.Count));

                if (foundIdx > 0)
                {
                    // if the left char of the space is special char, re-search another space
                    if (Paragraph.SPECIAL_CHAR_LIST.Contains(targetString[foundIdx - 1]))
                    {
                        foundIdx -= 1;
                    }
                }

                return foundIdx;
            }
        }
    }
}
