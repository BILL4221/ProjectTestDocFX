using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

namespace Runnex.Utilities
{
    public class StringEnum
    {
        protected delegate StringEnum ConversionHandler(string value);

        /// <summary>
        /// Value uniquely represent the enum
        /// </summary>
        public readonly string Value;

        private static Dictionary<Type, ConversionHandler> conversionHandlers;

        /// <summary>
        /// Convert from string to enum object
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static StringEnum FromString(Type type, string value)
        {
            RunClassConstructor(type);

            Debug.Assert(conversionHandlers.ContainsKey(type));
            return conversionHandlers[type](value);
        }

        /// <summary>
        /// make sure static constructor got invoked.
        /// if parsing happen before any reference to the enum class,
        /// its constructor won't invoke and conversion will failed.
        /// This RunClassConstructor function is guaranteed to run the constructor only once
        /// </summary>
        /// <param name="type"></param>
        protected static void RunClassConstructor(Type type)
        {
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(type.TypeHandle);
        }

        protected StringEnum(string value)
        {
            Value = value;
        }

        protected static void RegisterConversionHandler(Type type, ConversionHandler handler)
        {
            if (conversionHandlers == null)
                conversionHandlers = new Dictionary<Type, ConversionHandler>();

            conversionHandlers[type] = handler;
        }
    }

    public class StringEnum<T> : StringEnum where T : StringEnum
    {
        /// <summary>
        /// mapping enum value and enum object
        /// Key is Value uniquely defined in TAStringEnum
        /// </summary>
        private static Dictionary<string, T> map;

        static StringEnum()
        {
            RegisterConversionHandler(typeof(T), FromString);
        }

        /// <summary>
        /// getting members of this enum type
        /// </summary>
        public static IEnumerable<T> Members
        {
            get
            {
                RunClassConstructor(typeof(T));
                return (map != null) ? map.Values : null;
            }
        }

        /// <summary>
        /// Convert from string to enum object
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T FromString(string value)
        {
            RunClassConstructor(typeof(T));

            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            bool isValidValue = map.ContainsKey(value);
            Debug.Assert(isValidValue, string.Format("Invalid \"{0}\" string enum value: {1}", typeof(T).Name, value));

            T result = null;

            if (isValidValue)
            {
                result = map[value];
            }

            return result;
        }

        /// <summary>
        /// To string for debug and log readability
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string typeName = typeof(T).Name;
            return string.Format("{0}.{1}", typeName, Value);
        }

        protected static void AddMember(StringEnum<T> member)
        {
            if (map == null)
            {
                map = new Dictionary<string, T>();
            }

            if (member.Value != null)
            {
                map[member.Value] = member as T;
            }
        }

        protected StringEnum(string value) : base(value)
        {
            AddMember(this);
        }
    }
}

