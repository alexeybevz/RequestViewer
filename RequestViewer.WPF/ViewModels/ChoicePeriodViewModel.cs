using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using RequestViewer.Domain.Models;
using RequestViewer.WPF.Commands;
using RequestViewer.WPF.Stores;

namespace RequestViewer.WPF.ViewModels
{
    public class ChoicePeriodViewModel : ViewModelBase
    {
        private readonly PeriodsStore _periodsStore;
        private ObservableCollection<Period> _periods;
        private Period _selectedPeriod;

        public ObservableCollection<Period> Periods
        {
            get => _periods;
            set
            {
                _periods = value;
                OnPropertyChanged(nameof(Periods));
            }
        }

        public Period SelectedPeriod
        {
            get => _selectedPeriod;
            set
            {
                _selectedPeriod = value;
                OnPropertyChanged(nameof(SelectedPeriod));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        public bool CanSubmit => SelectedPeriod != null;

        public event Action<Period> PeriodSelected;

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public ChoicePeriodViewModel(PeriodsStore periodsStore, ModalNavigationStore modalNavigationStore)
        {
            Periods = new ObservableCollection<Period>();

            _periodsStore = periodsStore;
            _periodsStore.PeriodsLoaded += PeriodsStoreOnPeriodsLoaded;

            SubmitCommand = new SubmitSelectPeriodCommand(this);
            CancelCommand = new CloseModalCommand(modalNavigationStore);
        }

        protected override void Dispose()
        {
            _periodsStore.PeriodsLoaded -= PeriodsStoreOnPeriodsLoaded;
        }

        private void PeriodsStoreOnPeriodsLoaded()
        {
            var periods = _periodsStore.Periods.OrderByDescending(p => p.EndDate).ToList();

            Periods.Clear();
            
            periods.ForEach(p => { Periods.Add(p); });
        }

        public void RaisePeriodSelectedEvent()
        {
            PeriodSelected?.Invoke(SelectedPeriod);
        }
    }
}