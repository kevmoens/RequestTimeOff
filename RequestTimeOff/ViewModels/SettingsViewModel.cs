﻿using RequestTimeOff.Models;
using RequestTimeOff.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace RequestTimeOff.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
#pragma warning disable CS0067 // The event 'PropertyChanged' is never used;
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067 // The event 'PropertyChanged' is never used;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly IRequestTimeOffRepository _requestTimeOffRepository;
        private readonly Session _session;
        public SettingsViewModel(IRequestTimeOffRepository requestTimeOffRepository, Session session)
        {
            _requestTimeOffRepository = requestTimeOffRepository;
            _session = session;
            UpdatePasswordCommand = new DelegateCommand(OnUpdatePassword);
        }

        public ICommand UpdatePasswordCommand { get; set; }

        private string _origPassword;

        public string OrigPassword
        {
            get { return _origPassword; }
            set { _origPassword = value; OnPropertyChanged(); }
        }


        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        private string _passwordConfirm;
        public string PasswordConfirm
        {
            get { return _passwordConfirm; }
            set { _passwordConfirm = value; OnPropertyChanged(); }
        }

        private void OnUpdatePassword()
        {
            var user = _requestTimeOffRepository.UserQuery(u => (u.Username ?? "").ToUpper() == (_session.User.Username ?? "").ToUpper()).FirstOrDefault();
            if (user == null)
            {
                MessageBox.Show("Invalid Username");
                return;
            }
            if (ValidatePasswordFailed(user, OrigPassword))
            {
                MessageBox.Show("Invalid Password");
                return;
            }
            if (Password != PasswordConfirm)
            {
                MessageBox.Show("New Passwords do not match");
                return;
            }
            user.Password = Password;
            _requestTimeOffRepository.UpdateUser(user);
            OrigPassword = "";
            Password = "";
            PasswordConfirm = "";
            MessageBox.Show("Password updated");
        }

        bool ValidatePasswordFailed(User user, string password)
        {
            if (user.Password == password)
            {
                return false;
            }
            if (string.IsNullOrEmpty(user.Password) && string.IsNullOrEmpty(password))
            {
                return false;
            }
            return true;
        }
    }
}
