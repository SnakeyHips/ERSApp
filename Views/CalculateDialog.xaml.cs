using System;
using System.Linq;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ERSApp.Model;
using ERSApp.ViewModel;
using System.Collections.Generic;


namespace ERSApp.Views
{
    public partial class CalculateDialog : MetroWindow
    {
        Staff Selected;
        public CalculateDialog()
        {
            InitializeComponent();
        }

        private async void btnFind_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text != "")
            {
                try
                {
                    Selected = StaffViewModel.Staffs.First(x => x.Id == int.Parse(txtId.Text));
                    txtName.Text = Selected.Name;
                }
                catch
                {
                    await this.ShowMessageAsync("", "No staff found with ID provided.");
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
                double weekStart = CollectionManager.GetWeek(DateTime.Parse(dateStart.Text));
                double weekEnd = CollectionManager.GetWeek(DateTime.Parse(dateEnd.Text));
                double contractCount = 0.0;
                double appointedCount = 0.0;
                double absenceCount = 0.0;
                double unsocialCount = 0.0;

                List<Staff> SelectedRange = new List<Staff>();
                for(double i = weekStart; i < weekEnd; i++)
                {
                    Staff temp = CollectionManager.GetStaffRoster(i, Selected.Id);
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
                    unsocialCount += s.UnsocialHours;
                }
                txtContractCount.Text = contractCount.ToString();
                txtAppointedCount.Text = appointedCount.ToString();
                txtAbsenceCount.Text = absenceCount.ToString();
                txtUnsocialCount.Text = unsocialCount.ToString();
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
