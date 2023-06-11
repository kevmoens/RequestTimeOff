Feature: HomePagesUserYearInfo

Calculate User Information for the selected Year

@User
Scenario: Changing the year calculates the Year Info
	Given The user changes the year to 2023
	When Running the ChangeYear method
	Then The Year Info is calculated

	
