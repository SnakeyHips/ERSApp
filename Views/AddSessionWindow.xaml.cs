using System;
using System.Windows;
using ERSApp.Model;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ERSApp.Views
{
    public partial class AddSessionWindow : MetroWindow
    {
        public AddSessionWindow()
        {
            InitializeComponent();
            cboStartTime.Items.Add("08:00");
            cboStartTime.Items.Add("09:00");
            cboStartTime.Items.Add("10:00");
            cboStartTime.Items.Add("11:00");
            cboStartTime.Items.Add("12:00");
            cboEndTime.Items.Add("14:00");
            cboEndTime.Items.Add("15:00");
            cboEndTime.Items.Add("16:00");
            cboEndTime.Items.Add("17:00");
            cboEndTime.Items.Add("18:00");
            cboMDC.Items.Add("No");
            cboMDC.Items.Add("Yes");
            PopulateChairs(cboMDC.Text);
        }

        public void PopulateChairs(string MDC)
        {
            cboChairs.Items.Clear();
            if(MDC.Equals("Yes"))
            {
                cboChairs.Items.Add("3");
                cboChairs.Items.Add("6");
            }
            else
            {
                cboChairs.Items.Add("4");
                cboChairs.Items.Add("6");
                cboChairs.Items.Add("8");
                cboChairs.Items.Add("9");
                cboChairs.Items.Add("10");
            }
        }

        private void cboMDC_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(cboMDC.SelectedIndex == 0)
            {
                PopulateChairs("No");
            }
            else
            {
                PopulateChairs("Yes");
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
            else if (cboStartTime.Text == "")
            {
                await this.ShowMessageAsync("", "Please select a Start Time.");
            }
            else if (cboEndTime.Text == "")
            {
                await this.ShowMessageAsync("", "Please select an End Time.");
            }
            else if (txtLocation.Text == "")
            {
                await this.ShowMessageAsync("", "Please select a Location.");
            }
            else if (cboMDC.Text == "")
            {
                await this.ShowMessageAsync("", "Please select if session is MDC or not.");
            }
            else if (cboChairs.Text == "")
            {
                await this.ShowMessageAsync("", "Please select a Chair amount.");
            }
            else
            {
                Session temp = new Session()
                {
                    Date = dateSession.Text,
                    StartTime = cboStartTime.Text,
                    EndTime = cboEndTime.Text,
                    Length = CollectionManager.GetLength(cboStartTime.Text, cboEndTime.Text),
                    Location = txtLocation.Text,
                    MDC = cboMDC.Text == "Yes" ? "Yes" : "No",
                    Chairs = int.Parse(cboChairs.Text),
                    SV1Id = 0, SV1Name = "", SV1Start = "", SV1End = "",
                    DRI1Id = 0, DRI1Name = "", DRI1Start = "", DRI1End = "",
                    DRI2Id = 0, DRI2Name = "", DRI2Start = "", DRI2End = "",
                    RN1Id = 0, RN1Name = "", RN1Start = "", RN1End = "",
                    RN2Id = 0, RN2Name = "", RN2Start = "", RN2End = "",
                    RN3Id = 0, RN3Name = "", RN3Start = "", RN3End = "",
                    CCA1Id = 0, CCA1Name = "", CCA1Start = "", CCA1End = "",
                    CCA2Id = 0, CCA2Name = "", CCA2Start = "", CCA2End = "",
                    CCA3Id = 0, CCA3Name = "", CCA3Start = "", CCA3End = "",
                    State = "Incomplete"
                };
                if (CollectionManager.AddSession(temp) > 0)
                {
                    CollectionManager.SelectedDate = DateTime.Parse(dateSession.Text);
                    this.DialogResult = true;
                }
                else
                {
                    await this.ShowMessageAsync("", "Duplicate Session found.");
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
