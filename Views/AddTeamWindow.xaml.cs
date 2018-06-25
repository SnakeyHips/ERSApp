using System.Collections.Generic;
using System.Windows;
using ERSApp.Models;
using ERSApp.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ERSApp.Views
{
    public partial class AddTeamWindow : MetroWindow
    {
        public List<Staff> SVList { get; set; }
        public List<Staff> DRIList { get; set; }
        public List<Staff> RNList { get; set; }
        public List<Staff> CCAList { get; set; }

        public AddTeamWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            SVList = new List<Staff>();
            DRIList = new List<Staff>();
            RNList = new List<Staff>();
            CCAList = new List<Staff>();
            foreach (Staff x in StaffViewModel.Staffs)
            {
                switch (x.Role)
                {
                    case "SV":
                        SVList.Add(x);
                        break;
                    case "DRI":
                        DRIList.Add(x);
                        break;
                    case "RN":
                        RNList.Add(x);
                        break;
                    case "CCA":
                        CCAList.Add(x);
                        break;
                }
            }
        }

        private async void btnAddTeam_Click(object sender, RoutedEventArgs e)
        {
            //Makes sure each input has an input before being made. Possibly better way to do this but this works and is simple to update
            if (txtName.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Name.");
            }
            else if (cboSV1.Text == "")
            {
                await this.ShowMessageAsync("", "Please select a SV1.");
            }
            else if (cboDRI1.Text == "")
            {
                await this.ShowMessageAsync("", "Please select a DRI1.");
            }
            else if (cboDRI2.Text == "")
            {
                await this.ShowMessageAsync("", "Please select a DRI2.");
            }
            else if ((int)cboDRI2.SelectedValue == (int)cboDRI1.SelectedValue)
            {
                await this.ShowMessageAsync("", "Duplicate DRI2 found.");
            }
            else if (cboRN1.Text == "")
            {
                await this.ShowMessageAsync("", "Please select a RN1.");
            }
            else if (cboRN2.Text == "")
            {
                await this.ShowMessageAsync("", "Please select a RN2.");
            }
            else if ((int)cboRN2.SelectedValue == (int)cboRN1.SelectedValue)
            {
                await this.ShowMessageAsync("", "Duplicate RN2 found.");
            }
            else if (cboRN3.Text == "")
            {
                await this.ShowMessageAsync("", "Please select a RN3.");
            }
            else if ((int)cboRN3.SelectedValue == (int)cboRN1.SelectedValue ||
                    (int)cboRN3.SelectedValue == (int)cboRN2.SelectedValue)
            {
                await this.ShowMessageAsync("", "Duplicate RN3 found.");
            }
            else if (cboCCA1.Text == "")
            {
                await this.ShowMessageAsync("", "Please select a CCA1.");
            }
            else if (cboCCA2.Text == "")
            {
                await this.ShowMessageAsync("", "Please select a CCA2.");
            }
            else if ((int)cboCCA2.SelectedValue == (int)cboCCA1.SelectedValue)
            {
                await this.ShowMessageAsync("", "Duplicate CCA2 found.");
            }
            else if (cboCCA3.Text == "")
            {
                await this.ShowMessageAsync("", "Please select a CCA3.");
            }
            else if ((int)cboCCA3.SelectedValue == (int)cboCCA1.SelectedValue ||
                    (int)cboCCA3.SelectedValue == (int)cboCCA2.SelectedValue)
            {
                await this.ShowMessageAsync("", "Duplicate CCA3 found.");
            }
            else
            {
                Team temp = new Team()
                {
                    Name = txtName.Text,
                    SV1Id = (int)cboSV1.SelectedValue,
                    SV1Name = cboSV1.Text,
                    DRI1Id = (int)cboDRI1.SelectedValue,
                    DRI1Name = cboDRI1.Text,
                    DRI2Id = (int)cboDRI2.SelectedValue,
                    DRI2Name = cboDRI2.Text,
                    RN1Id = (int)cboRN1.SelectedValue,
                    RN1Name = cboRN1.Text,
                    RN2Id = (int)cboRN2.SelectedValue,
                    RN2Name = cboRN2.Text,
                    RN3Id = (int)cboRN3.SelectedValue,
                    RN3Name = cboRN3.Text,
                    CCA1Id = (int)cboCCA1.SelectedValue,
                    CCA1Name = cboCCA1.Text,
                    CCA2Id = (int)cboCCA2.SelectedValue,
                    CCA2Name = cboCCA2.Text,
                    CCA3Id = (int)cboCCA3.SelectedValue,
                    CCA3Name = cboCCA3.Text
                };
                if (TeamViewModel.AddTeam(temp) > 0)
                {
                    this.DialogResult = true;
                }
                else
                {
                    await this.ShowMessageAsync("", "Duplicate Team found.");
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
