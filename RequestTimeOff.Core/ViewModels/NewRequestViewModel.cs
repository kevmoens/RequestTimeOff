using FluentValidation;
using RequestTimeOff.Core.Models.Requests;
using RequestTimeOff.Models;
using RequestTimeOff.Models.Date;
using RequestTimeOff.Models.MessageBoxes;
using RequestTimeOff.Models.Requests;
using RequestTimeOff.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace RequestTimeOff.ViewModels
{
    public class NewRequestViewModel : INotifyPropertyChanged, INavigationAware
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
        private readonly IValidator<TimeOff> _validator;
        private readonly IRequestTimeOffRepository _requestTimeOffRepository;
        private readonly ISystemDateTime _systemDateTime;
        private readonly IMessageBox _messageBox;
        [ExcludeFromCodeCoverage]
        public NewRequestViewModel(Session session,
                                   INavigationService navigationService,
                                   ITimeOffDateRange timeOffDateRange,
                                   IValidator<TimeOff> validator,
                                   IRequestTimeOffRepository requestTimeOffRepository,
                                   ISystemDateTime systemDateTime,
                                   IMessageBox messageBox)
        {
            _session = session;
            _navigationService = navigationService;
            _timeOffDateRange = timeOffDateRange;
            _validator = validator;
            _requestTimeOffRepository = requestTimeOffRepository;
            _systemDateTime = systemDateTime;
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

        [ExcludeFromCodeCoverage]
        public ObservableCollection<TimeOffTypeDisplay> TimeOffTypes
        {
            get { return _timeOffTypes; }
            set { _timeOffTypes = value; OnPropertyChanged(); }
        }
        private ObservableCollection<TimeOffRangeDisplay> _timeOffRanges;

        [ExcludeFromCodeCoverage]
        public ObservableCollection<TimeOffRangeDisplay> TimeOffRanges
        {
            get { return _timeOffRanges; }
            set { _timeOffRanges = value; OnPropertyChanged(); }
        }

        private ObservableCollection<int> _reoccurrances = new ObservableCollection<int>();

        [ExcludeFromCodeCoverage]
        public ObservableCollection<int> Reoccurrances
        {
            get { return _reoccurrances; }
            set { _reoccurrances = value; OnPropertyChanged(); }
        }


        private TimeOffType _type = TimeOffType.Vacation;

        [ExcludeFromCodeCoverage]
        public TimeOffType Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged(); }
        }
        private TimeOffRange _range = TimeOffRange.FullDay;

        [ExcludeFromCodeCoverage]
        public TimeOffRange Range
        {
            get { return _range; }
            set { _range = value; OnPropertyChanged(); }
        }
        private DateTime _selectedDate = DateTime.Now.Date;

        [ExcludeFromCodeCoverage]
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; OnPropertyChanged(); }
        }
        private bool _isReoccurrance;

        [ExcludeFromCodeCoverage]
        public bool IsReoccurrance
        {
            get { return _isReoccurrance; }
            set { _isReoccurrance = value; OnPropertyChanged(); }
        }
        private int _reoccurance = 1;

        [ExcludeFromCodeCoverage]
        public int Reoccurance
        {
            get { return _reoccurance; }
            set { _reoccurance = value; OnPropertyChanged(); }
        }
        private ObservableCollection<TimeOff> _requests = new ObservableCollection<TimeOff>();

        [ExcludeFromCodeCoverage]
        public ObservableCollection<TimeOff> Requests
        {
            get { return _requests; }
            set { _requests = value; OnPropertyChanged(); }
        }
        private int _totalHours;

        [ExcludeFromCodeCoverage]
        public int TotalHours
        {
            get { return _totalHours; }
            set { _totalHours = value; OnPropertyChanged(); }
        }
        private string _description;

        [ExcludeFromCodeCoverage]
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

        [ExcludeFromCodeCoverage]
        public void OnAdd()
        {

            _timeOffDateRange.SelectedDate = SelectedDate;
            _timeOffDateRange.Reoccurance = IsReoccurrance ? Reoccurance : 1;
            var dates = _timeOffDateRange.GetDates();

            AddDatesToRequests(dates);

        }

        [ExcludeFromCodeCoverage]
        private void AddDatesToRequests(List<DateTimeOffset> dates)
        {
            var newRequests = new List<TimeOff>();
            if (_validator is ITimeOffValidator timeOffValidator)
            {
                (_validator as ITimeOffValidator).ExistingRequests = Requests.ToList();
            }
            foreach (var date in dates)
            {
                TimeOff timeOff = new TimeOff
                {
                    Date = date,
                    Type = Type,
                    Range = Range,
                    Username = _session.User.Username
                };
                
                var results = _validator.Validate(timeOff);
                if (results.IsValid == false)
                {
                    _messageBox.Show(results.Errors.First().ErrorMessage);
                    if (results.Errors.Any(t => t.Severity == Severity.Error))
                    {
                        return;
                    }
                }
                newRequests.Add(timeOff);
            }
            
            foreach (var req in newRequests)
            {
                TotalHours += req.Range.Hours();
                Requests.Add(req);
            }
        }

        [ExcludeFromCodeCoverage]
        public void OnSave()
        {
            foreach (var req in Requests)
            {
                req.Description = Description;
                _requestTimeOffRepository.AddTimeOff(req);
            }
            Clear();
            _navigationService.ViewNavigateTo("HomePage");
        }
        [ExcludeFromCodeCoverage]
        public void OnBack()
        {
            Clear();
            _navigationService.ViewNavigateTo("HomePage");
        }

        [ExcludeFromCodeCoverage]
        public void Clear()
        {
            Description = "";
            Requests.Clear();
            IsReoccurrance = false;
            Reoccurance = 1;
            Type = TimeOffType.Vacation;
            Range = TimeOffRange.FullDay;
            SelectedDate = DateTime.Today;
            TotalHours = 0;
        }

        [ExcludeFromCodeCoverage]
        public void OnRemoveItem(TimeOff timeOff)
        {
            TotalHours -= timeOff.Range.Hours();
            Requests.Remove(timeOff);
        }

        [ExcludeFromCodeCoverage]
        public void OnNavigatedTo(Dictionary<string, object> parameters)
        {
            Clear();
        }
        public void OnNavigated()
        {

        }
    }
}
