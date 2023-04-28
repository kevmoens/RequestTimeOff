using FluentAssertions;
using NSubstitute;
using RequestTimeOff.Models;
using RequestTimeOff.Models.MessageBoxes;
using RequestTimeOff.MVVM;
using RequestTimeOff.ViewModels;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace RequestTimeOff.Specflow.Steps.Models.ViewModels
{
    [Binding]
    public class UserEditViewModelStepDefinitions
    {
        private UserEditViewModel  _userEditViewModel;
        private readonly INavigationService _navigationService = Substitute.For<INavigationService>();
        private readonly IRequestTimeOffRepository _requestTimeOffRepository = Substitute.For<IRequestTimeOffRepository>();
        private readonly IMessageBox _messageBox = Substitute.For<IMessageBox>();

        private bool _navigated;
        private bool _addMethodRan;
        private bool _updateMethodRan;
        private string _messageShown;

        [Given(@"The user is on the User Edit page")]
        public void GivenTheUserIsOnTheUserEditPage()
        {
            _navigationService.WhenForAnyArgs(n => n.ViewNavigateTo(null)).Do(callInfo => _navigated = true);
            _messageBox.WhenForAnyArgs(m => m.Show("")).Do(callInfo => _messageShown = (string)callInfo.Args()[0]);
            _requestTimeOffRepository.WhenForAnyArgs(r => r.AddUser(null)).Do(callInfo => _addMethodRan = true);
            _requestTimeOffRepository.WhenForAnyArgs(r => r.UpdateUser(null)).Do(callInfo => _updateMethodRan = true);
            _userEditViewModel = new UserEditViewModel(_navigationService, _requestTimeOffRepository, _messageBox);        
        }

        [When(@"entering a new password of ""([^""]*)""")]
        public void WhenEnteringANewPasswordOf(string pwd)
        {
            _userEditViewModel.Password = pwd;
        }

        [When(@"entering a confirm password of ""([^""]*)""")]
        public void WhenEnteringAConfirmPasswordOf(string pwdVerify)
        {
            _userEditViewModel.PasswordVerify = pwdVerify;
        }

        [When(@"clicking on the save button of the User Edit page")]
        public void WhenClickingOnTheSaveButtonOfTheUserEditPage()
        {
            _userEditViewModel.OnSave();
        }

        [Then(@"the the User Edit page should display an error message of ""([^""]*)""")]
        public void ThenTheTheUserEditPageShouldDisplayAnErrorMessageOf(string expectedMessage)
        {
            _navigated.Should().BeFalse();
            _messageShown.Should().Be(expectedMessage);
            _addMethodRan.Should().Be(false);
            _updateMethodRan.Should().Be(false);
        }

        [Given(@"the user record on the User Edit page is new")]
        public void GivenTheUserRecordOnTheUserEditPageIsNew()
        {
            _userEditViewModel.IsNew = true;
            _userEditViewModel.Password = string.Empty;
            _userEditViewModel.PasswordVerify = string.Empty;
            _userEditViewModel.User = new User();
        }

        [Then(@"the Add User method should be ran")]
        public void ThenTheAddUserMethodShouldBeRan()
        {
            _navigated.Should().BeTrue();
            _messageShown.Should().BeNullOrEmpty();
            _addMethodRan.Should().Be(true);
            _updateMethodRan.Should().Be(false);
        }

        [Given(@"the user record on the User Edit page is an Existing user")]
        public void GivenTheUserRecordOnTheUserEditPageIsAnExistingUser()
        {
            _requestTimeOffRepository.UserQuery(u => true).ReturnsForAnyArgs(new List<User> { new User() });
            _userEditViewModel.Password = string.Empty;
            _userEditViewModel.PasswordVerify = string.Empty;
            _userEditViewModel.User = new User();
        }

        [Then(@"the Update User method should be ran")]
        public void ThenTheUpdateUserMethodShouldBeRan()
        {
            _navigated.Should().BeTrue();
            _messageShown.Should().BeNullOrEmpty();
            _addMethodRan.Should().Be(false);
            _updateMethodRan.Should().Be(true);
        }
    }
}
