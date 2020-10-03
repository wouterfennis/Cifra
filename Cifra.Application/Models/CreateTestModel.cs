namespace Cifra.Application
{
    public class CreateTestModel
    {
        public string Name { get; }
        public byte StandardizationFactor { get; }
        public byte MinimumGrade { get; }

        public CreateTestModel(string name, byte standardizationsFactor, byte minimumGrade)
        {
            Name = name;
            StandardizationFactor = standardizationsFactor;
            MinimumGrade = minimumGrade;
        }
    }
}