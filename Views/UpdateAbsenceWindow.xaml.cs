using System;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ERSApp.Model;
using ERSApp.ViewModel;
using System.Text.RegularExpressions;

namespace ERSApp.Views
{
    public partial class UpdateAbsenceWindow : MetroWindow
    {
        Absence Selected;
        public UpdateAbsenceWindow(Absence a)
        {
            InitializeComponent();
            this.Selected = a;
            txtId.Text = Selected.StaffId.ToString();
            txtName.Text = Selected.StaffName;
            txtHours.Text = Selected.Length.ToString();
            cboType.Items.Add("Day Off");
            cboType.Items.Add("Annual Leave");
            cboType.Items.Add("Sick Leave");
            cboType.Items.Add("Special Leave");
            cboType.Items.Add("Training");
            cboType.SelectedItem = Selected.Type;
            txtStart.Text = Selected.StartDate;
            txtEnd.Text = Selected.EndDate;
        }

        private async void btnUpdateAbsence_Click(object sender, RoutedEventArgs e)
        {
            if (txtHours.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a valid End Date.");
            }
            else
            {
                Selected.Type = cboType.Text;
                double old = Selected.Length;
                Selected.Length = double.Parse(txtHours.Text);
                AbsenceViewModel.UpdateAbsence(Selected);
                double absence = Selected.Length - old;
                StaffViewModel.UpdateAbsence(Selected.StaffId, absence, StaffViewModel.GetWeek(DateTime.Parse(txtStart.Text)));
                this.DialogResult = true;
            }
        }

        //Method to force only numbers in textbox input
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            //Use this in the xaml file to only allow numbers in input
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
