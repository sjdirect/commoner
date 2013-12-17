using Commoner.Core;
using NUnit.Framework;
using Commoner.Core.Testing;

namespace Common.Test.Unit.Core.Testing
{
    [TestFixture]
    public class DataGeneratorTests
    {
        DataGenerator _unitUnderTest = new DataGenerator();
        DataValidator _dataValidator = new DataValidator();
        const int _maxCharLength = 1000;

        [Test]
        public void GetRandomAlphaNumericString_NormalArguments_ReturnsAlphaNumericString()
        {
            string value = _unitUnderTest.GetRandomAlphaNumericString(_maxCharLength);
            Assert.IsFalse(string.IsNullOrEmpty(value));
            Assert.AreEqual(_maxCharLength, value.Length);
            Assert.IsTrue(_dataValidator.IsValidAlphaNumericString(value));
        }

        [Test]
        public void GetRandomAlphaNumericString_ZeroLength_ReturnsEmptyString()
        {
            string value = _unitUnderTest.GetRandomAlphaNumericString(0);
            Assert.AreEqual(string.Empty, value);
        }

        [Test]
        public void GetRandomAlphaString_NormalArguments_ReturnsAlphaString()
        {
            string value = _unitUnderTest.GetRandomAlphaString(_maxCharLength);
            Assert.IsFalse(string.IsNullOrEmpty(value));
            Assert.AreEqual(_maxCharLength, value.Length);
            Assert.IsTrue(_dataValidator.IsValidAlphaString(value));
        }

        [Test]
        public void GetRandomAlphaString_ZeroLength_ReturnsEmptyString()
        {
            string value = _unitUnderTest.GetRandomAlphaString(0);
            Assert.AreEqual(string.Empty, value);
        }

        [Test]
        public void GetRandomNumericString_NormalArguments_ReturnsNumericString()
        {
            string value = _unitUnderTest.GetRandomNumericString(_maxCharLength);
            Assert.IsFalse(string.IsNullOrEmpty(value));
            Assert.AreEqual(_maxCharLength, value.Length);
            Assert.IsTrue(_dataValidator.IsValidNumericString(value));
        }

        [Test]
        public void GetRandomNumericString_ZeroLength_ReturnsEmptyString()
        {
            string value = _unitUnderTest.GetRandomNumericString(0);
            Assert.AreEqual(string.Empty, value);
        }

        [Test]
        public void GetRandomInteger_NormalArguments_ReturnsInt()
        {
            int value = _unitUnderTest.GetRandomInteger(0, 0);
            Assert.AreEqual(0, value);

            value = _unitUnderTest.GetRandomInteger(5, 5);
            Assert.AreEqual(5, value);

            value = _unitUnderTest.GetRandomInteger(10, 20);
            Assert.IsTrue(value >= 10);
            Assert.IsTrue(value <= 20);
        }
    }
}
