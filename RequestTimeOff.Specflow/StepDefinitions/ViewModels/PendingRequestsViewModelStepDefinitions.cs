using FluentAssertions;
using NSubstitute;
using RequestTimeOff.Models;
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

        private bool _timeOffApproved = false;
        private bool _timeOffDeclined = false;
        public PendingRequestsViewModelStepDefinitions()
        {
            _pendingRequestsViewModel = new PendingRequestsViewModel(_requestTimeOffRepository);
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
            _requestTimeOffRepository.TimeOffQuery(Arg.Any<Func<TimeOff, bool>>()).ReturnsForAnyArgs(new System.Collections.Generic.List<TimeOff> { new TimeOff { Approved = false, Declined = false } });
        }

        [When(@"Admin clicks on the Approve button")]
        public void WhenAdminClicksOnTheApproveButton()
        {
            _pendingRequestsViewModel.OnApprove(new TimeOff());
        }

        [Then(@"The request is approved")]
        public void ThenTheRequestIsApproved()
        {
            _timeOffApproved.Should().BeTrue();
        }

        [When(@"Admin clicks on the Deny button")]
        public void WhenAdminClicksOnTheDenyButton()
        {
            _pendingRequestsViewModel.OnDecline(new TimeOff());
        }

        [Then(@"The request is denied")]
        public void ThenTheRequestIsDenied()
        {
            _timeOffDeclined.Should().BeTrue();
        }
    }
}
