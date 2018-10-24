using Automation.UI.Core.APILibraries.TranscriptData;
using Automation.UI.Core.CommonUtilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Automation.UI.Core.Selenium.PageObjects.Interpris.Product
{
    public class TranscribeEditorPage : BasePage
    {
        public const string SPEAKER_TAG_DEFAULT = "SPEAKER";

        public const string RGBA_GREY_VALUE = "#979797";

        // transcript text supporting constants
        public const string TRANSCRIPT_WORD_SPACE_STR = " ";
        public const int DEFAULT_PAGE_INDEX = 0;
        public const double LAST_NEW_WORD_ADDED_TIME = 0.1;
        public const double COMPARE_TIMESTAMP_DELTA = 0.01;

        #region Page Objects
        private readonly string divTextEditorContent = "//div[@class=\"DraftEditor-editorContainer\"]" +
            "//div[@data-contents=\"true\"]";
        private readonly string divPageContent = "//div[contains(@class,\"ant-layout-content\")]";

        private readonly string divTranscribedFirstParagraphText = "//div[@class=\"DraftEditor-editorContainer\"]" +
            "//div[@data-contents=\"true\"]//div[@class=\"transcriptParagraph\"]/div";

        private readonly string spanParagraphs = "//div[@class=\"DraftEditor-editorContainer\"]" +
            "//div[@data-contents=\"true\"]//div[@class=\"transcriptParagraph\"]//span[@data-text=\"true\"]";
        private readonly string divSpeakerTags = "//div[@class=\"DraftEditor-root\"]/parent::div[1]/following-sibling::div" +
            "//div[contains(@class,\"StyledName-\")]//input";
        private readonly string divTimeSpans = "//div[@class=\"DraftEditor-root\"]/parent::div[1]/following-sibling::div" +
            "//div[contains(@class,\"TimeSpan-\")]";

        private readonly string divSpeakerTagDropdownContainer = "//div[contains(@class,\"ant-select-dropdown dropdown-speakers " +
            "ant-select-dropdown--single ant-select-dropdown-placement-bottomLeft\")]";
        private readonly string liSpeakerTagsList = "//div[@class=\"ant-select-dropdown dropdown-speakers ant-select-dropdown--single ant-select-dropdown-placement-bottomLeft\"]" +
            "//ul[@role=\"listbox\"]/li[@role=\"option\"]";
        private readonly string ulSpeakerTagList = "//div[@class=\"ant-select-dropdown dropdown-speakers ant-select-dropdown--single ant-select-dropdown-placement-bottomLeft\"]" +
            "//ul[@role=\"listbox\"]";
        private readonly string btnExport = "//button[contains(@class,\"btn-export\")]/span[text()=\"Export\"]";
        private readonly string btnTranscriptSave = "//button[contains(@class,\"btn-save\")]/span[text()=\"Save\"]";

        private readonly string divStatusIndicatorPanel = "//div[contains(@class,\"styles__StyledSaveStatusIndicator\")]";
        private readonly string divSavingTextStatus = "//div[@class=\"SaveStatusIndicator__saving\"]";
        private readonly string divSavedTextStatus = "//div[@class=\"SaveStatusIndicator__success\"]";
        #endregion

        public TranscribeEditorPage(IWebDriver driver, string baseURL) : base(driver, baseURL, PageConstants.TRANSCRIPTION_PAGE_TEXT_EDITOR_URL) { }

        #region Properties
        public BaseWebObject DivTextEditorContent => FindWebElement(divTextEditorContent, true);
        public BaseWebObject DivPageContent => FindWebElement(divPageContent, true);
        public BaseWebObject DivTranscribedFirstParagraphText => FindWebElement(divTranscribedFirstParagraphText, true);
        public IList<BaseWebObject> SpanParagraphs => FindWebElements(spanParagraphs);
        public IList<BaseWebObject> DivSpeakerTags => FindWebElements(divSpeakerTags);
        public IList<BaseWebObject> DivTimeSpans => FindWebElements(divTimeSpans);
        public IList<BaseWebObject> LiSpeakerTagsList => FindWebElements(liSpeakerTagsList);
        public IList<BaseWebObject> DivSpeakerTagDropdownContainer => FindWebElements(divSpeakerTagDropdownContainer);
        public BaseWebObject UlSpeakerTagList => FindWebElement(ulSpeakerTagList, true);
        public BaseWebObject BtnExport => FindWebElement(btnExport, true);
        public BaseWebObject BtnTranscriptSave => FindWebElement(btnTranscriptSave, true);
        public BaseWebObject DivStatusIndicatorPanel => FindWebElement(divStatusIndicatorPanel, true);
        public BaseWebObject DivSavingTextStatus => FindWebElement(divSavingTextStatus, true);
        public BaseWebObject DivSavedTextStatus => FindWebElement(divSavedTextStatus, true);
        #endregion

        #region Methods
        /// <summary>
        /// Get the paragraph objects from UI
        /// </summary>
        /// <returns>List of Paragraph</returns>
        public IList<Paragraph> GetParagraphFromTextEditor()
        {
            IList<Paragraph> ParagraphList = new List<Paragraph>();

            IList<BaseWebObject> paragraphList = SpanParagraphs;
            IList<BaseWebObject> speakerTags = DivSpeakerTags;
            IList<BaseWebObject> timeSpanTags = DivTimeSpans;

            int paragraphCount = paragraphList.Count;

            for (int i = 0; i < paragraphCount; i++)
            {
                ParagraphList.Add(new Paragraph(speakerTags[i].Value.Trim(),
                    StringUtils.ConvertTimeStampToDouble(timeSpanTags[i].Text), 0,
                    paragraphList[i].Text.Trim()));
            }

            return ParagraphList;
        }

        /// <summary>
        /// Get list of all dropdown list which contain speaker tag
        /// </summary>
        /// <param name="speakerTag"></param>
        /// <returns></returns>
        public IList<BaseWebObject> GetAllDropDownsContainSpeakerTag(string speakerTag)
        {
            return FindWebElements("//ul[@role=\"listbox\"]//li[text()=\"" + speakerTag + "\" or " +
                "text()=\"" + speakerTag.ToUpper() + "\"]" +
                "/ancestor::div[contains(@class,\"dropdown-speakers\")]");
        }

        /// <summary>
        /// Get paragraph at the given index
        /// </summary>
        /// <param name="index">Real index of the paragraph in HTML DOM tree</param>
        /// <returns>Object of the paragraph</returns>
        public BaseWebObject GetParagraphAtIndex(int index)
        {
            BaseWebObject item = FindWebElement(string.Format("//div[@class=\"DraftEditor-editorContainer\"]" +
                "//div[@class=\"transcriptParagraph\"][{0}]//span[@data-text=\"true\"]", index));

            // scroll to the element
            ScrollElementToView(item);

            return item;
        }

        /// <summary>
        /// Get timespan at the given index
        /// </summary>
        /// <param name="index">Real index of the timespan in HTML DOM tree</param>
        /// <returns>Object of the timespan</returns>
        public BaseWebObject GetTimeSpanAtIndex(int index)
        {
            BaseWebObject item = FindWebElement(string.Format("//div[@class=\"DraftEditor-root\"]/parent::div[1]" +
                "/following-sibling::div/div[{0}]/div[contains(@class,\"TimeSpan-\")]", index));

            // scroll to the element
            ScrollElementToView(item);

            return item;
        }

        /// <summary>
        /// Get speaker tag at the given index
        /// </summary>
        /// <param name="index">Real index of the speaker tag in HTML DOM tree</param>
        /// <returns>Object of the speaker tag</returns>
        public BaseWebObject GetSpeakerTagAtIndex(int index)
        {
            BaseWebObject item = FindWebElement(string.Format("//div[@class=\"DraftEditor-root\"]/parent::div[1]" +
                "/following-sibling::div/div[{0}]/div[contains(@class,\"StyledName-\")]" +
                "//input[@class=\"ant-input ant-select-search__field\"]", index));

            // scroll to the element
            ScrollElementToView(item);

            return item;
        }

        /// <summary>
        /// Get tooltip element by speaker tag value
        /// </summary>
        /// <param name="speakerTagValue">speaker tag value</param>
        /// <returns>Object of the tooltip</returns>
        public BaseWebObject GetSpeakerTagTooltipByValue(string speakerTagValue)
        {
            return FindWebElement("//div[contains(@class,\"ant-tooltip\")]/div[@class=\"ant-tooltip-inner\" " +
                "and (text()=\"" + speakerTagValue + "\" or text()=\"" + speakerTagValue.ToUpper() + "\")]");
        }

        /// <summary>
        /// Get text of speaker tag
        /// </summary>
        /// <param name="index">Real index of the speaker tag in HTML DOM tree</param>
        /// <returns></returns>
        public string GetSpeakerTagValueAtIndex(int index)
        {
            try
            {
                BaseWebObject speakerTagObj = GetSpeakerTagAtIndex(index);

                if (!string.IsNullOrEmpty(speakerTagObj.Text))
                {
                    return speakerTagObj.Text;
                }
                else if (!string.IsNullOrEmpty(speakerTagObj.Value))
                {
                    return speakerTagObj.Value;
                }
            }
            catch
            {
                return "";
            }

            return "";
        }

        /// <summary>
        /// Get speaker tag edit icon at the given index
        /// </summary>
        /// <param name="index">Real index of the speaker tag edit icon in HTML DOM tree</param>
        /// <returns>Object of the speaker tag edit icon</returns>
        public BaseWebObject GetSpeakerTagEditIconAtIndex(int index)
        {
            return FindWebElement(string.Format("//div[@class=\"DraftEditor-root\"]/parent::div[1]" +
                "/following-sibling::div/div[{0}]/div[contains(@class,\"StyledName-\")]" +
                "/span[contains(@class,\"EditIcon-\")]", index));
        }

        /// <summary>
        /// Set text for speaker tag at index
        /// </summary>
        /// <param name="index">Real index of the speaker tag edit icon in HTML DOM tree<</param>
        /// <param name="text">text to update</param>
        public void SetTextSpeakerTagAtIndex(int index, string text)
        {
            BaseWebObject speakerTag = GetSpeakerTagAtIndex(index);

            speakerTag.Click();

            ActionMouseDoubleClick(speakerTag);

            if (string.IsNullOrEmpty(text))
            {
                TypeOnKeyboard(Keys.Delete);
            }
            else
            {
                speakerTag.SendKeys(text);
                TypeOnKeyboard(Keys.Enter);
            }

            GetParagraphAtIndex(index).Click();
        }

        /// <summary>
        /// Set caret for the text at given position
        /// </summary>
        /// <param name="paragraphIndex">html index of the paragraph</param>
        /// <param name="caretPos">position of the caret to set</param>
        public void SetCaretAtPositionInTextContentOfParagraph(int paragraphIndex, int caretPos)
        {
            SetCaretAtPositionInTextContent(GetParagraphAtIndex(paragraphIndex), caretPos);
        }

        /// <summary>
        /// Try to look up the Saving and Success Saved indicators
        /// </summary>
        public void WaitForTranscriptTextSaved()
        {
            // first the Saving status displayed
            BaseWebObject savingBox = DivSavingTextStatus;
            string savingBoxText = "Saving";
            string savedBoxText = "Last saved";

            if (savingBox != null && savingBox.Text.Contains(savingBoxText) && savingBox.IsVisible)
            {
                // find the success status
                BaseWebObject savedBox = DivSavedTextStatus;

                if (savedBox == null || !savedBox.Text.Contains(savedBoxText))
                {
                    throw new System.Exception("Transcript Text Saved: Saved message not found");
                }
            }
            else
            {
                throw new System.Exception("Transcript Text Saved: Saving message not found");
            }
        }

        /// <summary>
        /// Get saved datetime text
        /// </summary>
        /// <returns></returns>
        public string GetSavedDateTime()
        {
            string saveStatusHeader = "Last saved ";
            string saveStatusText = DivStatusIndicatorPanel.Text.Trim();

            int headerTextPos = saveStatusText.IndexOf(saveStatusHeader);

            if (headerTextPos >= 0)
            {
                return saveStatusText.Substring(saveStatusHeader.Length);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Delete all text in the current page
        /// </summary>
        public void DeleteAllTextInCurrentPage()
        {
            IList<BaseWebObject> ParagraphUIList = SpanParagraphs;

            bool isTextUpdated = false;

            if (ParagraphUIList.Count > 0)
            {
                ParagraphUIList[0].Click();
                ThreadUtils.SleepShortTime();

                TypeOnKeyboard(Keys.Control + "a");
                ThreadUtils.SleepShortTime();

                TypeOnKeyboard(Keys.Delete);

                isTextUpdated = true;
            }

            if (isTextUpdated)
            {
                WaitForTranscriptTextSaved();
            }
        }

        /// <summary>
        /// Insert a new word beside the current word
        /// </summary>
        /// <param name="insertWord"></param>
        /// <param name="paragraphUIIndex"></param>
        /// <param name="wordPosition"></param>
        /// <param name="insertAtHeadOfCurrentWord"></param>
        public void InsertWordToText(string insertWord, int paragraphUIIndex, int wordPosition,
            bool insertAtHeadOfCurrentWord = true)
        {
            if (insertAtHeadOfCurrentWord)
            {
                // insert the space bar first
                SetCaretAtPositionInTextContentOfParagraph(paragraphUIIndex, wordPosition);
                ThreadUtils.SleepShortTime();

                TypeOnKeyboard(TRANSCRIPT_WORD_SPACE_STR);

                // insert the word
                SetCaretAtPositionInTextContentOfParagraph(paragraphUIIndex, wordPosition);
                ThreadUtils.SleepShortTime();

                TypeOnKeyboard(insertWord);
            }
            else
            {
                SetCaretAtPositionInTextContentOfParagraph(paragraphUIIndex, wordPosition);
                ThreadUtils.SleepShortTime();

                // insert the word
                TypeOnKeyboard(TRANSCRIPT_WORD_SPACE_STR);
                TypeOnKeyboard(insertWord);
            }

            WaitForTranscriptTextSaved();
        }

        /// <summary>
        /// Type random text continuously within the given time
        /// </summary>
        /// <param name="paragraphUIIndex"></param>
        /// <param name="numberOfSeconds"></param>
        /// <returns></returns>
        public bool TypeTextAndVerifyAutoSaveEach60Seconds(int paragraphUIIndex, int numberOfSeconds = 60)
        {
            string startText = "Start typing text:";
            string firstSaveText = "";
            string secondSaveText = "";
            string thirdSaveText = "";

            // init the typing
            ActionMouseClick(DivTranscribedFirstParagraphText);
            ThreadUtils.SleepShortTime();
            TypeOnKeyboard(startText);
            ThreadUtils.SleepShortTime();

            // typing until time up
            for (int i = 1; i <= numberOfSeconds; i++)
            {
                string insertWord = RandomUtils.GetMilisecondString();

                TypeOnKeyboard(TRANSCRIPT_WORD_SPACE_STR);
                TypeOnKeyboard(insertWord);

                ThreadUtils.SleepShortTime();

                if (i % 5 == 0)
                {
                    string saveStatusText = GetSavedDateTime();

                    if (!string.IsNullOrEmpty(saveStatusText))
                    {
                        if (string.IsNullOrEmpty(firstSaveText))
                        {
                            firstSaveText = saveStatusText;
                        }
                        else if (string.IsNullOrEmpty(secondSaveText) &&
                            saveStatusText != firstSaveText)
                        {
                            secondSaveText = saveStatusText;

                            DateTime obj1 = StringUtils.ConvertStringToDateByFormat(
                                firstSaveText, StringUtils.TRANSCRIPT_SAVE_DATETIME_FORMAT_STR);
                            DateTime obj2 = StringUtils.ConvertStringToDateByFormat(
                                secondSaveText, StringUtils.TRANSCRIPT_SAVE_DATETIME_FORMAT_STR);

                            TestContext.Out.WriteLine("First saved status: {0}", obj1);
                            TestContext.Out.WriteLine("Second saved status: {0}", obj2);

                            if (obj2.Minute - obj1.Minute == 1)
                            {
                                return true;
                            }
                        }
                        else if (string.IsNullOrEmpty(thirdSaveText) &&
                            saveStatusText != firstSaveText &&
                            saveStatusText != secondSaveText)
                        {
                            thirdSaveText = saveStatusText;

                            DateTime obj1 = StringUtils.ConvertStringToDateByFormat(
                                secondSaveText, StringUtils.TRANSCRIPT_SAVE_DATETIME_FORMAT_STR);
                            DateTime obj2 = StringUtils.ConvertStringToDateByFormat(
                                thirdSaveText, StringUtils.TRANSCRIPT_SAVE_DATETIME_FORMAT_STR);

                            TestContext.Out.WriteLine("Third saved status: {0}", obj2);

                            if (obj2.Minute - obj1.Minute == 1)
                            {
                                return true;
                            }

                            break;
                        }
                    }
                }
            }

            return false;
        }
        #endregion
    }
}
