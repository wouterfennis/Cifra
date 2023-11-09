namespace Cifra.Commands.Models
{
    /// <summary>
    /// The Assignment entity.
    /// </summary>
    public sealed record Assignment
    {
        /// <summary>
        /// The id of the <see cref="Assignment"/>.
        /// </summary>
        public required uint? Id { get; init; }

        /// <summary>
        /// The number of questions the <see cref="Assignment"/> has.
        /// </summary>
        public required int NumberOfQuestions { get; init; }
    }
}