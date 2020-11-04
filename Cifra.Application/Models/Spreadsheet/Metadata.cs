using System;

namespace Cifra.Application.Models.Spreadsheet
{
    public sealed class Metadata
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public DateTime Created { get; set; }
        public string FileName { get; set; }
    }
}