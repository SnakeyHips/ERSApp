using System;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using ERSApp.Model;
using ERSApp.ViewModel;
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
            cboTime.Items.Add("08:00");
            cboTime.Items.Add("09:00");
            cboTime.Items.Add("10:00");
            cboTime.Items.Add("11:00");
            cboTime.Items.Add("12:00");
            cboType.Items.Add("Community");
            cboType.Items.Add("MDC");
            TypePopulate(cboType.Text);
        }

        public void TypePopulate(string type)
        {
            cboBeds.Items.Clear();
            cboTime.Items.Clear();
            if (type.Equals("MDC"))
            {
                cboSite.ItemsSource = SiteViewModel.MDCLocations;
                cboBeds.Items.Add("3");
                cboBeds.Items.Add("6");
            }
            else
            {
                cboSite.ItemsSource = SiteViewModel.CommunityLocations;
                cboBeds.Items.Add("4");
                cboBeds.Items.Add("6");
                cboBeds.Items.Add("8");
                cboBeds.Items.Add("9");
                cboBeds.Items.Add("10");
            }
        }

        private void cboType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
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

        private void cboSite_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
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
            else if (cboBeds.Text == "")
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
                    Beds = int.Parse(cboBeds.Text),
                    Bleeds = int.Parse(txtBleeds.Text),
                    SV1Id = 0, SV1Name = "", SV1LOD = 0.0,
                    DRI1Id = 0, DRI1Name = "", DRI1LOD = 0.0,
                    DRI2Id = 0, DRI2Name = "", DRI2LOD = 0.0,
                    RN1Id = 0, RN1Name = "", RN1LOD = 0.0,
                    RN2Id = 0, RN2Name = "", RN2LOD = 0.0,
                    RN3Id = 0, RN3Name = "", RN3LOD = 0.0,
                    CCA1Id = 0, CCA1Name = "", CCA1LOD = 0.0,
                    CCA2Id = 0, CCA2Name = "", CCA2LOD = 0.0,
                    CCA3Id = 0, CCA3Name = "", CCA3LOD = 0.0,
                    StaffCount = 0, State = 0
                };
                if (CollectionManager.AddSession(temp) > 0)
                {
                    if(SessionViewModel.Sessions.Count > 0)
                    {
                        if (SessionViewModel.Sessions[0].Date.Equals(dateSession.Text))
                        {
                            SessionViewModel.Sessions.Add(temp);
                        }
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
