using System;

namespace RDTools
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public class HideIfAttribute : ShowIfAttributeBase
    {
        public HideIfAttribute(string condition)
            : base(condition)
        {
            Inverted = true;
        }

        public HideIfAttribute(EConditionOperator conditionOperator, params string[] conditions)
            : base(conditionOperator, conditions)
        {
            Inverted = true;
        }

        public HideIfAttribute(string enumName, object enumValue)
            : base(enumName, enumValue as Enum)
        {
            Inverted = true;
        }
    }
}
