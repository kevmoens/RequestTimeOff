using FluentAssertions;
using NSubstitute;
using RequestTimeOff.Models;
using RequestTimeOff.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace RequestTimeOff.Specflow.Steps.Models.Requests
{
    [Binding]
    public class TimeOffDateRangeStepDefinitions
    {
        //Dependencies
        private readonly IRequestTimeOffRepository _requestTimeOffRepository = Substitute.For<IRequestTimeOffRepository>();

        //Converter
        private TimeOffDateRange _timeOffDateRange;

        //Output
        private List<DateTimeOffset> _result;
        [Given(@"A user wants to request (.*) day\(s\) off on ""([^""]*)""")]
        public void GivenAUserWantsToRequestDaySOffOn(int reoccurance, DateTimeOffset selectedDate)
        {
            _timeOffDateRange = new TimeOffDateRange(_requestTimeOffRepository)
            {
                SelectedDate = selectedDate,
                Reoccurance = reoccurance
            };
            _requestTimeOffRepository
                .HolidayQuery(h => h.Date == DateTimeOffset.Now)
                .ReturnsForAnyArgs(new List<Holiday>());
        }

        [Given(@"The following holidays exist")]
        public void GivenTheFollowingHolidaysExist(Table table)
        {

            _requestTimeOffRepository
                .HolidayQuery(Arg.Is(TimeOffDateRange.HolidayFilterBySelectedDate(_timeOffDateRange.SelectedDate)))
                .Returns(
                    (from row in table.Rows
                     select new Holiday()
                     {
                         Date = DateTimeOffset.Parse(row["Date"])
                     }).ToList()
                 );
        }

        [When(@"Calculating days off")]
        public void WhenCalculatingDaysOff()
        {
            _result = _timeOffDateRange.GetDates();
        }

        [Then(@"The following records are created")]
        public void ThenTheFollowingRecordsAreCreated(Table table)
        {
            List<DateTimeOffset> expected_result = new();
            foreach (var row in table.Rows)
            {
                expected_result.Add(DateTimeOffset.Parse(row["Date"]));
            }

            _result.Should().BeEquivalentTo(expected_result);
        }

    }
}
