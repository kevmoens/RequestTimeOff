using MaterialDesignThemes.Wpf;
using RequestTimeOff.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTimeOff.MVVM
{
    public class OpenDialog : IOpenDialog
    {
        public void Open()
        {
            DialogHost.OpenDialogCommand.Execute(null, null);
        }
        public void Close()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
