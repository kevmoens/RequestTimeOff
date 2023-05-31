using System.Windows;

namespace RequestTimeOff.Helpers
{
    // Create an attached property that can be put on a Control so it can be used to bind to a boolean property on the view model; 
    //when the value changes to true, the control gains focus
    public static class FocusHelper
    {
        public static bool GetIsFocused(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, value);

            if (value)
            {
                if (obj is FrameworkElement frameworkElement)
                {
                    frameworkElement.Focus();
                }
            }
        }

        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached("IsFocused", typeof(bool), typeof(FocusHelper),
                new UIPropertyMetadata(false, OnIsFocusedPropertyChanged));

        private static void OnIsFocusedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                if ((bool)e.NewValue)
                {
                    element.Focus();
                }
            }
        }
    }
}
