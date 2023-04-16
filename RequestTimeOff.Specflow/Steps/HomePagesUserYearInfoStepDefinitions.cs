using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using RequestTimeOff.Models;
using RequestTimeOff.Models.HomePages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RequestTimeOff.Specflow.Steps
{
    [Binding]
    public class HomePagesUserYearInfoStepDefinitions
    {
        private Session _session;
        private readonly IRequestTimeOffRepository _requestTimeOffRepository = Substitute.For<IRequestTimeOffRepository>();
        private readonly ILogger<UserYearInfo> _logger = Substitute.For<ILogger<UserYearInfo>>();

        private int _year;
        private IUserYearInfo _userInfo;
        [Given(@"The user changes the year to (.*)")]
        public void GivenTheUserChangesTheYearTo(int year)
        {
            _year = year;
        }

        [When(@"Running the ChangeYear method")]
        public async Task WhenRunningTheChangeYearMethod()
        {
            _session = new Session
            {
                User = new User()
            };
            _session.User.SickHrs = 40;
            _session.User.VacHrs = 200;

            var timeOffs = new List<TimeOff>
            {
                new TimeOff {
                    Date = new DateTimeOffset(2023, 1, 3, 0, 0, 0, TimeSpan.Zero),
                    Range = TimeOffRange.FullDay,
                    Type = TimeOffType.Sick
                },
                new TimeOff
                {
                    Date = new DateTimeOffset(2023, 1, 4, 0, 0, 0, TimeSpan.Zero),
                    Range = TimeOffRange.FullDay,
                    Type = TimeOffType.Vacation
                },
                new TimeOff
                {
                    Date = new DateTimeOffset(2023, 1, 5, 0, 0, 0, TimeSpan.Zero),
                    Range = TimeOffRange.AM,
                    Type = TimeOffType.Vacation
                },
                new TimeOff
                {
                    Date = new DateTimeOffset(2023, 1, 6, 0, 0, 0, TimeSpan.Zero),
                    Range = TimeOffRange.PM,
                    Type = TimeOffType.Vacation
                },
                new TimeOff
                {
                    Date = new DateTimeOffset(2023, 1, 9, 0, 0, 0, TimeSpan.Zero),
                    Range = TimeOffRange.AM,
                    Type = TimeOffType.Sick
                },
                new TimeOff
                {
                    Date = new DateTimeOffset(2023, 1, 10, 0, 0, 0, TimeSpan.Zero),
                    Range = TimeOffRange.PM,
                    Type = TimeOffType.Sick
                },
                new TimeOff
                {
                    Date = new DateTimeOffset(2023, 1, 11, 0, 0, 0, TimeSpan.Zero),
                    Range = TimeOffRange.PM,
                    Type = TimeOffType.Sick
                }
            };
            var startDate = new DateTimeOffset(_year, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var endDate = new DateTimeOffset(_year, 12, 31, 0, 0, 0, TimeSpan.Zero);
            var timeOffFilter = UserYearInfo.TimeOffFilterByRange(startDate, endDate);
            _requestTimeOffRepository
                .TimeOffQuery(timeOffFilter)
                .Returns(timeOffs);

            _userInfo = new UserYearInfo(_session, _requestTimeOffRepository, _logger);
            await _userInfo.ChangeYear(_year);
        }

        [Then(@"The Year Info is calculated")]
        public void ThenTheYearInfoIsCalculated()
        {
            _userInfo.Year.Should().Be(_year);
            _userInfo.SickRemain.Should().Be(20);
            _userInfo.VacRemain.Should().Be(184);
        }
    }
}
