using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using MahApps.Metro.Controls;
using ERSApp.Model;
using MahApps.Metro.Controls.Dialogs;

namespace ERSApp.Views
{
    public partial class ArchiveStaffWindow : MetroWindow
    {
        public ObservableCollection<double> Weeks { get; set; }
        public ObservableCollection<Staff> Roster { get; set; }

        public ArchiveStaffWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Weeks = CollectionManager.GetRosterWeeks();
            Roster = new ObservableCollection<Staff>();
        }

        private void lstWeeks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Roster = CollectionManager.GetRoster((double)lstWeeks.SelectedValue);
            lstRoster.ItemsSource = Roster;
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstRoster.SelectedIndex > -1)
            {
                MessageDialogResult choice = await this.ShowMessageAsync("",
                    "Are you sure you want to delete this Archive?",
                    MessageDialogStyle.AffirmativeAndNegative);
                if (choice == MessageDialogResult.Affirmative)
                {
                    Staff Selected = (Staff)lstRoster.SelectedItem;
                    CollectionManager.DeleteRoster(Selected.Id, (double)lstWeeks.SelectedValue);
                    lstRoster.Items.Remove(Selected);
                }
            }
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            CalculateDialog calculateDialog = new CalculateDialog();
            calculateDialog.Owner = this;
            calculateDialog.ShowDialog();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
