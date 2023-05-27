using System;
using System.Collections.Generic;

namespace RDTools.Extensions
{
    public static class StringExtensions
    {
        public static string ToUpperFirstChar(this string word) => char.ToUpper(word[0]) + word[1..];
    }
}
