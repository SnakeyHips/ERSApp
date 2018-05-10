using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System.Text.RegularExpressions;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ERSApp.Model;

namespace ERSApp.Views
{
    public partial class StaffSessionWindow : MetroWindow
    {
        Session Selected;
        public List<Staff> AvailableStaff = CollectionManager.GetAvailableStaff();
        public List<Staff> SVList { get; set; }
        public List<Staff> DSVList { get; set; }
        public List<Staff> DRIList { get; set; }
        public List<Staff> RNList { get; set; }
        public List<Staff> CCAList { get; set; }
        public List<string> TimesList { get; set; }
        double Week = CollectionManager.GetWeek(CollectionManager.SelectedDate);

        public StaffSessionWindow(Session s)
        {
            InitializeComponent();
            this.DataContext = this;
            CollectionManager.GetAvailableStaff();
            //Put all relevant roles in respective list
            SVList = AvailableStaff.Where(x => x.Role == "SV").ToList();
            DSVList = AvailableStaff.Where(x => x.Role == "DSV").ToList();
            DRIList = AvailableStaff.Where(x => x.Role == "DRI").ToList();
            RNList = AvailableStaff.Where(x => x.Role == "RN").ToList();
            CCAList = AvailableStaff.Where(x => x.Role == "CCA").ToList();

            this.Selected = s;
            double Start = double.Parse(Selected.ClinicTime.Substring(0, 2));
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
            tabSessionStaff.Header = DateTime.Parse(Selected.Date).DayOfWeek.ToString() +
                " - " + Selected.Date + " - " + Selected.Location;
            txtClinicTime.Text = Selected.ClinicTime;
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
            }
            if (Selected.DRI1Id != 0)
            {
                cboDRI1.SelectedValue = Selected.DRI1Id;
                txtDRI1LOD.Text = Selected.DRI1LOD.ToString();
            }
            if (Selected.DRI2Id != 0)
            {
                cboDRI2.SelectedValue = Selected.DRI2Id;
                txtDRI2LOD.Text = Selected.DRI2LOD.ToString();
            }
            if (Selected.RN1Id != 0)
            {
                cboRN1.SelectedValue = Selected.RN1Id;
                txtRN1LOD.Text = Selected.RN1LOD.ToString();
            }
            if (Selected.RN2Id != 0)
            {
                cboRN2.SelectedValue = Selected.RN2Id;
                txtRN2LOD.Text = Selected.RN2LOD.ToString();
            }
            if (Selected.RN3Id != 0)
            {
                cboRN3.SelectedValue = Selected.RN3Id;
                txtRN3LOD.Text = Selected.RN3LOD.ToString();
            }
            if (Selected.CCA1Id != 0)
            {
                cboCCA1.SelectedValue = Selected.CCA1Id;
                txtCCA1LOD.Text = Selected.CCA1LOD.ToString();
            }
            if (Selected.CCA2Id != 0)
            {
                cboCCA2.SelectedValue = Selected.CCA2Id;
                txtCCA2LOD.Text = Selected.CCA2LOD.ToString();
            }
            if (Selected.CCA3Id != 0)
            {
                cboCCA3.SelectedValue = Selected.CCA3Id;
                txtCCA3LOD.Text = Selected.CCA3LOD.ToString();
            }
        }

        //Listeners to enable corresponding time cbos
        private void cboSV1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtSV1LOD.IsEnabled = true;
        }

        private void cboDRI1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtDRI1LOD.IsEnabled = true;
        }

        private void cboDRI2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtDRI2LOD.IsEnabled = true;
        }

        private void cboRN1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtRN1LOD.IsEnabled = true;
        }

        private void cboRN2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtRN2LOD.IsEnabled = true;
        }

        private void cboRN3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtRN3LOD.IsEnabled = true;
        }

        private void cboCCA1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtCCA1LOD.IsEnabled = true;
        }

        private void cboCCA2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtCCA2LOD.IsEnabled = true;
        }

        private void cboCCA3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtCCA3LOD.IsEnabled = true;
        }

        private void btnAutoLOD_Click(object semder, RoutedEventArgs e)
        {
            if(cboSV1.SelectedItem != null)
            {
                txtSV1LOD.Text = txtLOD.Text;
            }
            if (cboDRI1.SelectedItem != null)
            {
                txtDRI1LOD.Text = txtLOD.Text;
            }
            if (cboDRI2.SelectedItem != null)
            {
                txtDRI2LOD.Text = txtLOD.Text;
            }
            if (cboRN1.SelectedItem != null)
            {
                txtRN1LOD.Text = txtLOD.Text;
            }
            if (cboRN2.SelectedItem != null)
            {
                txtRN2LOD.Text = txtLOD.Text;
            }
            if (cboRN3.SelectedItem != null)
            {
                txtRN3LOD.Text = txtLOD.Text;
            }
            if (cboCCA1.SelectedItem != null)
            {
                txtCCA1LOD.Text = txtLOD.Text;
            }
            if (cboCCA2.SelectedItem != null)
            {
                txtCCA2LOD.Text = txtLOD.Text;
            }
            if (cboCCA3.SelectedItem != null)
            {
                txtCCA3LOD.Text = txtLOD.Text;
            }
        }

        //Update Staff info - lots of if statements as properties can't be passed through methods
        //First one is commented as rest are same
        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //First check if there is a selected item
            if (cboSV1.SelectedItem != null)
            {
                if(txtSV1LOD.Text == "")
                {
                    await this.ShowMessageAsync("", "Please enter a LOD value for SV1.");
                    return;
                }
                else
                {
                    if(Selected.SV1Id == 0)
                    {
                        //Assign new appoint staff
                        Selected.SV1Id = (int)cboSV1.SelectedValue;
                        Selected.SV1Name = cboSV1.Text;
                        Selected.SV1LOD = double.Parse(txtSV1LOD.Text);
                        Selected.StaffCount++;
                        //Add onto new staff's appointed hours/New record created in sql method
                        CollectionManager.UpdateRoster(Selected.SV1Id, Selected.SV1LOD, 0.0, Week);
                    }
                    //Check if different staff selected from before
                    else if (Selected.SV1Id == (int)cboSV1.SelectedValue)
                    {
                        //Check if different times selected from before
                        if (Selected.SV1LOD != double.Parse(txtSV1LOD.Text))
                        {
                            //If so, update times and appointed hours to use times selected
                            double appointed = double.Parse(txtSV1LOD.Text) - Selected.SV1LOD;
                            CollectionManager.UpdateRoster(Selected.SV1Id, appointed, 0.0, Week);
                            Selected.SV1LOD = double.Parse(txtSV1LOD.Text);
                        }
                    }
                    //Check if no previous staff selected
                    else if (Selected.SV1Id != (int)cboSV1.SelectedValue)
                    {
                        //Update old staff's appointed hours to remove length
                        CollectionManager.UpdateRoster(Selected.SV1Id, -Selected.SV1LOD, 0.0, Week);
                   
                        //Assign new appoint staff
                        Selected.SV1Id = (int)cboSV1.SelectedValue;
                        Selected.SV1Name = cboSV1.Text;
                        Selected.SV1LOD = double.Parse(txtSV1LOD.Text);
                        //Add onto new staff's appointed hours/New record created in sql method
                        CollectionManager.UpdateRoster(Selected.SV1Id, Selected.SV1LOD, 0.0, Week);
                    }
                }
            }
            //Else remove if reset staff has been pressed
            else if (cboSV1.SelectedItem == null && Selected.SV1Id != 0)
            {
                //Remove staff's appointed hours
                CollectionManager.UpdateRoster(Selected.SV1Id, -Selected.SV1LOD, 0.0, Week);
                //Remove any saved staff info
                Selected.SV1Id = 0;
                Selected.SV1Name = "";
                Selected.SV1LOD = 0.0;
                Selected.StaffCount--;
            }

            //Do same for other roles
            if (cboDRI1.SelectedItem != null)
            {
                if (txtDRI1LOD.Text == "")
                {
                    await this.ShowMessageAsync("", "Please enter a LOD value for DRI1.");
                    return;
                }
                else
                {
                    if (Selected.DRI1Id == 0)
                    {
                        Selected.DRI1Id = (int)cboDRI1.SelectedValue;
                        Selected.DRI1Name = cboDRI1.Text;
                        Selected.DRI1LOD = double.Parse(txtDRI1LOD.Text);
                        Selected.StaffCount++;
                        CollectionManager.UpdateRoster(Selected.DRI1Id, Selected.DRI1LOD, 0.0, Week);
                    }
                    else if (Selected.DRI1Id == (int)cboDRI1.SelectedValue)
                    {
                        if (Selected.DRI1LOD != double.Parse(txtDRI1LOD.Text))
                        {
                            double appointed = double.Parse(txtDRI1LOD.Text) - Selected.DRI1LOD;
                            CollectionManager.UpdateRoster(Selected.DRI1Id, appointed, 0.0, Week);
                            Selected.DRI1LOD = double.Parse(txtDRI1LOD.Text);
                        }
                    }
                    else if (Selected.DRI1Id != (int)cboDRI1.SelectedValue)
                    {
                        CollectionManager.UpdateRoster(Selected.DRI1Id, -Selected.DRI1LOD, 0.0, Week);
                        Selected.DRI1Id = (int)cboDRI1.SelectedValue;
                        Selected.DRI1Name = cboDRI1.Text;
                        Selected.DRI1LOD = double.Parse(txtDRI1LOD.Text);
                        CollectionManager.UpdateRoster(Selected.DRI1Id, Selected.DRI1LOD, 0.0, Week);
                    }
                }
            }
            else if (cboDRI1.SelectedItem == null && Selected.DRI1Id != 0)
            {
                CollectionManager.UpdateRoster(Selected.DRI1Id, -Selected.DRI1LOD, 0.0, Week);
                Selected.DRI1Id = 0;
                Selected.DRI1Name = "";
                Selected.DRI1LOD = 0.0;
                Selected.StaffCount--;
            }

            if (cboDRI2.SelectedItem != null)
            {
                if ((int)cboDRI2.SelectedValue == (int)cboDRI1.SelectedValue)
                {
                    await this.ShowMessageAsync("", "Driver 2 duplicate selected.");
                    return;
                }
                else
                {
                    if(txtDRI2LOD.Text == "")
                    {
                        await this.ShowMessageAsync("", "Please enter a LOD value for DRI2.");
                        return;
                    }
                    else
                    {
                        if (Selected.DRI2Id == 0)
                        {
                            Selected.DRI2Id = (int)cboDRI2.SelectedValue;
                            Selected.DRI2Name = cboDRI2.Text;
                            Selected.DRI2LOD = double.Parse(txtDRI2LOD.Text);
                            Selected.StaffCount++;
                            CollectionManager.UpdateRoster(Selected.DRI2Id, Selected.DRI2LOD, 0.0, Week);
                        }
                        else if (Selected.DRI2Id == (int)cboDRI2.SelectedValue)
                        {
                            if (Selected.DRI2LOD != double.Parse(txtDRI2LOD.Text))
                            {
                                double appointed = double.Parse(txtDRI2LOD.Text) - Selected.DRI2LOD;
                                CollectionManager.UpdateRoster(Selected.DRI2Id, appointed, 0.0, Week);
                                Selected.DRI2LOD = double.Parse(txtDRI2LOD.Text);
                            }
                        }
                        else if (Selected.DRI2Id != (int)cboDRI2.SelectedValue)
                        {
                            CollectionManager.UpdateRoster(Selected.DRI2Id, -Selected.DRI2LOD, 0.0, Week);
                            Selected.DRI2Id = (int)cboDRI2.SelectedValue;
                            Selected.DRI2Name = cboDRI2.Text;
                            Selected.DRI2LOD = double.Parse(txtDRI2LOD.Text);
                            CollectionManager.UpdateRoster(Selected.DRI2Id, Selected.DRI2LOD, 0.0, Week);
                        }
                    }
                }
            }
            else if (cboDRI2.SelectedItem == null && Selected.DRI2Id != 0)
            {
                CollectionManager.UpdateRoster(Selected.DRI2Id, -Selected.DRI2LOD, 0.0, Week);
                Selected.DRI2Id = 0;
                Selected.DRI2Name = "";
                Selected.DRI2LOD = 0.0;
                Selected.StaffCount--;
            }

            //Get RN selected info
            if (cboRN1.SelectedItem != null)
            {
                if (txtRN1LOD.Text == "")
                {
                    await this.ShowMessageAsync("", "Please enter a LOD value for RN1.");
                    return;
                }
                else
                {
                    if (Selected.RN1Id == 0)
                    {
                        Selected.RN1Id = (int)cboRN1.SelectedValue;
                        Selected.RN1Name = cboRN1.Text;
                        Selected.RN1LOD = double.Parse(txtRN1LOD.Text);
                        CollectionManager.UpdateRoster(Selected.RN1Id, Selected.RN1LOD, 0.0, Week);
                    }
                    else if (Selected.RN1Id == (int)cboRN1.SelectedValue)
                    {
                        if (Selected.RN1LOD != double.Parse(txtRN1LOD.Text))
                        {
                            double appointed = double.Parse(txtRN1LOD.Text) - Selected.RN1LOD;
                            CollectionManager.UpdateRoster(Selected.RN1Id, appointed, 0.0, Week);
                            Selected.RN1LOD = double.Parse(txtRN1LOD.Text);
                        }
                    }
                    else if (Selected.RN1Id != (int)cboRN1.SelectedValue)
                    {
                        CollectionManager.UpdateRoster(Selected.RN1Id, -Selected.RN1LOD, 0.0, Week);
                        Selected.RN1Id = (int)cboRN1.SelectedValue;
                        Selected.RN1Name = cboRN1.Text;
                        Selected.RN1LOD = double.Parse(txtRN1LOD.Text);
                        CollectionManager.UpdateRoster(Selected.RN1Id, Selected.RN1LOD, 0.0, Week);
                    }
                }
            }
            else if (cboRN1.SelectedItem == null && Selected.RN1Id != 0)
            {
                CollectionManager.UpdateRoster(Selected.RN1Id, -Selected.RN1LOD, 0.0, Week);
                Selected.RN1Id = 0;
                Selected.RN1Name = "";
                Selected.RN1LOD = 0.0;
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
                    else
                    {
                        if (Selected.RN2Id == 0)
                        {
                            Selected.RN2Id = (int)cboRN2.SelectedValue;
                            Selected.RN2Name = cboRN2.Text;
                            Selected.RN2LOD = double.Parse(txtRN2LOD.Text);
                            CollectionManager.UpdateRoster(Selected.RN2Id, Selected.RN2LOD, 0.0, Week);
                        }
                        else if (Selected.RN2Id == (int)cboRN2.SelectedValue)
                        {
                            if (Selected.RN2LOD != double.Parse(txtRN2LOD.Text))
                            {
                                double appointed = double.Parse(txtRN2LOD.Text) - Selected.RN2LOD;
                                CollectionManager.UpdateRoster(Selected.RN2Id, appointed, 0.0, Week);
                                Selected.RN2LOD = double.Parse(txtRN2LOD.Text);
                            }
                        }
                        else if (Selected.RN2Id != (int)cboRN2.SelectedValue)
                        {
                            CollectionManager.UpdateRoster(Selected.RN2Id, -Selected.RN2LOD, 0.0, Week);
                            Selected.RN2Id = (int)cboRN2.SelectedValue;
                            Selected.RN2Name = cboRN2.Text;
                            Selected.RN2LOD = double.Parse(txtRN2LOD.Text);
                            CollectionManager.UpdateRoster(Selected.RN2Id, Selected.RN2LOD, 0.0, Week);
                        }
                    }
                }
            }
            else if (cboRN2.SelectedItem == null && Selected.RN2Id != 0)
            {
                CollectionManager.UpdateRoster(Selected.RN2Id, -Selected.RN2LOD, 0.0, Week);
                Selected.RN2Id = 0;
                Selected.RN2Name = "";
                Selected.RN2LOD = 0.0;
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
                    else
                    {
                        if (Selected.RN3Id == 0)
                        {
                            Selected.RN3Id = (int)cboRN3.SelectedValue;
                            Selected.RN3Name = cboRN3.Text;
                            Selected.RN3LOD = double.Parse(txtRN3LOD.Text);
                            CollectionManager.UpdateRoster(Selected.RN3Id, Selected.RN3LOD, 0.0, Week);
                        }
                        else if (Selected.RN3Id == (int)cboRN3.SelectedValue)
                        {
                            if (Selected.RN3LOD != double.Parse(txtRN3LOD.Text))
                            {
                                double appointed = double.Parse(txtRN3LOD.Text) - Selected.RN3LOD;
                                CollectionManager.UpdateRoster(Selected.RN3Id, appointed, 0.0, Week);
                                Selected.RN3LOD = double.Parse(txtRN3LOD.Text);
                            }
                        }
                        else if (Selected.RN3Id != (int)cboRN3.SelectedValue)
                        {
                            CollectionManager.UpdateRoster(Selected.RN3Id, -Selected.RN3LOD, 0.0, Week);
                            Selected.RN3Id = (int)cboRN3.SelectedValue;
                            Selected.RN3Name = cboRN3.Text;
                            Selected.RN3LOD = double.Parse(txtRN3LOD.Text);
                            CollectionManager.UpdateRoster(Selected.RN3Id, Selected.RN3LOD, 0.0, Week);
                        }
                    }
                }
            }
            else if (cboRN3.SelectedItem == null && Selected.RN3Id != 0)
            {
                CollectionManager.UpdateRoster(Selected.RN3Id, -Selected.RN3LOD, 0.0, Week);
                Selected.RN3Id = 0;
                Selected.RN3Name = "";
                Selected.RN3LOD = 0.0;
            }

            //Get CCA selected info
            if (cboCCA1.SelectedItem != null)
            {
                if (txtCCA1LOD.Text == "")
                {
                    await this.ShowMessageAsync("", "Please enter a LOD value for CCA1.");
                    return;
                }
                else
                {
                    if (Selected.CCA1Id == 0)
                    {
                        Selected.CCA1Id = (int)cboCCA1.SelectedValue;
                        Selected.CCA1Name = cboCCA1.Text;
                        Selected.CCA1LOD = double.Parse(txtCCA1LOD.Text);
                        Selected.StaffCount++;
                        CollectionManager.UpdateRoster(Selected.CCA1Id, Selected.CCA1LOD, 0.0, Week);
                    }
                    else if (Selected.CCA1Id == (int)cboCCA1.SelectedValue)
                    {
                        if (Selected.CCA1LOD != double.Parse(txtCCA1LOD.Text))
                        {
                            double appointed = double.Parse(txtCCA1LOD.Text) - Selected.CCA1LOD;
                            CollectionManager.UpdateRoster(Selected.CCA1Id, appointed, 0.0, Week);
                            Selected.CCA1LOD = double.Parse(txtCCA1LOD.Text);
                        }
                    }
                    else if (Selected.CCA1Id != (int)cboCCA1.SelectedValue)
                    {
                        CollectionManager.UpdateRoster(Selected.CCA1Id, -Selected.CCA1LOD, 0.0, Week);
                        Selected.CCA1Id = (int)cboCCA1.SelectedValue;
                        Selected.CCA1Name = cboCCA1.Text;
                        Selected.CCA1LOD = double.Parse(txtCCA1LOD.Text);
                        CollectionManager.UpdateRoster(Selected.CCA1Id, Selected.CCA1LOD, 0.0, Week);
                    }
                }
            }
            else if (cboCCA1.SelectedItem == null && Selected.CCA1Id != 0)
            {
                CollectionManager.UpdateRoster(Selected.CCA1Id, -Selected.CCA1LOD, 0.0, Week);
                Selected.CCA1Id = 0;
                Selected.CCA1Name = "";
                Selected.CCA1LOD = 0.0;
                Selected.StaffCount--;
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
                    else
                    {
                        if (Selected.CCA2Id == 0)
                        {
                            Selected.CCA2Id = (int)cboCCA2.SelectedValue;
                            Selected.CCA2Name = cboCCA2.Text;
                            Selected.CCA2LOD = double.Parse(txtCCA2LOD.Text);
                            Selected.StaffCount++;
                            CollectionManager.UpdateRoster(Selected.CCA2Id, Selected.CCA2LOD, 0.0, Week);
                        }
                        else if (Selected.CCA2Id == (int)cboCCA2.SelectedValue)
                        {
                            if (Selected.CCA2LOD != double.Parse(txtCCA2LOD.Text))
                            {
                                double appointed = double.Parse(txtCCA2LOD.Text) - Selected.CCA2LOD;
                                CollectionManager.UpdateRoster(Selected.CCA2Id, appointed, 0.0, Week);
                                Selected.CCA2LOD = double.Parse(txtCCA2LOD.Text);
                            }
                        }
                        else if (Selected.CCA2Id != (int)cboCCA2.SelectedValue)
                        {
                            CollectionManager.UpdateRoster(Selected.CCA2Id, -Selected.CCA2LOD, 0.0, Week);
                            Selected.CCA2Id = (int)cboCCA2.SelectedValue;
                            Selected.CCA2Name = cboCCA2.Text;
                            Selected.CCA2LOD = double.Parse(txtCCA2LOD.Text);
                            CollectionManager.UpdateRoster(Selected.CCA2Id, Selected.CCA2LOD, 0.0, Week);
                        }
                    }
                }
            }
            else if (cboCCA2.SelectedItem == null && Selected.CCA2Id != 0)
            {
                CollectionManager.UpdateRoster(Selected.CCA2Id, -Selected.CCA2LOD, 0.0, Week);
                Selected.CCA2Id = 0;
                Selected.CCA2Name = "";
                Selected.CCA2LOD = 0.0;
                Selected.StaffCount--;
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
                    else
                    {
                        if (Selected.CCA3Id == 0)
                        {
                            Selected.CCA3Id = (int)cboCCA3.SelectedValue;
                            Selected.CCA3Name = cboCCA3.Text;
                            Selected.CCA3LOD = double.Parse(txtCCA3LOD.Text);
                            Selected.StaffCount++;
                            CollectionManager.UpdateRoster(Selected.CCA3Id, Selected.CCA3LOD, 0.0, Week);
                        }
                        else if (Selected.CCA3Id == (int)cboCCA3.SelectedValue)
                        {
                            if (Selected.CCA3LOD != double.Parse(txtCCA3LOD.Text))
                            {
                                double appointed = double.Parse(txtCCA3LOD.Text) - Selected.CCA3LOD;
                                CollectionManager.UpdateRoster(Selected.CCA3Id, appointed, 0.0, Week);
                                Selected.CCA3LOD = double.Parse(txtCCA3LOD.Text);
                            }
                        }
                        else if (Selected.CCA3Id != (int)cboCCA3.SelectedValue)
                        {
                            CollectionManager.UpdateRoster(Selected.CCA3Id, -Selected.CCA3LOD, 0.0, Week);
                            Selected.CCA3Id = (int)cboCCA3.SelectedValue;
                            Selected.CCA3Name = cboCCA3.Text;
                            Selected.CCA3LOD = double.Parse(txtCCA3LOD.Text);
                            CollectionManager.UpdateRoster(Selected.CCA3Id, Selected.CCA3LOD, 0.0, Week);
                        }
                    }
                }
            }
            else if (cboCCA3.SelectedItem == null && Selected.CCA3Id != 0)
            {
                CollectionManager.UpdateRoster(Selected.CCA3Id, -Selected.CCA3LOD, 0.0, Week);
                Selected.CCA3Id = 0;
                Selected.CCA3Name = "";
                Selected.CCA3LOD = 0.0;
                Selected.StaffCount--;
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
            grdStaff.FindChildren<ComboBox>().ToList().ForEach(x =>
            {
                if (x.IsEnabled && x.Text == "")
                {
                    RolesFilled = false;
                }
            });
            Selected.State = RolesFilled ? 1 : 0;
            CollectionManager.UpdateSessionStaff(Selected);
            //Go back to main window
            this.DialogResult = true;
        }

        //Method to reset cbos if need to reselect staff
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            cboSV1.SelectedValue = null;
            txtSV1LOD.Text = null;
            txtSV1LOD.IsEnabled = false;

            cboDRI1.SelectedValue = null;
            txtDRI1LOD.Text = null;
            txtDRI1LOD.IsEnabled = false;
            cboDRI2.SelectedValue = null;
            txtDRI2LOD.Text = null;
            txtDRI2LOD.IsEnabled = false;

            cboRN1.SelectedValue = null;
            txtRN1LOD.Text = null;
            txtRN1LOD.IsEnabled = false;
            cboRN2.SelectedValue = null;
            txtRN2LOD.Text = null;
            txtRN2LOD.IsEnabled = false;
            cboRN3.SelectedValue = null;
            txtRN3LOD.Text = null;
            txtRN3LOD.IsEnabled = false;

            cboCCA1.SelectedValue = null;
            txtCCA1LOD.Text = null;
            txtCCA1LOD.IsEnabled = false;
            cboCCA2.SelectedValue = null;
            txtCCA2LOD.Text = null;
            txtCCA2LOD.IsEnabled = false;
            cboCCA3.SelectedValue = null;
            txtCCA3LOD.Text = null;
            txtCCA3LOD.IsEnabled = false;
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
