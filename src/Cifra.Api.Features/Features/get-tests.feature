# language=en

Feature: Get Tests

Scenario: Get tests
	Given a test is previously created
	When a request is made to retrieve all tests
	Then the previously created test is returned