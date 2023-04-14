using FluentAssertions;
using MaterialDesignThemes.Wpf;
using RequestTimeOff.Converters;
using System;
using System.Windows;
using TechTalk.SpecFlow;

namespace RequestTimeOff.Specflow.Steps
{
    [Binding]
    public class ConvertersWindowStateConverterStepDefinitions
    {
        private WindowState? _windowState;
        private PackIconKind _result;
        [Given(@"The window state is unknown")]
        public void GivenTheWindowStateIsUnknown()
        {
            _windowState = null;
        }

        [Given(@"The window state is normal")]
        public void GivenTheWindowStateIsNormal()
        {
            _windowState = WindowState.Normal;
        }

        [Given(@"The window state is maximize")]
        public void GivenTheWindowStateIsMaximize()
        {
            _windowState = WindowState.Maximized;
        }

        [When(@"running the converter WindowStateConverter")]
        public void WhenRunningTheConverterWindowStateConverter()
        {
            var converter = new WindowStateConverter();
            _result = (PackIconKind)converter.Convert(_windowState, typeof(PackIconKind), null, null);
        }

        [Then(@"the value is ""([^""]*)""")]
        public void ThenTheValueIs(PackIconKind state)
        {
            _result.Should().Be(state);
        }

        [Given(@"The convert back method gets ran")]
        public void GivenTheConvertBackMethodGetsRan()
        {
        }

        [When(@"running the ConvertBack on the converter")]
        public void WhenRunningTheConvertBackOnTheConverter()
        {
        }

        [Then(@"the WindowStateConverter ConvertBack result is null")]
        public void ThenTheWindowStateConverterConvertBackResultIsNull()
        {
            WindowStateConverter converter = new();
            var result = converter.ConvertBack(null, null, null, null);
            result.Should().BeNull();

        }

    }
}
