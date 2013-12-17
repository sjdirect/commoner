using System;
using System.Reflection;

namespace Commoner.Core.Testing
{
    /// <summary>
    /// A helper class for bypassing normal visibility rules to get and set the values of properties and fields on an object.
    /// </summary>
    public static class ValueHelper
    {
        /// <summary>
        /// Gets the value of a field from the provided object. The field may be public, private, internal, or protected.
        /// </summary>
        /// <param name="item">The object to get the field value from.</param>
        /// <param name="field">The name of the field to get the value of. The field name is case sensitive.</param>
        /// <returns>The value of the request field.</returns>
        public static object GetFieldValue(object item, string field)
        {
            FieldInfo fieldInfo = null;

            for (Type type = item.GetType(); fieldInfo == null && type.BaseType != null; type = type.BaseType)
            {
                fieldInfo = type.GetField(field, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            }

            if (fieldInfo == null)
            {
                throw new ArgumentException(string.Format("The field \"{0}\" was not found on type \"{1}\".", field, item.GetType()));
            }

            return fieldInfo.GetValue(item);
        }

        /// <summary>
        /// Sets the value of a field on the provided object. The field may be public, private, internal, or protected.
        /// </summary>
        /// <param name="item">The object to set the field value on.</param>
        /// <param name="field">The name of the field to set the value of. The field name is case sensitive.</param>
        /// <param name="value">The value to set the field to.</param>
        public static void SetFieldValue(object item, string field, object value)
        {
            FieldInfo fieldInfo = null;

            for (Type type = item.GetType(); fieldInfo == null && type.BaseType != null; type = type.BaseType)
            {
                fieldInfo = type.GetField(field, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            }

            if (fieldInfo == null)
            {
                throw new ArgumentException(string.Format("The field \"{0}\" was not found on type \"{1}\".", field, item.GetType()));
            }

            fieldInfo.SetValue(item, value);
        }

        /// <summary>
        /// Gets the value of a property from the provided object. The property may be public, private, internal, or protected.
        /// </summary>
        /// <param name="item">The object to get the property value from.</param>
        /// <param name="property">The name of the property to get the value of. The property name is case sensitive.</param>
        /// <returns>The value of the request property.</returns>
        public static object GetPropertyValue(object item, string property)
        {
            PropertyInfo propertyInfo = null;

            for (Type type = item.GetType(); propertyInfo == null && type.BaseType != null; type = type.BaseType)
            {
                propertyInfo = type.GetProperty(property, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            }

            if (propertyInfo == null)
            {
                throw new ArgumentException(string.Format("The property \"{0}\" was not found on type \"{1}\".", property, item.GetType()));
            }

            return propertyInfo.GetValue(item, null);
        }

        /// <summary>
        /// Sets the value of a property on the provided object. The property may be public, private, internal, or protected.
        /// </summary>
        /// <param name="item">The object to set the property value on.</param>
        /// <param name="property">The name of the property to set the value of. The property name is case sensitive.</param>
        /// <param name="value">The value to set the property to.</param>
        public static void SetPropertyValue(object item, string property, object value)
        {
            PropertyInfo propertyInfo = null;

            for (Type type = item.GetType(); propertyInfo == null && type.BaseType != null; type = type.BaseType)
            {
                propertyInfo = type.GetProperty(property, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            }

            if (propertyInfo == null)
            {
                throw new ArgumentException(string.Format("The property \"{0}\" was not found on type \"{1}\".", property, item.GetType()));
            }

            propertyInfo.SetValue(item, value, null);
        }
    }
}