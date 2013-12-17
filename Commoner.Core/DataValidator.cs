using System;
using System.Text.RegularExpressions;

namespace Commoner.Core
{
    public interface IDataValidator
    {
        bool IsEndDateAfterStartDate(DateTime endDate, DateTime startDate);
        bool IsEndDateAfterStartDate(DateTime endDate, DateTime startDate, bool checkTime);
        bool IsValidAlphaNumericString(string value);
        bool IsValidAlphaNumericString(string value, bool allowSpaces);
        bool IsValidAlphaString(string value);
        bool IsValidAlphaString(string value, bool allowSpaces);
        bool IsValidEmailAddress(string value);
        bool IsValidHexString(string value);
        bool IsValidNumericString(string value);
        bool IsValidNumericString(string value, bool allowSpaces);
    }

    /// <summary>
    /// A helper class for validating formats.
    /// </summary>
    public class DataValidator : IDataValidator 
    {
        private const bool _defaultIsAllowSpaces = false;
        private const bool _defaultCheckTime = true;

        /// <summary>
        /// Checks if the input is a valid alpha numeric string. Allows spaces and special characters.
        /// </summary>
        public bool IsValidAlphaNumericString(string value)
        {
            return IsValidAlphaNumericString(value, _defaultIsAllowSpaces);
        }

        /// <summary>
        /// Checks if the input is a valid alpha numeric string.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="allowSpaces">Whether to allow spaces.</param>
        public bool IsValidAlphaNumericString(string value, bool allowSpaces)
        {
            string regexPattern = string.Empty;
            if (allowSpaces)
            {
                regexPattern = @"^[A-Za-z0-9 ]*$";  //Any combonation of letters, numbers, and spaces
            }
            else
            {
                regexPattern = @"^[A-Za-z0-9]*$";   //Only letters and numbers
            }
            return IsRegexMatch(value, regexPattern);
        }

        /// <summary>
        /// Checks if the input is a valid alpha string. Allows spaces and special characters.
        /// </summary>
        public bool IsValidAlphaString(string value)
        {
            return IsValidAlphaString(value, _defaultIsAllowSpaces);
        }

        /// <summary>
        /// Checks if the input is a valid alpha string.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="allowSpaces">Whether to allow spaces.</param>
        public bool IsValidAlphaString(string value, bool allowSpaces)
        {
            string regexPattern = string.Empty;
            if (allowSpaces)
            {
                regexPattern = @"^[A-Za-z ]*$"; //Any combination of letters and spaces
            }
            else
            {
                regexPattern = @"^[A-Za-z]*$";  //Only letters
            }
            return IsRegexMatch(value, regexPattern);
        }

        /// <summary>
        /// Checks if the input is a valid email address.
        /// </summary>
        /// <param name="emailAddress">An e-mail address.</param>
        /// <returns>Returns a value that indicates whether the provided e-mail address is in a valid format</returns>
        public bool IsValidEmailAddress(string value)
        {
            return IsRegexMatch(value, @"^((?>[a-zA-Z\d!#$%&'*+\-/=?^_`{|}~]+\x20*|""((?=[\x01-\x7f])[^""\\]|\\[\x01-\x7f])*""\x20*)*(?<angle><))?((?!\.)(?>\.?[a-zA-Z\d!#$%&'*+\-/=?^_`{|}~]+)+|""((?=[\x01-\x7f])[^""\\]|\\[\x01-\x7f])*"")@(((?!-)[a-zA-Z\d\-]+(?<!-)\.)+[a-zA-Z]{2,}|\[(((?(?<!\[)\.)(25[0-5]|2[0-4]\d|[01]?\d?\d)){4}|[a-zA-Z\d\-]*[a-zA-Z\d]:((?=[\x01-\x7f])[^\\\[\]]|\\[\x01-\x7f])+)\])(?(angle)>)$");
        }

        /// <summary>
        /// Checks if the input is a valid hexadecimal string with no spaces or special chars. (0-9,A-F,a-f)
        /// </summary>
        public bool IsValidHexString(string value)
        {
            return IsRegexMatch(value, @"^[A-Fa-f0-9]*$");
        }

        /// <summary>
        /// Checks if the input is a valid numeric string with no spaces or special chars. (0-9 only)
        /// </summary>
        public bool IsValidNumericString(string value)
        {
            return IsValidNumericString(value, _defaultIsAllowSpaces);
        }

        /// <summary>
        /// Checks if the input is a valid numeric string with spaces but no special chars. (0-9 and spaces only)
        /// </summary>
        public bool IsValidNumericString(string value, bool allowSpaces)
        {
            string regexPattern = string.Empty;
            if (allowSpaces)
            {
                regexPattern = @"^[0-9 ]*$";  //Any combination of numbers and spaces
            }
            else
            {
                regexPattern = @"^[0-9]*$";   //Only numbers
            }
            return IsRegexMatch(value, regexPattern);
        }

        /// <summary>
        /// Checks if the end date comes after (or is equal to) start date, takes the time (hh:mm:ss) into account when making decision.
        /// </summary>
        public bool IsEndDateAfterStartDate(DateTime endDate, DateTime startDate)
        {
            return IsEndDateAfterStartDate(endDate, startDate, _defaultCheckTime);
        }

        /// <summary>
        /// Checks if the end date comes after (or is equal to) start date, optionally takes the time (hh:mm:ss) into account when making decision by the last boolean argument.
        /// </summary>
        public bool IsEndDateAfterStartDate(DateTime endDate, DateTime startDate, bool checkTime)
        {
            bool isEndDateAfterStartDate = false;

            if (checkTime)
                isEndDateAfterStartDate = (endDate >= startDate);
            else
                isEndDateAfterStartDate = (endDate.Date >= startDate.Date);

            return isEndDateAfterStartDate;
        }

        private bool IsRegexMatch(string value, string regexExpression)
        {
            bool isRegexMatch = false;
            if (!string.IsNullOrEmpty(value))
            {
                Regex regexPattern = new Regex(regexExpression);
                isRegexMatch = regexPattern.IsMatch(value);
            }
            return isRegexMatch;
        }
    }
}
