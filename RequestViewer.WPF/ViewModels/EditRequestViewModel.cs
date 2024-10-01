using System.Collections.ObjectModel;
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
        private bool _isSubmitting;
        private string _errorMessage;

        public ObservableCollection<DayViewModel> DayVMs
        {
            get => _days;
            set { _days = value; OnPropertyChanged(nameof(DayVMs)); }
        }

        public CheckBoxViewModel CheckBoxViewModel { get; }

        public bool CanSubmit => true;

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
            OnPropertyChanged(nameof(DayVMs));

            if (request == null)
                return;

            var vms = DayModelListCreator.DayModelsToDayViewModels(
                DayModelListCreator.Create(request.Period, request.Dates, request.IsApproved, true).ToList());

            foreach (var vm in vms)
                DayVMs.Add(vm);
        }
    }
}
