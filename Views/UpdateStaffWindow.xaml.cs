using System.Linq;
using System.Text;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ERSApp.Model;
using ERSApp.ViewModel;
using System.Windows.Controls;

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
                workpattern.Split(',').ToList().ForEach(x =>
                {
                    CheckBox cbx = (CheckBox)FindName("cbx" + x);
                    if(cbx != null)
                    {
                        cbx.IsChecked = true;
                    }
                });
            }
        }

        private string GetWorkPattern()
        {
            StringBuilder workpattern = new StringBuilder();
            grdUpdateStaff.FindChildren<CheckBox>().ToList().ForEach(x =>
            {
                if (x.IsChecked == true)
                {
                    workpattern.Append(x.Content);
                    workpattern.Append(",");
                }
            });
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
            else if (cboHours.Text == "")
            {
                await this.ShowMessageAsync("", "Please select Contract Hours.");
            }
            else
            {
                Selected.Role = cboRole.Text;
                Selected.ContractHours = double.Parse(cboHours.Text);
                Selected.WorkPattern = GetWorkPattern();
                StaffViewModel.UpdateStaff(Selected);
                this.DialogResult = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
