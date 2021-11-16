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