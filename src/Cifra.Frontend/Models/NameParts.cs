using Cifra.Frontend.Constants;

namespace Cifra.Frontend.Models
{
    internal class NameParts
    {
        public string FirstName { get; init; }
        public string? Infix { get; init; }
        public string LastName { get; init; }

        private NameParts(string firstName, string? infix, string lastName)
        {
            FirstName = firstName;
            Infix = infix;
            LastName = lastName;
        }

        public static NameParts Create(string completeName)
        {
            string[] parts = completeName.Split(' ');
            string firstName = parts.First();

            string? infix = null;
            string lastName = completeName
                    .Replace(firstName, string.Empty)
                    .TrimStart();
            foreach (var part in parts.Skip(1))
            {
                if (DutchInfixes.AllInfixes.Contains(part))
                {
                    infix += $" {part}";
                }
            }

            if (infix == null)
            {
                return new NameParts(firstName, infix, lastName);
            }

            infix = infix.TrimStart();
            lastName = lastName
                .Replace(infix, string.Empty)
                .TrimStart();

            return new NameParts(firstName, infix, lastName);
        }
    }
}
