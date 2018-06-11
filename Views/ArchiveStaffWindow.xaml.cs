using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using ERSApp.Models;
using ERSApp.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERSApp.Views
{
    public partial class ArchiveStaffWindow : MetroWindow
    {
        public ObservableCollection<double> Weeks { get; set; }
        public ObservableCollection<Staff> Roster { get; set; }

        public ArchiveStaffWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Weeks = StaffViewModel.GetRosterWeeks();
            Roster = new ObservableCollection<Staff>();
        }

        private void lstWeeks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Roster.Clear();
            foreach(Staff s in StaffViewModel.GetRoster((double)lstWeeks.SelectedValue))
            {
                Roster.Add(s);
            }
        }

        private async void btnClean_Click(object sender, RoutedEventArgs e)
        {
            MessageDialogResult choice = await this.ShowMessageAsync("",
                    "Are you sure you want to clean this Week?",
                    MessageDialogStyle.AffirmativeAndNegative);
            if (choice == MessageDialogResult.Affirmative)
            {
                //Reverse for loop so works
                for(int i = Roster.Count-1; i >= 0; i--)
                {
                    if (Roster[i].AppointedHours == 0 && Roster[i].AbsenceHours == 0 && Roster[i].UnsocialHours == 0)
                    {
                        StaffViewModel.DeleteRoster(Roster[i].Id, (double)lstWeeks.SelectedValue);
                        Roster.RemoveAt(i);
                    }
                }
            }
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            CalculateDialog calculateDialog = new CalculateDialog();
            calculateDialog.Owner = this;
            calculateDialog.ShowDialog();
        }

        private async void btnReportStaff_Click(object sender, RoutedEventArgs e)
        {
            ArchiveReportDialog archiveReportDialog = new ArchiveReportDialog();
            archiveReportDialog.DataContext = this;
            archiveReportDialog.Owner = this;
            archiveReportDialog.ShowDialog();
            if(archiveReportDialog.DialogResult == true)
            {
                List<Staff> Rosters = new List<Staff>();
                foreach (double week in archiveReportDialog.lstReportWeeks.SelectedItems)
                {
                    ObservableCollection<Staff> Temp = StaffViewModel.GetRoster(week);
                    foreach(Staff s in Temp)
                    {
                        bool match = false;
                        foreach(Staff r in Rosters)
                        {
                            if(r.Id == s.Id)
                            {
                                match = true;
                                r.ContractHours += s.ContractHours;
                                r.AppointedHours += s.AppointedHours;
                                r.AbsenceHours += s.AbsenceHours;
                                r.HolidayHours += s.HolidayHours;
                                r.UnsocialHours += s.UnsocialHours;
                                break;
                            }
                        }
                        if(match == false)
                        {
                            Rosters.Add(s);
                        }
                    }
                }
                if (Rosters.Count > 0)
                {
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Title = "Choose Report Save Location";
                    saveDialog.FileName = "Roster Report";
                    saveDialog.Filter = "PDF document (*.pdf)|*.pdf";
                    bool? result = saveDialog.ShowDialog();
            
                    if (result == true)
                    {
                        string start = archiveReportDialog.lstReportWeeks.SelectedItems[0].ToString();
                        string end = archiveReportDialog.lstReportWeeks.SelectedItems[archiveReportDialog.lstReportWeeks.SelectedItems.Count-1].ToString();
                        await CreateSessionReport(Rosters, start, end, saveDialog.FileName);
                    }
                }
            }
        }

        //Method for creating sessions pdf report
        private async Task CreateSessionReport(List<Staff> rosters, string start, string end, string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);

                //Document is A4 size with margins of 36 each side
                Document report = new Document(PageSize.A4, 10, 10, 10, 10);

                PdfWriter writer = PdfWriter.GetInstance(report, fs);

                //Table for displaying stock quantities with 2 being amount of columns
                PdfPTable rosterTable = new PdfPTable(9);
                rosterTable.SpacingBefore = 10f;
                rosterTable.WidthPercentage = 100;

                //Used for creating bold font
                Font title = FontFactory.GetFont(FontFactory.HELVETICA_BOLD);
                Font bold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8);
                bold.Color = BaseColor.WHITE;
                Font norm = FontFactory.GetFont(FontFactory.HELVETICA, 8);

                //Column titles with bold text for stock table
                string[] headers = { "Id", "Name", "Contract", "Appointed", "Absence", "Holiday", "Unsocial", "Neg", "CO"};

                foreach(string h in headers)
                {
                    rosterTable.AddCell(new PdfPCell(new Paragraph(h, bold))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BackgroundColor = BaseColor.GRAY
                    });
                }

                foreach (Staff s in rosters)
                {
                    rosterTable.AddCell(new Paragraph(s.Id.ToString(), norm));
                    rosterTable.AddCell(new Paragraph(s.Name, norm));
                    rosterTable.AddCell(new Paragraph(s.ContractHours.ToString(), norm));
                    rosterTable.AddCell(new Paragraph(s.AppointedHours.ToString(), norm));
                    rosterTable.AddCell(new Paragraph(s.AbsenceHours.ToString(), norm));
                    rosterTable.AddCell(new Paragraph(s.HolidayHours.ToString(), norm));
                    rosterTable.AddCell(new Paragraph(s.UnsocialHours.ToString(), norm));
                    double difference = s.ContractHours - (s.AppointedHours + s.AbsenceHours);
                    if (difference > 0)
                    {
                        rosterTable.AddCell(new Paragraph(difference.ToString(), norm));
                        rosterTable.AddCell(new Paragraph("0", norm));
                    }
                    else if (difference < 0)
                    {
                        rosterTable.AddCell(new Paragraph("0", norm));
                        rosterTable.AddCell(new Paragraph(Math.Abs(difference).ToString(), norm));
                    }
                    else
                    {
                        rosterTable.AddCell(new Paragraph("0", norm));
                        rosterTable.AddCell(new Paragraph("0", norm));
                    }
                }

                //Title used with date and time when created
                Paragraph titleParagraph = new Paragraph("Roster Report: Week " + start + " - Week " + end, title);
                titleParagraph.Alignment = Element.ALIGN_CENTER;

                //Creates and adds everything to pdf output
                report.Open();
                report.Add(titleParagraph);
                report.Add(rosterTable);
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
            this.DialogResult = false;
        }
        
        private string HoursString(double hours)
        {
            double trunc = Math.Truncate(hours);
            string hoursString = trunc.ToString();
            double remainder = hours - Math.Truncate(hours);
            if(remainder == 0.75)
            {
                hoursString += ":45";
            }
            else if (remainder == 0.5)
            {
                hoursString += ":30";
            }
            else if (remainder == 0.25)
            {
                hoursString += ":15";
            }
            else
            {
                hoursString += ":00";
            }
            return hoursString;
        }
    }
}
