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
            _calendarViewModel.Dates[0].Should().BeNull();
            _calendarViewModel.Dates[1].Should().BeNull();
            _calendarViewModel.Dates[2].Should().BeNull();
            _calendarViewModel.Dates[3].Should().BeNull();
            _calendarViewModel.Dates[4].Should().BeNull();
            _calendarViewModel.Dates[5].Should().BeNull();
            _calendarViewModel.Dates[6].Should().Be(1);
            _calendarViewModel.Dates[7].Should().Be(2);
            _calendarViewModel.Dates[8].Should().Be(3);
            _calendarViewModel.Dates[9].Should().Be(4);
            _calendarViewModel.Dates[10].Should().Be(5);
            _calendarViewModel.Dates[11].Should().Be(6);
            _calendarViewModel.Dates[12].Should().Be(7);
            _calendarViewModel.Dates[13].Should().Be(8);
            _calendarViewModel.Dates[14].Should().Be(9);
            _calendarViewModel.Dates[15].Should().Be(10);
            _calendarViewModel.Dates[16].Should().Be(11);
            _calendarViewModel.Dates[17].Should().Be(12);
            _calendarViewModel.Dates[18].Should().Be(13);
            _calendarViewModel.Dates[19].Should().Be(14);
            _calendarViewModel.Dates[20].Should().Be(15);
            _calendarViewModel.Dates[21].Should().Be(16);
            _calendarViewModel.Dates[22].Should().Be(17);
            _calendarViewModel.Dates[23].Should().Be(18);
            _calendarViewModel.Dates[24].Should().Be(19);
            _calendarViewModel.Dates[25].Should().Be(20);
            _calendarViewModel.Dates[26].Should().Be(21);
            _calendarViewModel.Dates[27].Should().Be(22);
            _calendarViewModel.Dates[28].Should().Be(23);
            _calendarViewModel.Dates[29].Should().Be(24);
            _calendarViewModel.Dates[30].Should().Be(25);
            _calendarViewModel.Dates[31].Should().Be(26);
            _calendarViewModel.Dates[32].Should().Be(27);
            _calendarViewModel.Dates[33].Should().Be(28);
            _calendarViewModel.Dates[34].Should().Be(29);
            _calendarViewModel.Dates[35].Should().Be(30);
            _calendarViewModel.Dates[36].Should().Be(31);
            _calendarViewModel.TimeOffs[0].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[1].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[2].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[3].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[4].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[5].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[6].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[7].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[8].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[9].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[10].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[11].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[12].Should().BeEquivalentTo(GetDateTimeOffs(7));
            _calendarViewModel.TimeOffs[13].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[14].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[15].Should().BeEquivalentTo(GetDateTimeOffs(10));
            _calendarViewModel.TimeOffs[16].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[17].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[18].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[19].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[20].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[21].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[22].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[23].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[24].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[25].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[26].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[27].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[28].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[29].Should().BeEquivalentTo(GetDateTimeOffs(24));
            _calendarViewModel.TimeOffs[30].Should().BeEquivalentTo(GetDateTimeOffs(25));
            _calendarViewModel.TimeOffs[31].Should().BeEquivalentTo(GetDateTimeOffs(26));
            _calendarViewModel.TimeOffs[32].Should().BeEquivalentTo(GetDateTimeOffs(27));
            _calendarViewModel.TimeOffs[33].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[34].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[35].Should().BeNullOrEmpty();
            _calendarViewModel.TimeOffs[36].Should().BeNullOrEmpty();
            _calendarViewModel.MonthName.Should().Be("July");
        }

        [Then(@"Correct data will be loaded into the calendar collections with no holidays")]
        public void ThenCorrectDataWillBeLoadedIntoTheCalendarCollectionsWithNoHolidays()
        {
            _calendarViewModel.Dates[0].Should().BeNull();
            _calendarViewModel.Dates[1].Should().BeNull();
            _calendarViewModel.Dates[2].Should().BeNull();
            _calendarViewModel.Dates[3].Should().BeNull();
            _calendarViewModel.Dates[4].Should().BeNull();
            _calendarViewModel.Dates[5].Should().Be(1);
            _calendarViewModel.Dates[6].Should().Be(2);
            _calendarViewModel.Dates[7].Should().Be(3);
            _calendarViewModel.Dates[8].Should().Be(4);
            _calendarViewModel.Dates[9].Should().Be(5);
            _calendarViewModel.Dates[10].Should().Be(6);
            _calendarViewModel.Dates[11].Should().Be(7);
            _calendarViewModel.Dates[12].Should().Be(8);
            _calendarViewModel.Dates[13].Should().Be(9);
            _calendarViewModel.Dates[14].Should().Be(10);
            _calendarViewModel.Dates[15].Should().Be(11);
            _calendarViewModel.Dates[16].Should().Be(12);
            _calendarViewModel.Dates[17].Should().Be(13);
            _calendarViewModel.Dates[18].Should().Be(14);
            _calendarViewModel.Dates[19].Should().Be(15);
            _calendarViewModel.Dates[20].Should().Be(16);
            _calendarViewModel.Dates[21].Should().Be(17);
            _calendarViewModel.Dates[22].Should().Be(18);
            _calendarViewModel.Dates[23].Should().Be(19);
            _calendarViewModel.Dates[24].Should().Be(20);
            _calendarViewModel.Dates[25].Should().Be(21);
            _calendarViewModel.Dates[26].Should().Be(22);
            _calendarViewModel.Dates[27].Should().Be(23);
            _calendarViewModel.Dates[28].Should().Be(24);
            _calendarViewModel.Dates[29].Should().Be(25);
            _calendarViewModel.Dates[30].Should().Be(26);
            _calendarViewModel.Dates[31].Should().Be(27);
            _calendarViewModel.Dates[32].Should().Be(28);
            _calendarViewModel.Dates[33].Should().Be(29);
            _calendarViewModel.Dates[34].Should().Be(30);
            _calendarViewModel.Dates[35].Should().Be(31);
            _calendarViewModel.Dates[36].Should().BeNull();
        }

        private ObservableCollection<TimeOff> GetDateTimeOffs(int day)
        {
            var timeoffs = _requestTimeOffRepository.TimeOffQuery(t => t.Date == new DateTimeOffset(DateTime.Today));

            return new ObservableCollection<TimeOff>(timeoffs.Where(t=> t.Date == new DateTimeOffset(2023, 7, day,0,0,0,TimeSpan.Zero)));
        }
    }
}
