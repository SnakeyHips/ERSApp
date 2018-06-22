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
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ERSApp.Views
{
    public partial class AbsenceViewWindow : MetroWindow
    {
        public ObservableCollection<Absence> ViewAbsences { get; set; }
        public AbsenceViewWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            ViewAbsences = new ObservableCollection<Absence>();
        }

        private async void btnView_Click(object sender, RoutedEventArgs e)
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
                ViewAbsences.Clear();
                foreach (Absence a in AbsenceViewModel.Absences)
                {
                    if (DateTime.Parse(a.StartDate).CompareTo(dateStart.SelectedDate.Value) >= 0
                    && DateTime.Parse(a.EndDate).CompareTo(dateEnd.SelectedDate.Value) <= 0)
                    {
                        ViewAbsences.Add(a);
                    }
                }
            }
        }

        private async void btnReportStaff_Click(object sender, RoutedEventArgs e)
        {
            if (ViewAbsences.Count > 0)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Title = "Choose Report Save Location";
                saveDialog.FileName = "Absence Report";
                saveDialog.Filter = "PDF document (*.pdf)|*.pdf";
                bool? result = saveDialog.ShowDialog();
                if (result == true)
                {
                    await CreateSessionReport(ViewAbsences, saveDialog.FileName);
                }
            }
            else
            {
                await this.ShowMessageAsync("", "No sessions found for that staff.");
            }
        }

        //Method for creating sessions pdf report
        private async Task CreateSessionReport(ObservableCollection<Absence> absences, string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);

                //Document is A4 size with margins of 36 each side
                Document report = new Document(PageSize.A4, 10, 10, 10, 10);

                PdfWriter writer = PdfWriter.GetInstance(report, fs);

                //Table for displaying stock quantities with 2 being amount of columns
                PdfPTable absenceTable = new PdfPTable(6);
                absenceTable.SpacingBefore = 10f;
                absenceTable.WidthPercentage = 100;

                //Used for creating bold font
                Font title = FontFactory.GetFont(FontFactory.HELVETICA_BOLD);
                Font bold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8);
                bold.Color = BaseColor.WHITE;
                Font norm = FontFactory.GetFont(FontFactory.HELVETICA, 8);

                //Column titles with bold text for stock table
                string[] headers = { "Staff ID", "Staff Name", "Type", "Start", "End", "Length"};

                foreach(string h in headers)
                {
                    absenceTable.AddCell(new PdfPCell(new Paragraph(h, bold))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BackgroundColor = BaseColor.GRAY
                    });
                }

                foreach(Absence a in absences)
                {
                    absenceTable.AddCell(new Paragraph(a.StaffId.ToString(), norm));
                    absenceTable.AddCell(new Paragraph(a.StaffName, norm));
                    absenceTable.AddCell(new Paragraph(a.Type, norm));
                    absenceTable.AddCell(new Paragraph(a.StartDate, norm));
                    absenceTable.AddCell(new Paragraph(a.EndDate, norm));
                    absenceTable.AddCell(new Paragraph(a.Hours.ToString(), norm));
                }

                //Title used with date and time when created
                Paragraph titleParagraph = new Paragraph("Absence Report: " + dateStart.Text + " - " + dateEnd.Text);
                titleParagraph.Alignment = Element.ALIGN_CENTER;

                //Creates and adds everything to pdf output
                report.Open();
                report.Add(titleParagraph);
                report.Add(absenceTable);
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
