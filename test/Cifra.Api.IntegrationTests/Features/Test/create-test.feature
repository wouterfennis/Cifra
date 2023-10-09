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
- I cannot set a minimum grade lower than 1 or higher than 10

Scenario: Creating a new test
	Given no tests are previously created
	When a request is made to create a new test with the following values:
		| Name           | Number of versions | Standardization factor | Minimum grade |
		| Math chapter 1 | 1                  | 9                      | 1             |
	Then the test is persisted with the following values:
		| Name           | Number of versions | Standardization factor | Minimum grade |
		| Math chapter 1 | 1                  | 9                      | 1             |

Scenario: No name is supplied
	Given no tests are previously created
	When a request is made to create a new test with the following values:
		| Name | Number of versions | Standardization factor | Minimum grade |
		|      | 1                  | 9                      | 1             |
	Then a create test validation message is displayed containing the following message
		| Failure reason   |
		| Name is required |

Scenario: No number of versions supplied
	Given no tests are previously created
	When a request is made to create a new test with the following values:
		| Name           | Number of versions | Standardization factor | Minimum grade |
		| Math chapter 1 | 0                  | 9                      | 1             |
	Then a create test validation message is displayed containing the following message
		| Failure reason                              |
		| Number of versions must be higher than zero |

Scenario: Standardization factor is too low
	Given no tests are previously created
	When a request is made to create a new test with the following values:
		| Name           | Number of versions | Standardization factor | Minimum grade |
		| Math chapter 1 | 1                  | 0                      | 1             |
	Then a create test validation message is displayed containing the following message
		| Failure reason                              |
		| Standardization factor must be higher than zero |

Scenario: Minimum grade is too low
	Given no tests are previously created
	When a request is made to create a new test with the following values:
		| Name           | Number of versions | Standardization factor | Minimum grade |
		| Math chapter 2 | 1                  | 9                      | 0             |
	Then a create test validation message is displayed containing the following message
		| Failure reason                     |
		| Minimum grade must be from 1 to 10 |

Scenario: Minimum grade is too high
	Given no tests are previously created
	When a request is made to create a new test with the following values:
		| Name           | Number of versions | Standardization factor | Minimum grade |
		| Math chapter 3 | 1                  | 9                      | 11            |
	Then a create test validation message is displayed containing the following message
		| Failure reason                     |
		| Minimum grade must be from 1 to 10 |
