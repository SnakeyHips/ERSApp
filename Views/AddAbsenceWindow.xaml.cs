using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ERSApp.Model;
using ERSApp.ViewModel;
using System.Text.RegularExpressions;

namespace ERSApp.Views
{
    public partial class AddAbsenceWindow : MetroWindow
    {
        public AddAbsenceWindow()
        {
            InitializeComponent();
            cboType.Items.Add("Day Off");
            cboType.Items.Add("Annual Leave");
            cboType.Items.Add("Sick Leave");
            cboType.Items.Add("Special Leave");
            cboType.Items.Add("Training");
        }

        private async void btnFind_Click(object sender, RoutedEventArgs e)
        {
            if(txtId.Text != "")
            {
                try
                {
                    txtName.Text = StaffViewModel.Staffs.First(x => x.Id == int.Parse(txtId.Text)).Name;
                }
                catch
                {
                    await this.ShowMessageAsync("", "No staff found with ID provided.");
                }
            }
        }

        private async void btnAddAbsence_Click(object sender, RoutedEventArgs e)
        {
            if(txtId.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Staff ID.");
            }
            else if (txtName.Text == "")
            {
                await this.ShowMessageAsync("", "Please find a valid Staff via ID.");
            }
            else if (cboType.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter an Absence Type");
            }
            else  if (dateStart.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Start Date.");
            }
            else if (DateTime.Parse(dateStart.Text).CompareTo(DateTime.Now) < 0)
            {
                await this.ShowMessageAsync("", "Please enter a valid Start Date.");
            }
            else if (dateEnd.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter an End Date.");
            }
            else if (DateTime.Parse(dateEnd.Text).CompareTo(DateTime.Parse(dateStart.Text)) < 0)
            {
                await this.ShowMessageAsync("", "Please enter a valid End Date.");
            }
            else if (txtHours.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter an Absence Length.");
            }
            else
            {
                Absence temp = new Absence()
                {
                    StaffId = int.Parse(txtId.Text),
                    StaffName = txtName.Text,
                    Type = cboType.Text,
                    StartDate = dateStart.Text,
                    EndDate = dateEnd.Text,
                    Length = double.Parse(txtHours.Text)
                };
                if(CollectionManager.AddAbsence(temp) > 0)
                {
                    AbsenceViewModel.Absences.Add(temp);
                    //Update staff's absence hours
                    CollectionManager.UpdateRoster(temp.StaffId, 0.0, temp.Length, CollectionManager.GetWeek(DateTime.Parse(dateStart.Text)));
                    
                    this.Close();
                }
                else
                {
                    await this.ShowMessageAsync("", "Duplicate Absence found.");
                }
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
            this.Close();
        }
    }
}
