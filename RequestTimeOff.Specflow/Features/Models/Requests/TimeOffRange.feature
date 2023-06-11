Feature: TimeOffRange

Return hours based off a TimeOffRange

@TimeOff
Scenario: Return hours based off a TimeOffRange of All Day
	Given I have a TimeOffRange of All Day
	When I call the GetHours method
	Then I should get 8 hours

@TimeOff
Scenario: Return hours based off a TimeOffRange of AM
Given I have a TimeOffRange of AM
	When I call the GetHours method
	Then I should get 4 hours

@TimeOff
Scenario: Return hours based off a TimeOffRange of PM
Given I have a TimeOffRange of PM
	When I call the GetHours method
	Then I should get 4 hours

@TimeOff
Scenario: Return hours based off a TimeOffRange of None
Given I have a TimeOffRange of None
	When I call the GetHours method
	Then I should get 0 hours
