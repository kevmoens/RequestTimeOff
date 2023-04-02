using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RequestTimeOff.Models.HomePages
{
    public interface IUserYearInfo
    {
        ObservableCollection<TimeOff> Schedule { get; set; }
        int SickRemain { get; set; }
        int VacRemain { get; set; }
        int Year { get; set; }

        Task ChangeYear(int year);
    }
}