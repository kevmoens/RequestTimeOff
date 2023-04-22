using FluentAssertions;
using NSubstitute;
using RequestTimeOff.Models;
using RequestTimeOff.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TechTalk.SpecFlow;

namespace RequestTimeOff.Specflow.Steps.Models.ViewModels
{
    [Binding]
    public class CalendarViewModelStepDefinitions
    {
        private CalendarViewModel _calendarViewModel;
        private IRequestTimeOffRepository _requestTimeOffRepository = Substitute.For<IRequestTimeOffRepository>();
        public CalendarViewModelStepDefinitions()
        {
            _calendarViewModel = new CalendarViewModel(_requestTimeOffRepository);
        }
        [Given(@"The current date is (.*)st July (.*)")]
        public void GivenTheCurrentDateIsStJuly(int day, int year)
        {
            _calendarViewModel.Month = 7;
            _calendarViewModel.Year = year;
        }

        [Given(@"the holidays are loaded")]
        public void GivenTheHolidaysAreLoaded()
        {
            _requestTimeOffRepository.HolidayQuery(h => true).ReturnsForAnyArgs(
                new List<Holiday>()
                {
                    new Holiday { Date = new DateTimeOffset(2023, 7, 4,0,0,0, TimeSpan.Zero) }
                }
                );
        }

        [Given(@"the time offs are loaded")]
        public void GivenTheTimeOffsAreLoaded()
        {
            _requestTimeOffRepository.TimeOffQuery(t => t.Date == new DateTime(2023, 7, 7)).ReturnsForAnyArgs(callInfo => 
                new List<TimeOff>()
                {
                    new TimeOff { Date = new DateTimeOffset(2023,7,7,0,0,0, TimeSpan.Zero), Type = TimeOffType.Sick },
                    new TimeOff { Date = new DateTimeOffset(2023,7,10,0,0,0, TimeSpan.Zero), Type = TimeOffType.Sick },
                    new TimeOff { Date = new DateTimeOffset(2023,7,10,0,0,0, TimeSpan.Zero), Type = TimeOffType.Vacation },
                    new TimeOff { Date = new DateTimeOffset(2023,7,24,0,0,0, TimeSpan.Zero), Type = TimeOffType.Vacation },
                    new TimeOff { Date = new DateTimeOffset(2023,7,25,0,0,0, TimeSpan.Zero), Type = TimeOffType.Vacation },
                    new TimeOff { Date = new DateTimeOffset(2023,7,26,0,0,0, TimeSpan.Zero), Type = TimeOffType.Vacation },
                    new TimeOff { Date = new DateTimeOffset(2023,7,27,0,0,0, TimeSpan.Zero), Type = TimeOffType.Vacation }
                }
                .Where(callInfo.Arg<Func<TimeOff, bool>>()).ToList()
                );
        }

        [When(@"Loading the month")]
        public void WhenLoadingTheMonth()
        {
            _calendarViewModel.LoadMonth();
        }

        [Then(@"Correct data will be loaded into the calendar collections")]
        public void ThenCorrectDataWillBeLoadedIntoTheCalendarCollections()
        {
            _calendarViewModel.Date10.Should().BeNull();
            _calendarViewModel.Date11.Should().BeNull();
            _calendarViewModel.Date12.Should().BeNull();
            _calendarViewModel.Date13.Should().BeNull();
            _calendarViewModel.Date14.Should().BeNull();
            _calendarViewModel.Date15.Should().BeNull();
            _calendarViewModel.Date16.Should().Be(1);
            _calendarViewModel.Date20.Should().Be(2);
            _calendarViewModel.Date21.Should().Be(3);
            _calendarViewModel.Date22.Should().Be(4);
            _calendarViewModel.Date23.Should().Be(5);
            _calendarViewModel.Date24.Should().Be(6);
            _calendarViewModel.Date25.Should().Be(7);
            _calendarViewModel.Date26.Should().Be(8);
            _calendarViewModel.Date30.Should().Be(9);
            _calendarViewModel.Date31.Should().Be(10);
            _calendarViewModel.Date32.Should().Be(11);
            _calendarViewModel.Date33.Should().Be(12);
            _calendarViewModel.Date34.Should().Be(13);
            _calendarViewModel.Date35.Should().Be(14);
            _calendarViewModel.Date36.Should().Be(15);
            _calendarViewModel.Date40.Should().Be(16);
            _calendarViewModel.Date41.Should().Be(17);
            _calendarViewModel.Date42.Should().Be(18);
            _calendarViewModel.Date43.Should().Be(19);
            _calendarViewModel.Date44.Should().Be(20);
            _calendarViewModel.Date45.Should().Be(21);
            _calendarViewModel.Date46.Should().Be(22);
            _calendarViewModel.Date50.Should().Be(23);
            _calendarViewModel.Date51.Should().Be(24);
            _calendarViewModel.Date52.Should().Be(25);
            _calendarViewModel.Date53.Should().Be(26);
            _calendarViewModel.Date54.Should().Be(27);
            _calendarViewModel.Date55.Should().Be(28);
            _calendarViewModel.Date56.Should().Be(29);
            _calendarViewModel.Date60.Should().Be(30);
            _calendarViewModel.Date61.Should().Be(31);
            _calendarViewModel.TimeOffs10.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs11.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs12.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs13.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs14.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs15.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs16.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs20.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs21.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs22.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs23.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs24.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs25.Should().BeEquivalentTo(GetDateTimeOffs(7));
            _calendarViewModel.TimeOffs26.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs30.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs31.Should().BeEquivalentTo(GetDateTimeOffs(10));
            _calendarViewModel.TimeOffs32.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs33.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs34.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs35.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs36.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs40.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs41.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs42.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs43.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs44.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs45.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs46.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs50.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs51.Should().BeEquivalentTo(GetDateTimeOffs(24));
            _calendarViewModel.TimeOffs52.Should().BeEquivalentTo(GetDateTimeOffs(25));
            _calendarViewModel.TimeOffs53.Should().BeEquivalentTo(GetDateTimeOffs(26));
            _calendarViewModel.TimeOffs54.Should().BeEquivalentTo(GetDateTimeOffs(27));
            _calendarViewModel.TimeOffs55.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs56.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs60.Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs61.Should().BeNullOrEmpty();
        }
        private ObservableCollection<TimeOff> GetDateTimeOffs(int day)
        {
            var timeoffs = _requestTimeOffRepository.TimeOffQuery(t => t.Date == new DateTimeOffset(DateTime.Today));

            return new ObservableCollection<TimeOff>(timeoffs.Where(t=> t.Date == new DateTimeOffset(2023, 7, day,0,0,0,TimeSpan.Zero)));
        }
    }
}
