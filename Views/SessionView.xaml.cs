using System;
using System.IO;
using System.Linq;
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
using System.Windows.Input;

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

        private void dateSession_SelectedDatesChanged(object sender, RoutedEventArgs e)
        {
            Mouse.Capture(null);
            CollectionManager.SelectedDate = dateSession.SelectedDate.Value.Date;
            SessionViewModel.LoadSessions();
            lstSessions.ItemsSource = SessionViewModel.Sessions;
        }

        private void btnAddSession_Click(object sender, RoutedEventArgs e)
        {
            AddSessionWindow addSessionWindow = new AddSessionWindow();
            addSessionWindow.Owner = mainWindow;
            addSessionWindow.ShowDialog();
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
                viewWindow.Owner = mainWindow;
                viewWindow.Show();
            }
        }

        private void btnOverviewSession_Click(object sender, RoutedEventArgs e)
        {
            OverviewWindow overviewWindow = new OverviewWindow();
            overviewWindow.Owner = mainWindow;
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
                        CollectionManager.DeleteSession(SessionViewModel.SelectedSession);
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

                if (Dates.Count() > 0)
                {
                    List<Session> ReportSessions = new List<Session>();
                    foreach (DateTime date in Dates)
                    {
                        ReportSessions.AddRange(CollectionManager.GetSessions(date.ToShortDateString()));
                    }
                    if (ReportSessions.Count() > 0)
                    {
                        SaveFileDialog saveDialog = new SaveFileDialog();
                        saveDialog.Title = "Choose Report Save Location";
                        saveDialog.FileName = "Session Report";
                        saveDialog.Filter = "PDF document (*.pdf)|*.pdf";
                        bool? result = saveDialog.ShowDialog();

                        if (result == true)
                        {
                            //disable main window and activate progress ring while report is being created
                            this.IsHitTestVisible = false;
                            await CreateSessionReport(ReportSessions, saveDialog.FileName);
                        }
                    }
                }
            }
        }

        //MEthod for creating sessions pdf report
        private async Task CreateSessionReport(List<Session> sessions, string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
                using (Document document = new Document())
                {
                    using (PdfSmartCopy copy = new PdfSmartCopy(document, fs))
                    {
                        document.Open();
                        foreach (Session s in sessions)
                        {
                            //Read in template here
                            PdfReader reader = new PdfReader("Templates/SessionTemplate.pdf");
                            using (MemoryStream ms = new MemoryStream())
                            {
                                using (PdfStamper stamp = new PdfStamper(reader, ms))
                                {
                                    //Put together all fields from pdf template
                                    AcroFields fields = stamp.AcroFields;
                                    fields.SetField("Date", s.Date);
                                    fields.SetField("StartTime", s.StartTime);
                                    fields.SetField("MDC", s.MDC);
                                    fields.SetField("Location", s.Location);
                                    fields.SetField("EndTime", s.EndTime);
                                    fields.SetField("Chairs", s.Chairs.ToString());
                                    fields.SetField("StartTime", s.StartTime);
                                    fields.SetField("SV1Name", s.SV1Name);
                                    fields.SetField("SV1Start", s.SV1Start);
                                    fields.SetField("SV1End", s.SV1End);
                                    fields.SetField("DRI1Name", s.DRI1Name);
                                    fields.SetField("DRI1Start", s.DRI1Start);
                                    fields.SetField("DRI1End", s.DRI1End);
                                    fields.SetField("DRI2Name", s.DRI2Name);
                                    fields.SetField("DRI2Start", s.DRI2Start);
                                    fields.SetField("DRI2End", s.DRI2End);
                                    fields.SetField("RN1Name", s.RN1Name);
                                    fields.SetField("RN1Start", s.RN1Start);
                                    fields.SetField("RN1End", s.RN1End);
                                    fields.SetField("RN2Name", s.RN2Name);
                                    fields.SetField("RN2Start", s.RN2Start);
                                    fields.SetField("RN2End", s.RN2End);
                                    fields.SetField("RN3Name", s.RN3Name);
                                    fields.SetField("RN3Start", s.RN3Start);
                                    fields.SetField("RN3End", s.RN3End);
                                    fields.SetField("CCA1Name", s.CCA1Name);
                                    fields.SetField("CCA1Start", s.CCA1Start);
                                    fields.SetField("CCA1End", s.CCA1End);
                                    fields.SetField("CCA2Name", s.CCA2Name);
                                    fields.SetField("CCA2Start", s.CCA2Start);
                                    fields.SetField("CCA2End", s.CCA2End);
                                    fields.SetField("CCA3Name", s.CCA3Name);
                                    fields.SetField("CCA3Start", s.CCA3Start);
                                    fields.SetField("CCA3End", s.CCA3End);
                                    stamp.FormFlattening = true;
                                }
                                reader = new PdfReader(ms.ToArray());
                                //Add page for each session
                                copy.AddPage(copy.GetImportedPage(reader, 1));
                            }
                        }
                    }
                }
                this.IsHitTestVisible = true;
                await mainWindow.ShowMessageAsync("", "Report created successfully.");
            }
            catch
            {
                this.IsHitTestVisible = true;
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
