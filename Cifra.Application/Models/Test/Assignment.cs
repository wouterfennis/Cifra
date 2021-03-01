using System;

namespace Cifra.Application.Models.Test
{
    /// <summary>
    /// The Assignment entity.
    /// </summary>
    public sealed class Assignment
    {
        /// <summary>
        /// The id of the <see cref="Assignment"/>.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// The number of questions the <see cref="Assignment"/> has.
        /// </summary>
        public int NumberOfQuestions;

        /// <summary>
        /// Ctor
        /// </summary>
        public Assignment(Guid id, int numberOfQuestions)
        {
            Id = id;
            NumberOfQuestions = numberOfQuestions;
        }

        public Assignment(int numberOfQuestions)
        {
            Id = Guid.NewGuid();
            NumberOfQuestions = numberOfQuestions;
        }
    }
}