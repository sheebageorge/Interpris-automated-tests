using NUnit.Framework;
using System;

namespace Automation.UI.Core.TestAttributes
{
    /// <summary>
    /// Priority level of test cases
    /// There are 5 levels now: Highest, High, Medium, Low, Lowest
    /// </summary>
    public class PriorityLevel
    {
        public const string Highest = "Highest";
        public const string High = "High";
        public const string Medium = "Medium";
        public const string Low = "Low";
        public const string Lowest = "Lowest";
    }

    /// <summary>
    /// Priority attribute of test cases
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PriorityAttribute : PropertyAttribute
    {
        public PriorityAttribute(string level) : base(level) { }
    }
}
