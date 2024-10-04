using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using RequestViewer.BusinessLogic.Services;
using RequestViewer.Domain.Models;
using RequestViewer.WPF.Commands;
using RequestViewer.WPF.Services;
using RequestViewer.WPF.Stores;

namespace RequestViewer.WPF.ViewModels
{
    public class AddRequestViewModel : ViewModelBase
    {
        private bool _isSubmitting;
        private string _errorMessage;

        public IEnumerable<User> Users { get; }

        public Period Period { get; }

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

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public AddRequestViewModel(RequestsStore requestsStore, ModalNavigationStore modalNavigationStore, IEnumerable<User> users, Period period, bool isApproved)
        {
            Users = users;
            Period = period;
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

            var vms = DayViewModelListCreator.DayModelsToDayViewModels(
                DayModelListCreator.Create(period, new List<Day>(), true, true).ToList());

            foreach (var vm in vms)
            {
                vm.IsOpenChanged += () => OnPropertyChanged(nameof(CanSubmit));
                DayVMs.Add(vm);
            }
        }
    }
}