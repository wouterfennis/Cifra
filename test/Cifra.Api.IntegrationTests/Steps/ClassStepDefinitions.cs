﻿using Cifra.Api.Client;
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
    public sealed class ClassStepDefinitions
    {
        private readonly ICifraApiClient _apiClient;
        private readonly ScenarioContext _scenarioContext;
        private readonly ClassBuilder _classRequestBuilder;
        private const string _classDetailsKey = "classDetails";
        private const string _getAllClassesResponseKey = "getAllClassesResponseKey";
        private const string _getAllClassesResponseMessage = "getAllClassesResponseMessage";
        private const string _createClassResponseKey = "createClassResponseKey";
        private const string _responseException = "responseException";
        private const string _updateClassResponseKey = "updateClassResponseKey";

        public ClassStepDefinitions(ScenarioContext scenarioContext, ClassBuilder classBuilder, ICifraApiClient cifraApiClient)
        {
            _apiClient = cifraApiClient;
            _scenarioContext = scenarioContext;
            _classRequestBuilder = classBuilder;
        }

        [Given(@"a class is previously created")]
        public async Task GivenAClassIsPreviouslyCreatedAsync()
        {
            CreateClassRequest createClassRequest = _classRequestBuilder.BuildRandomCreateClassRequest();
            CreateClassResponse createClassResponse = await _apiClient.ClassPOSTAsync("1", createClassRequest);

            createClassResponse.ClassId.Should().NotBe(0, "No Id was assigned to the class.");
            createClassResponse.ValidationMessages.Should().BeEmpty("No validation messages should be returned during a successfull creation of a class.");

            var classDetails = new ClassDetails
            {
                CreateClassRequest = createClassRequest,
                CreateClassResponse = createClassResponse
            };

            _scenarioContext.Add(_createClassResponseKey, createClassResponse.ClassId);
            _scenarioContext.Add(_classDetailsKey, classDetails);
        }

        [Given(@"no classes are previously created")]
        public void GivenNoClassArePreviouslyCreated()
        {
            // No implementation needed.
        }

        [When(@"a request is made to delete the class")]
        public async Task WhenARequestIsMadeToDeleteTheClass()
        {
            var @class = await GetCurrentClassAsync();

            var request = new DeleteClassRequest()
            {
                Name = @class.RetrievedClass.Name
            };

            try
            {
                var result = await _apiClient.ClassDELETEAsync("1", request);
            }
            catch (ApiException<DeleteClassResponse> exception)
            {
                _scenarioContext.Add(_responseException, exception);
            }
        }

        [Given(@"a request is made to create a new class with the following values:")]
        [When(@"a request is made to create a new class with the following values:")]
        public async Task WhenARequestIsMadeToCreateANewClassWithTheFollowingValuesAsync(Table table)
        {
            var @class = table.CreateInstance<ClassModel>();

            var request = @class.Adapt<Client.CreateClassRequest>();

            try
            {
                var result = await _apiClient.ClassPOSTAsync("1", request);
                _scenarioContext.Add(_createClassResponseKey, result.ClassId);
            }
            catch (ApiException<CreateClassResponse> exception)
            {
                _scenarioContext.Add(_responseException, exception);
            }
        }

        [When(@"the class name is changed to '([^']*)'")]
        public async Task WhenTheNameIsChangedToAsync(string newName)
        {
            var result = await GetCurrentClassAsync();
            var request = new UpdateClassRequest
            {
                UpdatedClass = result.RetrievedClass
            };
            request.UpdatedClass.Name = newName;

            await UpdateClass(request);
        }

        [When(@"a request is made to retrieve all classes")]
        public async Task WhenARequestIsMadeToRetrieveAllClassesAsync()
        {
            try
            {
                GetAllClassesResponse result = await _apiClient.ClassGETAsync("1");
                _scenarioContext.Add(_getAllClassesResponseKey, result.Classes);
            }
            catch (ApiException exception)
            {
                _scenarioContext.Add(_getAllClassesResponseMessage, exception.Message);
            }
        }

        [Given(@"the following students are present")]
        [When(@"the following students are added")]
        [When(@"the following students are updated")]
        public async Task WhenTheFollowingStudentsAreAddedAsync(Table table)
        {
            var students = table.CreateSet<StudentModel>();

            GetClassResponse result = await GetCurrentClassAsync();
            var request = new UpdateClassRequest
            {
                UpdatedClass = result.RetrievedClass
            };
            request.UpdatedClass.Students = students.Adapt<ICollection<Student>>();

            await UpdateClass(request);
        }

        [Then(@"the class is persisted with the following values:")]
        public async Task ThenTheClassIsPersistedWithTheFollowingValuesAsync(Table table)
        {
            var classes = table.CreateInstance<ClassModel>();

            var result = await GetCurrentClassAsync();

            var actualClass = result.RetrievedClass.Adapt<ClassModel>();
            actualClass.Should().BeEquivalentTo(classes);
        }

        [Then(@"a create class server message is displayed containing the following message")]
        public void ThenAServerMessageIsDisplayedContainingTheFollowingMessage(Table table)
        {
            var expectedValidationMessage = table.CreateInstance<ValidationMessageModel>();
            var exception = _scenarioContext.Get<ApiException<CreateClassResponse>>(_responseException);
            exception.Message.Should().Contain(expectedValidationMessage.FailureReason);
        }

        [Then(@"a update class validation message is returned containing '([^']*)'")]
        public void ThenAUpdateClassValidationMessageIsReturnedContaining(string message)
        {
            var exception = _scenarioContext.Get<ApiException<UpdateClassResponse>>(_responseException);
            exception.Result.ValidationMessages.Single().Message.Should().Be(message);
        }

        [Then(@"the previously created class is displayed")]
        public void ThenThePreviouslyCreatedClassIsReturned()
        {
            ClassDetails classDetails = _scenarioContext.Get<ClassDetails>(_classDetailsKey);
            List<Class> retrievedClasses = _scenarioContext.Get<List<Class>>(_getAllClassesResponseKey);

            retrievedClasses.Should().ContainSingle();
            var retrievedClass = retrievedClasses.Single();
            AssertClass(classDetails, retrievedClass);
        }

        [Then(@"the class no longer exists")]
        public async Task ThenTheClassNoLongerExistsAsync()
        {
            await WhenARequestIsMadeToRetrieveAllClassesAsync();
            List<Class> retrievedClasses = _scenarioContext.Get<List<Class>>(_getAllClassesResponseKey);
            retrievedClasses.Should().BeEmpty();
        }

        [Then(@"a message is displayed explaining that no classes are present")]
        public void ThenAMessageIsDisplayedExplainingThatNoClassesArePresent()
        {
            List<Class> retrievedClasses = _scenarioContext.Get<List<Class>>(_getAllClassesResponseKey);
            retrievedClasses.Should().BeEmpty();
        }

        [Then(@"the class is persisted with the following students:")]
        public async Task ThenTheTestIsPersistedWithTheFollowingStudentsAsync(Table table)
        {
            var expectedStudents = table.CreateSet<StudentModel>();
            GetClassResponse result = await GetCurrentClassAsync();

            result.RetrievedClass.Students.Should().BeEquivalentTo(expectedStudents);
        }

        private async Task<GetClassResponse> GetCurrentClassAsync()
        {
            var id = _scenarioContext.Get<int>(_createClassResponseKey);
            var result = await _apiClient.ClassGET2Async(id, "1");
            return result;
        }

        private async Task UpdateClass(UpdateClassRequest request)
        {
            try
            {
                var response = await _apiClient.ClassPUTAsync("1", request);
                _scenarioContext.Remove(_updateClassResponseKey);
                _scenarioContext.Add(_updateClassResponseKey, response);
            }
            catch (ApiException<UpdateClassResponse> exception)
            {
                _scenarioContext.Add(_responseException, exception);
            }
        }

        private void AssertClass(ClassDetails classDetails, Class retrievedClass)
        {
            classDetails.Should().NotBeNull();
            retrievedClass.Should().NotBeNull();

            retrievedClass.Id.Should().Be(classDetails.CreateClassResponse.ClassId);
            retrievedClass.Name.Should().Be(classDetails.CreateClassRequest.Name);
        }

        private void AssertStudents(List<Student> createdStudents, List<Student> retrievedStudents)
        {
            foreach (Student retrievedStudent in retrievedStudents)
            {
                createdStudents.Should().Contain(x => x.Id == retrievedStudent.Id &&
                x.FirstName == retrievedStudent.FirstName && x.Infix == retrievedStudent.Infix && x.LastName == retrievedStudent.LastName);
            }
        }
    }
}
