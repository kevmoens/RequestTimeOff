Feature: TimeOffValidator

A user that adds a request off record is only allowed to add requests that are validated

As a user,
I want to add time off requests,
so that I can avoid manually tracking my time off.

Background: Existing Users
Given There are existing users that can request time off
	
@TimeOff
Scenario: Username must be set
	Given When creating a request off record
	And Today is the first
	And Holidays are defined
	And There are existing time off records
	When the username is not set on the timeoff record
	And the request validation occurs
	Then the request returns the error "Username is required."

@TimeOff
Scenario: Date Must be after today
	Given When creating a request off record
	And Today is the first
	And Holidays are defined
	And There are existing time off records
	When the date is not set
	And the request validation occurs
	Then the request returns the error "Date must be on or after <DATE>"

@TimeOff
Scenario: Requesting a single date that is on a holiday
	Given When creating a request off record
	And Today is the first
	And Holidays are defined
	And There are existing time off records
	When the date is on a holiday
	And the request validation occurs
	Then the request returns the error "You can't request off a holiday."
	
@TimeOff
Scenario: Requesting a single date that is on a weekend
	Given When creating a request off record
	And Today is the first
	And Holidays are defined
	And There are existing time off records
	When the date is on a weekend
	And the request validation occurs
	Then the request returns the error "Request cannot be on a weekend."
	
@TimeOff
Scenario: Requesting a single date that is on a weekend Sunday
	Given When creating a request off record
	And Today is the first
	And Holidays are defined
	And There are existing time off records
	When the date is on a weekend Sunday
	And the request validation occurs
	Then the request returns the error "Request cannot be on a weekend."

@TimeOff
Scenario: Requesting a single date that is valid
	Given When creating a request off record
	And Today is the first
	And There are existing time off records
	And Holidays are defined
	When the date is valid
	And the request validation occurs
	Then the request returns the error ""

@TimeOff
Scenario: Validate Existing Requests when request doesn't have an existing date
	Given When creating a request off record and validating existing dates
	And Today is the third
	And Holidays are defined
	And There are existing time off records
	When the date is unique
	And the request validation occurs
	Then the request dates returns the error ""

@TimeOff
Scenario: Validate Existing Requests when just added request has the same date
	Given When creating a request off record and validating existing dates
	And Today is the third
	And Holidays are defined
	And There are existing time off records
	When the date is a duplicate from the just added request
	And the request validation occurs
	Then the request dates returns the error "Unable to add a duplicate date <DATE>"

@TimeOff
Scenario: Validate Existing Requests when previously added request has the same date
	Given When creating a request off record and validating existing dates
	When the date is a duplicate from a previously added request
	And the request validation occurs
	Then the request dates returns the error "Unable to add a duplicate date <DATE>"

@TimeOff
Scenario: Validate that no more than one employee of the same department is off on the same day
	Given When creating a request off record
	And Today is the first
	And Holidays are defined
	And There are existing time off records
	When Another User in the same department also has asked off
	And the request validation occurs
	Then the request returns the error "Two or more employees of the same department for the same day needs supervisor approval."

@TimeOff
Scenario: Username must not contain any spaces
	Given When creating a request off record
	And Today is the first
	And Holidays are defined
	And There are existing time off records
	When the username contains spaces
	And the request validation occurs
	Then the request returns the error "Remove invalid characters for Username."

@TimeOff
Scenario: Username must not contain any control characters
	Given When creating a request off record
	And Today is the first
	And Holidays are defined
	And There are existing time off records
	When the username contains any control characters
	And the request validation occurs
	Then the request returns the error "Remove invalid characters for Username."