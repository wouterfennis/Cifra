namespace Cifra.Application.Models.Test.Requests
{
    /// <summary>
    /// The request to create an test
    /// </summary>
    public sealed class CreateTestRequest
    {
        /// <summary>
        /// The name of the test
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The standardization factor
        /// </summary>
        public byte StandardizationFactor { get; set; }

        /// <summary>
        /// The minimum grade
        /// </summary>
        public byte MinimumGrade { get; set; }
    }
}