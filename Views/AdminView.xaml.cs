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
            switch (tabsAdmin.SelectedIndex)
            {
                case 0:
                    AddHolidayWindow addHolidayWindow = new AddHolidayWindow();
                    addHolidayWindow.Owner = mainWindow;
                    addHolidayWindow.ShowDialog();
                    break;
                case 1:
                    AddSiteWindow addSiteWindow = new AddSiteWindow();
                    addSiteWindow.Owner = mainWindow;
                    addSiteWindow.ShowDialog();
                    break;
            }
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            switch (tabsAdmin.SelectedIndex)
            {
                case 0:
                    if (AdminViewModel.SelectedHoliday != null)
                    {
                    UpdateHolidayWindow updateHolidayWindow = new UpdateHolidayWindow(AdminViewModel.SelectedHoliday);
                    updateHolidayWindow.Owner = mainWindow;
                    updateHolidayWindow.ShowDialog();
                    }
                    break;
                case 1:
                    if (AdminViewModel.SelectedSite != null)
                    {
                    UpdateSiteWindow updateSiteWindow = new UpdateSiteWindow(AdminViewModel.SelectedSite);
                    updateSiteWindow.Owner = mainWindow;
                    updateSiteWindow.ShowDialog();
                    }
                    break;
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            switch (tabsAdmin.SelectedIndex)
            {
                case 0:
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
                    break;
                case 1:
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
                    break;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }
    }
}
