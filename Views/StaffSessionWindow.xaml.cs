using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System.Text.RegularExpressions;
using ERSApp.Models;
using ERSApp.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using LiveCharts;
using LiveCharts.Wpf;
using FastMember;

namespace ERSApp.Views
{
    public partial class StaffSessionWindow : MetroWindow
    {
        Session Selected;
        TypeAccessor Accessor;
        public List<Staff> AvailableStaff { get; set; }
        public List<Staff> SVList { get; set; }
        public List<Staff> DRIList { get; set; }
        public List<Staff> RNList { get; set; }
        public List<Staff> CCAList { get; set; }
        public List<string> TimesList { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        double Week = StaffViewModel.GetWeek(SelectedDate.Date);

        public StaffSessionWindow(Session s)
        {
            InitializeComponent();
            this.DataContext = this;
            AvailableStaff = StaffViewModel.GetAvailableStaff();
            //Put all relevant roles in respective list
            SVList = new List<Staff>();
            DRIList = new List<Staff>();
            RNList = new List<Staff>();
            CCAList = new List<Staff>();
            foreach (Staff x in AvailableStaff)
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

            this.Selected = s;
            Accessor = TypeAccessor.Create(Selected.GetType());
            SeriesCollection = new SeriesCollection();
            double Start = double.Parse(Selected.Time.Substring(0, 2));
            double End = Start + Selected.LOD;

            TimesList = new List<string>();
            for (double i = Start - 1; i < End + 1.5; i++)
            {
                TimesList.Add(i + ":00");
                TimesList.Add(i + ":15");
                TimesList.Add(i + ":30");
                TimesList.Add(i + ":45");
            }

            //Enable amount of positions needed based on number of chairs
            if (Selected.Chairs > 5)
            {
                if (Selected.Type != "MDC")
                {
                    cboDRI2.IsEnabled = true;
                    //cboDRI2.BorderBrush = (SolidColorBrush)Application.Current.Resources["AccentColorBrush"]; - if want to change borderbrush
                    cboRN2.IsEnabled = true;
                    cboCCA2.IsEnabled = true;
                }
                if (Selected.Chairs > 8)
                {
                    cboRN3.IsEnabled = true;
                    cboCCA3.IsEnabled = true;
                }
            }

            //Set UI to match selected
            lblHeader.Content = DateTime.Parse(Selected.Date).DayOfWeek.ToString() +
                " - " + Selected.Date + " - " + Selected.Site;
            txtClinicTime.Text = Selected.Time;
            txtLOD.Text = Selected.LOD.ToString();
            txtType.Text = Selected.Type;
            txtChairs.Text = Selected.Chairs.ToString();
            txtBleeds.Text = Selected.Bleeds.ToString();
            txtStaffCount.Text = Selected.StaffCount.ToString();

            //Autopopulate cbos from StaffList
            //Pastel colours from colorhexa.com
            if (Selected.SV1Id != 0)
            {
                cboSV1.SelectedValue = Selected.SV1Id;
                txtSV1LOD.Text = Selected.SV1LOD.ToString();
                txtSV1UNS.Text = Selected.SV1UNS.ToString();
                SeriesCollection.Add(CreateRow("SV1", 255, 105, 97, Selected.SV1LOD));
            }
            if (Selected.DRI1Id != 0)
            {
                cboDRI1.SelectedValue = Selected.DRI1Id;
                txtDRI1LOD.Text = Selected.DRI1LOD.ToString();
                txtDRI1UNS.Text = Selected.DRI1UNS.ToString();
                SeriesCollection.Add(CreateRow("DRI1", 177, 156, 217, Selected.DRI1LOD));
            }
            if (Selected.DRI2Id != 0)
            {
                cboDRI2.SelectedValue = Selected.DRI2Id;
                txtDRI2LOD.Text = Selected.DRI2LOD.ToString();
                txtDRI2UNS.Text = Selected.DRI2UNS.ToString();
                SeriesCollection.Add(CreateRow("DRI2", 192, 174, 224, Selected.DRI2LOD));
            }
            if (Selected.RN1Id != 0)
            {
                cboRN1.SelectedValue = Selected.RN1Id;
                txtRN1LOD.Text = Selected.RN1LOD.ToString();
                txtRN1UNS.Text = Selected.RN1UNS.ToString();
                SeriesCollection.Add(CreateRow("RN1", 134, 197, 218, Selected.RN1LOD));
            }
            if (Selected.RN2Id != 0)
            {
                cboRN2.SelectedValue = Selected.RN2Id;
                txtRN2LOD.Text = Selected.RN2LOD.ToString();
                txtRN2UNS.Text = Selected.RN2UNS.ToString();
                SeriesCollection.Add(CreateRow("RN2", 153, 207, 224, Selected.RN2LOD));
            }
            if (Selected.RN3Id != 0)
            {
                cboRN3.SelectedValue = Selected.RN3Id;
                txtRN3LOD.Text = Selected.RN3LOD.ToString();
                txtRN3UNS.Text = Selected.RN3UNS.ToString();
                SeriesCollection.Add(CreateRow("RN3", 173, 216, 230, Selected.RN3LOD));
            }
            if (Selected.CCA1Id != 0)
            {
                cboCCA1.SelectedValue = Selected.CCA1Id;
                txtCCA1LOD.Text = Selected.CCA1LOD.ToString();
                txtCCA1UNS.Text = Selected.CCA1UNS.ToString();
                SeriesCollection.Add(CreateRow("CCA1", 139, 226, 139, Selected.CCA1LOD));
            }
            if (Selected.CCA2Id != 0)
            {
                cboCCA2.SelectedValue = Selected.CCA2Id;
                txtCCA2LOD.Text = Selected.CCA2LOD.ToString();
                txtCCA2UNS.Text = Selected.CCA2UNS.ToString();
                SeriesCollection.Add(CreateRow("CCA2", 160, 231, 160, Selected.CCA2LOD));
            }
            if (Selected.CCA3Id != 0)
            {
                cboCCA3.SelectedValue = Selected.CCA3Id;
                txtCCA3LOD.Text = Selected.CCA3LOD.ToString();
                txtCCA3UNS.Text = Selected.CCA3UNS.ToString();
                SeriesCollection.Add(CreateRow("CCA3", 180, 236, 180, Selected.CCA3LOD));
            }
            //Used to set window height if no chart present
            if (SeriesCollection.Count < 1)
            {
                this.Height = 575;
            }
        }

        //Listeners to enable corresponding time cbos
        private void cboSV1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtSV1LOD.IsEnabled = true;
            txtSV1UNS.IsEnabled = true;
        }

        private void cboDRI1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtDRI1LOD.IsEnabled = true;
            txtDRI1UNS.IsEnabled = true;
        }

        private void cboDRI2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtDRI2LOD.IsEnabled = true;
            txtDRI2UNS.IsEnabled = true;
        }

        private void cboRN1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtRN1LOD.IsEnabled = true;
            txtRN1UNS.IsEnabled = true;
        }

        private void cboRN2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtRN2LOD.IsEnabled = true;
            txtRN2UNS.IsEnabled = true;
        }

        private void cboRN3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtRN3LOD.IsEnabled = true;
            txtRN3UNS.IsEnabled = true;
        }

        private void cboCCA1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtCCA1LOD.IsEnabled = true;
            txtCCA1UNS.IsEnabled = true;
        }

        private void cboCCA2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtCCA2LOD.IsEnabled = true;
            txtCCA2UNS.IsEnabled = true;
        }

        private void cboCCA3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtCCA3LOD.IsEnabled = true;
            txtCCA3UNS.IsEnabled = true;
        }

        private void btnResetSV1_Click(object sender, RoutedEventArgs e)
        {
            cboSV1.SelectedValue = null;
            txtSV1LOD.Text = null;
            txtSV1LOD.IsEnabled = false;
            txtSV1UNS.Text = null;
            txtSV1UNS.IsEnabled = false;
        }

        private void btnResetDRI1_Click(object sender, RoutedEventArgs e)
        {
            cboDRI1.SelectedValue = null;
            txtDRI1LOD.Text = null;
            txtDRI1LOD.IsEnabled = false;
            txtDRI1UNS.Text = null;
            txtDRI1UNS.IsEnabled = false;
        }

        private void btnResetDRI2_Click(object sender, RoutedEventArgs e)
        {
            cboDRI2.SelectedValue = null;
            txtDRI2LOD.Text = null;
            txtDRI2LOD.IsEnabled = false;
            txtDRI2UNS.Text = null;
            txtDRI2UNS.IsEnabled = false;
        }

        private void btnResetRN1_Click(object sender, RoutedEventArgs e)
        {
            cboRN1.SelectedValue = null;
            txtRN1LOD.Text = null;
            txtRN1LOD.IsEnabled = false;
            txtRN1UNS.Text = null;
            txtRN1UNS.IsEnabled = false;
        }

        private void btnResetRN2_Click(object sender, RoutedEventArgs e)
        {
            cboRN2.SelectedValue = null;
            txtRN2LOD.Text = null;
            txtRN2LOD.IsEnabled = false;
            txtRN2UNS.Text = null;
            txtRN2UNS.IsEnabled = false;
        }

        private void btnResetRN3_Click(object sender, RoutedEventArgs e)
        {
            cboRN3.SelectedValue = null;
            txtRN3LOD.Text = null;
            txtRN3LOD.IsEnabled = false;
            txtRN3UNS.Text = null;
            txtRN3UNS.IsEnabled = false;
        }

        private void btnResetCCA1_Click(object sender, RoutedEventArgs e)
        {
            cboCCA1.SelectedValue = null;
            txtCCA1LOD.Text = null;
            txtCCA1LOD.IsEnabled = false;
            txtCCA1UNS.Text = null;
            txtCCA1UNS.IsEnabled = false;
        }

        private void btnResetCCA2_Click(object sender, RoutedEventArgs e)
        {
            cboCCA2.SelectedValue = null;
            txtCCA2LOD.Text = null;
            txtCCA2LOD.IsEnabled = false;
            txtCCA2UNS.Text = null;
            txtCCA2UNS.IsEnabled = false;
        }

        private void btnResetCCA3_Click(object sender, RoutedEventArgs e)
        {
            cboCCA3.SelectedValue = null;
            txtCCA3LOD.Text = null;
            txtCCA3LOD.IsEnabled = false;
            txtCCA3UNS.Text = null;
            txtCCA3UNS.IsEnabled = false;
        }

        private void btnAutoLOD_Click(object semder, RoutedEventArgs e)
        {
            if (cboSV1.SelectedItem != null)
            {
                txtSV1LOD.Text = txtLOD.Text;
                txtSV1UNS.Text = "0";
            }
            if (cboDRI1.SelectedItem != null)
            {
                txtDRI1LOD.Text = txtLOD.Text;
                txtDRI1UNS.Text = "0";
            }
            if (cboDRI2.SelectedItem != null)
            {
                txtDRI2LOD.Text = txtLOD.Text;
                txtDRI2UNS.Text = "0";
            }
            if (cboRN1.SelectedItem != null)
            {
                txtRN1LOD.Text = txtLOD.Text;
                txtRN1UNS.Text = "0";
            }
            if (cboRN2.SelectedItem != null)
            {
                txtRN2LOD.Text = txtLOD.Text;
                txtRN2UNS.Text = "0";
            }
            if (cboRN3.SelectedItem != null)
            {
                txtRN3LOD.Text = txtLOD.Text;
                txtRN3UNS.Text = "0";
            }
            if (cboCCA1.SelectedItem != null)
            {
                txtCCA1LOD.Text = txtLOD.Text;
                txtCCA1UNS.Text = "0";
            }
            if (cboCCA2.SelectedItem != null)
            {
                txtCCA2LOD.Text = txtLOD.Text;
                txtCCA2UNS.Text = "0";
            }
            if (cboCCA3.SelectedItem != null)
            {
                txtCCA3LOD.Text = txtLOD.Text;
                txtCCA3UNS.Text = "0";
            }
        }

        //Method for adding gantt row on chart
        private RowSeries CreateRow(string title, byte r, byte g, byte b, double lod)
        {
            if (xAxis.MaxValue < lod)
            {
                xAxis.MaxValue = lod;
            }

            return new RowSeries
            {
                Title = title,
                Fill = new SolidColorBrush(Color.FromRgb(r, g, b)),
                Values = new ChartValues<double> { lod }
            };
        }

        //Update Staff info - lots of if statements as properties can't be passed through methods
        //First one is commented as rest are same
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (Selected.Holiday)
            {
                UpdateStaffHoliday();
            }
            else
            {
                UpdateStaff();
            }

            //Check if LOD or Bleed values have changed
            if (txtLOD.Text != Selected.LOD.ToString())
            {
                Selected.LOD = double.Parse(txtLOD.Text);
            }
            if (txtBleeds.Text != Selected.Bleeds.ToString())
            {
                Selected.Bleeds = int.Parse(txtBleeds.Text);
            }

            //Go through and check if all roles are filled or not
            bool RolesFilled = true;
            foreach (ComboBox cbo in grdStaff.FindChildren<ComboBox>())
            {
                if (cbo.IsEnabled && cbo.Text == "")
                {
                    RolesFilled = false;
                }
            }
            Selected.State = RolesFilled ? 1 : 0;
            SessionViewModel.UpdateSessionStaff(Selected);
            //Go back to main window
            this.DialogResult = true;
        }

        private void Update(int id, string idname, string name, double lod, string lodname,
            double uns, string unsname, int cboid, string cboname, double txtlod, double txtuns)
        {
            if (id == 0)
            {
                //Assign new appoint staff
                Accessor[Selected, idname] = cboid;
                Accessor[Selected, name] = cboname;
                Accessor[Selected, lodname] = txtlod;
                Accessor[Selected, unsname] = txtuns;
                if (!name.StartsWith("RN"))
                {
                    Selected.StaffCount++;
                }
                //Add onto new staff's appointed hours/New record created in sql method
                StaffViewModel.UpdateAppointedUnsocial(cboid, txtlod, txtuns, Week);
            }
            //Check if same staff selected
            else if (id == cboid)
            {
                //Check if different times selected
                if (lod != txtlod)
                {
                    //If so, update times and appointed hours to use times selected
                    double appointed = txtlod - lod;
                    StaffViewModel.UpdateAppointed(id, appointed, Week);
                    Accessor[Selected, lodname] = txtlod;
                }
                //Check if different unsocial selected
                if (uns != txtuns)
                {
                    //If so, update times and appointed hours to use times selected
                    double unsocial = txtuns - uns;
                    StaffViewModel.UpdateUnsocial(id, unsocial, Week);
                    Accessor[Selected, unsname] = txtuns;
                }
            }
            //Check if different staff selected
            else if (id != cboid)
            {
                //Update old staff's appointed hours to remove length
                StaffViewModel.UpdateAppointedUnsocial(id, -lod, -uns, Week);
                //Assign new appoint staff
                Accessor[Selected, idname] = cboid;
                Accessor[Selected, name] = cboname;
                Accessor[Selected, lodname] = txtlod;
                Accessor[Selected, unsname] = txtuns;
                //Add onto new staff's appointed hours/New record created in sql method
                StaffViewModel.UpdateAppointedUnsocial(cboid, txtlod, txtuns, Week);
            }
        }

        private void UpdateHoliday(int id, string idname, string name, double lod, string lodname,
            double uns, string unsname, int cboid, string cboname, double txtlod, double txtuns)
        {
            if (id == 0)
            {
                //Assign new appoint staff
                Accessor[Selected, idname] = cboid;
                Accessor[Selected, name] = cboname;
                Accessor[Selected, lodname] = txtlod;
                Accessor[Selected, unsname] = txtuns;
                if (!name.StartsWith("RN"))
                {
                    Selected.StaffCount++;
                }
                //Add onto new staff's appointed hours/New record created in sql method
                StaffViewModel.UpdateAppointedHolidayUnsocial(cboid, txtlod, txtuns, Week);
            }
            //Check if same staff selected
            else if (id == cboid)
            {
                //Check if different times selected
                if (lod != txtlod)
                {
                    //If so, update times and appointed hours to use times selected
                    double appointed = txtlod - lod;
                    StaffViewModel.UpdateAppointedHoliday(id, appointed, Week);
                    Accessor[Selected, lodname] = txtlod;
                }
                //Check if different unsocial selected
                if (uns != txtuns)
                {
                    //If so, update times and appointed hours to use times selected
                    double unsocial = txtuns - uns;
                    StaffViewModel.UpdateUnsocial(id, unsocial, Week);
                    Accessor[Selected, unsname] = txtuns;
                }
            }
            //Check if different staff selected
            else if (id != cboid)
            {
                //Update old staff's appointed hours to remove length
                StaffViewModel.UpdateAppointedHolidayUnsocial(id, -lod, -uns, Week);
                //Assign new appoint staff
                Accessor[Selected, idname] = cboid;
                Accessor[Selected, name] = cboname;
                Accessor[Selected, lodname] = txtlod;
                Accessor[Selected, unsname] = txtuns;
                //Add onto new staff's appointed hours/New record created in sql method
                StaffViewModel.UpdateAppointedHolidayUnsocial(cboid, txtlod, txtuns, Week);
            }
        }

        private void Reset(int id, string idname, string name, double lod,
            string lodname, double uns, string unsname)
        {
            //Remove staff's appointed hours
            StaffViewModel.UpdateAppointedUnsocial(id, -lod, -uns, Week);
            //Remove any saved staff info
            Accessor[Selected, idname] = 0;
            Accessor[Selected, name] = "";
            Accessor[Selected, lodname] = 0.0;
            Accessor[Selected, unsname] = 0.0;
            if (!name.StartsWith("RN"))
            {
                Selected.StaffCount--;
            }
        }

        private async void UpdateStaff()
        {
            //First check if there is a selected item
            if (cboSV1.SelectedItem != null)
            {
                if (txtSV1LOD.Text == "")
                {
                    await this.ShowMessageAsync("", "Please enter a LOD value for SV1.");
                    return;
                }
                if (txtSV1UNS.Text == "")
                {
                    await this.ShowMessageAsync("", "Please enter a Unsocial value for SV1.");
                    return;
                }
                else
                {
                    Update(Selected.SV1Id, "SV1Id", "SV1Name", Selected.SV1LOD, "SV1LOD", Selected.SV1UNS, "SV1UNS",
                        (int)cboSV1.SelectedValue, cboSV1.Text, double.Parse(txtSV1LOD.Text), double.Parse(txtSV1UNS.Text));
                }
            }
            //Else remove if reset staff has been pressed
            else if (cboSV1.SelectedItem == null && Selected.SV1Id != 0)
            {
                Reset(Selected.SV1Id, "SV1Id", "SV1Name", Selected.SV1LOD, "SV1LOD", Selected.SV1UNS, "SV1UNS");
            }

            //Do same for other roles
            if (cboDRI1.SelectedItem != null)
            {
                if (txtDRI1LOD.Text == "")
                {
                    await this.ShowMessageAsync("", "Please enter a LOD value for DRI1.");
                    return;
                }
                if (txtDRI1UNS.Text == "")
                {
                    await this.ShowMessageAsync("", "Please enter a Unsocial value for DRI1.");
                    return;
                }
                else
                {
                    Update(Selected.DRI1Id, "DRI1Id", "DRI1Name", Selected.DRI1LOD, "DRI1LOD", Selected.DRI1UNS, "DRI1UNS",
                        (int)cboDRI1.SelectedValue, cboDRI1.Text, double.Parse(txtDRI1LOD.Text), double.Parse(txtDRI1UNS.Text));
                }
            }
            else if (cboDRI1.SelectedItem == null && Selected.DRI1Id != 0)
            {
                Reset(Selected.DRI1Id, "DRI1Id", "DRI1Name", Selected.DRI1LOD, "DRI1LOD", Selected.DRI1UNS, "DRI1UNS");
            }

            if (cboDRI2.SelectedItem != null)
            {
                if ((int)cboDRI2.SelectedValue == (int)cboDRI1.SelectedValue)
                {
                    await this.ShowMessageAsync("", "DRI2 duplicate selected.");
                    return;
                }
                else
                {
                    if (txtDRI2LOD.Text == "")
                    {
                        await this.ShowMessageAsync("", "Please enter a LOD value for DRI2.");
                        return;
                    }
                    if (txtDRI2UNS.Text == "")
                    {
                        await this.ShowMessageAsync("", "Please enter a Unsocial value for DRI2.");
                        return;
                    }
                    else
                    {
                        Update(Selected.DRI2Id, "DRI2Id", "DRI2Name", Selected.DRI2LOD, "DRI2LOD", Selected.DRI2UNS, "DRI2UNS",
                        (int)cboDRI2.SelectedValue, cboDRI2.Text, double.Parse(txtDRI2LOD.Text), double.Parse(txtDRI2UNS.Text));
                    }
                }
            }
            else if (cboDRI2.SelectedItem == null && Selected.DRI2Id != 0)
            {
                Reset(Selected.DRI2Id, "DRI2Id", "DRI2Name", Selected.DRI2LOD, "DRI2LOD", Selected.DRI2UNS, "DRI2UNS");
            }

            //Get RN selected info
            if (cboRN1.SelectedItem != null)
            {
                if (txtRN1LOD.Text == "")
                {
                    await this.ShowMessageAsync("", "Please enter a LOD value for RN1.");
                    return;
                }
                if (txtRN1UNS.Text == "")
                {
                    await this.ShowMessageAsync("", "Please enter a Unsocial value for RN1.");
                    return;
                }
                else
                {
                    Update(Selected.RN1Id, "RN1Id", "RN1Name", Selected.RN1LOD, "RN1LOD", Selected.RN1UNS, "RN1UNS",
                        (int)cboRN1.SelectedValue, cboRN1.Text, double.Parse(txtRN1LOD.Text), double.Parse(txtRN1UNS.Text));
                }
            }
            else if (cboRN1.SelectedItem == null && Selected.RN1Id != 0)
            {
                Reset(Selected.RN1Id, "RN1Id", "RN1Name", Selected.RN1LOD, "RN1LOD", Selected.RN1UNS, "RN1UNS");
            }

            if (cboRN2.SelectedItem != null)
            {
                if ((int)cboRN2.SelectedValue == (int)cboRN1.SelectedValue)
                {
                    await this.ShowMessageAsync("", "RN2 duplicate selected.");
                    return;
                }
                else
                {
                    if (txtRN2LOD.Text == "")
                    {
                        await this.ShowMessageAsync("", "Please enter a LOD value for RN2.");
                        return;
                    }
                    if (txtRN2UNS.Text == "")
                    {
                        await this.ShowMessageAsync("", "Please enter a Unsocial value for RN2.");
                        return;
                    }
                    else
                    {
                        Update(Selected.RN2Id, "RN2Id", "RN2Name", Selected.RN2LOD, "RN2LOD", Selected.RN2UNS, "RN2UNS",
                        (int)cboRN2.SelectedValue, cboRN2.Text, double.Parse(txtRN2LOD.Text), double.Parse(txtRN2UNS.Text));
                    }
                }
            }
            else if (cboRN2.SelectedItem == null && Selected.RN2Id != 0)
            {
                Reset(Selected.RN2Id, "RN2Id", "RN2Name", Selected.RN2LOD, "RN2LOD", Selected.RN2UNS, "RN2UNS");
            }

            if (cboRN3.SelectedItem != null)
            {
                if ((int)cboRN3.SelectedValue == (int)cboRN1.SelectedValue ||
                    (int)cboRN3.SelectedValue == (int)cboRN2.SelectedValue)
                {
                    await this.ShowMessageAsync("", "RN3 duplicate selected.");
                    return;
                }
                else
                {
                    if (txtRN3LOD.Text == "")
                    {
                        await this.ShowMessageAsync("", "Please enter a LOD value for RN3.");
                        return;
                    }
                    if (txtRN3UNS.Text == "")
                    {
                        await this.ShowMessageAsync("", "Please enter a Unsocial value for RN3.");
                        return;
                    }
                    else
                    {
                        Update(Selected.RN3Id, "RN3Id", "RN3Name", Selected.RN3LOD, "RN3LOD", Selected.RN3UNS, "RN3UNS",
                        (int)cboRN3.SelectedValue, cboRN3.Text, double.Parse(txtRN3LOD.Text), double.Parse(txtRN3UNS.Text));
                    }
                }
            }
            else if (cboRN3.SelectedItem == null && Selected.RN3Id != 0)
            {
                Reset(Selected.RN3Id, "RN3Id", "RN3Name", Selected.RN3LOD, "RN3LOD", Selected.RN3UNS, "RN3UNS");
            }

            //Get CCA selected info
            if (cboCCA1.SelectedItem != null)
            {
                if (txtCCA1LOD.Text == "")
                {
                    await this.ShowMessageAsync("", "Please enter a LOD value for CCA1.");
                    return;
                }
                if (txtCCA1UNS.Text == "")
                {
                    await this.ShowMessageAsync("", "Please enter a Unsocial value for CCA1.");
                    return;
                }
                else
                {
                    Update(Selected.CCA1Id, "CCA1Id", "CCA1Name", Selected.CCA1LOD, "CCA1LOD", Selected.CCA1UNS, "CCA1UNS",
                        (int)cboCCA1.SelectedValue, cboCCA1.Text, double.Parse(txtCCA1LOD.Text), double.Parse(txtCCA1UNS.Text));
                }
            }
            else if (cboCCA1.SelectedItem == null && Selected.CCA1Id != 0)
            {
                Reset(Selected.CCA1Id, "CCA1Id", "CCA1Name", Selected.CCA1LOD, "CCA1LOD", Selected.CCA1UNS, "CCA1UNS");
            }

            if (cboCCA2.SelectedItem != null)
            {
                if ((int)cboCCA2.SelectedValue == (int)cboCCA1.SelectedValue)
                {
                    await this.ShowMessageAsync("", "CCA2 duplicate selected.");
                    return;
                }
                else
                {
                    if (txtCCA2LOD.Text == "")
                    {
                        await this.ShowMessageAsync("", "Please enter a LOD value for CCA2.");
                        return;
                    }
                    if (txtCCA2UNS.Text == "")
                    {
                        await this.ShowMessageAsync("", "Please enter a Unsocial value for CCA2.");
                        return;
                    }
                    else
                    {
                        Update(Selected.CCA2Id, "CCA2Id", "CCA2Name", Selected.CCA2LOD, "CCA2LOD", Selected.CCA2UNS, "CCA2UNS",
                        (int)cboCCA2.SelectedValue, cboCCA2.Text, double.Parse(txtCCA2LOD.Text), double.Parse(txtCCA2UNS.Text));
                    }
                }
            }
            else if (cboCCA2.SelectedItem == null && Selected.CCA2Id != 0)
            {
                Reset(Selected.CCA2Id, "CCA2Id", "CCA2Name", Selected.CCA2LOD, "CCA2LOD", Selected.CCA2UNS, "CCA2UNS");
            }

            if (cboCCA3.SelectedItem != null)
            {
                if ((int)cboCCA3.SelectedValue == (int)cboCCA1.SelectedValue ||
                    (int)cboCCA3.SelectedValue == (int)cboCCA2.SelectedValue)
                {
                    await this.ShowMessageAsync("", "CCA3 duplicate selected.");
                    return;
                }
                else
                {
                    if (txtCCA3LOD.Text == "")
                    {
                        await this.ShowMessageAsync("", "Please enter a LOD value for CCA3.");
                        return;
                    }
                    if (txtCCA3UNS.Text == "")
                    {
                        await this.ShowMessageAsync("", "Please enter a Unsocial value for CCA3.");
                        return;
                    }
                    else
                    {
                        Update(Selected.CCA3Id, "CCA3Id", "CCA3Name", Selected.CCA3LOD, "CCA3LOD", Selected.CCA3UNS, "CCA3UNS",
                        (int)cboCCA3.SelectedValue, cboCCA3.Text, double.Parse(txtCCA3LOD.Text), double.Parse(txtCCA3UNS.Text));
                    }
                }
            }
            else if (cboCCA3.SelectedItem == null && Selected.CCA3Id != 0)
            {
                Reset(Selected.CCA3Id, "CCA3Id", "CCA3Name", Selected.CCA3LOD, "CCA3LOD", Selected.CCA3UNS, "CCA3UNS");
            }
        }

        private async void UpdateStaffHoliday()
        {
            //First check if there is a selected item
            if (cboSV1.SelectedItem != null)
            {
                if (txtSV1LOD.Text == "")
                {
                    await this.ShowMessageAsync("", "Please enter a LOD value for SV1.");
                    return;
                }
                if (txtSV1UNS.Text == "")
                {
                    await this.ShowMessageAsync("", "Please enter a Unsocial value for SV1.");
                    return;
                }
                else
                {
                    UpdateHoliday(Selected.SV1Id, "SV1Id", "SV1Name", Selected.SV1LOD, "SV1LOD", Selected.SV1UNS, "SV1UNS",
                        (int)cboSV1.SelectedValue, cboSV1.Text, double.Parse(txtSV1LOD.Text), double.Parse(txtSV1UNS.Text));
                }
            }
            //Else remove if reset staff has been pressed
            else if (cboSV1.SelectedItem == null && Selected.SV1Id != 0)
            {
                Reset(Selected.SV1Id, "SV1Id", "SV1Name", Selected.SV1LOD, "SV1LOD", Selected.SV1UNS, "SV1UNS");
            }

            //Do same for other roles
            if (cboDRI1.SelectedItem != null)
            {
                if (txtDRI1LOD.Text == "")
                {
                    await this.ShowMessageAsync("", "Please enter a LOD value for DRI1.");
                    return;
                }
                if (txtDRI1UNS.Text == "")
                {
                    await this.ShowMessageAsync("", "Please enter a Unsocial value for DRI1.");
                    return;
                }
                else
                {
                    UpdateHoliday(Selected.DRI1Id, "DRI1Id", "DRI1Name", Selected.DRI1LOD, "DRI1LOD", Selected.DRI1UNS, "DRI1UNS",
                        (int)cboDRI1.SelectedValue, cboDRI1.Text, double.Parse(txtDRI1LOD.Text), double.Parse(txtDRI1UNS.Text));
                }
            }
            else if (cboDRI1.SelectedItem == null && Selected.DRI1Id != 0)
            {
                Reset(Selected.DRI1Id, "DRI1Id", "DRI1Name", Selected.DRI1LOD, "DRI1LOD", Selected.DRI1UNS, "DRI1UNS");
            }

            if (cboDRI2.SelectedItem != null)
            {
                if ((int)cboDRI2.SelectedValue == (int)cboDRI1.SelectedValue)
                {
                    await this.ShowMessageAsync("", "DRI2 duplicate selected.");
                    return;
                }
                else
                {
                    if (txtDRI2LOD.Text == "")
                    {
                        await this.ShowMessageAsync("", "Please enter a LOD value for DRI2.");
                        return;
                    }
                    if (txtDRI2UNS.Text == "")
                    {
                        await this.ShowMessageAsync("", "Please enter a Unsocial value for DRI2.");
                        return;
                    }
                    else
                    {
                        UpdateHoliday(Selected.DRI2Id, "DRI2Id", "DRI2Name", Selected.DRI2LOD, "DRI2LOD", Selected.DRI2UNS, "DRI2UNS",
                        (int)cboDRI2.SelectedValue, cboDRI2.Text, double.Parse(txtDRI2LOD.Text), double.Parse(txtDRI2UNS.Text));
                    }
                }
            }
            else if (cboDRI2.SelectedItem == null && Selected.DRI2Id != 0)
            {
                Reset(Selected.DRI2Id, "DRI2Id", "DRI2Name", Selected.DRI2LOD, "DRI2LOD", Selected.DRI2UNS, "DRI2UNS");
            }

            //Get RN selected info
            if (cboRN1.SelectedItem != null)
            {
                if (txtRN1LOD.Text == "")
                {
                    await this.ShowMessageAsync("", "Please enter a LOD value for RN1.");
                    return;
                }
                if (txtRN1UNS.Text == "")
                {
                    await this.ShowMessageAsync("", "Please enter a Unsocial value for RN1.");
                    return;
                }
                else
                {
                    UpdateHoliday(Selected.RN1Id, "RN1Id", "RN1Name", Selected.RN1LOD, "RN1LOD", Selected.RN1UNS, "RN1UNS",
                        (int)cboRN1.SelectedValue, cboRN1.Text, double.Parse(txtRN1LOD.Text), double.Parse(txtRN1UNS.Text));
                }
            }
            else if (cboRN1.SelectedItem == null && Selected.RN1Id != 0)
            {
                Reset(Selected.RN1Id, "RN1Id", "RN1Name", Selected.RN1LOD, "RN1LOD", Selected.RN1UNS, "RN1UNS");
            }

            if (cboRN2.SelectedItem != null)
            {
                if ((int)cboRN2.SelectedValue == (int)cboRN1.SelectedValue)
                {
                    await this.ShowMessageAsync("", "RN2 duplicate selected.");
                    return;
                }
                else
                {
                    if (txtRN2LOD.Text == "")
                    {
                        await this.ShowMessageAsync("", "Please enter a LOD value for RN2.");
                        return;
                    }
                    if (txtRN2UNS.Text == "")
                    {
                        await this.ShowMessageAsync("", "Please enter a Unsocial value for RN2.");
                        return;
                    }
                    else
                    {
                        UpdateHoliday(Selected.RN2Id, "RN2Id", "RN2Name", Selected.RN2LOD, "RN2LOD", Selected.RN2UNS, "RN2UNS",
                        (int)cboRN2.SelectedValue, cboRN2.Text, double.Parse(txtRN2LOD.Text), double.Parse(txtRN2UNS.Text));
                    }
                }
            }
            else if (cboRN2.SelectedItem == null && Selected.RN2Id != 0)
            {
                Reset(Selected.RN2Id, "RN2Id", "RN2Name", Selected.RN2LOD, "RN2LOD", Selected.RN2UNS, "RN2UNS");
            }

            if (cboRN3.SelectedItem != null)
            {
                if ((int)cboRN3.SelectedValue == (int)cboRN1.SelectedValue ||
                    (int)cboRN3.SelectedValue == (int)cboRN2.SelectedValue)
                {
                    await this.ShowMessageAsync("", "RN3 duplicate selected.");
                    return;
                }
                else
                {
                    if (txtRN3LOD.Text == "")
                    {
                        await this.ShowMessageAsync("", "Please enter a LOD value for RN3.");
                        return;
                    }
                    if (txtRN3UNS.Text == "")
                    {
                        await this.ShowMessageAsync("", "Please enter a Unsocial value for RN3.");
                        return;
                    }
                    else
                    {
                        UpdateHoliday(Selected.RN3Id, "RN3Id", "RN3Name", Selected.RN3LOD, "RN3LOD", Selected.RN3UNS, "RN3UNS",
                        (int)cboRN3.SelectedValue, cboRN3.Text, double.Parse(txtRN3LOD.Text), double.Parse(txtRN3UNS.Text));
                    }
                }
            }
            else if (cboRN3.SelectedItem == null && Selected.RN3Id != 0)
            {
                Reset(Selected.RN3Id, "RN3Id", "RN3Name", Selected.RN3LOD, "RN3LOD", Selected.RN3UNS, "RN3UNS");
            }

            //Get CCA selected info
            if (cboCCA1.SelectedItem != null)
            {
                if (txtCCA1LOD.Text == "")
                {
                    await this.ShowMessageAsync("", "Please enter a LOD value for CCA1.");
                    return;
                }
                if (txtCCA1UNS.Text == "")
                {
                    await this.ShowMessageAsync("", "Please enter a Unsocial value for CCA1.");
                    return;
                }
                else
                {
                    UpdateHoliday(Selected.CCA1Id, "CCA1Id", "CCA1Name", Selected.CCA1LOD, "CCA1LOD", Selected.CCA1UNS, "CCA1UNS",
                        (int)cboCCA1.SelectedValue, cboCCA1.Text, double.Parse(txtCCA1LOD.Text), double.Parse(txtCCA1UNS.Text));
                }
            }
            else if (cboCCA1.SelectedItem == null && Selected.CCA1Id != 0)
            {
                Reset(Selected.CCA1Id, "CCA1Id", "CCA1Name", Selected.CCA1LOD, "CCA1LOD", Selected.CCA1UNS, "CCA1UNS");
            }

            if (cboCCA2.SelectedItem != null)
            {
                if ((int)cboCCA2.SelectedValue == (int)cboCCA1.SelectedValue)
                {
                    await this.ShowMessageAsync("", "CCA2 duplicate selected.");
                    return;
                }
                else
                {
                    if (txtCCA2LOD.Text == "")
                    {
                        await this.ShowMessageAsync("", "Please enter a LOD value for CCA2.");
                        return;
                    }
                    if (txtCCA2UNS.Text == "")
                    {
                        await this.ShowMessageAsync("", "Please enter a Unsocial value for CCA2.");
                        return;
                    }
                    else
                    {
                        UpdateHoliday(Selected.CCA2Id, "CCA2Id", "CCA2Name", Selected.CCA2LOD, "CCA2LOD", Selected.CCA2UNS, "CCA2UNS",
                        (int)cboCCA2.SelectedValue, cboCCA2.Text, double.Parse(txtCCA2LOD.Text), double.Parse(txtCCA2UNS.Text));
                    }
                }
            }
            else if (cboCCA2.SelectedItem == null && Selected.CCA2Id != 0)
            {
                Reset(Selected.CCA2Id, "CCA2Id", "CCA2Name", Selected.CCA2LOD, "CCA2LOD", Selected.CCA2UNS, "CCA2UNS");
            }

            if (cboCCA3.SelectedItem != null)
            {
                if ((int)cboCCA3.SelectedValue == (int)cboCCA1.SelectedValue ||
                    (int)cboCCA3.SelectedValue == (int)cboCCA2.SelectedValue)
                {
                    await this.ShowMessageAsync("", "CCA3 duplicate selected.");
                    return;
                }
                else
                {
                    if (txtCCA3LOD.Text == "")
                    {
                        await this.ShowMessageAsync("", "Please enter a LOD value for CCA3.");
                        return;
                    }
                    if (txtCCA3UNS.Text == "")
                    {
                        await this.ShowMessageAsync("", "Please enter a Unsocial value for CCA3.");
                        return;
                    }
                    else
                    {
                        UpdateHoliday(Selected.CCA3Id, "CCA3Id", "CCA3Name", Selected.CCA3LOD, "CCA3LOD", Selected.CCA3UNS, "CCA3UNS",
                        (int)cboCCA3.SelectedValue, cboCCA3.Text, double.Parse(txtCCA3LOD.Text), double.Parse(txtCCA3UNS.Text));
                    }
                }
            }
            else if (cboCCA3.SelectedItem == null && Selected.CCA3Id != 0)
            {
                Reset(Selected.CCA3Id, "CCA3Id", "CCA3Name", Selected.CCA3LOD, "CCA3LOD", Selected.CCA3UNS, "CCA3UNS");
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            //Use this in the xaml file to only allow numbers in input
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
