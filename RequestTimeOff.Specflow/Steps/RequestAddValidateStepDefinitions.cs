using FluentAssertions;
using NSubstitute;
using RequestTimeOff.Models;
using RequestTimeOff.Models.Date;
using RequestTimeOff.Models.Requests;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using Xunit;

namespace RequestTimeOff.Specflow.Steps
{
    [Binding]
    public class RequestAddValidateStepDefinitions
    {
        ValidateAdd _validateAdd;
        ISystemDateTime _systemDateTime = Substitute.For<ISystemDateTime>();
        Session _session;
        IRequestTimeOffRepository _requestTimeOffRepository = Substitute.For<IRequestTimeOffRepository>();

        [Given(@"When creating a request off record")]
        public void GivenWhenCreatingARequestOffRecord()
        {
            _validateAdd = new ValidateAdd(_systemDateTime, _requestTimeOffRepository, _session);
            _systemDateTime.Now().Returns(new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero));
            _requestTimeOffRepository
                .HolidayQuery(h => h.Date == _validateAdd.SelectedDate)
                .ReturnsForAnyArgs(new List<Holiday>());
        }

        [When(@"the date is not set")]
        public void WhenTheDateIsNotSet()
        {
            _validateAdd.SelectedDate = default;
        }


        [When(@"the date is on a holiday")]
        public void WhenTheDateIsOnAHoliday()
        {
            _validateAdd.SelectedDate = new DateTimeOffset(2023, 1, 2, 0, 0, 0, TimeSpan.Zero);
            _systemDateTime.Now().Returns(_validateAdd.SelectedDate);
            var holidays = new List<Holiday>
                    {
                        new Holiday {
                            Date = _systemDateTime.Now(),
                            Name = "New Years"
                        }
                };
            _requestTimeOffRepository
                .HolidayQuery(h => h.Date == _validateAdd.SelectedDate)
                .ReturnsForAnyArgs(holidays);
        }


        [When(@"the date is on a weekend")]
        public void WhenTheDateIsOnAWeekend()
        {
            _validateAdd.SelectedDate = new DateTimeOffset(2023, 1, 7, 0, 0, 0, TimeSpan.Zero);
        }

        [When(@"the date is valid")]
        public void WhenTheDateIsValid()
        {
            _validateAdd.SelectedDate = new DateTimeOffset(2023, 1, 3, 0, 0, 0, TimeSpan.Zero);
        }


        [Then(@"the request returns the error ""([^""]*)""")]
        public void ThenTheRequestReturnsTheError(string message)
        {
            _validateAdd.ValidateInput()
                .Should()
                .Be(message
                    .Replace("<DATE>", _systemDateTime.Now().Date.ToShortDateString())
                 );
        }


    }
}
