using SpreadsheetWriter.Abstractions;
using System.IO;
using System.Threading.Tasks;

namespace SpreadsheetWriter.Abstractions
{
    public interface ISpreadsheetFileBuilder
    {
        ISpreadsheetFileBuilder CreateNew(FileInfo fileInfo);
        ISpreadsheetWriter CreateSpreadsheetWriter(string name);
        void FillMetadata(Metadata metadata);
        Task SaveAsync();
    }
}