using RequestTimeOff.Models;
using RequestTimeOff.Models.MessageBoxes;
using RequestTimeOff.MVVM;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace RequestTimeOff.ViewModels
{
    public class HolidaysViewModel : INotifyPropertyChanged
    {
#pragma warning disable CS0067 // The event 'PropertyChanged' is never used;
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067 // The event 'PropertyChanged' is never used;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private readonly IRequestTimeOffRepository _requestTimeOffRepository;
        private readonly IDialogHost _dialogHost;
        public HolidaysViewModel(IRequestTimeOffRepository requestTimeOffRepository, IDialogHost dialogHost)
        {
            _requestTimeOffRepository = requestTimeOffRepository;
            _dialogHost = dialogHost;
            LoadedCommand = new DelegateCommand(OnLoaded);
            ChangedCommand = new DelegateCommand<Holiday>(OnChanged);
            AddCommand = new DelegateCommand(OnAdd);
            AddedCommand = new DelegateCommand(OnAdded);
            DeleteCommand = new DelegateCommand<Holiday>(OnDelete);

        }

        public ICommand LoadedCommand { get; set; }
        public ICommand ChangedCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand AddedCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private ObservableCollection<Holiday> _holidays;

        public ObservableCollection<Holiday> Holidays
        {
            get { return _holidays; }
            set { _holidays = value; OnPropertyChanged(); }
        }

        private bool _isAdding;

        public bool IsAdding
        {
            get { return _isAdding; }
            set { _isAdding = value; OnPropertyChanged(); }
        }
        private DateTime _newHoliday;

        public DateTime NewHoliday
        {
            get { return _newHoliday; }
            set { _newHoliday = value; OnPropertyChanged(); }
        }

        public void OnLoaded()
        {
            Holidays = new ObservableCollection<Holiday>(_requestTimeOffRepository.HolidayQuery(d => true).OrderByDescending(d => d.Date));
        }

        private void OnDelete(Holiday holiday)
        {
            _requestTimeOffRepository.RemoveHoliday(holiday);
            Holidays.Remove(holiday);
        }

        private void OnChanged(Holiday holiday)
        {
            _requestTimeOffRepository.UpdateHoliday(holiday);
        }

        private void OnAdd()
        {
            NewHoliday = DateTime.Today;
            IsAdding = true;
        }
        private void OnAdded()
        {
            var newHoliday = new Holiday() { Date = NewHoliday };
            _requestTimeOffRepository.AddHoliday(newHoliday);
            Holidays.Add(newHoliday);
            _dialogHost.Close();
        }
    }
}
