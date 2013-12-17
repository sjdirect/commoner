using System;
using Commoner.Core.Unity;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;

namespace Commoner.Test.Unit.Core.Unity
{
    [TestFixture]
    public class ConfigFileFactoryRegistrarTest
    {
        ConfigFileFactoryRegistrar _unitUnderTest;
        Mock<IUnityContainer> _fakeUnityContainer;

        [SetUp]
        public void TestSetUp()
        {
            _unitUnderTest = new ConfigFileFactoryRegistrar();
            _fakeUnityContainer = new Mock<IUnityContainer>();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Register_NullUnityContainer()
        {
            _unitUnderTest.Register(null);
        }

        [Test]
        public void Register_ValidUnityContainer()
        {
            _unitUnderTest.Register(_fakeUnityContainer.Object);
        }
    }
}
