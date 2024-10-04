using System;
using System.Windows.Input;
using RequestViewer.WPF.Commands;

namespace RequestViewer.WPF.ViewModels
{
    public class CheckBoxViewModel : ViewModelBase
    {
        private string _text;
        private bool _isSelected;

        public string Text
        {
            get => _text;
            private set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
                SelectedChanged?.Invoke(value);
            }
        }

        public event Action<bool> SelectedChanged;

        public ICommand SelectCheckBoxCommand { get; }

        public CheckBoxViewModel(string text)
        {
            Text = text;

            SelectCheckBoxCommand = new SelectCheckBoxCommand(this);
        }
    }
}