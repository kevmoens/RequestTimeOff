namespace RequestTimeOff.Models.MessageBoxes
{
    public class WPFDialogHost : IDialogHost
    {
        public void Close()
        {
            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
