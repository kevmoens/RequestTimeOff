Feature: ConvertersRTEColorConverter

Display the color of a Request Time Off based off the status

Scenario Outline: Display the correct color for the status
	Given the time off record is a '<TimeOffType>' with a status of '<approved>' and '<declined>'
	When Running the RTEColorConverter
	Then the color should be '<Color>'

	Examples: 
| TimeOffType | approved | declined | Color         |
| Vacation    | false    | false    | Requested     |
| Vacation    | true     | false    | Approved      |
| Vacation    | false    | true     | Declined      |
| Sick        | false    | false    | SickRequested |
| Sick        | true     | false    | SickApproved  |
| Sick        | false    | true     | Declined      |

Scenario: Display the correct color when passing in an incorrect number of parameters
	Given the time off record is bound by two parameters
	When Running the RTEColorConverter with two parameters
	Then the color should be the Default color
	

Scenario: The convert back is null
	Given The convert back method gets ran
	When running the ConvertBack on the converter
	Then the RTEColorConverter ConvertBack result is null