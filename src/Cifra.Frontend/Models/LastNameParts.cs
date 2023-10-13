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
            // Check if the last name has an infix
            foreach (string infix in DutchInfixes.AllInfixes)
            {
                int infixIndex = completeLastName.IndexOf(infix);
                if (infixIndex != -1)
                {
                    var lastName = completeLastName.Substring(0, infixIndex) + completeLastName.Substring(infixIndex + infix.Length);

                    return new LastNameParts(infix, lastName);
                }
            }

            return new LastNameParts(completeLastName);
        }
    }
}
