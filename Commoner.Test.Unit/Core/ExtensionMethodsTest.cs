using System;
using Commoner.Core;
using Moq;
using NUnit.Framework;

namespace Common.Test.Unit.Core
{
    [TestFixture]
    public class ExtensionMethodsTest
    {
        Mock<IDataValidator> _fakeDataValidator;
        Mock<IDataCopier> _fakeDataCopier;
        string _value = "SomeValueThatDoesn'tMatter";

        [SetUp] 
        public void SetUp()
        {
            _fakeDataValidator = new Mock<IDataValidator>();
            _fakeDataCopier = new Mock<IDataCopier>();

            ExtensionMethods.DataValidator = _fakeDataValidator.Object;
            ExtensionMethods.DataCopier = _fakeDataCopier.Object;
        }

        [Test]
        public void CopyProperties()
        {
            AClass a = new AClass(1, 2, 3, 4, 5);
            BClass b = new BClass();

            a.CopyPropertyValues(b);

            _fakeDataCopier.Verify(f => f.CopyProperties(a, b), Times.Once());
        }


        #region string

        [Test]
        public void TruncateForDisplay_NormalBehavior_TruncatesAndAppends()
        {
            string message = "The fox ran accross the road";
            Assert.AreEqual("The fox...", message.TruncateForDisplay(7));
        }

        [Test]
        public void TruncateForDisplay_ZeroLength_Appends()
        {
            string message = "The fox ran accross the road";
            Assert.AreEqual("...", message.TruncateForDisplay(0));
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void TruncateForDisplay_NullString_ThrowsException()
        {
            string message = null;
            message.TruncateForDisplay(10);
        }

        [Test]
        public void IsValidAlphaIsValidAlphaNumericString_CallsMockImpl()
        {
            _fakeDataValidator.Setup(f => f.IsValidAlphaNumericString(It.IsAny<string>(), It.IsAny<bool>())).Returns(true);

            _value.IsValidAlphaNumericString();
            _fakeDataValidator.Verify(f => f.IsValidAlphaNumericString(_value, false), Times.Once());

            _value.IsValidAlphaNumericString(true);
            _fakeDataValidator.Verify(f => f.IsValidAlphaNumericString(_value, true), Times.Once());
        }

        [Test]
        public void IsValidAlphaString_CallsMockImpl()
        {
            _fakeDataValidator.Setup(f => f.IsValidAlphaString(It.IsAny<string>(), It.IsAny<bool>())).Returns(true);

            _value.IsValidAlphaString();
            _fakeDataValidator.Verify(f => f.IsValidAlphaString(_value, false), Times.Once());

            _value.IsValidAlphaString(true);
            _fakeDataValidator.Verify(f => f.IsValidAlphaString(_value, true), Times.Once());
        }

        [Test]
        public void IsValidEmailAddress_CallsMockImpl()
        {
            _fakeDataValidator.Setup(f => f.IsValidEmailAddress(It.IsAny<string>())).Returns(true);

            _value.IsValidEmailAddress();
            _fakeDataValidator.Verify(f => f.IsValidEmailAddress(_value), Times.Once());
        }

        [Test]
        public void IsValidHexString_CallsMockImpl()
        {
            _fakeDataValidator.Setup(f => f.IsValidHexString(It.IsAny<string>())).Returns(true);

            _value.IsValidHexString();
            _fakeDataValidator.Verify(f => f.IsValidHexString(_value), Times.Once());
        }

        [Test]
        public void IsValidNumericString_CallsMockImpl()
        {
            _fakeDataValidator.Setup(f => f.IsValidNumericString(It.IsAny<string>(), It.IsAny<bool>())).Returns(true);

            _value.IsValidNumericString();
            _fakeDataValidator.Verify(f => f.IsValidNumericString(_value, false), Times.Once());

            _value.IsValidNumericString(true);
            _fakeDataValidator.Verify(f => f.IsValidNumericString(_value, true), Times.Once());
        }      

        #endregion

        #region DateTime

        [Test]
        public void IsAfter()
        {
            _fakeDataValidator.Setup(f => f.IsEndDateAfterStartDate(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<bool>())).Returns(true);

            DateTime date1 = new DateTime();
            DateTime date2 = new DateTime();

            date1.IsAfter(date2);
            _fakeDataValidator.Verify(f => f.IsEndDateAfterStartDate(date1, date2, false), Times.Once());

            date1.IsAfter(date2, true);
            _fakeDataValidator.Verify(f => f.IsEndDateAfterStartDate(date1, date2, true), Times.Once());
        }

        #endregion

        [Test]
        public void JustForCoverage()
        {
            ExtensionMethods.DataValidator = null;
            _value.IsValidNumericString();
        }
    }
}
