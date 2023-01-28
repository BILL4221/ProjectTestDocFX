using System.Collections;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System;
using System.Reflection;

namespace Runnex.Utilities.Editor
{
    [CustomPropertyDrawer(typeof(SerializableStringEnum), true)]
    public class StringEnumDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DropDownInput(position, property, label);
        }

        private void DropDownInput(Rect position, SerializedProperty property, GUIContent label)
        {
            System.Type enumType = null;
            if (fieldInfo.FieldType.IsArray)
            {
                //for SerializableT[] variable
                enumType = fieldInfo.FieldType.GetElementType().BaseType.GetGenericArguments()[0];
            }
            else if (fieldInfo.FieldType.IsGenericType)
            {
                //for List<SerializableT> variable
                enumType = fieldInfo.FieldType.GetGenericArguments()[0].BaseType.GetGenericArguments()[0];
            }
            else
            {
                //for SerializableT variable
                enumType = fieldInfo.FieldType.BaseType.GetGenericArguments()[0];
            }

            var members = enumType.GetStaticProperty<IEnumerable>("Members")?.OfType<StringEnum>();
            SerializedProperty valueProperty = property.FindPropertyRelative("value");
            string currentValue = valueProperty.stringValue;

            int length = members?.Count() ?? 0;
            string[] values = new string[length + 1];
            values[length] = "null";

            int currentIndex = length; //select null element as default
            for (int i = 0; i < length; i++)
            {
                StringEnum member = members.ElementAt(i);
                values[i] = member.Value;

                if (member.Value == currentValue)
                    currentIndex = i;
            }

            currentIndex = EditorGUI.Popup(position, label.text, currentIndex, values);
            if (currentIndex < 0 || currentIndex >= length)
            {
                valueProperty.stringValue = null;
            }
            else
            {
                valueProperty.stringValue = values[currentIndex];
            }
        }
    }
}