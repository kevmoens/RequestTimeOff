Feature: ConvertersHomePageCircularGaugeScaleSweepAngleConverter

Calculate the percentage of the sweep angle of the circular gauge scale.


Scenario: When the hours are 100 and Remain is 0 the sweep angle should be 360
Given the hours are 100
	When the remaining hours are 0
	And the sweep angle is calculated
	Then the sweep angle should be 360

Scenario: When the hours are 100 and Remain is 100 the sweep angle should be 0
Given the hours are 100
	When the remaining hours are 100
	And the sweep angle is calculated
	Then the sweep angle should be 0


Scenario: The convert back is null
	Given The convert back method gets ran for the HomePageCircularGaugeScaleSweepAngleConverter
	When running the ConvertBack on the HomePageCircularGaugeScaleSweepAngleConverter converter
	Then the HomePageCircularGaugeScaleSweepAngleConverter ConvertBack result is null