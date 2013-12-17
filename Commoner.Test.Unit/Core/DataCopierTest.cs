using System;
using Commoner.Core;
using NUnit.Framework;

namespace Common.Test.Unit.Core
{
    [TestFixture]
    public class DataCopierTest
    {
        DataCopier _unitUnderTest;

        [SetUp]
        public void SetUp()
        {
            _unitUnderTest = new DataCopier();
        }

        [Test]
        public void CopyData_AToB()
        {
            AClass a = new AClass(1, 2, 3, 4, 5);
            BClass b = new BClass();

            _unitUnderTest.CopyProperties(a, b);

            Assert.AreEqual(a.A, b.A);
            Assert.AreEqual(a.B, b.B);
            Assert.AreEqual(a.D, b.D);
            Assert.AreEqual(0, b.F);
        }

        [Test]
        public void Convert_AToC()
        {
            AClass a = new AClass(1, 2, 3, 4, 5);
            CClass c = new CClass();

            _unitUnderTest.CopyProperties(a, c);

            Assert.AreEqual(a.A, c.A);
            Assert.AreEqual(a.B, c.B);
            Assert.AreEqual(a.D, c.D);
            Assert.AreEqual(0, c.F);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Convert_NullFirstParam_ThrowsException()
        {
            _unitUnderTest.CopyProperties(null, new object());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Convert_NullSecondParam_ThrowsException()
        {
            _unitUnderTest.CopyProperties(new object(), null);
        }
    }

    public class AClass
    {
        public AClass()
        {
        }

        public AClass(int a, int b, int c, int d, int e)
        {
            A = a;
            B = b;
            C = c;
            D = d;
            E = e;
        }

        public int A { get; set; }
        public virtual int B { get; set; }
        protected int C { get; set; }
        internal int D { get; set; }
        private int E { get; set; }

    }

    public class BClass : AClass
    {
        public BClass()
        {
        }

        public BClass(int a, int b, int c, int d, int e, int f)
            : base(a, b, c, d, e)
        {
            F = f;
        }

        public int F { get; set; }
    }

    public class CClass
    {
        public CClass()
        {
        }

        public CClass(int a, int b, int c, int d, int e, int f)
        {
            A = a;
            B = b;
            C = c;
            D = d;
            E = e;
            F = f;
        }

        public int A { get; set; }
        public virtual int B { get; set; }
        protected int C { get; set; }
        internal int D { get; set; }
        private int E { get; set; }
        public int F { get; set; }
    }
}
