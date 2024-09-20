﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using RequestViewer.Domain.Models;
using RequestViewer.WPF.Commands;
using RequestViewer.WPF.Services;
using RequestViewer.WPF.Stores;

namespace RequestViewer.WPF.ViewModels
{
    public class EditRequestViewModel : ViewModelBase
    {
        private ObservableCollection<DayViewModel> _days;

        public ObservableCollection<DayViewModel> DayVMs
        {
            get => _days;
            set { _days = value; OnPropertyChanged(nameof(DayVMs)); }
        }

        public CheckBoxViewModel CheckBoxViewModel { get; }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public EditRequestViewModel(Request request, RequestsStore requestsStore, ModalNavigationStore modalNavigationStore)
        {
            DayVMs = new ObservableCollection<DayViewModel>();

            SubmitCommand = new EditRequestCommand(this, modalNavigationStore, requestsStore, request);
            CancelCommand = new CloseModalCommand(modalNavigationStore);

            CheckBoxViewModel = new CheckBoxViewModel("Выбрать все дни");
            CheckBoxViewModel.SelectedChanged += CheckBoxViewModelOnSelectedChanged;

            RefreshDayVMs(request);
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

        private void RefreshDayVMs(Request request)
        {
            DayVMs.Clear();

            if (request == null)
                return;

            var vms = DayViewModelListCreator.Create(request.Period, request.Dates, request.IsApproved, true).ToList();
            vms.ForEach(vm => DayVMs.Add(vm));
        }
    }
}
