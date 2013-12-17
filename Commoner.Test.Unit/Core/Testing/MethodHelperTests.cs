using System;
using Commoner.Core.Testing;
using NUnit.Framework;


namespace Common.Test.Unit.Core.Testing
{
	[TestFixture] 
    public class MethodHelperTest
	{
        private string _expectedReturnValue;
        private Type _targetClassType;
        private DummyClass _dummyClassInstance;

        [SetUp]
        public void TestFixtureSetup()
        {
            _expectedReturnValue = "Hello there";
            _targetClassType = typeof(DummyClass);
            _dummyClassInstance = new DummyClass();
        }

        [Test]
        public void RunStaticMethod_ProtectedStaticMethod_InvokesMethod()
        {
            object obj = MethodHelper.RunStaticMethod(_targetClassType, "GiveMeBackWhatIGiveYou_ProtectedStatic", new object[] { _expectedReturnValue });
            string actualReturnValue = Convert.ToString(obj);
            Assert.AreEqual(_expectedReturnValue, actualReturnValue);
        }

        [Test]
        public void RunStaticMethod_PrivateStaticMethod_InvokesMethod()
        {
            object obj = MethodHelper.RunStaticMethod(_targetClassType, "GiveMeBackWhatIGiveYou_PrivateStatic", new object[] { _expectedReturnValue });
            string actualReturnValue = Convert.ToString(obj);
            Assert.AreEqual(_expectedReturnValue, actualReturnValue);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void RunStaticMethod_MethodNameInvalid_ThrowsException()
        {
            MethodHelper.RunStaticMethod(_targetClassType, "MethodThatDoesNotExist", new object[] { _expectedReturnValue });
        }

        [Test]
        public void RunInstanceMethod_ProtectedInstanceMethod_InvokesMethod()
        {
            object obj = MethodHelper.RunInstanceMethod(_targetClassType, "GiveMeBackWhatIGiveYou_ProtectedInstance", _dummyClassInstance, new object[] { _expectedReturnValue });
            string actualReturnValue = Convert.ToString(obj);
            Assert.AreEqual(_expectedReturnValue, actualReturnValue);
        }

        [Test]
        public void RunInstanceMethod_PrivateInstanceMethod_InvokesMethod()
        {
            object obj = MethodHelper.RunInstanceMethod(_targetClassType, "GiveMeBackWhatIGiveYou_PrivateInstance", _dummyClassInstance, new object[] { _expectedReturnValue });
            string actualReturnValue = Convert.ToString(obj);
            Assert.AreEqual(_expectedReturnValue, actualReturnValue);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void RunInstanceMethod_MethodNameInvalid_ThrowsException()
        {
            MethodHelper.RunInstanceMethod(_targetClassType, "MethodThatDoesNotExist", _dummyClassInstance, new object[] { _expectedReturnValue });
        }
	}

}
