﻿using System;

namespace RDTools
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class EnumFlagsAttribute : DrawerAttribute
    {
    }
}
