using System;
using System.Collections;
using RDTools.AutoAttach.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RDTools.AutoAttach.Setters
{
    public class ClassSetter : SetterBase
    {
        public override int Order => 10000;

        public override bool Compatible(Type value)
        {
            return value.IsClass && !value.ImplementsOrInherits(typeof(ICollection));
        }

        public override bool TrySetField(Component target, object context, object currentValue, Type fieldType,
            AttachAttribute attribute, out object newValue)
        {
            if (!attribute.ReadOnly && currentValue != null)
            {
                if (currentValue is Object obj)
                {
                    if (obj)
                    {
                        newValue = null;
                        return false;
                    }
                }
                else
                {
                    newValue = null;
                    return false;
                }
            }

            newValue = GetComponent(target, context, fieldType, attribute);
            return currentValue != newValue;
        }
    }
}