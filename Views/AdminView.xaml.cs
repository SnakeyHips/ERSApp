using System.Windows;
using System.Windows.Controls;
using ERSApp.ViewModel;
using MahApps.Metro.Controls;

namespace ERSApp.Views
{
    public partial class AdminView : UserControl
    {
        MetroWindow mainWindow = (Application.Current.MainWindow as MetroWindow);

        public AdminView()
        {
            InitializeComponent();
            this.DataContext = new AdminViewModel();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }
    }
}
