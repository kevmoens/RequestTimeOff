﻿Feature: RequestsTimeOffDateRange

Calculate a range of dates to request off for.

Scenario: Create a request for one day off
	Given A user wants to request 1 day(s) off on "2023/01/03"
	When Calculating days off
	Then The following records are created
| Date       |
| 2023/01/03 |

Scenario: Create a request for multiple days off
	Given A user wants to request 3 day(s) off on "2023/01/03"
	When Calculating days off
	Then The following records are created
| Date       |
| 2023/01/03 |
| 2023/01/04 |
| 2023/01/05 |

Scenario: Create a request for multiple days off that span a weekend
	Given A user wants to request 5 day(s) off on "2023/01/03"
	When Calculating days off
	Then The following records are created
| Date       |
| 2023/01/03 |
| 2023/01/04 |
| 2023/01/05 |
| 2023/01/06 |
| 2023/01/09 |

Scenario: Create a request for multiple days off that span a holiday
	Given A user wants to request 5 day(s) off on "2023/07/03"
	And The following holidays exist
| Date       |
| 2023/07/04 |
	When Calculating days off
	Then The following records are created
| Date       |
| 2023/07/03 |
| 2023/07/05 |
| 2023/07/06 |
| 2023/07/07 |
| 2023/07/10 |



Scenario: Create a request for multiple days off that span multiple holidays
	Given A user wants to request 2 day(s) off on "2023/11/22"
	And The following holidays exist
| Date       |
| 2023/11/23 |
| 2023/11/24 |
	When Calculating days off
	Then The following records are created
| Date       |
| 2023/11/22 |
| 2023/11/27 |