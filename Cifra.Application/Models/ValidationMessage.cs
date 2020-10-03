namespace Cifra.Application.Models
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