Feature: Template feature for testing Rest APIs

# To run these tests: 
#	* Install Node
#	* run npm install -g json-server
#   * on command line, cd into the JsonServer directory in this project
#	* run json-server --watch db.json
# Tests can now be run through visual studio. 
# Avoid checking in db.json file if you are running these tests. This project is purely to demonstrate the framework

@LocalJsonServerPost
Scenario: Test Simple API Post request
	When I create a new blog post:
	| Field  | Value          |
	| Title  | CreateTestPost |
	| Author | Tester1        |
	Then the 'Post' response should be received in 5000 milliseconds
	And the 'Post' response status code should be 201

@LocalJsonServerGet
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

@LocalJsonServerDelete
Scenario: Test Simple API Delete request
	Given I have created a new blog post:
	| Field  | Value          |
	| Title  | DeleteTestPost |
	| Author | Tester3        |
	When I delete the created blog post
	Then the 'Delete' response should be received in 5000 milliseconds
	And the 'Delete' response status code should be 200

Scenario: Test simple Get with Oauth
	Given I have authenticated with the application
	When I get a blog post from an authorised endpoint
	Then the 'Get' response status code should be 200
