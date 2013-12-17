using Commoner.Core.Unity;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;


namespace Common.Test.Library.UnitTest
{
	[TestFixture] 
    public class FactoryTest
	{
        [Test]
        public void Container_Getter_ReturnsSameInstance()
        {
            IUnityContainer container = Factory.Container;
            Assert.AreSame(container, Factory.Container);
        }

        [Test]
        public void Dispose()
        {
            Assert.IsNotNull(Factory.Container);
            Factory.Dispose();
        }

        [Test]
        public void Configure()
        {
            Mock<IFactoryRegistrar> fakeRegistrar = new Mock<IFactoryRegistrar>();

            Factory.Configure(fakeRegistrar.Object);

            fakeRegistrar.Verify(f => f.Register(Factory.Container));
        }

        [Test]
        public void JustForCoverage()
        {
            Factory.Container = null;
        }
	}

}
