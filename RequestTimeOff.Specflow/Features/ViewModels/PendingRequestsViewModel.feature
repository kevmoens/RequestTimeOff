Feature: PendingRequestsViewModel

Admin can view pending requests to Approve or Deny them.

@TimeOff
Scenario: Admin can approve a pending request
	Given There are pending requests
	When Admin clicks on the Approve button
	Then The request is approved

@TimeOff
Scenario: Admin can deny a pending request
	Given There are pending requests
	When Admin clicks on the Deny button
	Then The request is denied
