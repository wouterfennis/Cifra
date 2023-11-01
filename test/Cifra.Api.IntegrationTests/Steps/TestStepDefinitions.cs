using Cifra.Api.Client;
using Cifra.Api.IntegrationTests.Builders;
using Cifra.Api.IntegrationTests.Models;
using Cifra.TestUtilities;
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
        private const string _getTestResponseMessage = "getTestResponseMessage";
        private const string _createTestResponseKey = "createTestResponseKey";
        private const string _responseException = "responseException";
        private const string _updateTestResponseKey = "updateTestResponseKey";

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

        [When(@"a request is made to delete the test")]
        public async Task WhenARequestIsMadeToDeleteTheTestAsync()
        {
            var test = _scenarioContext.Get<TestDetails>(_testDetailsKey);

            var request = new DeleteTestRequest()
            {
                TestId = test.CreateTestResponse.TestId
            };

            try
            {
                var result = await _apiClient.TestDELETEAsync("1", request);
            }
            catch (ApiException<DeleteTestResponse> exception)
            {
                _scenarioContext.Add(_responseException, exception);
            }
        }

        [Given(@"a request is made to create a new test with the following values:")]
        [When(@"a request is made to create a new test with the following values:")]
        public async Task WhenARequestIsMadeToCreateANewTestWithTheFollowingValuesAsync(Table table)
        {
            var test = table.CreateInstance<TestModel>();

            var request = test.Adapt<Client.CreateTestRequest>();

            try
            {
                var result = await _apiClient.TestPOSTAsync("1", request);
                _scenarioContext.Add(_createTestResponseKey, result.TestId);
            }
            catch (ApiException<CreateTestResponse> exception)
            {
                _scenarioContext.Add(_responseException, exception);
            }
        }

        [Given(@"the following assignments are present")]
        [When(@"the following assignments are added")]
        [When(@"the following assignments are updated")]
        public async Task WhenTheFollowingAssignmentsAreAddedAsync(Table table)
        {
            var assignments = table.CreateSet<AssignmentModel>();

            GetTestResponse result = await GetCurrentTest();
            var request = result.Adapt<UpdateTestRequest>();
            request.Test.Assignments = assignments.Adapt<ICollection<Assignment>>();

            await UpdateTest(request);
        }

        [When(@"the test name is changed to '([^']*)'")]
        public async Task WhenTheNameIsChangedToAsync(string newName)
        {
            GetTestResponse result = await GetCurrentTest();
            result.Test.Name = newName;
            var request = result.Adapt<UpdateTestRequest>();

            await UpdateTest(request);
        }

        [When(@"the number of versions is changed to '([^']*)'")]
        public async Task WhenTheNumberOfVersionsIsChangedToAsync(int newNumberOfVersions)
        {
            GetTestResponse result = await GetCurrentTest();
            result.Test.NumberOfVersions = newNumberOfVersions;

            var request = result.Adapt<UpdateTestRequest>();

            await UpdateTest(request);
        }

        [When(@"the standardization factor is changed to '([^']*)'")]
        public async Task WhenTheStandardizationFactorIsChangedTo(int newMinimumGrade)
        {
            GetTestResponse result = await GetCurrentTest();
            result.Test.StandardizationFactor = newMinimumGrade;

            var request = result.Adapt<UpdateTestRequest>();

            await UpdateTest(request);
        }

        [When(@"the minimum grade is changed to '([^']*)'")]
        public async Task WhenTheMinimumGradeIsChangedTo(int newMinimumGrade)
        {
            GetTestResponse result = await GetCurrentTest();
            result.Test.MinimumGrade = newMinimumGrade;

            var request = result.Adapt<UpdateTestRequest>();

            await UpdateTest(request);
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
                _scenarioContext.Add(_getTestResponseMessage, exception.Message);
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

        [Then(@"a create test validation message is displayed containing the following message")]
        public void ThenAValidationMessageIsDisplayedContainingTheFollowingMessage(Table table)
        {
            var expectedValidationMessage = table.CreateInstance<ValidationMessageModel>();
            var exception = _scenarioContext.Get<ApiException<CreateTestResponse>>(_responseException);
            exception.Result.ValidationMessages.Single().Message.Should().Be(expectedValidationMessage.FailureReason);
        }

        [Then(@"a create test validation message is returned containing '([^']*)'")]
        public void ThenACreateTestValidationMessageIsReturnedContaining(string message)
        {
            var exception = _scenarioContext.Get<ApiException<CreateTestResponse>>(_responseException);
            exception.Result.ValidationMessages.Single().Message.Should().Be(message);
        }

        [Then(@"a update test validation message is returned containing '([^']*)'")]
        public void ThenAUpdateTestValidationMessageIsReturnedContaining(string message)
        {
            var exception = _scenarioContext.Get<ApiException<UpdateTestResponse>>(_responseException);
            exception.Result.ValidationMessages.Single().Message.Should().Be(message);
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

        [Then(@"the test no longer exists")]
        public async Task ThenTheTestNoLongerExistsAsync()
        {
            await WhenARequestIsMadeToRetrieveAllTestsAsync();
            List<Test> retrievedTests = _scenarioContext.Get<List<Test>>(_getTestResponseKey);
            retrievedTests.Should().BeEmpty();
        }

        [Then(@"a message is displayed explaining that no tests are present")]
        public void ThenAMessageIsDisplayedExplainingThatNoTestsArePresent()
        {
            List<Test> retrievedTests = _scenarioContext.Get<List<Test>>(_getTestResponseKey);
            retrievedTests.Should().BeEmpty();
        }

        [Then(@"the test is persisted with the following assignments:")]
        public async Task ThenTheTestIsPersistedWithTheFollowingAssignmentsAsync(Table table)
        {
            var expectedAssignments = table.CreateSet<AssignmentModel>();
            GetTestResponse result = await GetCurrentTest();

            result.Test.Assignments.Should().BeEquivalentTo(expectedAssignments);
        }

        private async Task<GetTestResponse> GetCurrentTest()
        {
            var id = _scenarioContext.Get<int>(_createTestResponseKey);
            var result = await _apiClient.TestGET2Async(id, "1");
            return result;
        }

        private async Task UpdateTest(UpdateTestRequest request)
        {
            try
            {
                var response = await _apiClient.TestPUTAsync("1", request);
                _scenarioContext.Add(_updateTestResponseKey, response);
            }
            catch (ApiException<UpdateTestResponse> exception)
            {
                _scenarioContext.Add(_responseException, exception);
            }
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
