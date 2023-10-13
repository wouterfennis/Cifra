# language=en

Feature: Update test
As a teacher
I want to update a test
So I apply changes after feedback on the test was received

Definition of a test:
A series of questions or assignments for measuring the knowledge, intelligence, ability etc. of an individual or group. 

Acceptance Criteria:
- I can change the name
- I can change the number of verions that a test has
- I can change the standardization factor a test has 
- I can change the minimum grade a test has

Scenario: Updating the name of a test
	Given a request is made to create a new test with the following values:
		| Name           |
		| Math chapter 1 |
	When the test name is changed to 'Math chapter 1 revision'
	Then the test is persisted with the following values:
		| Name                    |
		| Math chapter 1 revision |

Scenario: Updating the number of versions of a test
	Given a request is made to create a new test with the following values:
		| Number of versions |
		| 1                  |
	When the number of versions is changed to '2'
	Then the test is persisted with the following values:
		| Number of versions |
		| 2                  |

Scenario: Updating the standardization factor of a test
	Given a request is made to create a new test with the following values:
		| Standardization factor |
		| 1                      |
	When the standardization factor is changed to '2'
	Then the test is persisted with the following values:
		| Standardization factor |
		| 2                      |

Scenario: Updating the minimum grade of a test
	Given a request is made to create a new test with the following values:
		| Minimum grade |
		| 1             |
	When the minimum grade is changed to '2'
	Then the test is persisted with the following values:
		| Minimum grade |
		| 2             |

Scenario: Updating the standardization factor of a test with a invalid number
	Given a request is made to create a new test with the following values:
		| Name           | Number of versions | Standardization factor | Minimum grade |
		| Math chapter 1 | 1                  | 9                      | 1             |
	When the standardization factor is changed to '<Invalid standardization factor>'
	Then a update test validation message is returned containing '<Failure reason>'
	Then the test is persisted with the following values:
		| Name           | Number of versions | Standardization factor | Minimum grade |
		| Math chapter 1 | 1                  | 9                      | 1             |
Examples:
	| Invalid standardization factor | Failure reason                                  |
	| -1                             | Standardization factor must be higher than zero |
	| 0                              | Standardization factor must be higher than zero |

Scenario: Updating the number of versions of a test with a invalid number
	Given a request is made to create a new test with the following values:
		| Name           | Number of versions | Standardization factor | Minimum grade |
		| Math chapter 1 | 1                  | 9                      | 1             |
	When the number of versions is changed to '<Invalid number of versions>'
	Then a update test validation message is returned containing '<Failure reason>'
	Then the test is persisted with the following values:
		| Name           | Number of versions | Standardization factor | Minimum grade |
		| Math chapter 1 | 1                  | 9                      | 1             |
Examples:
	| Invalid number of versions | Failure reason                              |
	| -1                         | Number of versions must be higher than zero |
	| 0                          | Number of versions must be higher than zero |

Scenario: Updating the minimum grade of a test with a invalid number
	Given a request is made to create a new test with the following values:
		| Name           | Number of versions | Standardization factor | Minimum grade |
		| Math chapter 1 | 1                  | 9                      | 1             |
	When the minimum grade is changed to '<Invalid minimum grade>'
	Then a update test validation message is returned containing '<Failure reason>'
	Then the test is persisted with the following values:
		| Name           | Number of versions | Standardization factor | Minimum grade |
		| Math chapter 1 | 1                  | 9                      | 1             |
Examples:
	| Invalid minimum grade | Failure reason                         |
	| 0                     | Minimum grade must be between 1 and 10 |
