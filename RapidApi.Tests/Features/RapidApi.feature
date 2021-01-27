Feature:  Feature for testing RapidApi MarketPlace APIs

@RapidAPI
Scenario: Test Simple API Get request
	Given I have created a new blog post:
	| Field  | Value       |
	| Title  | GetTestPost |
	| Author | Tester2     |
	When I get the created blog post	
	Then the result should be:
	| Field  | Value       |
	| Title  | GetTestPost |
	| Author | Tester2     |
	And the 'Get' response should be received in 5000 milliseconds
	And the 'Get' response status code should be 200


