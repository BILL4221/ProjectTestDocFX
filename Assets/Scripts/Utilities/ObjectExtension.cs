
using System;
using System.Reflection;
using UnityEngine;

namespace Runnex.Utilities
{
    public static class ObjectExtension
    {
        /// <summary>
        /// Useful for getting property with private getter for testing and editor purpose
        /// </summary>
        /// <param name="propertyName"></param>
        public static T GetStaticProperty<T>(this Type type, string propertyName)
        {
            PropertyInfo prop = null;
            Type currentType = type;
            while (prop == null && currentType != null && currentType != typeof(object))
            {
                prop = currentType.GetProperty(propertyName, (BindingFlags)~0);
                currentType = currentType.BaseType;
            }
            Debug.Assert(prop != null, "Invalid Property: " + propertyName);
            return (T)prop.GetValue(null, null);
        }
    }
}
