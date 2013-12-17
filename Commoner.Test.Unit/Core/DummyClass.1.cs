
namespace Common.Test.Unit.Core
{
    public class DummyClass
    {
        #region Variables
        
        #pragma warning disable 0414
        public      string          _somePublicStringValue;
        protected   string          _someProtectedStringValue;
        private     string          _somePrivateStringValue;
        internal    string          _someInternalStringValue;
        private     static string   _somePrivateStaticStringValue;

        private     string          _somePublicPropertyValue;
        private     string          _someProtectedPropertyValue;
        private     string          _somePrivatePropertyValue;
        private     string          _someInternalPropertyValue;
        private     static string   _somePrivateStaticPropertyValue;
        #endregion

        #region Properties
        public string SomePublicPropertyValue
        {
            get
            {
                return _somePublicPropertyValue;
            }
            set
            {
                _somePublicPropertyValue = value;
            }
        }

        protected string SomeProtectedPropertyValue
        {
            get
            {
                return _someProtectedPropertyValue;
            }
            set
            {
                _someProtectedPropertyValue = value;
            }
        }

        private string SomePrivatePropertyValue 
        {
            get
            {
                return _somePrivatePropertyValue;
            }
            set
            {
                _somePrivatePropertyValue = value;
            }
        }

        internal string SomeInternalPropertyValue
        {
            get
            {
                return _someInternalPropertyValue;
            }
            set
            {
                _someInternalPropertyValue = value;
            }
        }

        private static string SomePrivateStaticPropertyValue
        {
            get
            {
                return _somePrivateStaticPropertyValue;
            }
            set
            {
                _somePrivateStaticPropertyValue = value;
            }
        }
        #endregion

        #region Constructors
        public DummyClass()
        {
            _somePublicStringValue          = "somePublicStringValue";
            _someProtectedStringValue       = "someProtectedStringValue";
            _somePrivateStringValue         = "somePrivateStringValue";
            _someInternalStringValue        = "someInternalStringValue";
            _somePrivateStaticStringValue   = "somePrivateStaticStringValue";

            _somePublicPropertyValue        = "somePublicPropertyValue";
            _someProtectedPropertyValue     = "someProtectedPropertyValue";
            _somePrivatePropertyValue       = "somePrivatePropertyValue";
            _someInternalPropertyValue      = "someInternalPropertyValue";
            _somePrivateStaticPropertyValue = "somePrivateStaticPropertyValue";
        }
        #endregion

        #region Static
        public static string GiveMeBackWhatIGiveYou_PublicStatic(string value)
        {
            return value;
        }

        protected static string GiveMeBackWhatIGiveYou_ProtectedStatic(string value)
        {
            return value;
        }

        private static string GiveMeBackWhatIGiveYou_PrivateStatic(string value)
        {
            return value;
        }
        #endregion

        #region Instance
        public string GiveMeBackWhatIGiveYou_PublicInstance(string value)
        {
            return value;
        }

        protected string GiveMeBackWhatIGiveYou_ProtectedInstance(string value)
        {
            return value;
        }

        private string GiveMeBackWhatIGiveYou_PrivateInstance(string value)
        {
            return value;
        }
        #endregion
    }
}
