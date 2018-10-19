using System;
using System.Linq;

namespace Automation.UI.Core.CommonUtilities
{
    /// <summary>
    /// This class provides random string or number
    /// </summary>
    public class RandomUtils
    {
        public const string ALPHABET_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static Random random = new Random();

        /// <summary>
        /// Get miliseconds at the current time
        /// </summary>
        /// <returns>String of miliseconds</returns>
        public static string GetMilisecondString()
        {
            return string.Format("{0}", DateTimeOffset.Now.ToUnixTimeMilliseconds());
        }

        /// <summary>
        /// Get random integer number in range
        /// </summary>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <returns>random int</returns>
        public static int GetIntNumberInRange(int min, int max)
        {
            return random.Next(min, max); //for ints
        }

        /// <summary>
        /// Get Random email address
        /// </summary>
        /// <param name="username_head">Head string of email name</param>
        /// <param name="emailDomainName">Email domain to create the email address</param>
        /// <returns>A random email address</returns>
        public static string GetRandomEmailAddress(string username_head, string emailDomainName)
        {
            return string.Format("{0}_{1}{2}", username_head, RandomUtils.GetMilisecondString(), emailDomainName);
        }

        /// <summary>
        /// Generate a random alphabet string with given length
        /// </summary>
        /// <param name="length"></param>
        /// <returns>The random string</returns>
        public static string GetRandomAlphabetString(int length)
        {
            return new string(Enumerable.Repeat(ALPHABET_CHARS, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Get a random char from a string
        /// </summary>
        /// <param name="str"></param>
        /// <returns>The random char</returns>
        public static char GetRandomCharInString(string str)
        {
            int index = random.Next(str.Length);
            return str[index];
        }
    }
}
