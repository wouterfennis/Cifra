namespace Cifra.Application.Models.Test.Requests
{
    public class CreateTestRequest
    {
        public string Name { get; set; }
        public byte StandardizationFactor { get; set; }
        public byte MinimumGrade { get; set; }
    }
}