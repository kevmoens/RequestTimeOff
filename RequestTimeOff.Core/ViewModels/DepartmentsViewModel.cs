using RequestTimeOff.Models;
using RequestTimeOff.Models.MessageBoxes;
using RequestTimeOff.MVVM;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace RequestTimeOff.ViewModels
{
    public class DepartmentsViewModel : INotifyPropertyChanged
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
        private readonly IMessageBox _messageBox;
        public DepartmentsViewModel(IRequestTimeOffRepository requestTimeOffRepository, IDialogHost dialogHost, IMessageBox messageBox)
        {
            _requestTimeOffRepository = requestTimeOffRepository;
            _dialogHost = dialogHost;
            _messageBox = messageBox;
            LoadedCommand = new DelegateCommand(OnLoaded);
            ChangedCommand = new DelegateCommand<Department>(OnChanged);
            AddCommand = new DelegateCommand(OnAdd);
            AddedCommand = new DelegateCommand(OnAdded);
            DeleteCommand = new DelegateCommand<Department>(OnDelete);

        }

        public ICommand LoadedCommand { get; set; }
        public ICommand ChangedCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand AddedCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private ObservableCollection<Department>     _departments;

        public ObservableCollection<Department> Departments
        {
            get { return _departments; }
            set { _departments = value; OnPropertyChanged(); }
        }

        private bool _isAdding;

        public bool IsAdding
        {
            get { return _isAdding; }
            set { _isAdding = value; OnPropertyChanged(); }
        }
        private string _newDept;

        public string NewDept
        {
            get { return _newDept; }
            set { _newDept = value; OnPropertyChanged(); }
        }

        public void OnLoaded()
        {
            Departments = new ObservableCollection<Department>(_requestTimeOffRepository.DepartmentQuery(d => true).OrderBy(d => d.Dept));
        }

        private void OnDelete(Department department)
        {
            _requestTimeOffRepository.RemoveDepartment(department);
            Departments.Remove(department);
        }

        private void OnChanged(Department department)
        {
            _requestTimeOffRepository.UpdateDepartment(department);
        }

        private void OnAdd()
        {
            NewDept = string.Empty;
            IsAdding = true;
        }
        private void OnAdded()
        {
            if (_requestTimeOffRepository.DepartmentQuery(d => d.Dept.ToUpper() == NewDept.ToUpper()).Any())
            {
                _messageBox.Show($"Department [{NewDept}] already exists.");
                return;
            }
            var newDepartment = new Department() { Dept = NewDept };
            _requestTimeOffRepository.AddDepartment(newDepartment);
            Departments.Add(newDepartment);
            _dialogHost.Close();
        }
    }
}
