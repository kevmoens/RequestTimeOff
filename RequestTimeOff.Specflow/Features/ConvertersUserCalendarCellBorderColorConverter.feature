Feature: ConvertersUserCalendarCellBorderColorConverter

A short summary of the feature


Scenario: The current day is a holiday
	Given The current day is a holiday
	When running the converter UserCalendarCellBorderColorConverter
	Then the color is "Yellow"

	
Scenario: The current day I do not have off
	Given The current day has these time offs:
	| TimeOffType | Date       |
	When running the converter UserCalendarCellBorderColorConverter
	Then the color is "Transparent"

Scenario: The current day I have sick time off
	Given The current day has these time offs:
	| TimeOffType | Date       |
	| Sick        | 2023-04-14 |
	When running the converter UserCalendarCellBorderColorConverter
	Then the color is "Red"


Scenario: The current day I have vacation time off
	Given The current day has these time offs:
	| TimeOffType | Date       |
	| Vacation    | 2023-04-14 |
	When running the converter UserCalendarCellBorderColorConverter
	Then the color is "LightGreen"


Scenario: The current day I have vacation and Sick time off
	Given The current day has these time offs:
	| TimeOffType | Date       |
	| Vacation    | 2023-04-14 |
	| Sick        | 2023-04-14 |
	When running the converter UserCalendarCellBorderColorConverter
	Then the color is "Purple"

Scenario: The convert back is null
	Given The convert back method gets ran
	When running the ConvertBack on the converter
	Then the UserCalendarCellBorderColorConverter ConvertBack result is null