# language=en

Feature: View created tests
As a teacher
I can view my previously created classes
So I can review them

Acceptance Criteria:
- I can see every class I have created in the past
- All the data that each class contains can be reviewed

Scenario: View created class
	Given a class is previously created
	When a request is made to retrieve all classes
	Then the previously created class is displayed

Scenario: No classes are present to be viewed
	Given no classes are previously created
	When a request is made to retrieve all classes
	Then a message is displayed explaining that no classes are present