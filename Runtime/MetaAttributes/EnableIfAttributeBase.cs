using System;

namespace RDTools
{
    public abstract class EnableIfAttributeBase : MetaAttribute
    {
        public string[] Conditions { get; private set; }
        public EConditionOperator EConditionOperator { get; private set; }
        public bool Inverted { get; protected set; }

        /// <summary>
        ///		If this not null, <see cref="Conditions"/>[0] is name of an enum variable.
        /// </summary>
        public Enum EnumValue { get; private set; }

        public EnableIfAttributeBase(string condition)
        {
            EConditionOperator = EConditionOperator.And;
            Conditions = new string[1] { condition };
        }

        public EnableIfAttributeBase(EConditionOperator eConditionOperator, params string[] conditions)
        {
            EConditionOperator = eConditionOperator;
            Conditions = conditions;
        }

        public EnableIfAttributeBase(string enumName, Enum enumValue)
            : this(enumName)
        {
            if (enumValue == null)
            {
                throw new ArgumentNullException(nameof(enumValue), "This parameter must be an enum value.");
            }

            EnumValue = enumValue;
        }
    }
}
