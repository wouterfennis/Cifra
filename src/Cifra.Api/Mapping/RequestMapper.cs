using Cifra.Api.V1.Models.Class.Requests;
using Cifra.Api.V1.Models.Spreadsheet.Requests;
using Cifra.Api.V1.Models.Test.Requests;
using Cifra.Commands;
using Mapster;

namespace Cifra.Api.Mapping
{
    internal static class RequestMapper
    {
        public static CreateClassCommand MapToCommand(this CreateClassRequest input)
        {
            return input.Adapt<CreateClassCommand>();
        }

        public static DeleteClassCommand MapToCommand(this DeleteClassRequest input)
        {
            return input.Adapt<DeleteClassCommand>();
        }

        public static CreateTestCommand MapToCommand(this CreateTestRequest input)
        {
            return input.Adapt<CreateTestCommand>();
        }

        public static CreateTestResultsSpreadsheetCommand MapToCommand(this CreateTestResultsSpreadsheetRequest input)
        {
            return input.Adapt<CreateTestResultsSpreadsheetCommand>();
        }
        
        public static DeleteTestCommand MapToCommand(this DeleteTestRequest input)
        {
            return input.Adapt<DeleteTestCommand>();
        }
    }
}
