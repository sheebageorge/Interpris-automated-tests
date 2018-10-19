using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

namespace Automation.UI.Core.DataProcessing
{
    /// <summary>
    /// This class provides methods to process Excel files
    /// </summary>
    public class ExcelFileUtils
    {
        private const int EXCEL_SHEET_HEADER_ROW_IDX = 1;
        private const int MAX_EXCEL_READ_ROW = 10000;
        private const int MAX_EXCEL_READ_COL = 10000;

        /// <summary>
        /// Read Excel file and return lists of dictionary of strings
        /// </summary>
        /// <example>
        /// <code>
        /// Excel File:
        /// username   password
        /// abc123     pass1234@X
        /// xyz789     ABCpass@123
        /// List of Dictionaries of Strings:
        /// [{'username': 'abc123', 'password': 'pass1234@X'},
        ///  {'username': 'xyz789', 'password': 'ABCpass@123'}]
        /// </code>
        /// </example>
        /// <param name="dataDrivenFilePath">Path to the Excel file</param>
        /// <param name="dataDrivenExcelSheetName">Excel sheet name to get data</param>
        /// <returns>Lists of dictionary of strings</returns>
        public IList<Dictionary<string, string>> GetDataFromExcelFile(
            string dataDrivenFilePath, string dataDrivenExcelSheetName)
        {
            IList<Dictionary<string, string>> dataList = new List<Dictionary<string, string>>();

            FileInfo existingFile = new FileInfo(dataDrivenFilePath);

            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                // get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[dataDrivenExcelSheetName];

                int curCol = 1;

                List<string> headers = new List<string>();

                // Get headers
                while (curCol < MAX_EXCEL_READ_COL)
                {
                    var colVal = worksheet.Cells[EXCEL_SHEET_HEADER_ROW_IDX, curCol].Value;

                    if (colVal != null && colVal.GetType() != typeof(string))
                    {
                        colVal = colVal.ToString();
                    }

                    if (colVal == null || string.IsNullOrEmpty((string)colVal))
                    {
                        break;
                    }

                    headers.Add((string)colVal);

                    curCol++;
                }

                int curRow = 2;
                int maxColCount = headers.Count;

                // Get contents
                while (curRow < MAX_EXCEL_READ_ROW)
                {
                    Dictionary<string, string> dictRow = new Dictionary<string, string>();

                    bool isNotEmpty = false;

                    // read row until reaching the maximum of line or getting the first empty row
                    for (int j = 1; j <= maxColCount; j++)
                    {
                        var colVal = worksheet.Cells[curRow, j].Value;

                        if (colVal != null && colVal.GetType() != typeof(string))
                        {
                            colVal = colVal.ToString();
                        }

                        if (colVal != null && !string.IsNullOrEmpty((string)colVal))
                        {
                            dictRow.Add(headers[j - 1], (string)colVal);

                            isNotEmpty = true;
                        }
                        else
                        {
                            dictRow.Add(headers[j - 1], "");
                        }
                    }

                    if (!isNotEmpty)
                        break;

                    dataList.Add(dictRow);

                    curRow++;
                }
            }

            return dataList;
        }
    }
}
