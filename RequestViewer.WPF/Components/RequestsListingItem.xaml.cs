using System.Windows.Controls;

namespace RequestViewer.WPF.Components
{
    /// <summary>
    /// Логика взаимодействия для RequestsListingItem.xaml
    /// </summary>
    public partial class RequestsListingItem : UserControl
    {
        public RequestsListingItem()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            dropdown.IsOpen = false;
        }
    }
}
