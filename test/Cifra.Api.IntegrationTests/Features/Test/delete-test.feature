# language=en

Feature: Delete test
As a teacher
I want to delete a test
So I can only see tests that are still being used

Acceptance Criteria:
- I can delete a test

Scenario: Deleting a test
	Given a test is previously created
	When a request is made to delete the test
	Then a message is displayed explaining that no tests are present

Scenario: Test cannot be found
	Given no tests are previously created
	When a request is made to delete the test
	Then a message is displayed explaining to try again later