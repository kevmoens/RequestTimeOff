Feature: LoginViewModel

Validate Logging In

Scenario: Invalid Login Username
	Given A login witn an invalid Username
	When Logging In
	Then Login Fails with a message of "Invalid Username"

Scenario: Invalid Login Password
	Given A login witn an invalid password
	When Logging In
	Then Login Fails with a message of "Invalid Password"

Scenario: Valid Login as User
	Given A User with a Valid Username and Password 
	When Logging In
	Then Login Navigates to Home Page

Scenario: Valid Login as Admin
	Given An Admin with a Valid Username and Password
	When Logging In
	Then Login Navigates to Admin Home Page