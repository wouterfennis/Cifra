using Cifra.Application.Models.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cifra.Application.Extensions
{
    /// <summary>
    /// Extensions for the Name type
    /// </summary>
    internal static class NameExtensions
    {
        /// <summary>
        /// Converts a list of strings to a list of Names
        /// </summary>
        public static IEnumerable<Name> ToNames(this IEnumerable<string> input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            return input.Select(x => Name.CreateFromString(x));
        }
    }
}
