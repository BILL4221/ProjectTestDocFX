using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Runnex.Utilities
{
    // Reflections is very slow, only use this for objects that are instantiated very rarely ( eg. once per scene )
    public class AutoGetMonoBehaviour : MonoBehaviour
    {
        protected virtual void Awake()
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            foreach (var field in GetType().GetFields(flags))
            {
                var attributes = (AutoGetAttribute[])field.GetCustomAttributes(typeof(AutoGetAttribute), true);
                if(attributes.Length > 0)
                {
                    field.SetValue(this, FindObjectOfType(field.FieldType));
                }
            }
        }
    }
}
