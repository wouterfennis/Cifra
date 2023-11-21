
namespace Cifra.Api.IntegrationTests.Models
{
    internal class AssignmentModel
    {
        /// <summary>
        /// The id of the assignment.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// The number of questions of the assignment.
        /// </summary>
        public int? NumberOfQuestions { get; init; } = DefaultValues.NumberOfQuestions;
    }
}
