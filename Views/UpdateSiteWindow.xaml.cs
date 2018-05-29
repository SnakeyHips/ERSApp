using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ERSApp.Model;
using ERSApp.ViewModel;

namespace ERSApp.Views
{
    public partial class UpdateSiteWindow : MetroWindow
    {
        Site Selected;

        public UpdateSiteWindow(Site s)
        {
            InitializeComponent();
            this.Selected = s;
            txtName.Text = Selected.Name;
            txtType.Text = Selected.Type;
            txtTimes.Text = Selected.Times;
        }

        private async void btnUpdateSite_Click(object sender, RoutedEventArgs e)
        {
            if (txtTimes.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter Clinic Times.");
            }
            else
            {
                Selected.Times = txtTimes.Text;
                AdminViewModel.UpdateSite(Selected);
                this.DialogResult = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
