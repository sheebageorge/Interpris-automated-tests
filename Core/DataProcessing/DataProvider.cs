using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Automation.UI.Core.DataProcessing
{
    /// <summary>
    /// Data provider class
    /// It reads Data Mapping File to map the test case by defined ID with the given data file
    /// Then, it returns a list of dictionaries of strings (key-value) for the test case
    /// </summary>
    public class DataProvider
    {
        private static readonly string DataMappingFileName = ProjectConfigParams.GetConfigParamValue(
            ProjectConfigParams.CONF_KEY_DATA_MAPPINGS_FILE_NAME);

        private static readonly string DataFolder = Path.Combine(
            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
            ProjectConfigParams.GetConfigParamValue(ProjectConfigParams.CONF_KEY_DATA_FOLDER_NAME));

        private static IList<DataMappingsObject> DataMappingsObjectList = null;
        private static Dictionary<string, string> commonDataDict = null;

        private const string COMMON_DATA_PREFIX = "@Common";
        private const string COMMON_DATA_REGEX_PATTERN = @"@Common\[(.+?)\]";


        /// <summary>
        /// Get data mapping from a CSV file
        /// </summary>
        /// <param name="dataMappingFileName"></param>
        /// <returns>List of dictionaries of data</returns>
        public static IList<Dictionary<string, string>> GetDataMappingsFromCSVFile(string dataMappingFileName)
        {
            return new CSVFileUtils().GetDataFromCSVFile(Path.Combine(DataFolder, dataMappingFileName));
        }

        /// <summary>
        /// Prepare data for test case by defined ID
        /// </summary>
        /// <param name="testCaseID">Test Case ID</param>
        /// <returns>List of dictionaries of strings</returns>
        public static IEnumerable<Dictionary<string, string>> PrepareTestCases(string testID)
        {
            // Check if the Data Mapping List has been loaded or not to load it from the given sources
            if (DataMappingsObjectList == null)
            {
                string targetEnvironmentDataFilePathColName = DataMappingsObject.DATA_MAPPING_COL_DATA_FILE_PATH +
                    "_" + Environment.GetEnvironmentVariable(ProjectConfigParams.SUPPORTED_ENV_NAME).ToLower();

                IList<Dictionary<string, string>> dataMappings = GetDataMappingsFromCSVFile(DataMappingFileName);

                DataMappingsObjectList = new List<DataMappingsObject>();

                foreach (Dictionary<string, string> mappingLine in dataMappings)
                {
                    DataMappingsObjectList.Add(new DataMappingsObject(
                        mappingLine[DataMappingsObject.DATA_MAPPING_COL_TEST_ID],
                        mappingLine[DataMappingsObject.DATA_MAPPING_COL_DATA_TYPE],
                        mappingLine[DataMappingsObject.DATA_MAPPING_COL_DATA_FILE_PATH],
                        mappingLine[DataMappingsObject.DATA_MAPPING_COL_EXCEL_DATA_SHEETNAME]));
                }
            }

            IList<Dictionary<string, string>> dataSet = null;

            // Find the test case ID in the mapping list
            DataMappingsObject item = DataMappingsObjectList.FirstOrDefault(o => o.TestID == testID);

            // Check if the Common Data has been loaded or not to load it from the given sources
            if (commonDataDict == null && item.DataType == DataMappingsObject.DATA_TYPE_CSV)
            {
                string commonDataDictFilePath = Path.Combine(
                    Environment.GetEnvironmentVariable(ProjectConfigParams.SUPPORTED_ENV_NAME),
                    ProjectConfigParams.GetConfigParamValue(ProjectConfigParams.CONF_KEY_COMMON_DATA_MAPPING_FILE_NAME));

                commonDataDict = GetDataMappingsFromCSVFile(commonDataDictFilePath).Last();
            }

            if (item != null)
            {
                // get data for the test case
                switch (item.DataType)
                {
                    case DataMappingsObject.DATA_TYPE_CSV:
                        dataSet = new CSVFileUtils().GetDataFromCSVFile(Path.Combine(DataFolder,
                            Environment.GetEnvironmentVariable(ProjectConfigParams.SUPPORTED_ENV_NAME), item.DataFilePath));
                        break;

                    case DataMappingsObject.DATA_TYPE_EXCEL:
                        dataSet = new ExcelFileUtils().GetDataFromExcelFile(Path.Combine(DataFolder,
                            Environment.GetEnvironmentVariable(ProjectConfigParams.SUPPORTED_ENV_NAME), item.DataFilePath), item.ExcelDataSheetName);
                        break;

                    default:
                        break;
                }
            }
            else
            {
                throw new Exception(string.Format("Test Case {0} - Data not found!", testID));
            }

            if (dataSet != null)
            {
                switch (item.DataType)
                {
                    case DataMappingsObject.DATA_TYPE_CSV:
                        // find if test data contains common data and replace with common data
                        foreach (Dictionary<string, string> data in dataSet)
                        {
                            foreach (string key in data.Keys.ToList())
                            {
                                if (data[key].Contains(COMMON_DATA_PREFIX))
                                {
                                    string defaultValue = data[key];
                                    try
                                    {
                                        string commonDataKey = Regex.Match(defaultValue, COMMON_DATA_REGEX_PATTERN).Groups[1].Value;
                                        data[key] = commonDataDict[commonDataKey];
                                    }
                                    catch (Exception)
                                    {
                                        data[key] = defaultValue;
                                    }
                                }
                            }
                        }
                        break;

                    case DataMappingsObject.DATA_TYPE_EXCEL:
                        dataSet = new ExcelFileUtils().GetDataFromExcelFile(Path.Combine(DataFolder,
                            Environment.GetEnvironmentVariable(ProjectConfigParams.SUPPORTED_ENV_NAME), item.DataFilePath), item.ExcelDataSheetName);
                        break;

                    default:
                        break;
                }

                foreach (Dictionary<string, string> data in dataSet)
                {
                    yield return data;
                }
            }
        }
    }
}
