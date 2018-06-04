using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ERSApp.Models;
using ERSApp.ViewModels;

namespace ERSApp.Views
{
    public partial class AddSiteWindow : MetroWindow
    {
        public AddSiteWindow()
        {
            InitializeComponent();
            cboType.Items.Add("Community");
            cboType.Items.Add("MDC");
        }

        private async void btnAddSite_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Name.");
            }
            else if (cboType.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Type.");
            }
            else if (txtTimes.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter Clinic Times.");
            }
            else
            {
                Site temp = new Site()
                {
                    Name = txtName.Text,
                    Type = cboType.Text,
                    Times = txtTimes.Text
                };
                AdminViewModel.AddSite(temp);
                AdminViewModel.Sites.Add(temp);
                this.DialogResult = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
