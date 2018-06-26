using System.Windows;
using MahApps.Metro.Controls;
using ERSApp.ViewModels;

namespace ERSApp.Views
{
    public partial class TeamDialog : MetroWindow
    {
        public TeamDialog()
        {
            InitializeComponent();
            lstTeamDialog.ItemsSource = TeamViewModel.Teams;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(lstTeamDialog.SelectedIndex > -1)
            {
                this.DialogResult = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
