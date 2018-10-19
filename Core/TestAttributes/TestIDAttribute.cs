using Automation.UI.Core.DataProcessing;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Automation.UI.Core.TestAttributes
{
    /// <summary>
    /// Test ID attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TestIDAttribute : PropertyAttribute
    {
        #region Test Case Data Mapping constants
        private static readonly string TestCaseDataMappingFileName = ProjectConfigParams.GetConfigParamValue(
            ProjectConfigParams.CONF_KEY_TC_DATA_MAPPINGS_FILE_NAME);

        private const string TC_DATA_MAPPING_COL_ID = "test_id";
        private const string TC_DATA_MAPPING_COL_JIRA_ID = "test_jira_id";

        private static IList<Dictionary<string, string>> dataMappings = null;
        #endregion

        public const string PROPERTY_NAME = "TestID";

        public TestIDAttribute(string testcaseName) : base(testcaseName)
        {
            Properties.Set(PROPERTY_NAME, TestCaseID);
        }

        #region Methods
        /// <summary>
        /// Get test case ID from mapping file
        /// </summary>
        public string TestCaseID
        {
            get
            {
                if (dataMappings == null)
                {
                    dataMappings = DataProvider.GetDataMappingsFromCSVFile(TestCaseDataMappingFileName);
                }

                string testCaseName = (string)Properties.Get(PROPERTY_NAME);

                foreach (Dictionary<string, string> dataRow in dataMappings)
                {
                    if (testCaseName.Equals(dataRow[TC_DATA_MAPPING_COL_ID]))
                    {
                        return dataRow[TC_DATA_MAPPING_COL_JIRA_ID];
                    }
                }

                return string.Empty;
            }
        }
        #endregion
    }
}
