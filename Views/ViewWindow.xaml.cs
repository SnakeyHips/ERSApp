using System;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro.Controls;
using ERSApp.Model;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

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
            txtDate.Text = Selected.Date;
            txtLocation.Text = Selected.Location;
            txtStartTime.Text = Selected.StartTime;
            txtEndTime.Text = Selected.EndTime;
            txtMDC.Text = Selected.MDC;
            txtChairs.Text = Selected.Chairs.ToString();

            SeriesCollection = new SeriesCollection();
            xAxis.MinValue = int.Parse(Selected.StartTime.Substring(0, 2));
            xAxis.MaxValue = int.Parse(Selected.EndTime.Substring(0, 2));

            //Autopopulate cbos from StaffList
            if (Selected.SV1Id != 0)
            {
                txtSV1.Text = Selected.SV1Name;
                txtSV1Start.Text = Selected.SV1Start;
                txtSV1End.Text = Selected.SV1End;
                SeriesCollection.Add(CreateRow("SV1", 255, 105, 97, Selected.SV1Start, Selected.SV1End));
            }
            if (Selected.DRI1Id != 0)
            {
                txtDRI1.Text = Selected.DRI1Name;
                txtDRI1Start.Text = Selected.DRI1Start;
                txtDRI1End.Text = Selected.DRI1End;
                SeriesCollection.Add(CreateRow("DRI1", 177, 156, 217, Selected.DRI1Start, Selected.DRI1End));
            }
            if (Selected.DRI2Id != 0)
            {
                txtDRI2.Text = Selected.DRI2Name;
                txtDRI2Start.Text = Selected.DRI2Start;
                txtDRI2End.Text = Selected.DRI2End;
                SeriesCollection.Add(CreateRow("DRI2", 192, 174, 224, Selected.DRI2Start, Selected.DRI2End));
            }
            if (Selected.RN1Id != 0)
            {
                txtRN1.Text = Selected.RN1Name;
                txtRN1Start.Text = Selected.RN1Start;
                txtRN1End.Text = Selected.RN1End;
                SeriesCollection.Add(CreateRow("RN1", 134, 197, 218, Selected.RN1Start, Selected.RN1End));
            }
            if (Selected.RN2Id != 0)
            {
                txtRN2.Text = Selected.RN2Name;
                txtRN2Start.Text = Selected.RN2Start;
                txtRN2End.Text = Selected.RN2End;
                SeriesCollection.Add(CreateRow("RN2", 153, 207, 224, Selected.RN2Start, Selected.RN2End));
            }
            if (Selected.RN3Id != 0)
            {
                txtRN3.Text = Selected.RN3Name;
                txtRN3Start.Text = Selected.RN3Start;
                txtRN3End.Text = Selected.RN3End;
                SeriesCollection.Add(CreateRow("RN3", 173, 216, 230, Selected.RN3Start, Selected.RN3End));
            }
            if (Selected.CCA1Id != 0)
            {
                txtCCA1.Text = Selected.CCA1Name;
                txtCCA1Start.Text = Selected.CCA1Start;
                txtCCA1End.Text = Selected.CCA1End;
                SeriesCollection.Add(CreateRow("CCA1", 139, 226, 139, Selected.CCA1Start, Selected.CCA1End));
            }
            if (Selected.CCA2Id != 0)
            {
                txtCCA2.Text = Selected.CCA2Name;
                txtCCA2Start.Text = Selected.CCA2Start;
                txtCCA2End.Text = Selected.CCA2End;
                SeriesCollection.Add(CreateRow("CCA2", 160, 231, 160, Selected.CCA2Start, Selected.CCA2End));
            }
            if (Selected.CCA3Id != 0)
            {
                txtCCA3.Text = Selected.CCA3Name;
                txtCCA3Start.Text = Selected.CCA3Start;
                txtCCA3End.Text = Selected.CCA3End;
                SeriesCollection.Add(CreateRow("CCA3", 180, 236, 180, Selected.CCA3Start, Selected.CCA3End));
            }
        }

        //Method for adding gantt row on chart
        private RowSeries CreateRow(string title, byte r, byte g, byte b, string start, string end)
        {
            return new RowSeries
            {
                Title = title,
                Fill = new SolidColorBrush(Color.FromRgb(r, g, b)),
                Values = new ChartValues<GanttPoint>
                    {
                        new GanttPoint(
                            TimeSpan.Parse(start).TotalHours,
                            TimeSpan.Parse(end).TotalHours
                            )
                    }
            };
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
