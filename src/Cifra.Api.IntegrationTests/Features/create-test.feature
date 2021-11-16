﻿# language=en

Feature: Create test
As a teacher
I want to create a test
So I can assign a score to different assignments 

Definition of a test:
A series of questions or exercises for measuring the knowledge, intelligence, ability etc. of an individual or group. 

- Name, a custom reference to a test
- Number of versions, a test can have multiple versions. This is to prevent fraud during the execution of a test.
- Standardization factor, the standard which is applied during the calculation of the grade
- Minimum grade, the grade that is earned if no assignments where completed

Acceptance Criteria:
- I can give a name to a test
- I have to declare at least one version of the test
- I cannot set a standardization factor lower then 1
- I cannot set a minimum grade lower than 1 or higher than 10

Scenario: Creating a new test
	Given no tests have been created
	When a request is made to create a new test with the following values:
	Then the test is persisted
	Examples: 	
	| Name           | Number of versions | Standardization factor | Minimum grade |
	| Math chapter 1 | 1                  | 3                      | 1             |
	| Math chapter 1 | 2                  | 2                      | 10            |

Scenario: Creating a new test with invalid values
	Given no tests have been created
	When a request is made to create a new test with the following values:
	Then a validation message is returned containing "<Failure reason>"
	Examples:
	| Name           | Number of versions | Standardization factor | Minimum grade | Failure reason            |
	| Math chapter 1 | 0                  | 3                      | 1             | No number of versions     |
	| Math chapter 1 | 1                  | 3                      | 0             | Minimum grade is too low  |
	| Math chapter 1 | 1                  | 3                      | 11            | Minimum grade is too high |
