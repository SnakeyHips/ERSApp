using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ERSApp.Model;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;

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
            txtType.Text = Selected.Type;
            txtSite.Text = Selected.Site;
            txtTime.Text = Selected.Time;
            txtLOD.Text = Selected.LOD.ToString();
            PopulateChairs(Selected.Type);
            cboChairs.SelectedItem = Selected.Chairs.ToString();
            txtBleeds.Text = Selected.Bleeds.ToString();
        }

        public void PopulateChairs(string type)
        {
            cboChairs.Items.Clear();
            if (type.Equals("MDC"))
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

        private async void btnUpdateSession_Click(object sender, RoutedEventArgs e)
        {
            //Makes sure each input has an input before being made. Possibly better way to do this but this works and is simple to update
            if (Selected.SV1Id == 0 && Selected.DRI1Id == 0 && Selected.DRI2Id == 0 && Selected.RN1Id == 0 && Selected.RN2Id == 0
                && Selected.RN3Id == 0 && Selected.CCA1Id == 0 && Selected.CCA2Id == 0 && Selected.CCA3Id == 0)
            {
                if (txtLOD.Text == "")
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
                    Selected.LOD = double.Parse(txtLOD.Text);
                    Selected.Type = txtType.Text;
                    Selected.Chairs = int.Parse(cboChairs.Text);
                    Selected.Bleeds = int.Parse(txtBleeds.Text);
                    CollectionManager.UpdateSession(Selected);
                    this.DialogResult = true;
                }
            }
            else
            {
                await this.ShowMessageAsync("", "Please remove all staff before changing session details.");
            }
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
