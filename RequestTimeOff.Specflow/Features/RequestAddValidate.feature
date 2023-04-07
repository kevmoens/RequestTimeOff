Feature: RequestAddValidate

A user that adds a request off record is only allowed to add requests that are validated

Scenario: Date Must be after today
	Given When creating a request off record
	When the date is not set
	Then the request returns the error "Date must be on or after <DATE>"

Scenario: Requesting a single date that is on a holiday
	Given When creating a request off record
	When the date is on a holiday
	Then the request returns the error "You can't request off a holiday."
	
Scenario: Requesting a single date that is on a weekend
	Given When creating a request off record
	When the date is on a weekend
	Then the request returns the error "Request cannot be on a weekend."
		
Scenario: Requesting a single date that is valid
	Given When creating a request off record
	When the date is valid
	Then the request returns the error ""