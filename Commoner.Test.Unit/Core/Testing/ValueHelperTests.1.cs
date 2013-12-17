using System;
using Commoner.Core;
using NUnit.Framework;
using Commoner.Core.Testing;

namespace Common.Test.Unit.Core.Testing
{
    [TestFixture]
    public class ValueHelperTests
    {
        private Type _targetClassType;
        private DummyClass _dummyClassInstance;


        [SetUp]
        public void TestFixtureSetup()
        {
            _targetClassType = typeof(DummyClass);
            _dummyClassInstance = new DummyClass();
        }

        [Test]
        public void GetFieldValue_PublicString_ReturnsValue()
        {
            TestGetFieldValue("_somePublicStringValue", "somePublicStringValue");
        }

        [Test]
        public void GetFieldValue_ProtectedString_ReturnsValue()
        {
            TestGetFieldValue("_someProtectedStringValue", "someProtectedStringValue");
        }

        [Test]
        public void GetFieldValue_PrivateString_ReturnsValue()
        {
            TestGetFieldValue("_somePrivateStringValue", "somePrivateStringValue");
        }

        [Test]
        public void GetFieldValue_InternalString_ReturnsValue()
        {
            TestGetFieldValue("_someInternalStringValue", "someInternalStringValue");
        }

        [Test]
        public void GetFieldValue_PrivateStaticString_ReturnsValue()
        {
            TestGetFieldValue("_somePrivateStaticStringValue", "somePrivateStaticStringValue");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFieldValue_NonExistentField_ThrowsException()
        {
            TestGetFieldValue("SomeFieldThatDoesntExist", "someValue");
        }

        [Test]
        public void SetFieldValue_PublicString_ReturnsValue()
        {
            TestSetFieldValue("_somePublicStringValue", "_somePublicStringValue***");
        }

        [Test]
        public void SetFieldValue_ProtectedString_ReturnsValue()
        {
            TestSetFieldValue("_someProtectedStringValue", "someProtectedStringValue***");
        }

        [Test]
        public void SetFieldValue_PrivateString_ReturnsValue()
        {
            TestSetFieldValue("_somePrivateStringValue", "somePrivateStringValue***");
        }

        [Test]
        public void SetFieldValue_InternalString_ReturnsValue()
        {
            TestSetFieldValue("_someInternalStringValue", "someInternalStringValue***");
        }

        [Test]
        public void SetFieldValue_PrivateStaticString_ReturnsValue()
        {
            TestSetFieldValue("_somePrivateStaticStringValue", "somePrivateStaticStringValue***");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void SetFieldValue_NonExistentField_ThrowsException()
        {
            ValueHelper.SetFieldValue(_dummyClassInstance, "SomeFieldThatDoesntExist", "xxxx");
        }

        [Test]
        public void GetPropertyValue_PublicString_ReturnsValue()
        {
            TestGetPropertyValue("SomePublicPropertyValue", "somePublicPropertyValue");
        }

        [Test]
        public void GetPropertyValue_ProtectedString_ReturnsValue()
        {
            TestGetPropertyValue("SomeProtectedPropertyValue", "someProtectedPropertyValue");
        }

        [Test]
        public void GetPropertyValue_PrivateString_ReturnsValue()
        {
            TestGetPropertyValue("SomePrivatePropertyValue", "somePrivatePropertyValue");
        }

        [Test]
        public void GetPropertyValue_InternalString_ReturnsValue()
        {
            TestGetPropertyValue("SomeInternalPropertyValue", "someInternalPropertyValue");
        }

        [Test]
        public void GetPropertyValue_PrivateStaticString_ReturnsValue()
        {
            TestGetPropertyValue("SomePrivateStaticPropertyValue", "somePrivateStaticPropertyValue");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPropertyValue_NonExistentProperty_ThrowsException()
        {
            TestGetPropertyValue("SomePropertyThatDoesntExist", "someValue");
        }

        [Test]
        public void SetPropertyValue_PublicString_ReturnsValue()
        {
            TestSetPropertyValue("SomePublicPropertyValue", "somePublicPropertyValue***");
        }

        [Test]
        public void SetPropertyValue_ProtectedString_ReturnsValue()
        {
            TestSetPropertyValue("SomeProtectedPropertyValue", "someProtectedPropertyValue***");
        }

        [Test]
        public void SetPropertyValue_PrivateString_ReturnsValue()
        {
            TestSetPropertyValue("SomePrivatePropertyValue", "somePrivatePropertyValue***");
        }

        [Test]
        public void SetPropertyValue_InternalString_ReturnsValue()
        {
            TestSetPropertyValue("SomeInternalPropertyValue", "someInternalPropertyValue***");
        }

        [Test]
        public void SetPropertyValue_PrivateStaticString_ReturnsValue()
        {
            TestSetPropertyValue("SomePrivateStaticPropertyValue", "somePrivateStaticPropertyValue***");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void SetPropertyValue_NonExistentProperty_ThrowsException()
        {
            ValueHelper.SetPropertyValue(_dummyClassInstance, "SomePropertyThatDoesntExist", "xxxxx");
        }

        private void TestGetFieldValue(string fieldName, string expectedValue)
        {
            Assert.AreEqual(expectedValue, Convert.ToString(ValueHelper.GetFieldValue(_dummyClassInstance, fieldName)));
        }

        private void TestSetFieldValue(string fieldName, string newValue)
        {
            string originalValue = Convert.ToString(ValueHelper.GetFieldValue(_dummyClassInstance, fieldName));
            Assert.AreNotEqual(originalValue, newValue);
            ValueHelper.SetFieldValue(_dummyClassInstance, fieldName, newValue);
            TestGetFieldValue(fieldName, newValue);
            ValueHelper.SetFieldValue(_dummyClassInstance, fieldName, originalValue);
            TestGetFieldValue(fieldName, originalValue);
        }

        private void TestGetPropertyValue(string propertyName, string expectedValue)
        {
            Assert.AreEqual(expectedValue, Convert.ToString(ValueHelper.GetPropertyValue(_dummyClassInstance, propertyName)));
        }

        private void TestSetPropertyValue(string propertyName, string newValue)
        {
            string originalValue = Convert.ToString(ValueHelper.GetPropertyValue(_dummyClassInstance, propertyName));
            Assert.AreNotEqual(originalValue, newValue);
            ValueHelper.SetPropertyValue(_dummyClassInstance, propertyName, newValue);
            TestGetPropertyValue(propertyName, newValue);
            ValueHelper.SetPropertyValue(_dummyClassInstance, propertyName, originalValue);
            TestGetPropertyValue(propertyName, originalValue);
        }

        private string CreateFieldErrorMessage(string field)
        {
            return CreateErrorMessage(field, true);
        }

        private string CreatePropertyErrorMessage(string property)
        {
            return CreateErrorMessage(property, false);
        }

        private string CreateErrorMessage(string name, bool isField)
        {
            string fieldOrProp = "field";
            if (isField == false)
            {
                fieldOrProp = "property";
            }
            //"The property \"{0}\" was not found on type \"{1}\"."
            return string.Format("The {0} \"{1}\" was not found on type \"Common.Test.Library.UnitTest.DummyClass\".", fieldOrProp, name);
        }
    }
}
