using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ERSApp.Models;
using ERSApp.ViewModels;
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

        private async void btnFindById_Click(object sender, RoutedEventArgs e)
        {
            if(txtId.Text != "")
            {
                try
                {
                    int textId = int.Parse(txtId.Text);
                    foreach(Staff s in StaffViewModel.Staffs)
                    {
                        if(s.Id == textId)
                        {
                            txtName.Text = s.Name;
                            txtName.IsEnabled = false;
                            txtId.IsEnabled = false;
                            break;
                        }
                    }
                }
                catch
                {
                    await this.ShowMessageAsync("", "No staff found with ID provided.");
                }
            }
        }

        private void txtId_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtName.Clear();
        }

        private void btnFindByName_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text != "")
            {
                FindByNameDialog findByNameDialog = new FindByNameDialog(txtName.Text);
                findByNameDialog.Owner = this;
                findByNameDialog.ShowDialog();
                if(findByNameDialog.DialogResult == true)
                {
                    Staff Found = (Staff)findByNameDialog.lstNames.SelectedItem;
                    txtId.Text = Found.Id.ToString();
                    txtName.Text = Found.Name;
                    txtName.IsEnabled = false;
                    txtId.IsEnabled = false;
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
            else if (dateEnd.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter an End Date.");
            }
            else if (dateEnd.SelectedDate.Value.CompareTo(dateStart.SelectedDate.Value) < 0)
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
                    Hours = double.Parse(txtHours.Text)
                };
                if(AbsenceViewModel.AddAbsence(temp) > 0)
                {
                    AbsenceViewModel.Absences.Add(temp);
                    //Update staff's absence hours
                    StaffViewModel.UpdateAbsence(temp.StaffId, temp.Hours, StaffViewModel.GetWeek(DateTime.Parse(dateStart.Text)));
                    this.DialogResult = true;
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
            this.DialogResult = false;
        }
    }
}
