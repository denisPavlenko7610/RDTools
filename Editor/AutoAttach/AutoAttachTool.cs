using System;
using System.Collections.Generic;
using RDragonTools.AutoAttach.Setters;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RDragonTools.AutoAttach.Editor
{
    [EditorTool(nameof(AutoAttachTool), typeof(MonoBehaviour))]
    internal partial class AutoAttachTool : EditorTool
    {
        private static readonly Dictionary<Type, FieldData[]> Cache = 
            new Dictionary<Type, FieldData[]>();

        private static readonly List<SetterBase> Setters = new List<SetterBase>();

        public static int MaxDepth => 7;
        
        private void OnEnable()
        {
            foreach (Object o in targets)
            {
                Set((MonoBehaviour)o);
            }
        }

        private static bool Set(Component monoBehaviour)
        {
            if (!Cache.TryGetValue(monoBehaviour.GetType(), out var values))
                return false;

            int set = 0;
            foreach (FieldData data in values)
            {
                Type fieldType = data.Field.FieldType;

                if (!FindSetter(fieldType, out SetterBase setter))
                    continue;

                object context = data.GetContext(monoBehaviour);

                data.attribute.BeforeSet(context);
                if (setter.TrySetField(monoBehaviour, context, data.Field.GetValue(context), data.Field.FieldType, data.attribute, out object newValue))
                {
                    data.Field.SetValue(context, newValue);
                    set++;
                }

                data.attribute.AfterSet(context);
            }

            if (set > 0)
                EditorUtility.SetDirty(monoBehaviour);

            return set > 0;
        }

        private static bool FindSetter(Type type, out SetterBase setter)
        {
            foreach (SetterBase autoSetter in Setters)
            {
                if (autoSetter.Compatible(type))
                {
                    setter = autoSetter;
                    return true;
                }
            }

            setter = null;
            return false;
        }
    }
}