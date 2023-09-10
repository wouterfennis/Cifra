using Cifra.Api.Client;
using Cifra.Api.IntegrationTests.Builders;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Cifra.Api.IntegrationTests.Steps
{
    [Binding]
    public sealed class TestStepDefinitions
    {
        private readonly ICifraApiClient _apiClient;
        private readonly ScenarioContext _scenarioContext;
        private readonly TestBuilder _testRequestBuilder;
        private const string _testDetailsKey = "testDetails";
        private const string _getTestResponseKey = "getTestResponseKey";

        public TestStepDefinitions(ScenarioContext scenarioContext, TestBuilder testBuilder, ICifraApiClient cifraApiClient)
        {
            _apiClient = cifraApiClient;
            _scenarioContext = scenarioContext;
            _testRequestBuilder = testBuilder;
        }

        [Given(@"a test is previously created")]
        public async Task GivenATestIsPreviouslyCreatedAsync()
        {
            CreateTestRequest createTestRequest = _testRequestBuilder.BuildRandomCreateTestRequest();
            CreateTestResponse createTestResponse = await _apiClient.TestPOSTAsync("1", createTestRequest);

            createTestResponse.TestId.Should().NotBe(0, "No Id was assigned to the test.");
            createTestResponse.ValidationMessages.Should().BeEmpty("No validation messages should be returned during a successfull creation of a test.");

            var testDetails = new TestDetails
            {
                CreateTestRequest = createTestRequest,
                CreateTestResponse = createTestResponse
            };

            _scenarioContext.Add(_testDetailsKey, testDetails);
        }

        [When(@"a request is made to retrieve all tests")]
        public async Task WhenARequestIsMadeToRetrieveAllTestsAsync()
        {
            GetAllTestsResponse result = await _apiClient.TestGETAsync("1");
            _scenarioContext.Add(_getTestResponseKey, result.Tests);
        }

        [Then(@"the previously created test is displayed")]
        public void ThenThePreviouslyCreatedTestIsReturned()
        {
            TestDetails testDetails = _scenarioContext.Get<TestDetails>(_testDetailsKey);
            List<Test> retrievedTests = _scenarioContext.Get<List<Test>>(_getTestResponseKey);

            retrievedTests.Should().ContainSingle();
            var retrievedTest = retrievedTests.Single();
            AssertTest(testDetails, retrievedTest);
        }

        private void AssertTest(TestDetails testDetails, Test retrievedTest)
        {
            testDetails.Should().NotBeNull();
            retrievedTest.Should().NotBeNull();

            retrievedTest.Id.Should().Be(testDetails.CreateTestResponse.TestId);
            retrievedTest.Name.Should().Be(testDetails.CreateTestRequest.Name);
            retrievedTest.StandardizationFactor.Should().Be(testDetails.CreateTestRequest.StandardizationFactor);
            retrievedTest.MinimumGrade.Should().Be(testDetails.CreateTestRequest.MinimumGrade);
            retrievedTest.NumberOfVersions.Should().Be(testDetails.CreateTestRequest.NumberOfVersions);
        }

        private void AssertAssignments(List<Assignment> createdAssignments, List<Assignment> retrievedAssignments)
        {
            foreach (Assignment retrievedAssignment in retrievedAssignments)
            {
                createdAssignments.Should().Contain(x => x.Id == retrievedAssignment.Id && 
                x.NumberOfQuestions == retrievedAssignment.NumberOfQuestions);
            }
        }
    }
}
