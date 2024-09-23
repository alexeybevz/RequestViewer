﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using RequestViewer.Domain.Models;
using RequestViewer.WPF.Commands;
using RequestViewer.WPF.Services;
using RequestViewer.WPF.Stores;

namespace RequestViewer.WPF.ViewModels
{
    public class AddRequestViewModel : ViewModelBase
    {
        private readonly RequestsStore _requestsStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly IEnumerable<User> _users;
        private readonly Period _period;
        private bool _isSubmitting;

        public IEnumerable<User> Users => _users;
        public Period Period => _period;

        private ObservableCollection<DayViewModel> _days;

        public ObservableCollection<DayViewModel> DayVMs
        {
            get => _days;
            set { _days = value; OnPropertyChanged(nameof(DayVMs)); }
        }

        public CheckBoxViewModel CheckBoxViewModel { get; }
        public bool CanSubmit => DayVMs.Count(x => x.IsOpen) > 0;

        public bool IsSubmitting
        {
            get => _isSubmitting;
            set
            {
                _isSubmitting = value;
                OnPropertyChanged(nameof(IsSubmitting));
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public AddRequestViewModel(RequestsStore requestsStore, ModalNavigationStore modalNavigationStore, IEnumerable<User> users, Period period, bool isApproved)
        {
            _requestsStore = requestsStore;
            _modalNavigationStore = modalNavigationStore;
            _users = users;
            _period = period;
            DayVMs = new ObservableCollection<DayViewModel>();

            CheckBoxViewModel = new CheckBoxViewModel("Выбрать все дни");
            CheckBoxViewModel.SelectedChanged += CheckBoxViewModelOnSelectedChanged;

            SubmitCommand = new AddRequestCommand(this, modalNavigationStore, requestsStore, isApproved);
            CancelCommand = new CloseModalCommand(modalNavigationStore);

            RefreshDayVMs(period);
        }

        protected override void Dispose()
        {
            CheckBoxViewModel.SelectedChanged -= CheckBoxViewModelOnSelectedChanged;
        }

        private void CheckBoxViewModelOnSelectedChanged(bool isSelected)
        {
            var days = DayVMs.Where(d => !d.IsHeader && d.Day != null).ToList();

            foreach (var vm in days)
            {
                vm.IsOpen = isSelected;
            }
        }

        private void RefreshDayVMs(Period period)
        {
            DayVMs.Clear();
            OnPropertyChanged(nameof(DayVMs));

            var vms = DayViewModelListCreator.Create(period, new List<Day>(), true, true).ToList();
            vms.ForEach(vm =>
            {
                vm.IsOpenChanged += () => OnPropertyChanged(nameof(CanSubmit));
                DayVMs.Add(vm);
            });
        }
    }
}