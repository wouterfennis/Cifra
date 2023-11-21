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
- I can change the assignments of a test

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

Scenario: Add assignment to test
	Given a request is made to create a new test with the following values:
		| Name           |
		| Math chapter 1 |
	When the following assignments are added
		| Number of questions |
		| 1                   |
		| 2                   |
	Then the test is persisted with the following assignments:
		| Id | Number of questions |
		| 1  | 1                   |
		| 2  | 2                   |

Scenario: Update existing assignments to test
	Given a request is made to create a new test with the following values:
		| Name           |
		| Math chapter 1 |
	And the following assignments are present
		| Number of questions |
		| 1                   |
		| 2                   |
		| 3                   |
	When the following assignments are updated
		| Id | Number of questions |
		| 1  | 3                   |
		| 0  | 4                   |
	Then the test is persisted with the following assignments:
		| Id | Number of questions |
		| 1  | 3                   |
		| 4  | 4                   |

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
	| Invalid standardization factor | Failure reason                               |
	| -1                             | Standardization factor must be higher than 1 |
	| 0                              | Standardization factor must be higher than 1 |

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
	| Invalid number of versions | Failure reason                                   |
	| -1                         | There should be at least one version of the test |
	| 0                          | There should be at least one version of the test |

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
	| Invalid minimum grade | Failure reason                     |
	| 0                     | Minimum grade must be from 1 to 10 |
