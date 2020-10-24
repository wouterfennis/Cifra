using System;
using System.Collections.Generic;
using System.Text;

namespace Cifra.FileSystem.FileEntity
{
    internal class Question
    {
        public byte MaximalScore { get; set; }
        public IEnumerable<string> QuestionNames { get; set; }
    }
}
