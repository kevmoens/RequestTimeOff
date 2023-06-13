using AutoMapper;
using FluentAssertions;
using NSubstitute;
using RequestTimeOff.Core.Models.Requests;
using RequestTimeOff.Core.MVVM;
using RequestTimeOff.Models;
using RequestTimeOff.MVVM;
using RequestTimeOff.ViewModels;
using System;
using TechTalk.SpecFlow;

namespace RequestTimeOff.Specflow.StepDefinitions.ViewModels
{
    [Binding]
    public class PendingRequestsViewModelStepDefinitions
    {
        private PendingRequestsViewModel _pendingRequestsViewModel;
        private IRequestTimeOffRepository _requestTimeOffRepository = Substitute.For<IRequestTimeOffRepository>();
        private IOpenDialog _openDialog = Substitute.For<IOpenDialog>();
        private IMapper _mapper = Substitute.For<IMapper>();
        private INavigationService _navigationService = Substitute.For<INavigationService>();


        TimeOff _request = new TimeOff { Approved = false, Declined = false, Date = DateTimeOffset.Parse("2023/01/03"), Username = "TUser" };
        private bool _timeOffApproved = false;
        private bool _timeOffDeclined = false;
        public PendingRequestsViewModelStepDefinitions()
        {
            _pendingRequestsViewModel = new PendingRequestsViewModel(_requestTimeOffRepository, _openDialog, _mapper, _navigationService);
            _requestTimeOffRepository
                .WhenForAnyArgs(r => r.UpdateTimeOff(Arg.Any<TimeOff>()))
                .Do(r => 
                { 
                    _timeOffApproved = r.Arg<TimeOff>().Approved;
                    _timeOffDeclined = r.Arg<TimeOff>().Declined;
                }); 
        }
        [Given(@"There are pending requests")]
        public void GivenThereArePendingRequests()
        {
            _mapper.Map<PendingTimeOff>(_request).ReturnsForAnyArgs(new PendingTimeOff() { Date = _request.Date, Username = _request.Username });
            _requestTimeOffRepository.TimeOffQuery(Arg.Any<Func<TimeOff, bool>>()).ReturnsForAnyArgs(new System.Collections.Generic.List<TimeOff> { _request, new TimeOff { Approved = true, Declined = false, Date = DateTimeOffset.Parse("2023/01/03"), Username = "TUser2" } });
            _requestTimeOffRepository.UserQuery(Arg.Any<Func<User, bool>>()).ReturnsForAnyArgs(new System.Collections.Generic.List<User> { new User()
            {
                Username = "TUser",
                Dept = "PROG"
            }, new User() { Username = "TUser2", Dept = "PROG" } });
        }

        [When(@"Admin clicks on the Approve button")]
        public void WhenAdminClicksOnTheApproveButton()
        {
            _pendingRequestsViewModel.OnApprove(_request);
        }

        [Then(@"The request is approved")]
        public void ThenTheRequestIsApproved()
        {
            _timeOffApproved.Should().BeTrue();
        }

        [When(@"Admin clicks on the Deny button")]
        public void WhenAdminClicksOnTheDenyButton()
        {
            _pendingRequestsViewModel.OnDecline(_request);
        }

        [Then(@"The request is denied")]
        public void ThenTheRequestIsDenied()
        {
            _timeOffDeclined.Should().BeTrue();
        }
    }
}
