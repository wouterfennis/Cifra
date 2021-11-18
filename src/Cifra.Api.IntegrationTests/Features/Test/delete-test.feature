# language=en

Feature: Delete test
As a teacher
I want to delete a test
So I can only see tests that are still being used

Acceptance Criteria:
- I can delete a test

Scenario: Deleting a test
	Given a test is already created
	When a request is made to delete the test
	Then the test is deleted

Scenario: Deleting a test fails
	Given a test is already created
	When a request is made to delete the test
	And the deletion fails
	Then a message is displayed explaining to try again later