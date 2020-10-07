namespace Cifra.Application.Validation
{
    public class ValidationMessage
    {
        public string Field { get; }
        public string Message { get; }

        public ValidationMessage(string field, string message)
        {
            Field = field;
            Message = message;
        }
    }
}