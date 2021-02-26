using System.Collections.Generic;

namespace Cifra.FileSystem.FileEntity
{
    /// <summary>
    /// The question entity.
    /// </summary>
    public sealed class Question
    {
        /// <summary>
        /// The maximum score.
        /// </summary>
        public byte MaximumScore { get; set; }

        /// <summary>
        /// The names of the question.
        /// </summary>
        public IEnumerable<string> QuestionNames { get; set; }
    }
}
