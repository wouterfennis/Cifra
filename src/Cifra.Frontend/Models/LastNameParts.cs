using Cifra.Frontend.Constants;

namespace Cifra.Frontend.Models
{
    internal class LastNameParts
    {
        public string? Infix { get; init; }
        public string LastName { get; init; }

        private LastNameParts(string? infix, string lastName)
        {
            Infix = infix;
            LastName = lastName;
        }

        private LastNameParts(string lastName)
        {
            LastName = lastName;
        }

        public static LastNameParts Create(string completeLastName)
        {
            string[] parts = completeLastName.Split(' ');

            string? infix = null;
            foreach (var part in parts)
            {
                if (DutchInfixes.AllInfixes.Contains(part))
                {
                    infix += $" {part}";
                }
            }

            if (infix == null)
            {
                return new LastNameParts(completeLastName);
            }
            
            infix = infix.TrimStart();
            string lastName = completeLastName
                .Replace(infix, string.Empty)
                .TrimStart();

            return new LastNameParts(infix, lastName);
        }
    }
}
