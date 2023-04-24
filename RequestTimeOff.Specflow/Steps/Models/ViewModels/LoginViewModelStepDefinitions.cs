using FluentAssertions;
using NSubstitute;
using RequestTimeOff.Models;
using RequestTimeOff.Models.MessageBoxes;
using RequestTimeOff.Models.Sessions;
using RequestTimeOff.MVVM;
using RequestTimeOff.ViewModels;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RequestTimeOff.Specflow.Steps.Models.ViewModels
{
    [Binding]
    public class LoginViewModelStepDefinitions
    {
        private LoginViewModel _loginViewModel;
        private readonly INavigationService _navigationService = Substitute.For<INavigationService>();
        private readonly IRequestTimeOffRepository _requestTimeOffRepository = Substitute.For<IRequestTimeOffRepository>();
        private readonly Session _session = Substitute.For<Session>();
        private readonly ISessionLoad _sessionLoad = Substitute.For<ISessionLoad>();
        private readonly IMessageBox _messageBox = Substitute.For<IMessageBox>();

        private string _navigatedTo = string.Empty;
        private string _message = string.Empty;
        public LoginViewModelStepDefinitions()
        {
            _navigationService.When(x => x.NavigateTo(Arg.Any<string>()))
                .Do(x => _navigatedTo = x.Arg<string>());
            _messageBox.When(x => x.Show(Arg.Any<string>()))
                .Do(x => _message = x.Arg<string>());
            _sessionLoad.Initialize().ReturnsForAnyArgs(Task.CompletedTask);
            _loginViewModel = new LoginViewModel(_navigationService, _requestTimeOffRepository, _session, _sessionLoad, _messageBox);   
        }
        [Given(@"A login witn an invalid Username")]
        public void GivenALoginWitnAnInvalidUsername()
        {
            _requestTimeOffRepository.UserQuery(u => true)
                .ReturnsForAnyArgs(new System.Collections.Generic.List<User>());
        }

        [When(@"Logging In")]
        public void WhenLoggingIn()
        {
            _loginViewModel.OnLogin();
        }

        [Then(@"Login Fails with a message of ""([^""]*)""")]
        public void ThenLoginFailsWithAMessageOf(string expectedMessage)
        {
            _message.Should().Be(expectedMessage);
            _navigatedTo.Should().BeEmpty();
        }

        [Given(@"A login witn an invalid password")]
        public void GivenALoginWitnAnInvalidPassword()
        {
            _requestTimeOffRepository.UserQuery(u => true)
                .ReturnsForAnyArgs(new System.Collections.Generic.List<User>
                {
                    new User() {
                        Username = "test",
                        Password = "test"
                    }
                });
            _loginViewModel.Username = "test";
            _loginViewModel.Password = "badPassword";
        }

        [Given(@"A User with a Valid Username and Password")]
        public void GivenAUserWithAValidUsernameAndPassword()
        {
            _requestTimeOffRepository.UserQuery(u => true)
                .ReturnsForAnyArgs(new System.Collections.Generic.List<User>
                {
                    new User() {
                        Username = "test",
                        Password = "test"
                    }
                });
            _loginViewModel.Username = "test";
            _loginViewModel.Password = "test";
        }

        [Then(@"Login Navigates to Home Page")]
        public void ThenLoginNavigatesToHomePage()
        {
            _navigatedTo.Should().Be("Home");
            _message.Should().BeEmpty();
        }

        [Given(@"An Admin with a Valid Username and Password")]
        public void GivenAnAdminWithAValidUsernameAndPassword()
        {
            _requestTimeOffRepository.UserQuery(u => true)
                .ReturnsForAnyArgs(new System.Collections.Generic.List<User>
                {
                    new User() {
                        Username = "test",
                        Password = "test",
                        IsAdmin = true
                    }
                });
            _loginViewModel.Username = "test";
            _loginViewModel.Password = "test";
        }

        [Then(@"Login Navigates to Admin Home Page")]
        public void ThenLoginNavigatesToAdminHomePage()
        {
            _navigatedTo.Should().Be("HomeAdmin");
            _message.Should().BeEmpty();
        }
    }
}
