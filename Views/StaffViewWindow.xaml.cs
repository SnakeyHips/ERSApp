using System;
using System.IO;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using ERSApp.Models;
using ERSApp.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ERSApp.Views
{
    /// <summary>
    /// Interaction logic for StaffViewWindow.xaml
    /// </summary>
    public partial class StaffViewWindow : MetroWindow
    {
        public ObservableCollection<Session> StaffSessions { get; set; }
        Staff Selected;
        public StaffViewWindow(Staff s)
        {
            InitializeComponent();
            this.DataContext = this;
            StaffSessions = new ObservableCollection<Session>();
            Selected = s;
            lblHeader.Content = Selected.Id + " - " + Selected.Name;
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            if (dateStart.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Start Date.");
            }
            else if (dateEnd.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter an End Date.");
            }
            else if (dateEnd.SelectedDate.Value.CompareTo(dateStart.SelectedDate.Value) < 0)
            {
                await this.ShowMessageAsync("", "Please enter a valid End Date.");
            }
            else
            {
                StaffSessions.Clear();
                List<string> Dates = new List<string>();
                for (DateTime dt = dateStart.SelectedDate.Value; dt <= dateEnd.SelectedDate.Value; dt = dt.AddDays(1))
                {
                    Dates.Add(dt.ToShortDateString());
                }

                if (Dates.Count > 0)
                {
                    foreach (string date in Dates)
                    {
                        Session temp = SessionViewModel.GetStaffSession(date, Selected.Id.ToString());
                        if (temp != null)
                        {
                            StaffSessions.Add(temp);
                        }
                    }
                }
            }
        }

        private async void btnReportStaff_Click(object sender, RoutedEventArgs e)
        {
            if (StaffSessions.Count > 0)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Title = "Choose Report Save Location";
                saveDialog.FileName = Selected.Id + " Report";
                saveDialog.Filter = "PDF document (*.pdf)|*.pdf";
                bool? result = saveDialog.ShowDialog();
                if (result == true)
                {
                    await CreateSessionReport(StaffSessions, saveDialog.FileName);
                }
            }
            else
            {
                await this.ShowMessageAsync("", "No sessions found for that staff.");
            }
        }

        //Method for creating sessions pdf report
        private async Task CreateSessionReport(ObservableCollection<Session> sessions, string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);

                //Document is A4 size with margins of 36 each side
                Document report = new Document(PageSize.A4, 10, 10, 10, 10);

                PdfWriter writer = PdfWriter.GetInstance(report, fs);

                //Table for displaying stock quantities with 2 being amount of columns
                PdfPTable sessionTable = new PdfPTable(8);
                sessionTable.SpacingBefore = 10f;
                sessionTable.WidthPercentage = 100;

                //Used for creating bold font
                Font title = FontFactory.GetFont(FontFactory.HELVETICA_BOLD);
                Font bold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8);
                bold.Color = BaseColor.WHITE;
                Font norm = FontFactory.GetFont(FontFactory.HELVETICA, 8);

                //Column titles with bold text for stock table
                string[] headers = { "Day", "Date", "Location", "Time", "LOD", "Chairs", "OCC", "Estimate" };

                foreach (string h in headers)
                {
                    sessionTable.AddCell(new PdfPCell(new Paragraph(h, bold))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BackgroundColor = BaseColor.GRAY
                    });
                }

                foreach (Session s in sessions)
                {
                    sessionTable.AddCell(new Paragraph(s.Day, norm));
                    sessionTable.AddCell(new Paragraph(s.Date, norm));
                    sessionTable.AddCell(new Paragraph(s.Site, norm));
                    sessionTable.AddCell(new Paragraph(s.Time, norm));
                    sessionTable.AddCell(new Paragraph(s.LOD.ToString(), norm));
                    sessionTable.AddCell(new Paragraph(s.Chairs.ToString(), norm));
                    sessionTable.AddCell(new Paragraph(s.OCC.ToString(), norm));
                    sessionTable.AddCell(new Paragraph(s.Estimate.ToString(), norm));
                }

                //Title used with date and time when created
                Paragraph titleParagraph = new Paragraph(Selected.Id + " - " + Selected.Name
                    + " Report: " + sessions[0].Date + " - " + sessions[sessions.Count - 1].Date, title);
                titleParagraph.Alignment = Element.ALIGN_CENTER;

                //Creates and adds everything to pdf output
                report.Open();
                report.Add(titleParagraph);
                report.Add(sessionTable);
                report.Close();

                await this.ShowMessageAsync("", "Report created successfully!");
            }
            catch
            {
                //Most common issue for report not producing is that previous file is already open
                await this.ShowMessageAsync("", "Report failed to create. Please make sure a report is not already open.");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
