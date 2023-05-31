using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RequestTimeOff.Models.HomePages
{
    public interface IUserYearInfo
    {
        ObservableCollection<TimeOff> Schedule { get; }
        int SickRemain { get; }
        int VacRemain { get; }
        int Year { get; }
        string Username { get; set; }

        Task ChangeYear(int year);
    }
}