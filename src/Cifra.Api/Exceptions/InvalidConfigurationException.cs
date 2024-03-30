using System;

namespace Cifra.Api.Exceptions
{
    [Serializable]
    internal class InvalidConfigurationException : Exception
    {
        public InvalidConfigurationException()
        {
        }

        public InvalidConfigurationException(string? message) : base(message)
        {
        }

        public InvalidConfigurationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}