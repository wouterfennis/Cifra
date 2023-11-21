# language=en

Feature: Create class
As a teacher
I want to create a class
So I can assign a score to different students 

Definition of a class:
A set of students. 

- Name, a custom reference to a class
- Students, a class can have multiple students. A student only belongs to one class

Acceptance Criteria:
- I can give a name to a class

Scenario: Creating a new class
	Given no classes are previously created
	When a request is made to create a new class with the following values:
		| Name |
		| H1a  |
	Then the class is persisted with the following values:
		| Name |
		| H1a  |

Scenario: No name is supplied
	Given no classes are previously created
	When a request is made to create a new class with the following values:
		| Name |
		|      |
	Then a create class server message is displayed containing the following message
		| Failure reason                  |
		| Supplied class data was invalid |