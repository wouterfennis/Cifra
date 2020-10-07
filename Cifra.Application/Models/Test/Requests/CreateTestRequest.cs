namespace Cifra.Application.Models.Test.Requests
{
    public class CreateTestRequest
    {
        public string Name { get; }
        public byte StandardizationFactor { get; }
        public byte MinimumGrade { get; }

        public CreateTestRequest(string name, byte standardizationsFactor, byte minimumGrade)
        {
            Name = name;
            StandardizationFactor = standardizationsFactor;
            MinimumGrade = minimumGrade;
        }
    }
}