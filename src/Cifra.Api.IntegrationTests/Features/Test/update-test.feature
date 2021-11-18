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
	Given a test is already created with the name 'Math chapter 1'
	When the name is changed to 'Math chapter 1 revision'
	Then the test is updated

Scenario: Updating the name of a test with a invalid name
	Given a test is already created with the name 'Math chapter 1'
	When the name is changed to '<Invalid test name>'
	Then a validation message is returned containing '<Failure reason>'
	And the test not updated
	Examples:
	| Invalid test name | Failure reason                           |
	| ""                | The name of the test should have content |
	| " "               | The name of the test should have content |

Scenario: Updating the number of versions of a test
	Given a test is already created with '2' versions
	When the number of versions is changed to '1'
	Then the test is updated

Scenario: Updating the number of versions of a test with a invalid number
	Given a test is already created with the number of versions of '2'
	When the number of versions is changed to '<Invalid number of versions>'
	Then a validation message is returned containing '<Failure reason>'
	And the test not updated
	Examples:
	| Invalid number of versions | Failure reason                                    |
	| -1                         | The number of versions should be higher than zero |
	| 0                          | The number of versions should be higher than zero |

Scenario: Updating the standardization factor of a test
	Given a test is already created with the standization factor of '2'
	When the standardization factor is changed to '1'
	Then the test is updated

Scenario: Updating the standardization factor of a test with a invalid number
	Given a test is already created with the standization factor of '2'
	When the standardization factor is changed to '<Invalid standardization factor>'
	Then a validation message is returned containing '<Failure reason>'
	And the test not updated
	Examples:
	| Invalid standardization factor | Failure reason                                        |
	| -1                             | The standardization factor should be higher than zero |
	| 0                              | The standardization factor should be higher than zero |

Scenario: Updating the minimum grade of a test
	Given a test is already created with the minimum grade of '2'
	When the minimum grade is changed to '1'
	Then the test is updated

Scenario: Updating the minimum grade of a test with a invalid number
	Given a test is already created with the minimum grade of '2'
	When the minimum grade is changed to '<Invalid minimum grade>'
	Then a validation message is returned containing '<Failure reason>'
	And the test not updated
	Examples:
	| Invalid minimum grade | Failure reason                               |
	| -1                    | The minimum grade should be higher than zero |
	| 0                     | The minimum grade should be higher than zero |