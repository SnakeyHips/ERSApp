using System;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ERSApp.Views
{
    public partial class StaffReportDialog : MetroWindow
    {
        public StaffReportDialog()
        {
            InitializeComponent();
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (dateStart.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Start Date.");
            }
            else if (dateEnd.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter an End Date.");
            }
            else if (DateTime.Parse(dateEnd.Text).CompareTo(DateTime.Parse(dateStart.Text)) < 0)
            {
                await this.ShowMessageAsync("", "Please enter a valid End Date.");
            }
            else
            {
                DialogResult = true;
            }
        }

        private void btnWeek_Click(object sender, RoutedEventArgs e)
        {
            if(dateEnd.SelectedDate != null)
            {
                dateStart.SelectedDate = dateEnd.SelectedDate.Value.AddDays(-28);
            }
        }

        private void btnMonth_Click(object sender, RoutedEventArgs e)
        {
            if (dateEnd.SelectedDate != null)
            {
                dateStart.SelectedDate = dateEnd.SelectedDate.Value.AddMonths(-3);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
