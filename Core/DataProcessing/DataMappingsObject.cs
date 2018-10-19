
namespace Automation.UI.Core.DataProcessing
{
    /// <summary>
    /// Wrapper class of Data Mapping File
    /// </summary>
    public class DataMappingsObject
    {
        public const string DATA_TYPE_CSV = "CSV";
        public const string DATA_TYPE_EXCEL = "Excel";

        public const string DATA_MAPPING_COL_TEST_ID = "test_id";
        public const string DATA_MAPPING_COL_DATA_TYPE = "data_type";
        public const string DATA_MAPPING_COL_DATA_FILE_PATH = "data_file_path";
        public const string DATA_MAPPING_COL_EXCEL_DATA_SHEETNAME = "excel_data_sheet_name";

        public DataMappingsObject(string testID, string dataType,
            string dataFilePath, string excelDataSheetName)
        {
            TestID = testID;
            DataType = dataType;
            DataFilePath = dataFilePath;
            ExcelDataSheetName = excelDataSheetName;
        }

        #region Properties
        public string TestID { get; }
        public string DataType { get; }
        public string DataFilePath { get; }
        public string ExcelDataSheetName { get; }
        #endregion
    }
}
