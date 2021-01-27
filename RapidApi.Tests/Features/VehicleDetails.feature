Feature: MockAPITest
	I should be able to GET vehicle details from registration

@MockAPITest
Scenario: Vehicle details GET
	When I GET vehicle details
		| TestName            | Application | Authorization | Method | Parameter | ContentType                    | Client_ID                  | Client_Secret              | 
		| VehicleDetailsTest1 | vehicle     | Basic         | GET    | KW55DFM   | application/json;charset=utf-8 | a934bnj3n45ou5i7nln443b54b | kjihi56b1212vghc35b34kb45k | 
		| VehicleDetailsTest2 | vehicle     | Basic         | GET    |           | application/json;charset=utf-8 | a934bnj3n45ou5i7nln443b54b | kjihi56b1212vghc35b34kb45k | 
		| VehicleDetailsTest3 | vehicle     | Basic         | DELETE | KW55DFM   | application/json;charset=utf-8 | a934bnj3n45ou5i7nln443b54b | kjihi56b1212vghc35b34kb45k | 
	Then verify response time
         | TestName            | MaxResponseTime |
         | VehicleDetailsTest1 | 5000            |
         | VehicleDetailsTest2 | 5000            |
         | VehicleDetailsTest3 | 5000            |
	And verify response code
	| TestName            | StatusCode |
	| VehicleDetailsTest1 | 200        |
	| VehicleDetailsTest2 | 404        |
	| VehicleDetailsTest3 | 405        |