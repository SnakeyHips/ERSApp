using System;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ERSApp.Models;
using ERSApp.ViewModels;

namespace ERSApp.Views
{
    public partial class UpdateHolidayWindow : MetroWindow
    {
        Holiday Selected;

        public UpdateHolidayWindow(Holiday h)
        {
            InitializeComponent();
            this.Selected = h;
            txtName.Text = Selected.Name;
            dateHoliday.SelectedDate = DateTime.Parse(Selected.Date);
        }

        private async void btnUpdateHoliday_Click(object sender, RoutedEventArgs e)
        {
            if (dateHoliday.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Date.");
            }
            else
            {
                Selected.Date = dateHoliday.SelectedDate.Value.ToShortDateString();
                AdminViewModel.UpdateHoliday(Selected);
                this.DialogResult = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
