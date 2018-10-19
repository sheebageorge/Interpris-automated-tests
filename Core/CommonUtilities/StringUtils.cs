using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Automation.UI.Core.CommonUtilities
{
    public class StringUtils
    {
        public const string NUMBER_FILTER_REGEX = @"[^0-9\.]+";

        // the current format should be "5:48 pm 09/04/18"
        public const string TRANSCRIPT_SAVE_DATETIME_FORMAT_STR = "h:mm tt MM/dd/yy";

        public const char TIMESTAMP_DELIMITER_CHAR = ':';

        /// <summary>
        /// Get all indexes of the substring in the big string
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns>List of indexes of the substring in the big string</returns>
        public static List<int> AllIndexesOf(string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");

            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }

        /// <summary>
        /// Count word with loop and character tests.
        /// </summary>
        public static int WordCount(string str)
        {
            return str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Count();
        }

        /// <summary>
        /// Convert a string to datetime by format
        /// </summary>
        /// <param name="strDateTime"></param>
        /// <param name="strDateTimeFormat"></param>
        /// <returns>DateTime object</returns>
        public static DateTime ConvertStringToDateByFormat(string strDateTime, string strDateTimeFormat)
        {
            return DateTime.ParseExact(strDateTime, strDateTimeFormat, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Convert mmm:SS to double value
        /// </summary>
        /// <param name="strTimeStamp"></param>
        /// <returns></returns>
        public static double ConvertTimeStampToDouble(string strTimeStamp)
        {
            int countDeli = strTimeStamp.Count(x => x == TIMESTAMP_DELIMITER_CHAR);

            if (countDeli == 1)
            {
                int colonPos = strTimeStamp.IndexOf(TIMESTAMP_DELIMITER_CHAR);

                return double.Parse(strTimeStamp.Substring(0, colonPos)) * 60 +
                    double.Parse(strTimeStamp.Substring(colonPos + 1));
            }
            else if (countDeli == 2)
            {
                int colonPos1 = strTimeStamp.IndexOf(TIMESTAMP_DELIMITER_CHAR);
                int colonPos2 = strTimeStamp.IndexOf(TIMESTAMP_DELIMITER_CHAR, colonPos1 + 1);

                string hourStr = strTimeStamp.Substring(0, colonPos1);
                string minStr = strTimeStamp.Substring(colonPos1 + 1, colonPos2 - colonPos1 - 1);
                string secondStr = strTimeStamp.Substring(colonPos2 + 1);

                return double.Parse(hourStr) * 60 * 60 +
                    double.Parse(minStr) * 60 +
                    double.Parse(secondStr);
            }

            return double.Parse(strTimeStamp);
        }

        /// <summary>
        /// Filter int value from string 
        /// </summary>
        /// <param name="input">string that contains int</param>
        /// <returns>int value filtered from input</returns>
        public static int FilterIntFromString(string input)
        {
            try
            {
                string text = Regex.Split(input, NUMBER_FILTER_REGEX).Where(c => c != "." && c.Trim() != "").First();
                int.TryParse(text, out int value);
                return value;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Filter float value from string 
        /// </summary>
        /// <param name="input">string that contains int</param>
        /// <returns>float value filtered from input</returns>
        public static float FilterFloatFromString(string input)
        {
            try
            {
                string text = Regex.Split(input, NUMBER_FILTER_REGEX).Where(c => c != "." && c.Trim() != "").First();
                float.TryParse(text, out float value);
                return value;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Filter int value from string 
        /// </summary>
        /// <param name="input">string that contains int</param>
        /// <returns>int value filtered from input</returns>
        public static IList<int> FilterIntsFromString(string input)
        {
            try
            {
                return Regex.Split(input, NUMBER_FILTER_REGEX).Where(c => c != "." && c.Trim() != "").Select(int.Parse).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Filter float value from string 
        /// </summary>
        /// <param name="input">string that contains int</param>
        /// <returns>float value filtered from input</returns>
        public static IList<float> FilterFloatsFromString(string input)
        {
            try
            {
                return Regex.Split(input, NUMBER_FILTER_REGEX).Where(c => c != "." && c.Trim() != "").Select(float.Parse).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
