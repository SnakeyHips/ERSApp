using System.Windows;
using System.Windows.Controls;
using ERSApp.ViewModel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ERSApp.Views
{
    public partial class AdminView : UserControl
    {
        MetroWindow mainWindow = (Application.Current.MainWindow as MetroWindow);

        public AdminView()
        {
            InitializeComponent();
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                this.DataContext = new AdminViewModel();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tabHolidays.IsSelected)
            {
                AddHolidayWindow addHolidayWindow = new AddHolidayWindow();
                addHolidayWindow.Owner = mainWindow;
                addHolidayWindow.ShowDialog();
            }
            else if (tabSites.IsSelected)
            {
                AddSiteWindow addSiteWindow = new AddSiteWindow();
                addSiteWindow.Owner = mainWindow;
                addSiteWindow.ShowDialog();
            }
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (tabHolidays.IsSelected)
            {
                if(AdminViewModel.SelectedHoliday != null)
                {
                    UpdateHolidayWindow updateHolidayWindow = new UpdateHolidayWindow(AdminViewModel.SelectedHoliday);
                    updateHolidayWindow.Owner = mainWindow;
                    updateHolidayWindow.ShowDialog();
                }
            }
            else if (tabSites.IsSelected)
            {
                if (AdminViewModel.SelectedSite != null)
                {
                    UpdateSiteWindow updateSiteWindow = new UpdateSiteWindow(AdminViewModel.SelectedSite);
                    updateSiteWindow.Owner = mainWindow;
                    updateSiteWindow.ShowDialog();
                }
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (tabHolidays.IsSelected)
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
            else if (tabSites.IsSelected)
            {
                if (AdminViewModel.SelectedSite != null)
                {
                    MessageDialogResult choice = await mainWindow.ShowMessageAsync("",
                                "Are you sure you want to delete this Site?",
                                MessageDialogStyle.AffirmativeAndNegative);
                    if (choice == MessageDialogResult.Affirmative)
                    {
                        AdminViewModel.DeleteSite(AdminViewModel.SelectedSite);
                        AdminViewModel.Sites.Remove(AdminViewModel.SelectedSite);
                    }
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }
    }
}
