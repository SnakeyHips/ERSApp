using System.Windows;
using ERSApp.ViewModel;
using MahApps.Metro.Controls;

namespace ERSApp.Views
{
    public partial class OverviewWindow : MetroWindow
    {
        public OverviewWindow()
        {
            InitializeComponent();
            this.DataContext = new SessionViewModel();
            tabOverview.Header = CollectionManager.SelectedDate.ToShortDateString();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
