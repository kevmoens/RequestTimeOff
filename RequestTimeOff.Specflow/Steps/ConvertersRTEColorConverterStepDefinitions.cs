using FluentAssertions;
using RequestTimeOff.Converters;
using RequestTimeOff.Models;
using RequestTimeOff.Models.HomePages;
using System;
using System.Windows.Media;
using TechTalk.SpecFlow;

namespace RequestTimeOff.Specflow.Steps
{
    [Binding]
    public class ConvertersRTEColorConverterStepDefinitions
    {
        private RTEColorBrushes _colors = new();
        private TimeOffType _timeOffType;
        private bool _approved;
        private bool _declined;
        private SolidColorBrush _colorBrush;

        [Given(@"the time off record is a '([^']*)' with a status of '([^']*)' and '([^']*)'")]
        public void GivenTheTimeOffRecordIsAWithAStatusOfAnd(TimeOffType timeOffType, bool approved, bool declined)
        {
            _timeOffType = timeOffType;
            _approved = approved;
            _declined = declined;
        }
               
        [Given(@"the time off record is bound by two parameters")]
        public void GivenTheTimeOffRecordIsBoundByTwoParameters()
        {            
        }

        [When(@"Running the RTEColorConverter with two parameters")]
        public void WhenRunningTheRTEColorConverterWithTwoParameters()
        {
            var converter = new RTEColorConverter();
            _colorBrush = (SolidColorBrush)converter.Convert(new object[] { _approved, _declined }, null, null, null);
        }

        [When(@"Running the RTEColorConverter")]
        public void WhenRunningTheRTEColorConverter()
        {
            var converter = new RTEColorConverter();
            _colorBrush = (SolidColorBrush)converter.Convert(new object[] { _timeOffType, _approved, _declined }, null, null, null);
        }


        [Then(@"the color should be '([^']*)'")]
        public void ThenTheColorShouldBe(RTEColorNames expectedBrush)
        {
            _colorBrush.Color.Should().BeEquivalentTo(_colors[expectedBrush.ToString()].Color);  
        }
        [Then(@"the color should be the Default color")]
        public void ThenTheColorShouldBeTheDefaultColor()
        {
            _colorBrush.Color.Should().BeEquivalentTo(_colors["Default"].Color);
        }

        [Given(@"The convert back method gets ran")]
        public void GivenTheConvertBackMethodGetsRan()
        {
        }

        [When(@"running the ConvertBack on the converter")]
        public void WhenRunningTheConvertBackOnTheConverter()
        {
        }

        [Then(@"the RTEColorConverter ConvertBack result is null")]
        public void ThenTheRTEColorConverterConvertBackResultIsNull()
        {
            RTEColorConverter converter = new();
            var result = converter.ConvertBack(null, null, null, null);
            result.Should().BeNull();

        }

    }
}
