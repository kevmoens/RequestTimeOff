using FluentAssertions;
using RequestTimeOff.Converters;
using RequestTimeOff.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using TechTalk.SpecFlow;

namespace RequestTimeOff.Specflow.Steps
{
    [Binding]
    public class ConvertersUserCalendarCellBorderColorConverterStepDefinitions
    {
        //Input
        private ObservableCollection<TimeOff> _timeOffs;
        private ObservableCollection<Holiday> _holidays;
        private int? _day;
        //Output
        private Color _color;

        [Given(@"The current day is a holiday")]
        public void GivenTheCurrentDayIsAHoliday()
        {
            _day = 1;
            _holidays = new ObservableCollection<Holiday>
            {
                new Holiday { Date = new DateTime(2019, 1, 1) },
                new Holiday { Date = new DateTime(2019, 1, 2) }
            };
        }

        [When(@"running the converter UserCalendarCellBorderColorConverter")]
        public void WhenRunningTheConverterUserCalendarCellBorderColorConverter()
        {
            var converter = new UserCalendarCellBorderColorConverter();
            _color = ((SolidColorBrush)converter.Convert(new object[] { _timeOffs, _holidays, _day }, null, null, null)).Color;
        }


        [Then(@"the color is ""([^""]*)""")]
        public void ThenTheColorIs(string color)
        {
            _color.Should().Be((Color)ColorConverter.ConvertFromString(color));
        }

        [Given(@"The current day has these time offs:")]
        public void GivenTheCurrentDayHasTheseTimeOffs(Table table)
        {
            _timeOffs = new ObservableCollection<TimeOff>();
            foreach (var row in table.Rows)
            {
                _timeOffs.Add(new TimeOff
                {
                    Type = Enum.Parse<TimeOffType>(row["TimeOffType"]),
                    Date = DateTimeOffset.Parse(row["Date"])
                });
                _day = _timeOffs[0].Date.Day;
            }
        }

        [Given(@"The convert back method gets ran UserCalendarCellBorderColorConverter")]
        public void GivenTheConvertBackMethodGetsRanUserCalendarCellBorderColorConverter()
        {
        }

        [When(@"running the ConvertBack on the UserCalendarCellBorderColorConverter converter")]
        public void WhenRunningTheConvertBackOnTheUserCalendarCellBorderColorConverterConverter()
        {
        }

        [Then(@"the UserCalendarCellBorderColorConverter ConvertBack result is null")]
        public void ThenTheUserCalendarCellBorderColorConverterConvertBackResultIsNull()
        {
            UserCalendarCellBorderColorConverter converter = new();
            var result = converter.ConvertBack(null, null, null, null);
            result.Should().BeEquivalentTo(Array.Empty<object>());

        }
    }
}
