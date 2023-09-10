# language=en

Feature: View created tests
As a teacher
I can view my previously created tests
So I can review them

Acceptance Criteria:
- I can see every test I have created in the past
- All the data that each test contains can be reviewed

Scenario: View created test
	Given a test is previously created
	When a request is made to retrieve all tests
	Then the previously created test is displayed

Scenario: No tests are present to be viewed
	Given no tests are previously created
	When a request is made to retrieve all tests
	Then a message is displayed explaining that no tests are present

Scenario: Tests cannot be retrieved
	Given a test is previously created
	When a request is made to retrieve all tests
	And the tests cannot be retrieved
	Then a message is displayed explaining to try again later