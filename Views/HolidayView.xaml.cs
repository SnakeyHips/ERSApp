using System.Windows;
using System.Windows.Controls;
using ERSApp.ViewModel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ERSApp.Views
{
    public partial class HolidayView : UserControl
    {
        MetroWindow mainWindow = (Application.Current.MainWindow as MetroWindow);

        public HolidayView()
        {
            InitializeComponent();
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                this.DataContext = new AdminViewModel();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddHolidayWindow addHolidayWindow = new AddHolidayWindow();
            addHolidayWindow.Owner = mainWindow;
            addHolidayWindow.ShowDialog();
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateHolidayWindow updateHolidayWindow = new UpdateHolidayWindow(AdminViewModel.SelectedHoliday);
            updateHolidayWindow.Owner = mainWindow;
            updateHolidayWindow.ShowDialog();
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (AdminViewModel.SelectedHoliday != null)
            {
                MessageDialogResult choice = await mainWindow.ShowMessageAsync("",
                        "Are you sure you want to delete this Holiday?",
                        MessageDialogStyle.AffirmativeAndNegative);
                if (choice == MessageDialogResult.Affirmative)
                {
                    AdminViewModel.DeleteHoliday(AdminViewModel.SelectedHoliday);
                    AdminViewModel.Holidays.Remove(AdminViewModel.SelectedHoliday);
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }
    }
}
