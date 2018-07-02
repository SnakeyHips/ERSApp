using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using ERSApp.Models;
using ERSApp.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ERSApp.Views
{
    public partial class AddStaffWindow : MetroWindow
    {
        public AddStaffWindow()
        {
            InitializeComponent();
            cboRole.Items.Add("SV");
            cboRole.Items.Add("DRI");
            cboRole.Items.Add("RN");
            cboRole.Items.Add("CCA");
            cboHours.Items.Add("20");
            cboHours.Items.Add("25");
            cboHours.Items.Add("30");
            cboHours.Items.Add("37.5");
        }

        private void cboRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboSkill.Items.Clear();
            foreach(Skill s in AdminViewModel.Skills)
            {
                if (s.Role.Equals(cboRole.SelectedItem))
                {
                    cboSkill.Items.Add(s.Name);
                }
            }
        }

        private async void btnAddStaff_Click(object sender, RoutedEventArgs e)
        {
            //Makes sure each input has an input before being made
            if (txtId.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter an ID.");
            }
            else if (txtName.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a First Name.");
            }
            else if (cboRole.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Role.");
            }
            else if (cboSkill.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Skill.");
            }
            else if (txtAddress.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Address.");
            }
            else if (txtNumber.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Contact Number.");
            }
            else if (txtNumber.Text.Length < 11)
            {
                await this.ShowMessageAsync("", "Please enter a valid Contact Number.");
            }
            else if (cboHours.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter Contract Hours.");
            }
            else
            {
                Staff temp = new Staff()
                {
                    Id = int.Parse(txtId.Text),
                    Name = txtName.Text,
                    Role = cboRole.Text,
                    Skill = cboSkill.Text,
                    Address = txtAddress.Text,
                    Number = txtNumber.Text,
                    ContractHours = double.Parse(cboHours.Text),
                    WorkPattern = GetWorkPattern()
                };
                if (StaffViewModel.AddStaff(temp) > 0)
                {
                    StaffViewModel.Staffs.Add(temp);
                    this.DialogResult = true;
                }
                else
                {
                    await this.ShowMessageAsync("", "Duplicate Staff ID found.");
                }
            }

        }

        private string GetWorkPattern()
        {
            StringBuilder workpattern = new StringBuilder();
            foreach (CheckBox cbx in grdAddStaff.FindChildren<CheckBox>())
            {
                if (cbx.IsChecked == true)
                {
                    workpattern.Append(cbx.Content);
                    workpattern.Append(",");
                }
            }
            if (workpattern.Length > 0)
            {
                workpattern.Remove(workpattern.Length - 1, 1);
            }
            return workpattern.ToString();
        }

        //Method to force only numbers in textbox input
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            //Use this in the xaml file to only allow numbers in input
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
