using SpreadsheetWriter.Abstractions;

namespace SpreadsheetWriter.Test
{
    public class TestExcelRange : IExcelRange
    {
        private readonly string _address;

        public TestExcelRange(string address)
        {
            _address = address;
        }

        public string Address => _address;
    }
}
