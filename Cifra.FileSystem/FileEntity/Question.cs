using System.Collections.Generic;

namespace Cifra.FileSystem.FileEntity
{
    public sealed class Question
    {
        public byte MaximumScore { get; set; }
        public IEnumerable<string> QuestionNames { get; set; }
    }
}
