Feature: HomePagesUserYearInfo

A short summary of the feature

@tag1
Scenario: Changing the year calculates the Year Info
	Given The user changes the year to 2023
	When Running the ChangeYear method
	Then The Year Info is calculated