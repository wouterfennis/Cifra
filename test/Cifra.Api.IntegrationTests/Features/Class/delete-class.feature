# language=en

Feature: Delete class
As a teacher
I want to delete a class
So I can only see classes that are still being used

Acceptance Criteria:
- The deleted class is no longer stored 

Scenario: Deleting a class
	Given a class is previously created
	When a request is made to delete the class
	Then the class no longer exists