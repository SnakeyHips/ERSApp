using System.Collections.Generic;
using System.Windows;
using ERSApp.Models;
using ERSApp.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ERSApp.Views
{
    public partial class UpdateTeamWindow : MetroWindow
    {
        Team Selected;
        public List<Staff> SVList { get; set; }
        public List<Staff> DRIList { get; set; }
        public List<Staff> RNList { get; set; }
        public List<Staff> CCAList { get; set; }

        public UpdateTeamWindow(Team s)
        {
            InitializeComponent();
            this.DataContext = new SiteViewModel();
            Selected = s;
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
            cboSV1.SelectedValue = Selected.SV1Id;
            cboDRI1.SelectedValue = Selected.DRI1Id;
            cboDRI2.SelectedValue = Selected.DRI2Id;
            cboRN1.SelectedValue = Selected.RN1Id;
            cboRN2.SelectedValue = Selected.RN2Id;
            cboRN3.SelectedValue = Selected.RN3Id;
            cboCCA1.SelectedValue = Selected.CCA1Id;
            cboCCA2.SelectedValue = Selected.CCA2Id;
            cboCCA3.SelectedValue = Selected.CCA3Id;
        }

        private async void btnUpdateTeam_Click(object sender, RoutedEventArgs e)
        {
            //Makes sure each input has an input before being made. Possibly better way to do this but this works and is simple to update
            if (cboSV1.Text == "")
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
                Selected.SV1Id = (int)cboSV1.SelectedValue;
                Selected.SV1Name = cboSV1.Text;
                Selected.DRI1Id = (int)cboDRI1.SelectedValue;
                Selected.DRI1Name = cboDRI1.Text;
                Selected.DRI2Id = (int)cboDRI2.SelectedValue;
                Selected.DRI2Name = cboDRI2.Text;
                Selected.RN1Id = (int)cboRN1.SelectedValue;
                Selected.RN1Name = cboRN1.Text;
                Selected.RN2Id = (int)cboRN2.SelectedValue;
                Selected.RN2Name = cboRN2.Text;
                Selected.RN3Id = (int)cboRN3.SelectedValue;
                Selected.RN3Name = cboRN3.Text;
                Selected.CCA1Id = (int)cboCCA1.SelectedValue;
                Selected.CCA1Name = cboCCA1.Text;
                Selected.CCA2Id = (int)cboCCA2.SelectedValue;
                Selected.CCA2Name = cboCCA2.Text;
                Selected.CCA3Id = (int)cboCCA3.SelectedValue;
                Selected.CCA3Name = cboCCA3.Text;
                this.DialogResult = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
