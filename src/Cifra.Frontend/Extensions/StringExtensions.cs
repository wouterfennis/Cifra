using System.Text.RegularExpressions;

namespace Cifra.Frontend.Extensions
{
    internal static class StringExtensions
    {
        public static string? ToDisplayFormat(this string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var words = Regex.Matches(input, @"([A-Z][a-z]+)|([a-z]+)")
                .Select(x => x.Value);

            var joinedWords = string.Join(" ", words);
            return char.ToUpper(joinedWords.First()) + joinedWords.Substring(1);
        }
    }
}
