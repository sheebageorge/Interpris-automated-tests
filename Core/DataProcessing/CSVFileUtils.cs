using System.Collections.Generic;
using System.IO;

namespace Automation.UI.Core.DataProcessing
{
    /// <summary>
    /// This class provides methods to process CSV files
    /// </summary>
    public class CSVFileUtils
    {
        /// <summary>
        /// Read CSV file and return lists of dictionary of strings
        /// </summary>
        /// <example>
        /// <code>
        /// CSV File:
        /// username,password
        /// abc123,pass1234@X
        /// xyz789,ABCpass@123
        /// List of Dictionaries of Strings:
        /// [{'username': 'abc123', 'password': 'pass1234@X'},
        ///  {'username': 'xyz789', 'password': 'ABCpass@123'}]
        /// </code>
        /// </example>
        /// <param name="dataDrivenFilePath">Path to the CSV file</param>
        /// <returns>Lists of dictionary of strings</returns>
        public IList<Dictionary<string, string>> GetDataFromCSVFile(string dataDrivenFilePath)
        {
            IList<Dictionary<string, string>> dataList = new List<Dictionary<string, string>>();

            using (StreamReader reader = File.OpenText(dataDrivenFilePath))
            {
                CsvHelper.CsvReader csv = new CsvHelper.CsvReader(reader);

                csv.Read();
                csv.ReadHeader();

                string value;

                List<string> headers = new List<string>();
                for (int i = 0; csv.TryGetField(i, out value); i++)
                {
                    headers.Add(value);
                }

                while (csv.Read())
                {
                    Dictionary<string, string> dictRow = new Dictionary<string, string>();

                    for (int i = 0; i < headers.Count; i++)
                    {
                        if (csv.TryGetField(i, out value))
                        {
                            dictRow.Add(headers[i], value);
                        }
                        else
                        {
                            dictRow.Add(headers[i], "");
                        }
                    }

                    dataList.Add(dictRow);
                }
            }

            return dataList;
        }
    }
}
