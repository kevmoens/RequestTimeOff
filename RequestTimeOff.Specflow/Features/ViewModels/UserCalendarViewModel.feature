Feature: UserCalendarViewModel

When loading the user calendar view model 
Then the calendar collections will be populated with the correct data in the correct order


Scenario: Load the usercalendar view model
	Given The current date on the usercalendar view model is 1st July 2023 
	And the holidays are loaded usercalendar view model
	And the time offs are loaded usercalendar view model
	When Loading the month usercalendar view model
	Then Correct data will be loaded into the calendar collections usercalendar view model
	
Scenario: Load the calendar view model with no holidays
	Given The current date on the usercalendar is 1st July 2022 usercalendar view model
	And the holidays are loaded usercalendar view model
	And the time offs are loaded usercalendar view model
	When Loading the month usercalendar view model
	Then Correct data will be loaded into the calendar collections with no holidays usercalendar view model
