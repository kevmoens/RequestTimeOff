Feature: UserEditViewModel

Allow editing of user profile information
@tag1
Scenario: When on the User Edit page make sure the password and verify password match before saving	
	Given The user is on the User Edit page 
	When entering a new password of "test"
	And entering a confirm password of "test1"
	And clicking on the save button of the User Edit page
	Then the the User Edit page should display an error message of "Passwords do not match"


Scenario: When adding a new user on the User Edit page make sure the Add User method is ran 
	Given The user is on the User Edit page 
	And the user record on the User Edit page is new
	When clicking on the save button of the User Edit page
	Then the Add User method should be ran

Scenario: When editing an existing user on the User Edit page make sure the Update User method is ran 
	Given The user is on the User Edit page 
	And the user record on the User Edit page is an Existing user
	When clicking on the save button of the User Edit page
	Then the Update User method should be ran