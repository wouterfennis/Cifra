namespace Cifra.Application.Models.Test.Commands
{
    /// <summary>
    /// The request to update a test
    /// </summary>
    public sealed class UpdateTestCommand
    {
        /// <summary>
        /// The name of the test
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The number of versions of this test that exist.
        /// </summary>
        public int NumberOfVersions { get; set; }

        /// <summary>
        /// The standardization factor
        /// </summary>
        public int StandardizationFactor { get; set; }

        /// <summary>
        /// The minimum grade
        /// </summary>
        public int MinimumGrade { get; set; }


        // TODO : Add assignments
    }
}