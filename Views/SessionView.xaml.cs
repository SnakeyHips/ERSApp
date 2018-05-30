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
    public partial class SessionView : UserControl
    {
        MetroWindow mainWindow = (Application.Current.MainWindow as MetroWindow);

        public SessionView()
        {
            InitializeComponent();
            this.DataContext = new SessionViewModel();
        }

        private void btnAddSession_Click(object sender, RoutedEventArgs e)
        {
            AddSessionWindow addSessionWindow = new AddSessionWindow();
            addSessionWindow.Owner = mainWindow;
            addSessionWindow.ShowDialog();
            if(addSessionWindow.DialogResult == true)
            {
                Calendar cal = mainWindow.FindChild<Calendar>("dateCalender");
                if(cal.SelectedDate != addSessionWindow.dateSession.SelectedDate)
                {
                    cal.SelectedDate = addSessionWindow.dateSession.SelectedDate;
                    cal.DisplayDate = addSessionWindow.dateSession.SelectedDate.Value;
                    SelectedDate.Date = addSessionWindow.dateSession.SelectedDate.Value;
                }
            }
        }

        private async void btnUpdateSession_Click(object sender, RoutedEventArgs e)
        {
            if (lstSessions.SelectedIndex > -1)
            {
                if (SessionViewModel.SelectedSession.SV1Id == 0 && SessionViewModel.SelectedSession.DRI1Id == 0 &&
                    SessionViewModel.SelectedSession.DRI2Id == 0 && SessionViewModel.SelectedSession.RN1Id == 0 &&
                    SessionViewModel.SelectedSession.RN2Id == 0 && SessionViewModel.SelectedSession.RN3Id == 0 &&
                    SessionViewModel.SelectedSession.CCA1Id == 0 && SessionViewModel.SelectedSession.CCA2Id == 0 &&
                    SessionViewModel.SelectedSession.CCA3Id == 0)
                {
                    UpdateSessionWindow updateSessionWindow = new UpdateSessionWindow(SessionViewModel.SelectedSession);
                    updateSessionWindow.Owner = mainWindow;
                    updateSessionWindow.ShowDialog();
                }
                else
                {
                    await mainWindow.ShowMessageAsync("", "Please unappoint all staff before updating Session.");
                }

            }
        }

        private void btnViewSession_Click(object sender, RoutedEventArgs e)
        {
            if (lstSessions.SelectedIndex > -1)
            {
                ViewWindow viewWindow = new ViewWindow(SessionViewModel.SelectedSession);
                viewWindow.Show();
            }
        }

        private void btnOverviewSession_Click(object sender, RoutedEventArgs e)
        {
            OverviewWindow overviewWindow = new OverviewWindow();
            overviewWindow.Show();
        }

        private void btnStaffSession_Click(object sender, RoutedEventArgs e)
        {
            if (lstSessions.SelectedIndex > -1)
            {
                StaffSessionWindow staffSessionWindow = new StaffSessionWindow(SessionViewModel.SelectedSession);
                staffSessionWindow.Owner = mainWindow;
                staffSessionWindow.ShowDialog();
            }
        }

        private async void btnDelSession_Click(object sender, RoutedEventArgs e)
        {
            if (lstSessions.SelectedIndex > -1)
            {
                if (SessionViewModel.SelectedSession.SV1Id == 0 && SessionViewModel.SelectedSession.DRI1Id == 0 &&
                    SessionViewModel.SelectedSession.DRI2Id == 0 && SessionViewModel.SelectedSession.RN1Id == 0 &&
                    SessionViewModel.SelectedSession.RN2Id == 0 && SessionViewModel.SelectedSession.RN3Id == 0 &&
                    SessionViewModel.SelectedSession.CCA1Id == 0 && SessionViewModel.SelectedSession.CCA2Id == 0 &&
                    SessionViewModel.SelectedSession.CCA3Id == 0)
                {
                    MessageDialogResult choice = await mainWindow.ShowMessageAsync("",
                            "Are you sure you want to delete this Session?",
                            MessageDialogStyle.AffirmativeAndNegative);
                    if (choice == MessageDialogResult.Affirmative)
                    {
                        SessionViewModel.DeleteSession(SessionViewModel.SelectedSession);
                        SessionViewModel.Sessions.Remove(SessionViewModel.SelectedSession);
                    }
                }
                else
                {
                    await mainWindow.ShowMessageAsync("", "Please unappoint all staff before deleting Session.");
                }
            }
        }

        private async void btnReportSession_Click(object sender, RoutedEventArgs e)
        {
            StaffReportDialog staffReportDialog = new StaffReportDialog();
            staffReportDialog.Owner = mainWindow;
            staffReportDialog.ShowDialog();
            if (staffReportDialog.DialogResult == true)
            {
                List<DateTime> Dates = new List<DateTime>();
                DateTime Start = DateTime.Parse(staffReportDialog.dateStart.Text);
                DateTime End = DateTime.Parse(staffReportDialog.dateEnd.Text);

                for (DateTime dt = Start; dt <= End; dt = dt.AddDays(1))
                {
                    Dates.Add(dt);
                }

                if (Dates.Count > 0)
                {
                    List<Session> ReportSessions = new List<Session>();
                    foreach (DateTime date in Dates)
                    {
                        ReportSessions.AddRange(SessionViewModel.GetSessions(date.ToShortDateString()));
                    }
                    if (ReportSessions.Count > 0)
                    {
                        SaveFileDialog saveDialog = new SaveFileDialog();
                        saveDialog.Title = "Choose Report Save Location";
                        saveDialog.FileName = "Session Report";
                        saveDialog.Filter = "PDF document (*.pdf)|*.pdf";
                        bool? result = saveDialog.ShowDialog();

                        if (result == true)
                        {
                            //disable main window and activate progress ring while report is being created
                            await CreateSessionReport(ReportSessions, saveDialog.FileName);
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
                Document report = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);

                PdfWriter writer = PdfWriter.GetInstance(report, fs);

                //Table for displaying stock quantities with 2 being amount of columns
                PdfPTable sessionTable = new PdfPTable(17);
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
                sessionTable.AddCell(new Paragraph("RN1", bold));
                sessionTable.AddCell(new Paragraph("RN2", bold));
                sessionTable.AddCell(new Paragraph("RN3", bold));
                sessionTable.AddCell(new Paragraph("SV1", bold));
                sessionTable.AddCell(new Paragraph("DRI1", bold));
                sessionTable.AddCell(new Paragraph("DRI2", bold));
                sessionTable.AddCell(new Paragraph("CCA1", bold));
                sessionTable.AddCell(new Paragraph("CCA2", bold));
                sessionTable.AddCell(new Paragraph("CCA3", bold));
                sessionTable.AddCell(new Paragraph("Count", bold));

                foreach (Session s in sessions)
                {
                    sessionTable.AddCell(new Paragraph(DateTime.Parse(s.Date).DayOfWeek.ToString(), norm));
                    sessionTable.AddCell(new Paragraph(s.Date, norm));
                    sessionTable.AddCell(new Paragraph(s.Site, norm));
                    sessionTable.AddCell(new Paragraph(s.Time, norm));
                    sessionTable.AddCell(new Paragraph(s.LOD.ToString(), norm));
                    sessionTable.AddCell(new Paragraph(s.Chairs.ToString(), norm));
                    sessionTable.AddCell(new Paragraph(s.Bleeds.ToString(), norm));
                    sessionTable.AddCell(new Paragraph(s.RN1Name, norm));
                    sessionTable.AddCell(new Paragraph(s.RN2Name, norm));
                    sessionTable.AddCell(new Paragraph(s.RN3Name, norm));
                    sessionTable.AddCell(new Paragraph(s.SV1Name, norm));
                    sessionTable.AddCell(new Paragraph(s.DRI1Name, norm));
                    sessionTable.AddCell(new Paragraph(s.DRI2Name, norm));
                    sessionTable.AddCell(new Paragraph(s.CCA1Name, norm));
                    sessionTable.AddCell(new Paragraph(s.CCA2Name, norm));
                    sessionTable.AddCell(new Paragraph(s.CCA3Name, norm));
                    sessionTable.AddCell(new Paragraph(s.StaffCount.ToString(), norm));
                }

                //Title used with date and time when created
                Paragraph titleParagraph = new Paragraph(
                    "Session Report: " + sessions[0].Date + " - " + sessions[sessions.Count - 1].Date, header);
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
