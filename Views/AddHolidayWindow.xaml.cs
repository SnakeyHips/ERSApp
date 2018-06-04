using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ERSApp.Models;
using ERSApp.ViewModels;

namespace ERSApp.Views
{
    public partial class AddHolidayWindow : MetroWindow
    {
        public AddHolidayWindow()
        {
            InitializeComponent();
        }

        private async void btnAddHoliday_Click(object sender, RoutedEventArgs e)
        {
            if(txtName.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Name.");
            }
            else if (dateHoliday.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Date.");
            }
            else
            {
                Holiday temp = new Holiday()
                {
                    Name = txtName.Text,
                    Date = dateHoliday.SelectedDate.Value.ToShortDateString()
                };
                AdminViewModel.AddHoliday(temp);
                AdminViewModel.Holidays.Add(temp);
                this.DialogResult = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
