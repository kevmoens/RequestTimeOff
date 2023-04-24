using RequestTimeOff.Models;
using RequestTimeOff.Models.MessageBoxes;
using RequestTimeOff.Models.Requests;
using RequestTimeOff.MVVM;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace RequestTimeOff.ViewModels
{
    public class NewRequestViewModel : INotifyPropertyChanged
    {
#pragma warning disable CS0067 // The event 'PropertyChanged' is never used;
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067 // The event 'PropertyChanged' is never used;
        [ExcludeFromCodeCoverage]
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private readonly Session _session;
        private readonly INavigationService _navigationService;
        private readonly ITimeOffDateRange _timeOffDateRange;
        private readonly IValidateAdd _validateAdd;
        private readonly IRequestTimeOffRepository _requestTimeOffRepository;
        private readonly IMessageBox _messageBox;
        [ExcludeFromCodeCoverage]
        public NewRequestViewModel(Session session, INavigationService navigationService, ITimeOffDateRange timeOffDateRange, IValidateAdd validateAdd, IRequestTimeOffRepository requestTimeOffRepository, IMessageBox messageBox)
        {
            _session = session;
            _navigationService = navigationService;
            _timeOffDateRange = timeOffDateRange;
            _validateAdd = validateAdd;
            _requestTimeOffRepository = requestTimeOffRepository;
            _messageBox = messageBox;
            LoadValues();
            AddCommand = new DelegateCommand(OnAdd);
            SaveCommand = new DelegateCommand(OnSave);
            BackCommand = new DelegateCommand(OnBack);
            RemoveItemCommand = new DelegateCommand<TimeOff>(OnRemoveItem);

        }
        public ICommand AddCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand RemoveItemCommand { get; set; }

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

        private ObservableCollection<int> _reoccurrances = new ObservableCollection<int>();

        public ObservableCollection<int> Reoccurrances
        {
            get { return _reoccurrances; }
            set { _reoccurrances = value; OnPropertyChanged(); }
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
        private DateTime _selectedDate = DateTime.Now.Date;

        public DateTime SelectedDate
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

        [ExcludeFromCodeCoverage]
        private void LoadValues()
        {
            LoadTimeOffTypes();
            LoadTimeOffRanges();
            LoadReoccrrances();
        }

        [ExcludeFromCodeCoverage]
        private void LoadTimeOffTypes()
        {
            TimeOffTypes = new ObservableCollection<TimeOffTypeDisplay>
            {
                new TimeOffTypeDisplay { Type = TimeOffType.Vacation, Description = "Vacation" },
                new TimeOffTypeDisplay { Type = TimeOffType.Sick, Description = "Sick" }
            };
        }

        [ExcludeFromCodeCoverage]
        private void LoadTimeOffRanges()
        {
            TimeOffRanges = new ObservableCollection<TimeOffRangeDisplay>
            {
                new TimeOffRangeDisplay { Range = TimeOffRange.AM, Description = "AM" },
                new TimeOffRangeDisplay { Range = TimeOffRange.PM, Description = "PM" },
                new TimeOffRangeDisplay { Range = TimeOffRange.FullDay, Description = "Full Day" }
            };
        }

        [ExcludeFromCodeCoverage]
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
            //TODO Don't let all employees of the same department all ask off on the same day
            //TODO Asking off more than 7 days in a row requires asking a manager in person
            //TODO If you ask off more than 3 days in a row you must ask off 2 weeks in advance

            if (ValidateAdd() == false)
            {
                return;
            }

            _timeOffDateRange.SelectedDate = SelectedDate;
            _timeOffDateRange.Reoccurance = IsReoccurrance ? Reoccurance : 1;
            var dates = _timeOffDateRange.GetDates();

            _validateAdd.NewDates = dates;
            string message = _validateAdd.ValidateDates();
            if (string.IsNullOrEmpty(message) == false)
            {
                _messageBox.Show(message);
                return;
            }   

            foreach (var date in dates)
            {
                TotalHours += Range.Hours();
                TimeOff timeOff = new TimeOff
                {
                    Date = date,
                    Type = Type,
                    Range = Range,
                    Username = _session.User.Username
                };
                Requests.Add(timeOff);
            }
        }

        private bool ValidateAdd()
        {
            _validateAdd.SelectedDate = SelectedDate;
            _validateAdd.ExistingRequests = Requests; 
            string message = _validateAdd.ValidateInput();
            if (string.IsNullOrEmpty(message) == false)
            {
                _messageBox.Show(message);
                return false;
            }
            return true;
        }

        public void OnSave()
        {
            foreach (var req in Requests)
            {
                req.Description = Description;
                _requestTimeOffRepository.AddTimeOff(req);
            }
            _navigationService.ViewNavigateTo("HomePage");
        }
        [ExcludeFromCodeCoverage]
        public void OnBack()
        {
            _navigationService.ViewNavigateTo("HomePage");
        }

        [ExcludeFromCodeCoverage]
        public void OnRemoveItem(TimeOff timeOff)
        {
            TotalHours -= timeOff.Range.Hours();
            Requests.Remove(timeOff);
        }
    }
}
