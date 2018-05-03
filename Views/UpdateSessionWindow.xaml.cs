using System;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ERSApp.Model;
using System.Windows.Controls;

namespace ERSApp.Views
{
    public partial class UpdateSessionWindow : MetroWindow
    {
        Session Selected;

        public UpdateSessionWindow(Session s)
        {
            InitializeComponent();
            this.Selected = s;
            txtDate.Text = Selected.Date;
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
            txtLocation.Text = Selected.Location;
            cboMDC.Items.Add("No");
            cboMDC.Items.Add("Yes");
            cboStartTime.SelectedItem = Selected.StartTime;
            cboEndTime.SelectedItem = Selected.EndTime;
            cboMDC.SelectedItem = Selected.MDC;
            PopulateChairs(cboMDC.Text);
            cboChairs.SelectedItem = Selected.Chairs.ToString();
        }

        public void PopulateChairs(string MDC)
        {
            cboChairs.Items.Clear();
            if (MDC.Equals("Yes"))
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

        private void cboMDC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboMDC.SelectedIndex == 0)
            {
                PopulateChairs("No");
            }
            else
            {
                PopulateChairs("Yes");
            }
        }

        private async void btnUpdateSession_Click(object sender, RoutedEventArgs e)
        {
            //Makes sure each input has an input before being made. Possibly better way to do this but this works and is simple to update
            if(Selected.SV1Id == 0 && Selected.DRI1Id == 0 && Selected.DRI2Id == 0 && Selected.RN1Id == 0 && Selected.RN2Id == 0
                && Selected.RN3Id == 0 && Selected.CCA1Id == 0 && Selected.CCA2Id == 0 && Selected.CCA3Id == 0)
            {
                if (cboStartTime.Text == "")
                {
                    await this.ShowMessageAsync("", "Please select a Start Time.");
                }
                else if (cboEndTime.Text == "")
                {
                    await this.ShowMessageAsync("", "Please select an End Time.");
                }
                else if (txtLocation.Text == "")
                {
                    await this.ShowMessageAsync("", "Please enter a Location.");
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
                    Selected.StartTime = cboStartTime.Text;
                    Selected.EndTime = cboEndTime.Text;
                    Selected.Length = CollectionManager.GetLength(cboStartTime.Text, cboEndTime.Text);
                    Selected.Location = txtLocation.Text;
                    Selected.MDC = cboMDC.Text;
                    Selected.Chairs = int.Parse(cboChairs.Text);
                    CollectionManager.UpdateSession(Selected);
                    this.Close();
                }
            }
            else
            {
                await this.ShowMessageAsync("", "Please remove all staff before changing session details.");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
