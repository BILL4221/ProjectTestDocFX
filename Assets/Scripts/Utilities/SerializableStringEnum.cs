using System;
using UnityEngine;

namespace Runnex.Utilities
{
    public abstract class SerializableStringEnum
    {
        public abstract Type EnumType { get; }

        public string StringValue { get { return value; } }

        [SerializeField]
        protected string value;

        public SerializableStringEnum(StringEnum stringEnum)
        {
            value = (stringEnum != null) ? stringEnum.Value : null;
        }
    }

    public class SerializableStringEnum<T> : SerializableStringEnum where T : StringEnum
    {
        public override Type EnumType { get { return typeof(T); } }
        private T cachedEnum;

        public SerializableStringEnum(StringEnum stringEnum) : base(stringEnum) { }

        /// <summary>
        /// get string enum type
        /// </summary>
        /// <returns></returns>
        public T ToEnum()
        {
            if (cachedEnum?.Value != value)
            {
                cachedEnum = StringEnum.FromString(EnumType, value) as T;
            }
            return cachedEnum;
        }

        /// <summary>
        /// compare serializable against non-serializable string enum
        /// </summary>
        /// <param name="serializable"></param>
        /// <param name="nonSerialize"></param>
        /// <returns></returns>
        public static bool operator ==(SerializableStringEnum<T> serializable, T nonSerialize)
        {
            if (ReferenceEquals(serializable, null) && nonSerialize == null)
            {
                return false;
            }

            return serializable.ToEnum() == nonSerialize;
        }

        /// <summary>
        /// compare serializable against non-serializable string enum
        /// </summary>
        /// <param name="serializable"></param>
        /// <param name="nonSerialize"></param>
        /// <returns></returns>
        public static bool operator !=(SerializableStringEnum<T> serializable, T nonSerialize)
        {
            return !(serializable == nonSerialize);
        }

        /// <summary>
        /// compare serializable/non-serializable string enum against each other
        /// there will be warning if this is not implemented
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            bool result = false;

            if (obj is T)
            {
                result = (value == (obj as T).Value);
            }
            else if (obj is SerializableStringEnum)
            {
                result = (value == (obj as SerializableStringEnum).StringValue);
            }

            return result;
        }

        /// <summary>
        /// get hash code
        /// there will be warning if this is not implemented
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int result = 0;
            if (!string.IsNullOrEmpty(value))
            {
                result = value.GetHashCode();
            }

            return result;
        }

        /// <summary>
        /// get string value
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return value;
        }
    }
}
