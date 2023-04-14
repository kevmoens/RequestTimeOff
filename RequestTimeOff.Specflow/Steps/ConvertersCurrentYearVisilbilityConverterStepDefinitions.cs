using FluentAssertions;
using RequestTimeOff.Converters;
using System;
using System.Windows;
using TechTalk.SpecFlow;

namespace RequestTimeOff.Specflow.Steps
{
    [Binding]
    public class SConvertersCurrentYearVisilbilityConverterStepDefinitions
    {
        private object _currentYear;
        private Visibility _visibility;

        [Given(@"the current year is this year")]
        public void GivenTheCurrentYearIsThisYear()
        {
            _currentYear = DateTime.Now.Year;
        }

        [Given(@"the current year is not this year")]
        public void GivenTheCurrentYearIsNotThisYear()
        {
            _currentYear = DateTime.Now.AddYears(1).Year;
        }

        [Given(@"the current year is null")]
        public void GivenTheCurrentYearIsNull()
        {
            _currentYear = null;
        }

        [When(@"running the converter")]
        public void WhenRunningTheConverter()
        {
            var converter = new CurrentYearVisibilityConverter();
            _visibility = (Visibility)converter.Convert(_currentYear, typeof(Visibility), null, System.Globalization.CultureInfo.InvariantCulture);
        }


        [Then(@"the result is ""([^""]*)""")]
        public void ThenTheResultIs(Visibility expectedVisibility)
        {
            //Visibility expected = (Visibility)Enum.Parse(typeof(Visibility), expectedVisibility);
            _visibility.Should().Be(expectedVisibility);
        }

        [Given(@"The convert back method gets ran")]
        public void GivenTheConvertBackMethodGetsRan()
        {
        }

        [When(@"running the ConvertBack on the converter")]
        public void WhenRunningTheConvertBackOnTheConverter()
        {
        }

        [Then(@"the CurrentYearVisibilityConverter ConvertBack result is null")]
        public void ThenTheCurrentYearVisibilityConverterConvertBackResultIsNull()
        {
            var converter = new CurrentYearVisibilityConverter();
            var result = converter.ConvertBack(null, null, null, null);
            result.Should().BeNull();
        }


    }
}
