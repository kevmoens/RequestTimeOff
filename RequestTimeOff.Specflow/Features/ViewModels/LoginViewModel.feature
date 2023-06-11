Feature: LoginViewModel

Validate Logging In

@Login
Scenario: Invalid Login Username
	Given A login witn an invalid Username
	When Logging In
	Then Login Fails with a message of "Invalid Username"

@Login
Scenario: Invalid Login Password
	Given A login witn an invalid password
	When Logging In
	Then Login Fails with a message of "Invalid Password"

@Login
Scenario: Valid Login as User
	Given A User with a Valid Username and Password 
	When Logging In
	Then Login Navigates to Home Page

@Login
Scenario: Valid Login as Admin
	Given An Admin with a Valid Username and Password
	When Logging In
	Then Login Navigates to Admin Home Page