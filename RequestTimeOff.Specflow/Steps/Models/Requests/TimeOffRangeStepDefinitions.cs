using FluentAssertions;
using RequestTimeOff.Models;
using System;
using TechTalk.SpecFlow;

namespace RequestTimeOff.Specflow.Steps.Models.Requests
{
    [Binding]
    public class TimeOffRangeStepDefinitions
    {
        private TimeOffRange _timeOffRange;
        private int _hours = -1;
        [Given(@"I have a TimeOffRange of All Day")]
        public void GivenIHaveATimeOffRangeOfAllDay()
        {
            _timeOffRange = TimeOffRange.FullDay;
        }

        [When(@"I call the GetHours method")]
        public void WhenICallTheGetHoursMethod()
        {
            _hours = _timeOffRange.Hours();
        }

        [Then(@"I should get (.*) hours")]
        public void ThenIShouldGetHours(int hours)
        {
            _hours.Should().Be(hours);
        }

        [Given(@"I have a TimeOffRange of AM")]
        public void GivenIHaveATimeOffRangeOfAM()
        {
            _timeOffRange = TimeOffRange.AM;
        }

        [Given(@"I have a TimeOffRange of PM")]
        public void GivenIHaveATimeOffRangeOfPM()
        {
            _timeOffRange = TimeOffRange.PM;
        }

        [Given(@"I have a TimeOffRange of None")]
        public void GivenIHaveATimeOffRangeOfNone()
        {
            _timeOffRange = TimeOffRange.None;
        }
    }
}
