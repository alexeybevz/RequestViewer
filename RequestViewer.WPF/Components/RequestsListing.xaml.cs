using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DropdownMenuControl;

namespace RequestViewer.WPF.Components
{
    /// <summary>
    /// Логика взаимодействия для RequestsListing.xaml
    /// </summary>
    public partial class RequestsListing : UserControl
    {
        public RequestsListing()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var element = FindOpenedDropdownMenu(RequestsListBox, "dropdownG");
            element.IsOpen = false;
        }

        public DropdownMenu FindOpenedDropdownMenu(FrameworkElement element, string sChildName)
        {
            DropdownMenu childElement = null;
            var nChildCount = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < nChildCount; i++)
            {
                FrameworkElement child = VisualTreeHelper.GetChild(element, i) as FrameworkElement;

                if (child == null)
                    continue;

                if (child is DropdownMenu && child.Name.Equals(sChildName))
                {
                    childElement = (DropdownMenu)child;

                    if (childElement.IsOpen)
                        break;
                }

                childElement = FindOpenedDropdownMenu(child, sChildName);

                if (childElement != null)
                    break;
            }
            return childElement;
        }
    }
}
