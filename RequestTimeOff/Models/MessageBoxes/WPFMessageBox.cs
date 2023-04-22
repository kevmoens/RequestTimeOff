using System.Windows;

namespace RequestTimeOff.Models.MessageBoxes
{
    internal class WPFMessageBox : IMessageBox
    {
        public void Show(string message)
        {
            MessageBox.Show(message);
        }
    }
}
