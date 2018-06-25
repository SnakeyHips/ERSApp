using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ERSApp.Models;
using ERSApp.ViewModels;

namespace ERSApp.Views
{
    public partial class UpdateStaffWindow : MetroWindow
    {
        Staff Selected;

        public UpdateStaffWindow(Staff s)
        {
            InitializeComponent();
            this.Selected = s;
            txtId.Text = Selected.Id.ToString();
            txtName.Text = Selected.Name;
            cboRole.Items.Add("SV");
            cboRole.Items.Add("DRI");
            cboRole.Items.Add("RN");
            cboRole.Items.Add("CCA");
            cboRole.SelectedItem = Selected.Role;
            txtAddress.Text = Selected.Address;
            txtNumber.Text = Selected.Number;
            cboHours.Items.Add("20");
            cboHours.Items.Add("25");
            cboHours.Items.Add("30");
            cboHours.Items.Add("37.5");
            cboHours.SelectedItem = Selected.ContractHours.ToString();
            SetCheckboxes(Selected.WorkPattern);
        }

        //Method to go through and check checkboxes based on work pattern string
        private void SetCheckboxes(string workpattern)
        {
            if(workpattern != null)
            {
                string[] pattern = workpattern.Split(',');
                foreach(string p in pattern)
                {
                    CheckBox cbx = (CheckBox)FindName("cbx" + p);
                    if (cbx != null)
                    {
                        cbx.IsChecked = true;
                    }
                }
            }
        }

        private string GetWorkPattern()
        {
            StringBuilder workpattern = new StringBuilder();
            foreach(CheckBox cbx in grdUpdateStaff.FindChildren<CheckBox>())
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

        private async void btnUpdateStaff_Click(object sender, RoutedEventArgs e)
        {
            //Makes sure each input has an input before being made. Possibly better way to do this but this works and is simple to update
            if (cboRole.Text == "")
            {
                await this.ShowMessageAsync("", "Please select a Role.");
            }
            else if (txtAddress.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Address.");
            }
            else if (txtNumber.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Contact Number.");
            }
            else if (cboHours.Text == "")
            {
                await this.ShowMessageAsync("", "Please select Contract Hours.");
            }
            else
            {
                Selected.Role = cboRole.Text;
                Selected.Address = txtAddress.Text;
                Selected.Number = txtNumber.Text;
                Selected.ContractHours = double.Parse(cboHours.Text);
                Selected.WorkPattern = GetWorkPattern();
                StaffViewModel.UpdateStaff(Selected);
                this.DialogResult = true;
            }
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
