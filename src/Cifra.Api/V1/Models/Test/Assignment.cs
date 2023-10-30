namespace Cifra.Api.V1.Models.Test
{
    /// <summary>
    /// The Assignment entity.
    /// </summary>
    public sealed class Assignment
    {
        /// <summary>
        /// The id of the <see cref="Assignment"/>.
        /// </summary>
        public required uint Id { get; init; }

        /// <summary>
        /// The number of questions the <see cref="Assignment"/> has.
        /// </summary>
        public required int NumberOfQuestions { get; init; }
    }
}