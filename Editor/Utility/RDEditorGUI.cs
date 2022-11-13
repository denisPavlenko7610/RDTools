using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace RDTools.Editor
{
    public static class RDEditorGUI
    {
        private delegate void PropertyFieldFunction(Rect rect, SerializedProperty property, GUIContent label,
            bool includeChildren);

        public static void PropertyField(Rect rect, SerializedProperty property, bool includeChildren) =>
            PropertyField_Implementation(rect, property, includeChildren, DrawPropertyField);

        public static void PropertyField_Layout(SerializedProperty property, bool includeChildren)
        {
            Rect dummyRect = new Rect();
            PropertyField_Implementation(dummyRect, property, includeChildren, DrawPropertyField_Layout);
        }

        private static void DrawPropertyField(Rect rect, SerializedProperty property, GUIContent label,
            bool includeChildren) => EditorGUI.PropertyField(rect, property, label, includeChildren);

        private static void DrawPropertyField_Layout(Rect rect, SerializedProperty property, GUIContent label,
            bool includeChildren) => EditorGUILayout.PropertyField(property, label, includeChildren);

        private static void PropertyField_Implementation(Rect rect, SerializedProperty property, bool includeChildren,
            PropertyFieldFunction propertyFieldFunction)
        {
            SpecialCaseDrawerAttribute specialCaseAttribute =
                PropertyUtility.GetAttribute<SpecialCaseDrawerAttribute>(property);
            if (specialCaseAttribute != null)
            {
                specialCaseAttribute.GetDrawer().OnGUI(rect, property);
            }
            else
            {
                // Check if visible
                bool visible = PropertyUtility.IsVisible(property);
                if (!visible)
                {
                    return;
                }

                // Validate
                ValidatorAttribute[] validatorAttributes = PropertyUtility.GetAttributes<ValidatorAttribute>(property);
                foreach (var validatorAttribute in validatorAttributes)
                {
                    validatorAttribute.GetValidator().ValidateProperty(property);
                }

                // Check if enabled and draw
                EditorGUI.BeginChangeCheck();
                bool enabled = PropertyUtility.IsEnabled(property);

                using (new EditorGUI.DisabledScope(disabled: !enabled))
                {
                    propertyFieldFunction.Invoke(rect, property, PropertyUtility.GetLabel(property), includeChildren);
                }

                // Call OnValueChanged callbacks
                if (EditorGUI.EndChangeCheck())
                {
                    PropertyUtility.CallOnValueChangedCallbacks(property);
                }
            }
        }

        public static float GetIndentLength(Rect sourceRect)
        {
            Rect indentRect = EditorGUI.IndentedRect(sourceRect);
            float indentLength = indentRect.x - sourceRect.x;

            return indentLength;
        }

        public static void Dropdown(
            Rect rect, SerializedObject serializedObject, object target, FieldInfo dropdownField,
            string label, int selectedValueIndex, object[] values, string[] displayOptions)
        {
            EditorGUI.BeginChangeCheck();

            int newIndex = EditorGUI.Popup(rect, label, selectedValueIndex, displayOptions);
            object newValue = values[newIndex];

            object dropdownValue = dropdownField.GetValue(target);
            if (dropdownValue == null || !dropdownValue.Equals(newValue))
            {
                Undo.RecordObject(serializedObject.targetObject, "Dropdown");
                dropdownField.SetValue(target, newValue);
            }
        }

        public static void HelpBox(Rect rect, string message, MessageType type, UnityEngine.Object context = null,
            bool logToConsole = false)
        {
            EditorGUI.HelpBox(rect, message, type);

            if (logToConsole)
            {
                DebugLogMessage(message, type, context);
            }
        }

        private static void DebugLogMessage(string message, MessageType type, UnityEngine.Object context)
        {
            switch (type)
            {
                case MessageType.None:
                case MessageType.Info:
                    Debug.Log(message, context);
                    break;
                case MessageType.Warning:
                    Debug.LogWarning(message, context);
                    break;
                case MessageType.Error:
                    Debug.LogError(message, context);
                    break;
            }
        }
    }
}