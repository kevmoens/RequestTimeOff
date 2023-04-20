Feature: WindowStateConverter

A short summary of the feature

Scenario: When the state of the window is unknown
	Given The window state is unknown
	When running the converter WindowStateConverter
	Then the value is "WindowMaximize"

Scenario: When the state of the window is Normal
	Given The window state is normal
	When running the converter WindowStateConverter
	Then the value is "WindowMaximize"
	

Scenario: When the state of the window is Maximize
	Given The window state is maximize
	When running the converter WindowStateConverter
	Then the value is "WindowRestore"
	

Scenario: The convert back is null
	Given The convert back method gets ran WindowStateConverter
	When running the ConvertBack on the WindowStateConverter converter
	Then the WindowStateConverter ConvertBack result is null