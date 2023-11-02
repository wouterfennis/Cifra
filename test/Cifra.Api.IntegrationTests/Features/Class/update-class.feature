# language=en

Feature: Update class
As a teacher
I want to update a class
So I apply changes after feedback on the class was received

Definition of a class:
A set of students

Acceptance Criteria:
- I can change the name
- I can change the students

Scenario: Updating the name of a class
	Given a request is made to create a new class with the following values:
		| Name |
		| H1a  |
	When the class name is changed to 'H2a'
	Then the class is persisted with the following values:
		| Name |
		| H2a  |

Scenario: Add student to class
	Given a request is made to create a new class with the following values:
		| Name |
		| H1a  |
	When the following students are added
		| Firstname | Infix | Lastname |
		| John      | the   | Doe      |
	Then the class is persisted with the following students:
		| Firstname | Infix | Lastname |
		| John      | the   | Doe      |

Scenario: Update existing students to class
	Given a request is made to create a new class with the following values:
		| Name |
		| H2a  |
	And the following students are present
		| Firstname | Infix | Lastname |
		| John      | the   | Doe      |
		| Bob       | the   | Builder  |
	When the following students are updated
		| Id | Firstname | Infix | Lastname |
		| 1  | John      | the   | Doep     |
		| 0  | Sam       | of    | Land     |
	Then the class is persisted with the following students:
		| Id | Number of questions |
		| 1  | 3                   |
		| 4  | 4                   |