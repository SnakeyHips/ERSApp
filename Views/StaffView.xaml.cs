using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using ERSApp.Model;
using ERSApp.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERSApp.Views
{
    public partial class StaffView : UserControl
    {
        MetroWindow mainWindow = (Application.Current.MainWindow as MetroWindow);

        public StaffView()
        {
            InitializeComponent();
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                this.DataContext = new StaffViewModel();
            }
        }

        private void btnAddStaff_Click(object sender, RoutedEventArgs e)
        {
            AddStaffWindow addStaffWindow = new AddStaffWindow();
            addStaffWindow.Owner = mainWindow;
            addStaffWindow.ShowDialog();
        }

        private void btnUpdateStaff_Click(object sender, RoutedEventArgs e)
        {
            if (lstStaff.SelectedIndex > -1)
            {
                UpdateStaffWindow updateStaffWindow = new UpdateStaffWindow(StaffViewModel.SelectedStaff);
                updateStaffWindow.Owner = mainWindow;
                updateStaffWindow.ShowDialog();
            }
        }

        private void btnArchiveStaff_Click(object sender, RoutedEventArgs e)
        {
            ArchiveStaffWindow ArchiveStaffWindow = new ArchiveStaffWindow();
            ArchiveStaffWindow.Owner = mainWindow;
            ArchiveStaffWindow.ShowDialog();
        }

        private async void btnReportStaff_Click(object sender, RoutedEventArgs e)
        {
            if(StaffViewModel.SelectedStaff != null)
            {
                StaffReportDialog staffReportDialog = new StaffReportDialog();
                staffReportDialog.Owner = mainWindow;
                staffReportDialog.ShowDialog();
                if (staffReportDialog.DialogResult == true)
                {
                    List<string> Dates = new List<string>();
                    DateTime Start = DateTime.Parse(staffReportDialog.dateStart.Text);
                    DateTime End = DateTime.Parse(staffReportDialog.dateEnd.Text);

                    for (DateTime dt = Start; dt <= End; dt = dt.AddDays(1))
                    {
                        Dates.Add(dt.ToShortDateString());
                    }

                    if (Dates.Count > 0)
                    {
                        List<Session> ReportSessions = new List<Session>();
                        foreach (string date in Dates)
                        {
                            Session temp = SessionViewModel.GetStaffSession(date, StaffViewModel.SelectedStaff.Id.ToString());
                            if(temp != null)
                            {
                                ReportSessions.Add(temp);
                            }
                        }
                        if (ReportSessions.Count > 0)
                        {
                            SaveFileDialog saveDialog = new SaveFileDialog();
                            saveDialog.Title = "Choose Report Save Location";
                            saveDialog.FileName = StaffViewModel.SelectedStaff.Id + " Report";
                            saveDialog.Filter = "PDF document (*.pdf)|*.pdf";
                            bool? result = saveDialog.ShowDialog();

                            if (result == true)
                            {
                                //disable main window and activate progress ring while report is being created
                                await CreateSessionReport(ReportSessions, saveDialog.FileName);
                            }
                        }
                        else
                        {
                            await mainWindow.ShowMessageAsync("", "No sessions found for that staff.");
                        }
                    }
                }
            }
        }

        //Method for creating sessions pdf report
        private async Task CreateSessionReport(List<Session> sessions, string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);

                //Document is A4 size with margins of 36 each side
                Document report = new Document(PageSize.A4, 10, 10, 10, 10);

                PdfWriter writer = PdfWriter.GetInstance(report, fs);

                //Table for displaying stock quantities with 2 being amount of columns
                PdfPTable sessionTable = new PdfPTable(7);
                sessionTable.SpacingBefore = 10f;
                sessionTable.WidthPercentage = 100;

                //Used for creating bold font
                Font header = FontFactory.GetFont(FontFactory.HELVETICA_BOLD);
                Font bold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8);
                Font norm = FontFactory.GetFont(FontFactory.HELVETICA, 8);

                //Column titles with bold text for stock table
                sessionTable.AddCell(new Paragraph("Day", bold));
                sessionTable.AddCell(new Paragraph("Date", bold));
                sessionTable.AddCell(new Paragraph("Location", bold));
                sessionTable.AddCell(new Paragraph("Time", bold));
                sessionTable.AddCell(new Paragraph("LOD", bold));
                sessionTable.AddCell(new Paragraph("Chairs", bold));
                sessionTable.AddCell(new Paragraph("Bleeds", bold));

                foreach (Session s in sessions)
                {
                    sessionTable.AddCell(new Paragraph(DateTime.Parse(s.Date).DayOfWeek.ToString(), norm));
                    sessionTable.AddCell(new Paragraph(s.Date, norm));
                    sessionTable.AddCell(new Paragraph(s.Site, norm));
                    sessionTable.AddCell(new Paragraph(s.Time, norm));
                    sessionTable.AddCell(new Paragraph(s.LOD.ToString(), norm));
                    sessionTable.AddCell(new Paragraph(s.Chairs.ToString(), norm));
                    sessionTable.AddCell(new Paragraph(s.Bleeds.ToString(), norm));
                }

                //Title used with date and time when created
                Paragraph titleParagraph = new Paragraph(
                    StaffViewModel.SelectedStaff.Id + " - " + StaffViewModel.SelectedStaff.Name+ " Report: " + 
                    sessions[0].Date + " - " + sessions[sessions.Count - 1].Date, header);
                titleParagraph.Alignment = Element.ALIGN_CENTER;

                //Creates and adds everything to pdf output
                report.Open();
                report.Add(titleParagraph);
                report.Add(sessionTable);
                report.Close();

                await mainWindow.ShowMessageAsync("", "Report created successfully!");
            }
            catch
            {
                //Most common issue for report not producing is that previous file is already open
                await mainWindow.ShowMessageAsync("", "Report failed to create. Please make sure a report is not already open.");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }
    }
}
