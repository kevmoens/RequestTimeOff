using FluentAssertions;
using NSubstitute;
using RequestTimeOff.Models;
using RequestTimeOff.Models.MessageBoxes;
using RequestTimeOff.ViewModels;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace RequestTimeOff.Specflow.StepDefinitions.ViewModels
{
    [Binding]
    public class SettingsViewModelStepDefinitions
    {
        private readonly IRequestTimeOffRepository _requestTimeOffRepository = Substitute.For<IRequestTimeOffRepository>();
        private readonly Session _session = new Session();
        private readonly IMessageBox _messageBox = Substitute.For<IMessageBox>();

        private string _messageText = string.Empty;
        private bool _userUpdated = false;

        private SettingsViewModel _settingsViewModel;


        public SettingsViewModelStepDefinitions()
        {
            var user = new User { Username = "user", Password = "oldpwd" };
            _session.User = user;
            _messageBox.WhenForAnyArgs(mb => mb.Show("")).Do(info =>
            {
                _messageText = (string)info.Args()[0];
            });
            _requestTimeOffRepository.WhenForAnyArgs(r => r.UpdateUser(user)).Do(info =>
            {
                _userUpdated = true;
            });
        }

        [Given(@"A User is updating their settings")]
        public void GivenAUserIsUpdatingTheirSettings()
        {
            _settingsViewModel = new SettingsViewModel(_requestTimeOffRepository, _session, _messageBox);
        }

        [When(@"the user enters their valid existing password on the Settings View")]
        public void WhenTheUserEntersTheirValidExistingPasswordOnTheSettingsView()
        {
            _requestTimeOffRepository.UserQuery(u => true).ReturnsForAnyArgs(info =>
            {
                return new List<User>
                {
                    _session.User
                };
            });
            _settingsViewModel.OrigPassword = "oldpwd";
        }

        [When(@"the user doesn't exist for the Settings View")]
        public void WhenTheUserDoesntExistForTheSettingsView()
        {
            _requestTimeOffRepository.UserQuery(u => true).ReturnsForAnyArgs(info =>
            {
                return new List<User>();
            });
        }
        [When(@"the user enters their an invalid existing password on the Settings View")]
        public void WhenTheUserEntersTheirAnInvalidExistingPasswordOnTheSettingsView()
        {
            _requestTimeOffRepository.UserQuery(u => true).ReturnsForAnyArgs(info =>
            {
                return new List<User>
                {
                    _session.User
                };
            });
            _settingsViewModel.OrigPassword = "badpwd";
        }
        [When(@"the user enters an invalid existing password on the Settings View")]
        public void WhenTheUserEntersAnInvalidExistingPasswordOnTheSettingsView()
        {
            _requestTimeOffRepository.UserQuery(u => true).ReturnsForAnyArgs(info =>
            {
                return new List<User>
                {
                    _session.User
                };
            });
            _settingsViewModel.OrigPassword = "badpwd";
        }


        [Then(@"the user on the Settings View get the message ""([^""]*)""")]
        public void ThenTheUserOnTheSettingsViewGetTheMessage(string expectedMessage)
        {
            _messageText.Should().Be(expectedMessage);
            _userUpdated.Should().BeFalse();
        }

        [When(@"the user enters their new password that doesn't match the confirmed password on the Settings View")]
        public void WhenTheUserEntersTheirNewPasswordThatDoesntMatchTheConfirmedPasswordOnTheSettingsView()
        {
            _settingsViewModel.Password = "newpwd";
            _settingsViewModel.PasswordConfirm = "newbadpwd";
        }

        [When(@"the user enters their new password matching the confirmed password on the Settings View")]
        public void WhenTheUserEntersTheirNewPasswordMatchingTheConfirmedPasswordOnTheSettingsView()
        {
            _settingsViewModel.Password = "newpwd";
            _settingsViewModel.PasswordConfirm = "newpwd";
        }

        [When(@"the Settings View OnUpdatePassword is ran")]
        public void WhenTheSettingsViewOnUpdatePasswordIsRan()
        {
            _settingsViewModel.OnUpdatePassword();
        }


        [Then(@"the user successfully updates their password for their Settings View")]
        public void ThenTheUserSuccessfullyUpdatesTheirPasswordForTheirSettingsView()
        {
            _userUpdated.Should().BeTrue();
            _messageText.Should().Be("Password updated");
            //Reset Values
            _settingsViewModel.OrigPassword.Should().BeEmpty();
            _settingsViewModel.Password.Should().BeEmpty();
            _settingsViewModel.PasswordConfirm.Should().BeEmpty();
        }
    }
}
