using System.Windows;
using System.Windows.Controls;
using ERSApp.ViewModel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ERSApp.Views
{
    public partial class SiteView : UserControl
    {
        MetroWindow mainWindow = (Application.Current.MainWindow as MetroWindow);

        public SiteView()
        {
            InitializeComponent();
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                this.DataContext = new AdminViewModel();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddSiteWindow addSiteWindow = new AddSiteWindow();
            addSiteWindow.Owner = mainWindow;
            addSiteWindow.ShowDialog();
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (AdminViewModel.SelectedSite != null)
            {
                UpdateSiteWindow updateSiteWindow = new UpdateSiteWindow(AdminViewModel.SelectedSite);
                updateSiteWindow.Owner = mainWindow;
                updateSiteWindow.ShowDialog();
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }
    }
}
