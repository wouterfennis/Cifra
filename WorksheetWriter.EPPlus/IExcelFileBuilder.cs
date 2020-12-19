using SpreadsheetWriter.Abstractions;
using System.IO;
using System.Threading.Tasks;

namespace SpreadsheetWriter.EPPlus
{
    public interface IExcelFileBuilder
    {
        IExcelFileBuilder CreateNew(FileInfo fileInfo);
        ISpreadsheetWriter CreateSpreadsheetWriter(string name);
        void FillMetadata(Metadata metadata);
        Task SaveAsync();
    }
}