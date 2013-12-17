using System;
using System.Reflection;

namespace Commoner.Core.Testing
{
    /// <summary>
    /// A helper class for bypassing normal visibility rules to invoke methods on an object.
    /// </summary>
	public static class MethodHelper
	{
        /// <summary>
        ///	Runs a method on a type, given its parameters. This is useful for calling private methods.
        /// </summary>
        /// <returns>The return value of the called method.</returns>
        public static object RunStaticMethod(System.Type t, string strMethod, object[] aobjParams)
        {
            BindingFlags eFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            return RunMethod(t, strMethod, null, aobjParams, eFlags);
        }

        /// <summary>
        /// Runs a method on an instance, given its parameters. This is useful for calling private methods.
        /// </summary>
        /// <returns>The return value of the called method.</returns>
        public static object RunInstanceMethod(System.Type t, string strMethod, object objInstance, object[] aobjParams)
        {
            BindingFlags eFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            return RunMethod(t, strMethod, objInstance, aobjParams, eFlags);
        }

        private static object RunMethod(System.Type t, string strMethod, object objInstance, object[] aobjParams, BindingFlags eFlags)
        {
            MethodInfo m;
            try
            {
                m = t.GetMethod(strMethod, eFlags);
                if (m == null)
                {
                    throw new ArgumentException("There is no method '" + strMethod + "' for type '" + t.ToString() + "'.");
                }

                object objRet = m.Invoke(objInstance, aobjParams);
                return objRet;
            }
            catch
            {
                throw;
            }
        }
	}

}
