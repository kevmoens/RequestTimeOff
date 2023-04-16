using FluentAssertions;
using RequestTimeOff.Converters;
using System;
using TechTalk.SpecFlow;

namespace RequestTimeOff.Specflow.Steps
{
    [Binding]
    public class ConvertersHomePageCircularGaugeScaleSweepAngleConverterStepDefinitions
    {
        private int _hours;
        private int _remaining;
        private double _sweepAngle;
        [Given(@"the hours are (.*)")]
        public void GivenTheHoursAre(int hours)
        {
            _hours = hours;
        }

        [When(@"the remaining hours are (.*)")]
        public void WhenTheRemainingHoursAre(int remaining)
        {
            _remaining = remaining;
        }

        [When(@"the sweep angle is calculated")]
        public void WhenTheSweepAngleIsCalculated()
        {
            var converter = new HomePageCircularGaugeScaleSweepAngleConverter();
            _sweepAngle = (double)converter.Convert(new object[] { _hours, _remaining }, null, null, null);
        }

        [Then(@"the sweep angle should be (.*)")]
        public void ThenTheSweepAngleShouldBe(double expected)
        {
            _sweepAngle.Should().Be(expected);
        }


        [Given(@"The convert back method gets ran for the HomePageCircularGaugeScaleSweepAngleConverter")]
        public void GivenTheConvertBackMethodGetsRanForTheHomePageCircularGaugeScaleSweepAngleConverter()
        {
        }

        [When(@"running the ConvertBack on the HomePageCircularGaugeScaleSweepAngleConverter converter")]
        public void WhenRunningTheConvertBackOnTheHomePageCircularGaugeScaleSweepAngleConverterConverter()
        {
        }

        [Then(@"the HomePageCircularGaugeScaleSweepAngleConverter ConvertBack result is null")]
        public void ThenTheHomePageCircularGaugeScaleSweepAngleConverterConvertBackResultIsNull()
        {
            HomePageCircularGaugeScaleSweepAngleConverter converter = new();
            var result = converter.ConvertBack(null, null, null, null);
            result.Should().BeEquivalentTo(Array.Empty<object>());
        }
    }
}
