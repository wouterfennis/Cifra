using Cifra.Api.Client;
using Cifra.Api.IntegrationTests.Builders;
using Cifra.Api.IntegrationTests.Models;
using FluentAssertions;
using Mapster;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

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
        private const string _getTestResponseStatusCode = "getTestResponseStatusCode";
        private const string _getTestResponseMessage = "getTestResponseMessage";
        private const string _createTestResponseException = "createTestResponseException";

        public TestStepDefinitions(ScenarioContext scenarioContext, TestBuilder testBuilder, ICifraApiClient cifraApiClient)
        {
            _apiClient = cifraApiClient;
            _scenarioContext = scenarioContext;
            _testRequestBuilder = testBuilder;
        }

        [Given(@"a test is previously created")]
        public async Task GivenATestIsPreviouslyCreatedAsync()
        {
            Client.CreateTestRequest createTestRequest = _testRequestBuilder.BuildRandomCreateTestRequest();
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

        [Given(@"no tests are previously created")]
        public void GivenNoTestsArePreviouslyCreated()
        {
            // No implementation needed.
        }

        [When(@"a request is made to create a new test with the following values:")]
        public async Task WhenARequestIsMadeToCreateANewTestWithTheFollowingValuesAsync(Table table)
        {
            var test = table.CreateInstance<TestModel>();

            var request = test.Adapt<Client.CreateTestRequest>();

            try
            {
                var result = await _apiClient.TestPOSTAsync("1", request);
                _scenarioContext.Add(_getTestResponseKey, result);
            }
            catch (ApiException<CreateTestResponse> exception)
            {
                _scenarioContext.Add(_createTestResponseException, exception);
            }
        }

        [Then(@"the test is persisted with the following values:")]
        public async Task ThenTheTestIsPersistedWithTheFollowingValuesAsync(Table table)
        {
            var tests = table.CreateSet<TestModel>();

            var result = await _apiClient.TestGETAsync("1");

            var actualTests = result.Tests.Adapt<IEnumerable<TestModel>>();
            actualTests.Should().BeEquivalentTo(tests);
        }

        [Then(@"a validation message is displayed containing the following message")]
        public void ThenAValidationMessageIsDisplayedContainingTheFollowingMessage(Table table)
        {
            var expectedValidationMessage = table.CreateInstance<ValidationMessageModel>();
            var exception = _scenarioContext.Get<ApiException<CreateTestResponse>>(_createTestResponseException);
            exception.Result.ValidationMessages.Single().Message.Should().Be(expectedValidationMessage.FailureReason);
        }

        [When(@"a request is made to retrieve all tests")]
        public async Task WhenARequestIsMadeToRetrieveAllTestsAsync()
        {
            try
            {
                GetAllTestsResponse result = await _apiClient.TestGETAsync("1");
                _scenarioContext.Add(_getTestResponseKey, result.Tests);
            }
            catch (ApiException exception)
            {
                _scenarioContext.Add(_getTestResponseStatusCode, exception.StatusCode);
                _scenarioContext.Add(_getTestResponseMessage, exception.Message);
            }
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

        [Then(@"a message is displayed explaining that no tests are present")]
        public void ThenAMessageIsDisplayedExplainingThatNoTestsArePresent()
        {
            List<Test> retrievedTests = _scenarioContext.Get<List<Test>>(_getTestResponseKey);
            retrievedTests.Should().BeEmpty();
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
