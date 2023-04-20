Feature: CurrentYearVisilbilityConverter

Return Visibility.Visible if the current year is the same as the year of the given date, otherwise Visibility.Collapsed.

Scenario: The current year is this year
Given the current year is this year
	When running the converter
	Then the result is "Visible"

Scenario: The current year is not this year
Given the current year is not this year
	When running the converter
	Then the result is "Hidden"


Scenario: The current year is null
Given the current year is null
	When running the converter
	Then the result is "Hidden"

Scenario: The convert back is null
	Given The convert back method gets ran for CurrentYearVisibilityConverter
	When running the ConvertBack on the converter for CurrentYearVisibilityConverter
	Then the CurrentYearVisibilityConverter ConvertBack result is null