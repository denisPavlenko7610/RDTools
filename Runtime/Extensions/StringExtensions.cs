namespace RDTools.Runtime
{
    public static class StringExtensions
    {
        public static string ToUpperFirstChar(this string word) => char.ToUpper(word[0]) + word[1..];
    }
}
