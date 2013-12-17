using System;
using Commoner.Core;
using NUnit.Framework;

namespace Common.Test.Unit.Core
{
    [TestFixture]
    public class DataValidatorTests
    {
        DataValidator _unitUnderTest = new DataValidator();
        string _alpha;
        string _alphaWithSpaces;
        string _numeric;
        string _numericWithSpaces;
        string _alphaNumeric;
        string _alphaNumericWithSpaces;
        string _specialCharacters;
        string _specialCharactersWithSpaces;

        [SetUp]
        public void SetUp()
        {
            _alpha = "ABCXYZabcxyz";
            _alphaWithSpaces = _alpha + " ";

            _numeric = "123";
            _numericWithSpaces = _numeric + " ";

            _alphaNumeric = _alpha + _numeric;
            _alphaNumericWithSpaces = _alphaNumeric + " ";

            _specialCharacters = "€Ω⅓“”‘’↑Áíéá≥";
            _specialCharactersWithSpaces = _specialCharacters + " ";
        }

        [Test]
        public void IsValidAlphaString_AlphaString_IsRecognized()
        {
            Assert.IsTrue(_unitUnderTest.IsValidAlphaString(_alpha));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaString(_alphaWithSpaces));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaString(_numeric));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaString(_numericWithSpaces));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaString(_alphaNumeric));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaString(_alphaNumericWithSpaces));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaString(_specialCharacters));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaString(_specialCharactersWithSpaces));
        }

        [Test]
        public void IsValidAlphaString_AlphaStringWithStaces_IsRecognized()
        {
            Assert.IsTrue(_unitUnderTest.IsValidAlphaString(_alpha, true));
            Assert.IsTrue(_unitUnderTest.IsValidAlphaString(_alphaWithSpaces, true));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaString(_numeric, true));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaString(_numericWithSpaces, true));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaString(_alphaNumeric, true));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaString(_alphaNumericWithSpaces, true));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaString(_specialCharacters, true));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaString(_specialCharactersWithSpaces, true));
        }

        [Test]
        public void IsValidAlphaString_NullOrEmptyString_ReturnsFalse()
        {
            Assert.IsFalse(_unitUnderTest.IsValidAlphaString(null));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaString(null, true));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaString(""));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaString("", true));
        }

        [Test]
        public void IsValidNumericString_NumericString_IsRecognized()
        {
            Assert.IsTrue(_unitUnderTest.IsValidNumericString(_numeric));
            Assert.IsFalse(_unitUnderTest.IsValidNumericString(_numericWithSpaces));
            Assert.IsFalse(_unitUnderTest.IsValidNumericString(_alpha));
            Assert.IsFalse(_unitUnderTest.IsValidNumericString(_alphaWithSpaces));
            Assert.IsFalse(_unitUnderTest.IsValidNumericString(_alphaNumeric));
            Assert.IsFalse(_unitUnderTest.IsValidNumericString(_alphaNumericWithSpaces));
            Assert.IsFalse(_unitUnderTest.IsValidNumericString(_specialCharacters));
            Assert.IsFalse(_unitUnderTest.IsValidNumericString(_specialCharactersWithSpaces));
        }

        [Test]
        public void IsValidNumericString_NumericStringWithStaces_IsRecognized()
        {
            Assert.IsTrue(_unitUnderTest.IsValidNumericString(_numeric, true));
            Assert.IsTrue(_unitUnderTest.IsValidNumericString(_numericWithSpaces, true));
            Assert.IsFalse(_unitUnderTest.IsValidNumericString(_alpha, true));
            Assert.IsFalse(_unitUnderTest.IsValidNumericString(_alphaWithSpaces, true));
            Assert.IsFalse(_unitUnderTest.IsValidNumericString(_alphaNumeric, true));
            Assert.IsFalse(_unitUnderTest.IsValidNumericString(_alphaNumericWithSpaces, true));
            Assert.IsFalse(_unitUnderTest.IsValidNumericString(_specialCharacters, true));
            Assert.IsFalse(_unitUnderTest.IsValidNumericString(_specialCharactersWithSpaces, true));
        }

        [Test]
        public void IsValidNumericString_NullOrEmptyString_ReturnsFalse()
        {
            Assert.IsFalse(_unitUnderTest.IsValidNumericString(null));
            Assert.IsFalse(_unitUnderTest.IsValidNumericString(null, true));
            Assert.IsFalse(_unitUnderTest.IsValidNumericString(""));
            Assert.IsFalse(_unitUnderTest.IsValidNumericString("", true));
        }

        [Test]
        public void IsValidAlphaNumericString_AlphaNumericString_IsRecognized()
        {
            Assert.IsTrue(_unitUnderTest.IsValidAlphaNumericString(_alphaNumeric));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaNumericString(_alphaNumericWithSpaces));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaNumericString(_specialCharacters));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaNumericString(_specialCharactersWithSpaces));
        }

        [Test]
        public void IsValidAlphaNumericString_AlphaNumericStringWithStaces_IsRecognized()
        {
            Assert.IsTrue(_unitUnderTest.IsValidAlphaNumericString(_alphaNumeric, true));
            Assert.IsTrue(_unitUnderTest.IsValidAlphaNumericString(_alphaNumericWithSpaces, true));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaNumericString(_specialCharacters, true));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaNumericString(_specialCharactersWithSpaces, true));
        }

        [Test]
        public void IsValidAlphaNumericString_NullOrEmptyString_ReturnsFalse()
        {
            Assert.IsFalse(_unitUnderTest.IsValidAlphaNumericString(null));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaNumericString(null, true));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaNumericString(""));
            Assert.IsFalse(_unitUnderTest.IsValidAlphaNumericString("", true));
        }

        [Test]
        public void IsValidEmailAddress_ValidEmail_IsRecognized()
        {
            Assert.IsTrue(_unitUnderTest.IsValidEmailAddress("skd@yahoo.com"));
            Assert.IsTrue(_unitUnderTest.IsValidEmailAddress("d@s.uk"));
            Assert.IsTrue(_unitUnderTest.IsValidEmailAddress("jksdajf3kj3@gmail.com"));
            Assert.IsTrue(_unitUnderTest.IsValidEmailAddress("3245ksjefkjas@htom.net"));
            Assert.IsTrue(_unitUnderTest.IsValidEmailAddress("_3245ksjefkjas@htom.net"));

            Assert.IsFalse(_unitUnderTest.IsValidEmailAddress(null));
            Assert.IsFalse(_unitUnderTest.IsValidEmailAddress(""));
            Assert.IsFalse(_unitUnderTest.IsValidEmailAddress("_34skd@"));
            Assert.IsFalse(_unitUnderTest.IsValidEmailAddress(".net"));
            Assert.IsFalse(_unitUnderTest.IsValidEmailAddress(".com"));
            Assert.IsFalse(_unitUnderTest.IsValidEmailAddress("@.uk"));
        }

        [Test]
        public void IsValidEmailAddress_NullOrEmptyString_ReturnsFalse()
        {
            Assert.IsFalse(_unitUnderTest.IsValidEmailAddress(null));
            Assert.IsFalse(_unitUnderTest.IsValidEmailAddress(""));
        }

        [Test]
        public void IsValidHexString_HexString_IsRecognized()
        {
            string hexString = "0123456789ABCDEFabcdef";
            Assert.IsTrue(_unitUnderTest.IsValidHexString(hexString));
            Assert.IsFalse(_unitUnderTest.IsValidHexString(_alpha));
            Assert.IsFalse(_unitUnderTest.IsValidHexString(_alphaWithSpaces));
            Assert.IsTrue(_unitUnderTest.IsValidHexString(_numeric));
            Assert.IsFalse(_unitUnderTest.IsValidHexString(_numericWithSpaces));
            Assert.IsFalse(_unitUnderTest.IsValidHexString(_alphaNumeric));
            Assert.IsFalse(_unitUnderTest.IsValidHexString(_alphaNumericWithSpaces));
            Assert.IsFalse(_unitUnderTest.IsValidHexString(_specialCharacters));
            Assert.IsFalse(_unitUnderTest.IsValidHexString(_specialCharactersWithSpaces));
        }

        [Test]
        public void IsHexString_NullOrEmptyString_ReturnsFalse()
        {
            Assert.IsFalse(_unitUnderTest.IsValidHexString(null));
            Assert.IsFalse(_unitUnderTest.IsValidHexString(""));
        }

        [Test]
        public void IsEndDateAfterStartDate_Dates_IsRecognized()
        {
            DateTime today = DateTime.Now;
            DateTime yesterday = DateTime.Now.AddDays(-1);
            DateTime thirtySecondsAgo = DateTime.Now.AddSeconds(-30);

            //Valid, time is considered by default
            Assert.IsTrue(_unitUnderTest.IsEndDateAfterStartDate(today, yesterday));
            Assert.IsTrue(_unitUnderTest.IsEndDateAfterStartDate(today, thirtySecondsAgo));

            //Invalid, time is considered by default
            Assert.IsFalse(_unitUnderTest.IsEndDateAfterStartDate(yesterday, today));
            Assert.IsFalse(_unitUnderTest.IsEndDateAfterStartDate(thirtySecondsAgo, today));

            //Valid, time NOT considered
            Assert.IsTrue(_unitUnderTest.IsEndDateAfterStartDate(today, yesterday, false));

            //Valid, time NOT considered - dates equal
            Assert.IsTrue(_unitUnderTest.IsEndDateAfterStartDate(today, thirtySecondsAgo, false));
        }
    }
}
