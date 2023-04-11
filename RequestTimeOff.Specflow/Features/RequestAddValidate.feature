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
	
Scenario: Requesting a single date that is on a weekend Sunday
	Given When creating a request off record
	When the date is on a weekend Sunday
	Then the request returns the error "Request cannot be on a weekend."
		
Scenario: Requesting a single date that is valid
	Given When creating a request off record
	When the date is valid
	Then the request returns the error ""

Scenario: Validate Existing Requests when request doesn't have an existing date
	Given When creating a request off record and validating existing dates
	When the date is unique
	Then the request dates returns the error ""

Scenario: Validate Existing Requests when just added request has the same date
	Given When creating a request off record and validating existing dates
	When the date is a duplicate from the just added request
	Then the request dates returns the error "Unable to add a duplicate date <DATE>"

Scenario: Validate Existing Requests when previously added request has the same date
	Given When creating a request off record and validating existing dates
	When the date is a duplicate from a previously added request
	Then the request dates returns the error "Unable to add a duplicate date <DATE>"