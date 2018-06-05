using System;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using ERSApp.Models;
using ERSApp.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Controls;

namespace ERSApp.Views
{
    public partial class AddSessionWindow : MetroWindow
    {

        public AddSessionWindow()
        {
            InitializeComponent();
            this.DataContext = new SiteViewModel();
            cboType.Items.Add("Community");
            cboType.Items.Add("MDC");
            TypePopulate(cboType.Text);
        }

        public void TypePopulate(string type)
        {
            cboChairs.Items.Clear();
            cboTime.Items.Clear();
            if (type.Equals("MDC"))
            {
                cboSite.ItemsSource = SiteViewModel.MDCLocations;
                cboChairs.Items.Add("3");
                cboChairs.Items.Add("6");
            }
            else
            {
                cboSite.ItemsSource = SiteViewModel.CommunityLocations;
                cboChairs.Items.Add("4");
                cboChairs.Items.Add("6");
                cboChairs.Items.Add("8");
                cboChairs.Items.Add("9");
                cboChairs.Items.Add("10");
            }
        }

        private void cboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboType.SelectedIndex == 0)
            {
                TypePopulate("Community");
            }
            else
            {
                TypePopulate("MDC");
            }
        }

        private void cboSite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboSite.SelectedItem != null)
            {
                cboTime.Items.Clear();
                Site Selected = (Site)cboSite.SelectedItem;
                string[] Times = Selected.Times.Split('/');
                foreach(string t in Times)
                {
                    cboTime.Items.Add(t);
                }
            }
        }

        private async void btnAddSession_Click(object sender, RoutedEventArgs e)
        {
            //Makes sure each input has an input before being made. Possibly better way to do this but this works and is simple to update
            if (dateSession.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Date.");
            }
            else if (DateTime.Parse(dateSession.Text).CompareTo(DateTime.Now) < 0)
            {
                await this.ShowMessageAsync("", "Please enter a valid Date.");
            }
            else if (cboType.Text == "")
            {
                await this.ShowMessageAsync("", "Please select a Session Type.");
            }
            else if (cboSite.Text == "")
            {
                await this.ShowMessageAsync("", "Please select a Collection Site.");
            }
            else if (!CheckSite(cboSite.Text))
            {
                await this.ShowMessageAsync("", "Please enter a valid Collection Site.");
            }
            else if (cboTime.Text == "")
            {
                await this.ShowMessageAsync("", "Please select a Clinic Time.");
            }
            else if (txtLOD.Text == "")
            {
                await this.ShowMessageAsync("", "Please select a Length Of Day.");
            }
            else if (double.Parse(txtLOD.Text) < 0)
            {
                await this.ShowMessageAsync("", "Please select a valid Length Of Day.");
            }
            else if (cboChairs.Text == "")
            {
                await this.ShowMessageAsync("", "Please select a Chair amount.");
            }
            else if (txtBleeds.Text == "")
            {
                await this.ShowMessageAsync("", "Please select an estimated Bleed amount.");
            }
            else
            {
                Session temp = new Session()
                {
                    Date = dateSession.Text,
                    Type = cboType.Text,
                    Site = cboSite.Text,
                    Time = cboTime.Text,
                    LOD = double.Parse(txtLOD.Text),
                    Chairs = int.Parse(cboChairs.Text),
                    Bleeds = int.Parse(txtBleeds.Text),
                    Holiday = CheckHoliday(),
                    SV1Id = 0, SV1Name = "", SV1LOD = 0.0, SV1UNS = 0.0,
                    DRI1Id = 0, DRI1Name = "", DRI1LOD = 0.0, DRI1UNS = 0.0,
                    DRI2Id = 0, DRI2Name = "", DRI2LOD = 0.0, DRI2UNS = 0.0,
                    RN1Id = 0, RN1Name = "", RN1LOD = 0.0, RN1UNS = 0.0,
                    RN2Id = 0, RN2Name = "", RN2LOD = 0.0, RN2UNS = 0.0,
                    RN3Id = 0, RN3Name = "", RN3LOD = 0.0, RN3UNS = 0.0,
                    CCA1Id = 0, CCA1Name = "", CCA1LOD = 0.0, CCA1UNS = 0.0,
                    CCA2Id = 0, CCA2Name = "", CCA2LOD = 0.0, CCA2UNS = 0.0,
                    CCA3Id = 0, CCA3Name = "", CCA3LOD = 0.0, CCA3UNS = 0.0,
                    StaffCount = 0, State = 0
                };
                if (SessionViewModel.AddSession(temp) > 0)
                {
                    if(SelectedDate.Date.ToShortDateString().Equals(dateSession.Text))
                    {
                        SessionViewModel.Sessions.Add(temp);
                    }
                    this.DialogResult = true;
                }
                else
                {
                    await this.ShowMessageAsync("", "Duplicate Session found.");
                }
            }
        }

        //Method for checking valid site entered
        private bool CheckSite(string site)
        {
            bool allowed = false;
            foreach(Site s in cboSite.Items)
            {
                if (s.Name == cboSite.Text)
                {
                    allowed = true;
                    break;
                }
            }
            return allowed;
        }
        
                //Method for checking if selected date is weekend/holiday
        public bool CheckHoliday()
        {
            bool holiday = false;
            if (dateSession.SelectedDate.Value.DayOfWeek.Equals(DayOfWeek.Saturday) 
                || dateSession.SelectedDate.Value.DayOfWeek.Equals(DayOfWeek.Sunday))
            {
                holiday = true;
            }
            else
            {
                string date = dateSession.SelectedDate.Value.ToShortDateString();
                foreach (Holiday h in AdminViewModel.Holidays)
                {
                    if (h.Date.Equals(date))
                    {
                        holiday = true;
                    }
                }
            }
            return holiday;
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
