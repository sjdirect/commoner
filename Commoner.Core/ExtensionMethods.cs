
using System;
namespace Commoner.Core
{
    public static class ExtensionMethods
    {
        static IDataValidator _dataValidator;
        static IDataCopier _dataCopier;

        public static IDataValidator DataValidator 
        {
            get
            {
                if (_dataValidator == null)
                    _dataValidator = new DataValidator();

                return _dataValidator;
            }
            set
            {
                _dataValidator = value;
            }
        }

        public static IDataCopier DataCopier
        {
            get
            {
                if (_dataCopier == null)
                    _dataCopier = new DataCopier();

                return _dataCopier;
            }
            set
            {
                _dataCopier = value;
            }
        }

        /// <summary>
        /// Copies all property values where the names match
        /// </summary>
        public static void CopyPropertyValues(this object from, object to)
        {
            DataCopier.CopyProperties(from, to);
        }

        #region string

        /// <summary>
        /// Truncates the string to the length provided and add "..." to the end
        /// </summary>
        public static string TruncateForDisplay(this string value, int maxLength)
        {
            string valueToReturn = value;
            if (value.Length > maxLength)
            {
                valueToReturn = valueToReturn.Substring(0, maxLength);
                valueToReturn += "...";
            }
            return valueToReturn;
        }

        public static bool IsValidAlphaNumericString(this string value)
        {
           return IsValidAlphaNumericString(value, false);
        }

        public static bool IsValidAlphaNumericString(this string value, bool allowSpaces)
        {
            return DataValidator.IsValidAlphaNumericString(value, allowSpaces);
        }

        public static bool IsValidAlphaString(this string value)
        {
            return IsValidAlphaString(value, false);
        }

        public static bool IsValidAlphaString(this string value, bool allowSpaces)
        {
            return DataValidator.IsValidAlphaString(value, allowSpaces);
        }

        public static bool IsValidEmailAddress(this string value)
        {
            return DataValidator.IsValidEmailAddress(value);
        }

        public static bool IsValidHexString(this string value)
        {
            return DataValidator.IsValidHexString(value);
        }

        public static bool IsValidNumericString(this string value)
        {
            return IsValidNumericString(value, false);
        }

        public static bool IsValidNumericString(this string value, bool allowSpaces)
        {
            return DataValidator.IsValidNumericString(value, allowSpaces);
        }        

        #endregion

        #region DateTime

        public static bool IsAfter(this DateTime startDate, DateTime endDate)
        {
            return IsAfter(startDate, endDate, false);
        }

        public static bool IsAfter(this DateTime startDate, DateTime endDate, bool checkTime)
        {
            return _dataValidator.IsEndDateAfterStartDate(startDate, endDate, checkTime);
        }

        #endregion

    }
}
