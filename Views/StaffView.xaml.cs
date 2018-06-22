using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using ERSApp.ViewModels;

namespace ERSApp.Views
{
    public partial class StaffView : UserControl
    {
        MetroWindow mainWindow = (Application.Current.MainWindow as MetroWindow);

        public StaffView()
        {
            InitializeComponent();
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                this.DataContext = new StaffViewModel();
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

        private void btnViewStaff_Click(object sender, RoutedEventArgs e)
        {
            if (lstStaff.SelectedIndex > -1)
            {
                StaffViewWindow staffViewWindow = new StaffViewWindow(StaffViewModel.SelectedStaff);
                staffViewWindow.Show();
            }
        }

        private void btnArchiveStaff_Click(object sender, RoutedEventArgs e)
        {
            ArchiveStaffWindow ArchiveStaffWindow = new ArchiveStaffWindow();
            ArchiveStaffWindow.Owner = mainWindow;
            ArchiveStaffWindow.ShowDialog();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }
    }
}
