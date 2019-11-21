using System;

namespace EDS.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Checks that the given input both starts and ends with the given pattern.
        /// </summary>
        /// <param name="input">the string to examine</param>
        /// <param name="pattern">the pattern to search for</param>
        /// <returns>Returns true if the string both starts and ends with the given pattern</returns>
        public static bool IsSurroundedBy(this string input, string pattern)
        {
            return input.IsSurroundedBy(pattern, pattern);
        }

        /// <summary>
        /// Checks that the given string starts with the given start pattern and ends with the given end pattern.
        /// </summary>
        /// <param name="input">the string to examine.</param>
        /// <param name="leadingPattern">the pattern to search at the beginning of the string</param>
        /// <param name="trailingPattern">the pattern to search at the end of the string</param>
        /// <returns>Returns true if the string starts with the leading pattern and ends with the trailing pattern</returns>
        public static bool IsSurroundedBy(this string input, string leadingPattern, string trailingPattern)
        {
            var isSurroundedBy = (input.StartsWith(leadingPattern) && input.EndsWith(trailingPattern));

            return isSurroundedBy;
        }

        public static string TrimFirstAndLast(this string input, string leadingPattern, string trailingPattern)
        {
            var trimmedString = input;

            //trim the head
            if (input.StartsWith(leadingPattern))
            {
                trimmedString = trimmedString.TrimFirstIndexOf(leadingPattern);
            }

            if (input.EndsWith(trailingPattern))
            {
                trimmedString = trimmedString.TrimLastIndexOf(trailingPattern);
            }

            //trim the tail

            return trimmedString;
        }

        public static string TrimFirstIndexOf(this string input, string pattern)
        {
            var trimmedString = input;

            var firstIndex = input.IndexOf(pattern);

            if (firstIndex >= 0)
            {
                trimmedString = input.Substring(++firstIndex);
            }

            return trimmedString;
        }

        public static string TrimLastIndexOf(this string input, string pattern)
        {
            var trimmedString = input;

            var lastIndexOf = input.LastIndexOf(pattern);

            if (lastIndexOf >= 0)
            {
                trimmedString = input.Substring(0, lastIndexOf);
            }

            return trimmedString;
        }

        public static bool IsValidPath(this string path)
        {
            var isValidPath = false;

            if (!string.IsNullOrWhiteSpace(path))
            {

            }

            return isValidPath;
        }
    }
}
