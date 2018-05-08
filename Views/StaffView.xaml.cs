using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ERSApp.Model;
using ERSApp.ViewModel;
using MahApps.Metro.Controls;

namespace ERSApp.Views
{
    public partial class StaffView : UserControl
    {
        MetroWindow mainWindow = (Application.Current.MainWindow as MetroWindow);

        public StaffView()
        {
            InitializeComponent();
            this.DataContext = new StaffViewModel();
        }

        private void dateStaff_SelectedDatesChanged(object sender, RoutedEventArgs e)
        {
            Mouse.Capture(null);
            foreach(Staff s in StaffViewModel.Staffs)
            {
                s.Status = CollectionManager.GetStatus(s.Id, dateStaff.SelectedDate.Value);
            }
        }

        private void btnAddStaff_Click(object sender, RoutedEventArgs e)
        {
            AddStaffWindow addStaffWindow = new AddStaffWindow();
            addStaffWindow.Owner = mainWindow;
            addStaffWindow.ShowDialog();
        }

        private void btnUpdateStaff_Click(object sender, RoutedEventArgs e)
        {
            if (lstStaff.SelectedIndex > -1)
            {
                UpdateStaffWindow updateStaffWindow = new UpdateStaffWindow(StaffViewModel.SelectedStaff);
                updateStaffWindow.Owner = mainWindow;
                updateStaffWindow.ShowDialog();
            }
        }

        private void btnArchiveStaff_Click(object sender, RoutedEventArgs e)
        {
            ArchiveStaffWindow ArchiveStaffWindow = new ArchiveStaffWindow();
            ArchiveStaffWindow.Owner = mainWindow;
            ArchiveStaffWindow.Show();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }
    }
}
