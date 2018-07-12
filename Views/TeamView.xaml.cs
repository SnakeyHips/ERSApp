using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using ERSApp.ViewModels;

namespace ERSApp.Views
{
    public partial class TeamView : UserControl
    {
        MetroWindow mainWindow = (Application.Current.MainWindow as MetroWindow);

        public TeamView()
        {
            InitializeComponent();
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                this.DataContext = new TeamViewModel();
            }
        }

        private void btnAddTeam_Click(object sender, RoutedEventArgs e)
        {
            AddTeamWindow addTeamWindow = new AddTeamWindow();
            addTeamWindow.Owner = mainWindow;
            addTeamWindow.ShowDialog();
        }

        private void btnUpdateTeam_Click(object sender, RoutedEventArgs e)
        {
            if (lstTeam.SelectedIndex > -1)
            {
                UpdateTeamWindow updateTeamWindow = new UpdateTeamWindow(TeamViewModel.SelectedTeam);
                updateTeamWindow.Owner = mainWindow;
                updateTeamWindow.ShowDialog();
            }
        }

        private void btnViewTeam_Click(object sender, RoutedEventArgs e)
        {
            if (lstTeam.SelectedIndex > -1)
            {
                //TeamViewWindow staffViewWindow = new TeamViewWindow(TeamViewModel.SelectedTeam);
                //staffViewWindow.Show();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }
    }
}
