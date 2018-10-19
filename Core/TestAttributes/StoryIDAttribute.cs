using Automation.UI.Core.DataProcessing;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Automation.UI.Core.TestAttributes
{
    /// <summary>
    /// Story ID attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class StoryIDAttribute : PropertyAttribute
    {
        #region Story Data Mapping constants
        private static readonly string StoryDataMappingFileName = ProjectConfigParams.GetConfigParamValue(
            ProjectConfigParams.CONF_KEY_SR_DATA_MAPPINGS_FILE_NAME);

        private const string SR_DATA_MAPPING_COL_ID = "story_id";
        private const string SR_DATA_MAPPING_COL_JIRA_ID = "story_jira_id";

        private static IList<Dictionary<string, string>> dataMappings = null;
        #endregion

        public const string PROPERTY_NAME = "StoryID";

        public StoryIDAttribute(string storyName) : base(storyName)
        {
            Properties.Set(PROPERTY_NAME, StoryID);
        }

        #region Methods
        /// <summary>
        /// Get Story ID from mapping file
        /// </summary>
        public string StoryID
        {
            get
            {
                if (dataMappings == null)
                {
                    dataMappings = DataProvider.GetDataMappingsFromCSVFile(StoryDataMappingFileName);
                }

                string storyName = (string)Properties.Get(PROPERTY_NAME);

                foreach (Dictionary<string, string> dataRow in dataMappings)
                {
                    if (storyName.Equals(dataRow[SR_DATA_MAPPING_COL_ID]))
                    {
                        return dataRow[SR_DATA_MAPPING_COL_JIRA_ID];
                    }
                }

                return string.Empty;
            }
        }
        #endregion
    }
}
