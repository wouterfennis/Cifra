using Cifra.Api.V1.Models.Class.Requests;
using Cifra.Api.V1.Models.Spreadsheet.Requests;
using Cifra.Api.V1.Models.Test.Requests;
using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Spreadsheet.Commands;
using Cifra.Application.Models.Test.Commands;
using Mapster;

namespace Cifra.Api.Mapping
{
    internal static class RequestMapper
    {
        public static CreateClassCommand MapToCommand(this CreateClassRequest input)
        {
            return input.Adapt<CreateClassCommand>();
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
