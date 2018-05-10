using System;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro.Controls;
using ERSApp.Model;

namespace ERSApp.Views
{
    public partial class ViewWindow : MetroWindow
    {
        Session Selected;

        public ViewWindow(Session s)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Selected = s;
            //Set UI to match selected
            lblHeader.Content = DateTime.Parse(Selected.Date).DayOfWeek.ToString() +
                " - " + Selected.Date + " - " + Selected.Location;
            txtClinicTime.Text = Selected.ClinicTime;
            txtLOD.Text = Selected.LOD.ToString();
            txtType.Text = Selected.Type;
            txtChairs.Text = Selected.Chairs.ToString();
            txtBleeds.Text = Selected.Bleeds.ToString();

            //Autopopulate cbos from StaffList
            if (Selected.SV1Id != 0)
            {
                txtSV1.Text = Selected.SV1Name;
                txtSV1LOD.Text = Selected.SV1LOD.ToString();
            }
            if (Selected.DRI1Id != 0)
            {
                txtDRI1.Text = Selected.DRI1Name;
                txtDRI1LOD.Text = Selected.DRI1LOD.ToString();
            }
            if (Selected.DRI2Id != 0)
            {
                txtDRI2.Text = Selected.DRI2Name;
                txtDRI2LOD.Text = Selected.DRI2LOD.ToString();
            }
            if (Selected.RN1Id != 0)
            {
                txtRN1.Text = Selected.RN1Name;
                txtRN1LOD.Text = Selected.RN1LOD.ToString();
            }
            if (Selected.RN2Id != 0)
            {
                txtRN2.Text = Selected.RN2Name;
                txtRN2LOD.Text = Selected.RN2LOD.ToString();
            }
            if (Selected.RN3Id != 0)
            {
                txtRN3.Text = Selected.RN3Name;
                txtRN3LOD.Text = Selected.RN3LOD.ToString();
            }
            if (Selected.CCA1Id != 0)
            {
                txtCCA1.Text = Selected.CCA1Name;
                txtCCA1LOD.Text = Selected.CCA1LOD.ToString();
            }
            if (Selected.CCA2Id != 0)
            {
                txtCCA2.Text = Selected.CCA2Name;
                txtCCA2LOD.Text = Selected.CCA2LOD.ToString();
            }
            if (Selected.CCA3Id != 0)
            {
                txtCCA3.Text = Selected.CCA3Name;
                txtCCA3LOD.Text = Selected.CCA3LOD.ToString();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
