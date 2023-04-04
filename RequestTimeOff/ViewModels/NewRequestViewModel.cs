using Microsoft.Extensions.DependencyInjection;
using RequestTimeOff.Models;
using RequestTimeOff.Models.Requests;
using RequestTimeOff.MVVM;
using RequestTimeOff.MVVM.Events;
using RequestTimeOff.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RequestTimeOff.ViewModels
{
    public class NewRequestViewModel : INotifyPropertyChanged
    {
#pragma warning disable CS0067 // The event 'PropertyChanged' is never used;
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067 // The event 'PropertyChanged' is never used;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly INavigationService _navigationService;
        public NewRequestViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            LoadValues();
            AddCommand = new DelegateCommand(OnAdd);
            SaveCommand = new DelegateCommand(OnSave);
            BackCommand = new DelegateCommand(OnBack);
        }
        public ICommand AddCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand BackCommand { get; set; }

        private ObservableCollection<TimeOffTypeDisplay> _timeOffTypes;

        public ObservableCollection<TimeOffTypeDisplay> TimeOffTypes
        {
            get { return _timeOffTypes; }
            set { _timeOffTypes = value; OnPropertyChanged(); }
        }
        private ObservableCollection<TimeOffRangeDisplay> _timeOffRanges;

        public ObservableCollection<TimeOffRangeDisplay> TimeOffRanges
        {
            get { return _timeOffRanges; }
            set { _timeOffRanges = value; OnPropertyChanged(); }
        }

        private TimeOffType _type = TimeOffType.Vacation;

        public TimeOffType Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged(); }
        }
        private TimeOffRange _range = TimeOffRange.FullDay;

        public TimeOffRange Range
        {
            get { return _range; }
            set { _range = value; OnPropertyChanged(); }
        }
        private DateTimeOffset _selectedDate;

        public DateTimeOffset SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; OnPropertyChanged(); }
        }
        private bool _isReoccurrance;

        public bool IsReoccurrance
        {
            get { return _isReoccurrance; }
            set { _isReoccurrance = value; OnPropertyChanged(); }
        }

        private ObservableCollection<int> _reoccurrances = new ObservableCollection<int>();

        public ObservableCollection<int> Reoccurrances
        {
            get { return _reoccurrances; }
            set { _reoccurrances = value; OnPropertyChanged(); }
        }

        private int _reoccurance = 1;

        public int Reoccurance
        {
            get { return _reoccurance; }
            set { _reoccurance = value; OnPropertyChanged(); }
        }
        private ObservableCollection<TimeOff> _requests = new ObservableCollection<TimeOff>();

        public ObservableCollection<TimeOff> Requests
        {
            get { return _requests; }
            set { _requests = value; OnPropertyChanged(); }
        }
        private int _totalHours;

        public int TotalHours
        {
            get { return _totalHours; }
            set { _totalHours = value; OnPropertyChanged(); }
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        private void LoadValues()
        {
            LoadTimeOffTypes();
            LoadTimeOffRanges();
            LoadReoccrrances();
        }

        private void LoadTimeOffTypes()
        {
            TimeOffTypes = new ObservableCollection<TimeOffTypeDisplay>
            {
                new TimeOffTypeDisplay { Type = TimeOffType.Vacation, Description = "Vacation" },
                new TimeOffTypeDisplay { Type = TimeOffType.Sick, Description = "Sick" }
            };
        }

        private void LoadTimeOffRanges()
        {
            TimeOffRanges = new ObservableCollection<TimeOffRangeDisplay>
            {
                new TimeOffRangeDisplay { Range = TimeOffRange.AM, Description = "AM" },
                new TimeOffRangeDisplay { Range = TimeOffRange.PM, Description = "PM" },
                new TimeOffRangeDisplay { Range = TimeOffRange.FullDay, Description = "Full Day" }
            };
        }

        private void LoadReoccrrances()
        {
            Reoccurrances.Add(1);
            Reoccurrances.Add(2);
            Reoccurrances.Add(3);
            Reoccurrances.Add(4);
            Reoccurrances.Add(5);
            Reoccurrances.Add(6);
            Reoccurrances.Add(7);
            Reoccurrances.Add(8);
            Reoccurrances.Add(9);
            Reoccurrances.Add(10);
        }

        public void OnAdd()
        {

        }
        public void OnSave()
        {
        }
        public void OnBack()
        {
            _navigationService.ViewNavigateTo("HomePage");
        }

    }
}
