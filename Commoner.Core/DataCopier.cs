using System;
using System.Reflection;

namespace Commoner.Core
{
    public interface IDataCopier
    {
        void CopyProperties(object from, object to);
    }

    public class DataCopier : IDataCopier
    {
        BindingFlags _bindingFlags;

        public DataCopier()
            : this(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        {
        }

        public DataCopier(BindingFlags flags)
        {
            _bindingFlags = flags;
        }

        public void CopyProperties(object from, object to)
        {
            if (from == null)
                throw new ArgumentNullException("from");

            if (to == null)
                throw new ArgumentNullException("to");

            PropertyInfo[] toProperties = to.GetType().GetProperties(_bindingFlags);

            Type fromType = from.GetType();

            foreach (PropertyInfo toProperty in toProperties)
            {
                PropertyInfo fromProperty = fromType.GetProperty(toProperty.Name, _bindingFlags);
                if (fromProperty != null)
                    toProperty.SetValue(to, fromProperty.GetValue(from, null), null);
            }
        }
    }
}
