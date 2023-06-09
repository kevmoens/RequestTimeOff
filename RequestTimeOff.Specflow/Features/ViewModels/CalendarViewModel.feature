﻿Feature: CalendarViewModel

When loading the calendar view model 
Then the calendar collections will be populated with the correct data in the correct order

@Calendar
Scenario: Load the calendar view model
	Given The current date is 1st July 2023
	* the holidays are loaded
	* the time offs are loaded
	When Loading the month
	Then Correct data will be loaded into the calendar collections
	
@Calendar
Scenario: Load the calendar view model with no holidays
	Given The current date is 1st July 2022
	* the holidays are loaded
	* the time offs are loaded
	When Loading the month
	Then Correct data will be loaded into the calendar collections with no holidays
