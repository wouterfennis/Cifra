# language=en

Feature: Create test
As a teacher
I want to create a test
So I can assign a score to different assignments 

Definition of a test:
A series of questions or assignments for measuring the knowledge, intelligence, ability etc. of an individual or group. 

- Name, a custom reference to a test
- Number of versions, a test can have multiple versions. This is to prevent fraud during the execution of a test.
- Standardization factor, the standard which is applied during the calculation of the grade
- Minimum grade, the grade that is earned if no assignments were completed

Acceptance Criteria:
- I can give a name to a test
- I have to declare at least one version of the test
- I cannot set a standardization factor lower than 1
- I cannot set a minimum grade lower than 0 or higher than 10

Scenario: Creating a new test
	Given no tests have been created
	When a request is made to create a new test
	Then the test is persisted
	Examples: 	
	| Name              | Number of versions | Standardization factor | Minimum grade |
	| Math chapter 1    | 1                  | 9                      | 1             |
	| History chapter 2 | 2                  | 9                      | 10            |

Scenario: Creating a new test fails
	Given no tests have been created
	When a request is made to create a new test
	And the test cannot be saved
	Then a message is displayed explaining to try again later
	
Scenario: Creating a new test with invalid values
	Given no tests have been created
	When a request is made to create a new test with the following values:
	Then a validation message is displayed containing '<Failure reason>'
	Examples:
	| Name           | Number of versions | Standardization factor | Minimum grade | Failure reason            |
	| Math chapter 1 | 0                  | 9                      | 1             | No number of versions     |
	| Math chapter 1 | 1                  | 9                      | -1            | Minimum grade is too low  |
	| Math chapter 1 | 1                  | 9                      | 11            | Minimum grade is too high |
