using UnityEngine;

namespace Runnex.Utilities.Extensions
{
    /// <summary>
    /// Extension for Vector
    /// </summary>
    public static class VectorExtension
    {
        /// <summary>
        /// Convert Vector3's Y to 0
        /// This is convenient for check the distance on the XZ plane
        /// </summary>
        /// <param name="vector">Vector to Convert</param>
        /// <returns>Vector with Y = 0</returns>
        public static Vector3 ToXZ(this Vector3 vector)
        {
            return new Vector3(vector.x, 0f, vector.z);
        }

        /// <summary>
        /// Clamp Vector3
        /// </summary>
        /// <param name="vector">vector to clamp</param> 
        /// <param name="min">min</param>
        /// <param name="max">max</param>
        /// <returns></returns>
        public static Vector3 Clamp(this Vector3 vector, Vector3 min, Vector3 max)
        {
            return new Vector3(
                Mathf.Clamp(vector.x, min.x, max.x),
                Mathf.Clamp(vector.y, min.y, max.y),
                Mathf.Clamp(vector.z, min.z, max.z)
            );
        }

        /// <summary>
        /// clones a Vector3 object
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3 Clone(this Vector3 vector) => new Vector3(vector.x, vector.y, vector.z);

        /// <summary>
        /// calculate the inverse linear interpolation of a vector
        /// </summary>
        /// <param name="value"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static float InverseLerp(this Vector3 value, Vector3 from, Vector3 to)
        {
            float sqrMagnitude = (to - from).sqrMagnitude; 
            Debug.Assert(sqrMagnitude > 0); 
            return Vector3.Dot(value - from, to - from) / sqrMagnitude;
        }
    }
}