Feature: SettingsViewModel

Allow a user to change their settings

@User
Scenario: When a user changes their settings it will update the password successfully
	Given A User is updating their settings 
	When the user enters their valid existing password on the Settings View
	And the user enters their new password matching the confirmed password on the Settings View
	And the Settings View OnUpdatePassword is ran
	Then the user successfully updates their password for their Settings View	

@User
Scenario: When a user changes their settings and the user doesn't exist
	Given A User is updating their settings 
	When the user doesn't exist for the Settings View
	And the Settings View OnUpdatePassword is ran
	Then the user on the Settings View get the message "Invalid Username"

@User
Scenario: When a user changes their settings and the user has a password that doesn't match the previous password
	Given A User is updating their settings 
	When the user enters an invalid existing password on the Settings View
	And the Settings View OnUpdatePassword is ran
	Then the user on the Settings View get the message "Invalid Password"
		
@User
Scenario: When a user changes their settings and the user has a password that doesn't match the confirmed password
	Given A User is updating their settings 
	When the user enters their valid existing password on the Settings View
	And the user enters their new password that doesn't match the confirmed password on the Settings View
	And the Settings View OnUpdatePassword is ran
	Then the user on the Settings View get the message "New Passwords do not match"