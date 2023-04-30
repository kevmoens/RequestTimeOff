using FluentAssertions;
using NSubstitute;
using RequestTimeOff.Models;
using RequestTimeOff.Models.Date;
using RequestTimeOff.Models.Requests;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace RequestTimeOff.Specflow.Steps.Models.Requests
{
    [Binding]
    public class ValidateAddStepDefinitions
    {
        ValidateAdd _validateAdd;
        readonly ISystemDateTime _systemDateTime = Substitute.For<ISystemDateTime>();
        Session _session;
        readonly IRequestTimeOffRepository _requestTimeOffRepository = Substitute.For<IRequestTimeOffRepository>();
        private string _errorMessage = string.Empty;

        [Given(@"When creating a request off record")]
        public void GivenWhenCreatingARequestOffRecord()
        {
            _validateAdd = new ValidateAdd(_systemDateTime, _requestTimeOffRepository, _session);
            _systemDateTime.Now().Returns(new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero));

            HolidayQueryForNonHolidays();

            HolidayQueryForNewYearsHoliday();
        }

        private void HolidayQueryForNonHolidays()
        {
            _requestTimeOffRepository
                            .HolidayQuery(h => h.Date == _systemDateTime.Now())
                            .ReturnsForAnyArgs(new List<Holiday>());
        }

        private void HolidayQueryForNewYearsHoliday()
        {
            var holidays = new List<Holiday>
            {
                new Holiday {
                    Date = new DateTimeOffset(2023, 1, 2, 0, 0, 0, TimeSpan.Zero),
                    Name = "New Years"
                }
            };
            _requestTimeOffRepository
                .HolidayQuery(Arg.Is<Func<Holiday, bool>>(h => h(holidays[0])))
                .Returns(holidays);
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
        }

        [When(@"the date is on a weekend")]
        public void WhenTheDateIsOnAWeekend()
        {
            _validateAdd.SelectedDate = new DateTimeOffset(2023, 1, 7, 0, 0, 0, TimeSpan.Zero);
        }

        [When(@"the date is on a weekend Sunday")]
        public void WhenTheDateIsOnAWeekendSunday()
        {
            _validateAdd.SelectedDate = new DateTimeOffset(2023, 1, 8, 0, 0, 0, TimeSpan.Zero);
        }

        [When(@"the date is valid")]
        public void WhenTheDateIsValid()
        {
            _validateAdd.SelectedDate = new DateTimeOffset(2023, 1, 3, 0, 0, 0, TimeSpan.Zero);
        }

        [Then(@"the request returns the error ""([^""]*)""")]
        public void ThenTheRequestReturnsTheError(string message)
        {
            try
            {
                _validateAdd.ValidateInput();
            }   catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
            _errorMessage
                .Should()
                .Be(message
                    .Replace("<DATE>", _systemDateTime.Now().Date.ToShortDateString())
                 );
        }

        [Given(@"When creating a request off record and validating existing dates")]
        public void GivenWhenCreatingARequestOffRecordAndValidatingExistingDates()
        {
            _session = new Session()
            {
                User = new User()
                {
                    Username = "kevin"
                }
            };
            _validateAdd = new ValidateAdd(_systemDateTime, _requestTimeOffRepository, _session);
            CurrentTimeOffsOtherThanJan3();
            CurrentTimeOffsForJan3();

            _validateAdd.ExistingRequests = new System.Collections.ObjectModel.ObservableCollection<TimeOff>
            {
                new TimeOff
                {
                    Date = new DateTimeOffset(2023,1,4,0,0,0,TimeSpan.Zero),
                    Username = "kevin"
                },
                new TimeOff
                {
                    Date = new DateTimeOffset(2023,1,5,0,0,0,TimeSpan.Zero),
                    Username = "kevin"
                }
            };
        }

        private void CurrentTimeOffsOtherThanJan3()
        {
            _requestTimeOffRepository
                            .TimeOffQuery(h => h.Date == _systemDateTime.Now())
                            .ReturnsForAnyArgs(new List<TimeOff>());
        }

        private void CurrentTimeOffsForJan3()
        {
            var timeOffs = new List<TimeOff>
            {
                new TimeOff {
                    Date = new DateTimeOffset(2023, 1, 3, 0, 0, 0, TimeSpan.Zero),
                    Username = "kevin"
                }
            };
            _requestTimeOffRepository
                .TimeOffQuery(Arg.Is<Func<TimeOff, bool>>(t => t(timeOffs[0])))
                .Returns(timeOffs);
        }

        [When(@"the date is unique")]
        public void WhenTheDateIsUnique()
        {
            _validateAdd.NewDates = new List<DateTimeOffset>
            {
                new DateTimeOffset(2023,1,2,0,0,0,TimeSpan.Zero)
            };
            _systemDateTime.Now().Returns(_validateAdd.NewDates[0]);
        }

        [When(@"the date is a duplicate from the just added request")]
        public void WhenTheDateIsADuplicateFromTheJustAddedRequest()
        {
            _validateAdd.NewDates = new List<DateTimeOffset>
            {
                new DateTimeOffset(2023,1,3,0,0,0,TimeSpan.Zero)
            };
            _systemDateTime.Now().Returns(_validateAdd.NewDates[0]);
        }

        [When(@"the date is a duplicate from a previously added request")]
        public void WhenTheDateIsADuplicateFromAPreviouslyAddedRequest()
        {
            _validateAdd.NewDates = new List<DateTimeOffset>
            {
                new DateTimeOffset(2023,1,4,0,0,0,TimeSpan.Zero)
            };
            _systemDateTime.Now().Returns(_validateAdd.NewDates[0]);

        }

        [Then(@"the request dates returns the error ""([^""]*)""")]
        public void ThenTheRequestDatesReturnsTheError(string message)
        {
            try
            {
                _validateAdd.ValidateDates();
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
            _errorMessage
                .Should()
                .Be(message
                    .Replace("<DATE>", _systemDateTime.Now().Date.ToShortDateString())
                 );
        }

    }
}
