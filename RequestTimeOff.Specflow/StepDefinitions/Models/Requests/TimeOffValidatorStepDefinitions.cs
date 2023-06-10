using FluentAssertions;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NSubstitute;
using RequestTimeOff.Core.Models.Requests;
using RequestTimeOff.Models;
using RequestTimeOff.Models.Date;
using RequestTimeOff.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace RequestTimeOff.Specflow.StepDefinitions.Models.Requests
{
    [Binding]
    public class TimeOffValidatorStepDefinitions
    {
        //Unit of code tested
        private TimeOffValidator _timeOffValidator;
        private List<TimeOff> _existingRequests;
        //Model being validated
        private TimeOff _timeOff;
        //result being checked
        private string _errorMessage = string.Empty;
        //dependencies
        private ISystemDateTime _systemDate = Substitute.For<ISystemDateTime>();
        private Session _session;
        private IRequestTimeOffRepository _requestTimeOffRepository = Substitute.For<IRequestTimeOffRepository>();

        public TimeOffValidatorStepDefinitions()
        {
            _session = new Session()
            {
                User = new User()
                {
                    Dept = "PROG",
                    FullName = "Test User",
                    Username = "TUser"
                }
            };
        }
        [Given(@"When creating a request off record")]
        public void GivenWhenCreatingARequestOffRecord()
        {
            _systemDate.Now().Returns(new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero));

            var timeOffs = new List<TimeOff>
            {
                new TimeOff {
                    Date = new DateTimeOffset(2023, 1, 3, 0, 0, 0, TimeSpan.Zero),
                    Username = "TUser"
                }
                ,new TimeOff { Date = new DateTimeOffset(2023,1,17,0,0, 0,TimeSpan.Zero),
                    Username = "User2"
                }
                ,new TimeOff { Date = new DateTimeOffset(2023,1,18,0,0, 0,TimeSpan.Zero),
                    Username = "User3"
                }
            };
            _requestTimeOffRepository
                .TimeOffQuery(t => true)
                .ReturnsForAnyArgs(callInfo => timeOffs.Where(callInfo.Arg<Func<TimeOff, bool>>()).ToList());
            var users = new List<User>
            {
                new User {
                    Username = "TUser",
                    Dept = "PROG",
                    VacHrs = 100,
                    SickHrs = 40,
                }
                ,new User {
                    Username = "User2",
                    Dept = "PROG",
                    VacHrs = 80,
                    SickHrs = 30,
                }
                ,new User {
                    Username = "User3",
                    Dept = "NETW",
                    VacHrs = 60,
                    SickHrs = 20,
                }
            };
            _requestTimeOffRepository
                .UserQuery(u => true)
                .ReturnsForAnyArgs(callInfo => users.Where(callInfo.Arg<Func<User, bool>>()).ToList());

            _timeOff = new TimeOff()
            {
                Username = "TUser",
                Date = _systemDate.Now(),
                Range = TimeOffRange.FullDay,
                Type = TimeOffType.Vacation,
                Description = "Family Vacation"
            };

            HolidayQueryForNonHolidays();

            HolidayQueryForNewYearsHoliday();
        }

        private void HolidayQueryForNonHolidays()
        {
            _requestTimeOffRepository
                            .HolidayQuery(h => h.Date == _systemDate.Now())
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


        [Then(@"the request returns the error ""([^""]*)""")]
        public void ThenTheRequestReturnsTheError(string message)
        {
            ValidationResult validationResults;
            try
            {
                _errorMessage = string.Empty;
                _timeOffValidator = new TimeOffValidator(_systemDate, _requestTimeOffRepository, _session);
                validationResults = _timeOffValidator.Validate(_timeOff);
                if (validationResults?.Errors?.Count > 0)
                {
                    _errorMessage = validationResults.Errors[0].ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
            _errorMessage
                .Should()
                .Be(message
                    .Replace("<DATE>", _systemDate.Now().Date.ToShortDateString())
                 );
        }

        [When(@"the date is not set")]
        public void WhenTheDateIsNotSet()
        {
            _timeOff.Date = default;
        }

        [When(@"the date is on a holiday")]
        public void WhenTheDateIsOnAHoliday()
        {
            _timeOff.Date = new DateTimeOffset(2023, 1, 2, 0, 0, 0, TimeSpan.Zero);
        }

        [When(@"the date is on a weekend")]
        public void WhenTheDateIsOnAWeekend()
        {
            _timeOff.Date = new DateTimeOffset(2023, 1, 7, 0, 0, 0, TimeSpan.Zero);
        }

        [When(@"the date is on a weekend Sunday")]
        public void WhenTheDateIsOnAWeekendSunday()
        {
            _timeOff.Date = new DateTimeOffset(2023, 1, 8, 0, 0, 0, TimeSpan.Zero);
        }

        [When(@"the date is valid")]
        public void WhenTheDateIsValid()
        {
            _timeOff.Date = new DateTimeOffset(2023, 1, 4, 0, 0, 0, TimeSpan.Zero);
        }

        [When(@"Another User in the same department also has asked off")]
        public void WhenAnotherUserInTheSameDepartmentAlsoHasAskedOff()
        {
            _timeOff.Date = new DateTimeOffset(2023, 1, 17, 0, 0, 0, TimeSpan.Zero);
        }

        [Given(@"When creating a request off record and validating existing dates")]
        public void GivenWhenCreatingARequestOffRecordAndValidatingExistingDates()
        {
            _systemDate.Now().Returns(new DateTimeOffset(2023, 1, 3, 0, 0, 0, TimeSpan.Zero));
            var users = new List<User>
            {
                new User {
                    Username = "TUser",
                    Dept = "PROG",
                    VacHrs = 100,
                    SickHrs = 40,
                }
                ,new User {
                    Username = "User2",
                    Dept = "PROG",
                    VacHrs = 80,
                    SickHrs = 30,
                }
                ,new User {
                    Username = "User3",
                    Dept = "NETW",
                    VacHrs = 60,
                    SickHrs = 20,
                }
            };
            _requestTimeOffRepository
                .UserQuery(u => true)
                .ReturnsForAnyArgs(callInfo => users.Where(callInfo.Arg<Func<User, bool>>()).ToList());

            _session = new Session()
            {
                User = users[0]
            };

            CurrentTimeOffsOtherThanJan3();
            CurrentTimeOffsForJan3();

        }

        private void CurrentTimeOffsOtherThanJan3()
        {
            _requestTimeOffRepository
                            .TimeOffQuery(h => h.Date == _systemDate.Now())
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
            _timeOff = new TimeOff()
            {
                Username = "TUser",
                Date = _systemDate.Now().AddDays(6),
                Range = TimeOffRange.FullDay,
                Type = TimeOffType.Vacation,
                Description = "Family Vacation"
            };
            _requestTimeOffRepository
                .HolidayQuery(h => true)
                .ReturnsForAnyArgs(new List<Holiday>());

            _existingRequests = new List<TimeOff>
            {
                new TimeOff
                {
                    Date = new DateTimeOffset(2023,1,4,0,0,0,TimeSpan.Zero),
                    Username = "TUser"
                }
            };
        }

        [Then(@"the request dates returns the error ""([^""]*)""")]
        public void ThenTheRequestDatesReturnsTheError(string message)
        {
            ValidationResult validationResults;
            try
            {
                _errorMessage = string.Empty;
                _timeOffValidator = new TimeOffValidator(_systemDate, _requestTimeOffRepository, _session);
                _timeOffValidator.ExistingRequests = _existingRequests;
                validationResults = _timeOffValidator.Validate(_timeOff);
                if (validationResults?.Errors?.Count > 0)
                {
                    _errorMessage = validationResults.Errors[0].ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
            _errorMessage
                .Should()
                .Be(message
                    .Replace("<DATE>", _systemDate.Now().Date.AddDays(1).ToShortDateString())
                 );
        }

        [When(@"the date is a duplicate from the just added request")]
        public void WhenTheDateIsADuplicateFromTheJustAddedRequest()
        {
            _existingRequests = new List<TimeOff>
            {
                new TimeOff
                {
                    Date = new DateTimeOffset(2023,1,4,0,0,0,TimeSpan.Zero),
                    Username = "TUser"
                },
                new TimeOff
                {
                    Date = new DateTimeOffset(2023,1,5,0,0,0,TimeSpan.Zero),
                    Username = "TUser"
                }
            };

            _timeOff = new TimeOff()
            {
                Username = "TUser",
                Date = _systemDate.Now().AddDays(1),
                Range = TimeOffRange.FullDay,
                Type = TimeOffType.Vacation,
                Description = "Family Vacation"
            };

            _requestTimeOffRepository
                .HolidayQuery(h => true)
                .ReturnsForAnyArgs(new List<Holiday>());

        }

        [When(@"the date is a duplicate from a previously added request")]
        public void WhenTheDateIsADuplicateFromAPreviouslyAddedRequest()
        {

            _timeOff = new TimeOff()
            {
                Username = "TUser",
                Date = _systemDate.Now().AddDays(1),
                Range = TimeOffRange.FullDay,
                Type = TimeOffType.Vacation,
                Description = "Family Vacation"
            };

            _requestTimeOffRepository
                .HolidayQuery(h => true)
                .ReturnsForAnyArgs(new List<Holiday>());


            var timeOffs = new List<TimeOff>
            {
                new TimeOff {
                    Date = _systemDate.Now().AddDays(1),
                    Username = "TUser"
                }
            };
            _requestTimeOffRepository
                .TimeOffQuery(t => true)
                .ReturnsForAnyArgs(callInfo => timeOffs.Where(callInfo.Arg<Func<TimeOff, bool>>()).ToList());
        }
        [When(@"the username is not set on the timeoff record")]
        public void WhenTheUsernameIsNotSetOnTheTimeoffRecord()
        {
            _timeOff.Username = null;
        }

        [When(@"the username contains spaces")]
        public void WhenTheUsernameContainsSpaces()
        {
            _timeOff.Username = "T User";
        }

        [When(@"the username contains any control characters")]
        public void WhenTheUsernameContainsAnyControlCharacters()
        {
            _timeOff.Username = "\tTUser";
        }

    }
}
