using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ERSApp.Models;
using ERSApp.ViewModels;
using System.Collections.Generic;


namespace ERSApp.Views
{
    public partial class CalculateDialog : MetroWindow
    {
        public CalculateDialog()
        {
            InitializeComponent();
        }

        private async void btnFindById_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text != "")
            {
                try
                {
                    int textId = int.Parse(txtId.Text);
                    foreach (Staff s in StaffViewModel.Staffs)
                    {
                        if (s.Id == textId)
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
                if (findByNameDialog.DialogResult == true)
                {
                    Staff Found = (Staff)findByNameDialog.lstNames.SelectedItem;
                    txtId.Text = Found.Id.ToString();
                    txtName.Text = Found.Name;
                    txtName.IsEnabled = false;
                    txtId.IsEnabled = false;
                }
            }
        }

        private void btnWeek_Click(object sender, RoutedEventArgs e)
        {
            if (dateEnd.SelectedDate != null)
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

        private async void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Staff ID.");
            }
            else if (txtName.Text == "")
            {
                await this.ShowMessageAsync("", "Please find a valid Staff via ID.");
            }
            else if (dateStart.Text == "")
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
                 double weekStart = StaffViewModel.GetWeek(DateTime.Parse(dateStart.Text));
                double weekEnd = StaffViewModel.GetWeek(DateTime.Parse(dateEnd.Text));
                double contractCount = 0.0;
                double appointedCount = 0.0;
                double absenceCount = 0.0;
                double lowrateuCount = 0.0;
                double highrateuCount = 0.0;
                double overtimeCount = 0.0;

                List<Staff> SelectedRange = new List<Staff>();
                for(double i = weekStart; i <= weekEnd; i++)
                {
                    Staff temp = StaffViewModel.GetStaffRoster(i, int.Parse(txtId.Text));
                    if (temp != null)
                    {
                        SelectedRange.Add(temp);
                    }
                }

                foreach(Staff s in SelectedRange)
                {
                    contractCount += s.ContractHours;
                    appointedCount += s.AppointedHours;
                    absenceCount += s.AbsenceHours;
                    lowrateuCount += s.LowRateUHours;
                    highrateuCount += s.HighRateUHours;
                    overtimeCount += s.OvertimeHours;
                }
                txtContractCount.Text = contractCount.ToString();
                txtAppointedCount.Text = appointedCount.ToString();
                txtAbsenceCount.Text = absenceCount.ToString();
                txtLowRateUCount.Text = lowrateuCount.ToString();
                txtHighRateUCount.Text = highrateuCount.ToString();
                txtOvertimeCount.Text = overtimeCount.ToString();
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
            DialogResult = false;
        }
    }
}
