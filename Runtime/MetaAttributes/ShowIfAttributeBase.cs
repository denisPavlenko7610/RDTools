using System;

namespace RDTools
{
    public class ShowIfAttributeBase : MetaAttribute
    {
        public string[] Conditions { get; private set; }
        public EConditionOperator EConditionOperator { get; private set; }
        public bool Inverted { get; protected set; }

        /// <summary>
        ///		If this not null, <see cref="Conditions"/>[0] is name of an enum variable.
        /// </summary>
        public Enum EnumValue { get; private set; }

        public ShowIfAttributeBase(string condition)
        {
            EConditionOperator = EConditionOperator.And;
            Conditions = new string[1] { condition };
        }

        public ShowIfAttributeBase(EConditionOperator eConditionOperator, params string[] conditions)
        {
            EConditionOperator = eConditionOperator;
            Conditions = conditions;
        }

        public ShowIfAttributeBase(string enumName, Enum enumValue)
            : this(enumName)
        {
            EnumValue = enumValue ?? throw new ArgumentNullException(nameof(enumValue), "This parameter must be an enum value.");
        }
    }
}