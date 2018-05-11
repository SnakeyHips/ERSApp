using System;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using ERSApp.Model;

namespace ERSApp.Views
{
    public partial class ViewWindow : MetroWindow
    {
        Session Selected;
        public SeriesCollection SeriesCollection { get; set; }

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
            txtStaffCount.Text = Selected.StaffCount.ToString();
            txtChairs.Text = Selected.Chairs.ToString();
            txtBleeds.Text = Selected.Bleeds.ToString();
            SeriesCollection = new SeriesCollection();

            //Autopopulate cbos from StaffList
            if (Selected.SV1Id != 0)
            {
                txtSV1.Text = Selected.SV1Name;
                txtSV1LOD.Text = Selected.SV1LOD.ToString();
                SeriesCollection.Add(CreateRow("SV1", 255, 105, 97, Selected.SV1LOD));
            }
            if (Selected.DRI1Id != 0)
            {
                txtDRI1.Text = Selected.DRI1Name;
                txtDRI1LOD.Text = Selected.DRI1LOD.ToString();
                SeriesCollection.Add(CreateRow("DRI1", 177, 156, 217, Selected.DRI1LOD));
            }
            if (Selected.DRI2Id != 0)
            {
                txtDRI2.Text = Selected.DRI2Name;
                txtDRI2LOD.Text = Selected.DRI2LOD.ToString();
                SeriesCollection.Add(CreateRow("DRI2", 192, 174, 224, Selected.DRI2LOD));
            }
            if (Selected.RN1Id != 0)
            {
                txtRN1.Text = Selected.RN1Name;
                txtRN1LOD.Text = Selected.RN1LOD.ToString();
                SeriesCollection.Add(CreateRow("RN1", 134, 197, 218, Selected.RN1LOD));
            }
            if (Selected.RN2Id != 0)
            {
                txtRN2.Text = Selected.RN2Name;
                txtRN2LOD.Text = Selected.RN2LOD.ToString();
                SeriesCollection.Add(CreateRow("RN2", 153, 207, 224, Selected.RN2LOD));
            }
            if (Selected.RN3Id != 0)
            {
                txtRN3.Text = Selected.RN3Name;
                txtRN3LOD.Text = Selected.RN3LOD.ToString();
                SeriesCollection.Add(CreateRow("RN3", 173, 216, 230, Selected.RN3LOD));
            }
            if (Selected.CCA1Id != 0)
            {
                txtCCA1.Text = Selected.CCA1Name;
                txtCCA1LOD.Text = Selected.CCA1LOD.ToString();
                SeriesCollection.Add(CreateRow("CCA1", 139, 226, 139, Selected.CCA1LOD));
            }
            if (Selected.CCA2Id != 0)
            {
                txtCCA2.Text = Selected.CCA2Name;
                txtCCA2LOD.Text = Selected.CCA2LOD.ToString();
                SeriesCollection.Add(CreateRow("CCA2", 160, 231, 160, Selected.CCA2LOD));
            }
            if (Selected.CCA3Id != 0)
            {
                txtCCA3.Text = Selected.CCA3Name;
                txtCCA3LOD.Text = Selected.CCA3LOD.ToString();
                SeriesCollection.Add(CreateRow("CCA3", 180, 236, 180, Selected.CCA3LOD));
            }
            //Used to set window height if no chart present
            if (SeriesCollection.Count < 1)
            {
                this.Height = 575;
            }
        }

        //Method for adding gantt row on chart
        private RowSeries CreateRow(string title, byte r, byte g, byte b, double lod)
        {
            if (xAxis.MinValue > -lod)
            {
                xAxis.MinValue = -lod;
            }

            return new RowSeries
            {
                Title = title,
                Fill = new SolidColorBrush(Color.FromRgb(r, g, b)),
                Values = new ChartValues<GanttPoint> { new GanttPoint(-lod, lod) }
            };
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
