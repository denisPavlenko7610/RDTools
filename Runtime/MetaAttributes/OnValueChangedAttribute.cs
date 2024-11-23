using System;

namespace RDTools
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class OnValueChangedAttribute : MetaAttribute
    {
        public string CallbackName { get; private set; }

        public OnValueChangedAttribute(string callbackName)
        {
            CallbackName = callbackName;
        }
    }
}
