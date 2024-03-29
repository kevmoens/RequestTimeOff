﻿using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using RequestTimeOff.Core.Models.Requests;
using RequestTimeOff.Core.MVVM;
using RequestTimeOff.Core.MVVM.Events;
using RequestTimeOff.Models;
using RequestTimeOff.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RequestTimeOff.ViewModels
{
    public class PendingRequestsViewModel : INotifyPropertyChanged
    {
#pragma warning disable CS0067 // The event 'PropertyChanged' is never used;
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067 // The event 'PropertyChanged' is never used;
        [ExcludeFromCodeCoverage]
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly IRequestTimeOffRepository _requestTimeOffRepository;
        private readonly IOpenDialog _openDialog;
        private readonly IMapper _mapper;
        private readonly INavigationService _navigationService;

        [ExcludeFromCodeCoverage]
        public PendingRequestsViewModel(IRequestTimeOffRepository requestTimeOffRepository, IOpenDialog openDialog, IMapper mapper, INavigationService navigationService)
        {
            _requestTimeOffRepository = requestTimeOffRepository;
            _openDialog = openDialog;
            _mapper = mapper;
            _navigationService = navigationService;
            LoadedCommand = new DelegateCommand(OnLoaded);
            ApproveCommand = new DelegateCommand<TimeOff>(OnApprove);
            DeclineCommand = new DelegateCommand<TimeOff>(OnDecline);
            DeclineAcceptCommand = new DelegateCommand(OnDeclineAccept);
            DeclineCancelCommand = new DelegateCommand(OnDeclineCancel);
            ViewTeamMembersTransactionsCommand = new DelegateCommand<TimeOff>(OnViewTeamMembersTransactions);
            UserDetailsCommand = new DelegateCommand<TimeOff>(OnUserDetails);
        }

        public ICommand LoadedCommand { get; set; }
        public ICommand ApproveCommand { get; set; }
        public ICommand DeclineCommand { get; set; }
        public ICommand DeclineAcceptCommand { get; set; }
        public ICommand DeclineCancelCommand { get; set; }
        public ICommand ViewTeamMembersTransactionsCommand { get; set; }
        public ICommand UserDetailsCommand { get; set; }

        private ObservableCollection<TimeOff> _requests;

        [ExcludeFromCodeCoverage]
        public ObservableCollection<TimeOff> Requests
        {
            get { return _requests; }
            set { _requests = value; OnPropertyChanged(); }
        }

        private TimeOff _selectedRequest;

        [ExcludeFromCodeCoverage]
        public TimeOff SelectedRequest
        {
            get { return _selectedRequest; }
            set { _selectedRequest = value; OnPropertyChanged(); }
        }
        private string _reason;

        [ExcludeFromCodeCoverage]
        public string Reason
        {
            get { return _reason; }
            set { _reason = value; OnPropertyChanged(); }
        }

        private bool _conflictVisibility;
        [ExcludeFromCodeCoverage]
        public bool ConflictVisibility
        {
            get { return _conflictVisibility; }
            set { _conflictVisibility = value; OnPropertyChanged(); }
        }

        private bool _declineVisibility;
        [ExcludeFromCodeCoverage]
        public bool DeclineVisibility
        {
            get { return _declineVisibility; }
            set { _declineVisibility = value; OnPropertyChanged(); }
        }


        [ExcludeFromCodeCoverage]
        private void OnLoaded()
        {
            var requests = _requestTimeOffRepository
                .TimeOffQuery(t => t.Approved == false && t.Declined == false)
                .OrderBy(t => t.Username)
                .OrderBy(t => t.Date)
                .ToList();

            Requests = new ObservableCollection<TimeOff>();
            foreach (var request in requests)
            {
                var currentUser = _requestTimeOffRepository.UserQuery(t => t.Username == request.Username).First();
                
                PendingTimeOff newReq;
                newReq = _mapper.Map<PendingTimeOff>(request);
                var teamMemberRequests = _requestTimeOffRepository
                    .TimeOffQuery(t => t.Declined == false
                                && t.Username != request.Username
                                && t.Date.Date == request.Date.Date)
                    .OrderBy(t => t.Username)
                    .Where(t => _requestTimeOffRepository.UserQuery(u => u.Username == t.Username).First().Dept == currentUser.Dept)
                    .ToList();

                newReq.TeamMemberTimeOffs = new ObservableCollection<TimeOff>(teamMemberRequests);
                Requests.Add(newReq);
            }
            
        }
        public void OnApprove(TimeOff timeOff)
        {
            timeOff.Approved = true;
            _requestTimeOffRepository.UpdateTimeOff(timeOff);
            // Stryker disable once all
            PendingRequestUpdatePubSub.Instance.Publish(new PendingRequestUpdate());
            // Stryker disable once all
            OnLoaded();
        }

        [ExcludeFromCodeCoverage]
        public void OnDecline(TimeOff timeOff)
        {
            SelectedRequest = timeOff;
            Reason = string.Empty;
            ConflictVisibility = false;
            DeclineVisibility = true;
            _openDialog.Open();
        }

        [ExcludeFromCodeCoverage]
        private void OnDeclineAccept()
        {
            SelectedRequest.Declined = true;
            SelectedRequest.Reason = Reason;
            _requestTimeOffRepository.UpdateTimeOff(SelectedRequest);
            DeclineVisibility = false;

            // Stryker disable once all
            PendingRequestUpdatePubSub.Instance.Publish(new PendingRequestUpdate());
            // Stryker disable once all
            _openDialog.Close();
            OnLoaded();
        }


        [ExcludeFromCodeCoverage]
        private void OnDeclineCancel()
        {
            _openDialog.Close();
        }


        [ExcludeFromCodeCoverage]
        private void OnViewTeamMembersTransactions(TimeOff request)
        {
            SelectedRequest = request;
            ConflictVisibility = true;
            DeclineVisibility = false;
            _openDialog.Open();            
        }


        [ExcludeFromCodeCoverage]
        private void OnUserDetails (TimeOff request)
        {
            var user = _requestTimeOffRepository.UserQuery(t => t.Username == request.Username).First();
            _navigationService.ViewNavigateTo("HomePage", new Dictionary<string, object> { { "User", user } });
        }
    }
}
