using System;

namespace RDTools
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ShowNonSerializedFieldAttribute : SpecialCaseDrawerAttribute
    {
    }
}
