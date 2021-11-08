using TechTalk.SpecFlow;

namespace Cifra.Api.Features.Steps
{
    [Binding]
    public sealed class TestStepDefinitions
    {

        private readonly ScenarioContext _scenarioContext;

        public TestStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"a test is previously created")]
        public void GivenATestIsPreviouslyCreated()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"a request is made to retrieve all tests")]
        public void WhenARequestIsMadeToRetrieveAllTests()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the previously created test is returned")]
        public void ThenThePreviouslyCreatedTestIsReturned()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
