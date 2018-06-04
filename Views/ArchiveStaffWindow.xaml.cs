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

        //Delete method not used atm
        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstRoster.SelectedIndex > -1)
            {
                MessageDialogResult choice = await this.ShowMessageAsync("",
                    "Are you sure you want to delete this Archive?",
                    MessageDialogStyle.AffirmativeAndNegative);
                if (choice == MessageDialogResult.Affirmative)
                {
                    Staff Selected = (Staff)lstRoster.SelectedItem;
                    StaffViewModel.DeleteRoster(Selected.Id, (double)lstWeeks.SelectedValue);
                    //lstRoster.Items.Remove(Selected);
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
                                r.AppointedHours += s.ContractHours;
                                r.AbsenceHours += s.AbsenceHours;
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

        //MEthod for creating sessions pdf report
        private async Task CreateSessionReport(List<Staff> rosters, string start, string end, string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);

                //Document is A4 size with margins of 36 each side
                Document report = new Document(PageSize.A4, 10, 10, 10, 10);

                PdfWriter writer = PdfWriter.GetInstance(report, fs);

                //Table for displaying stock quantities with 2 being amount of columns
                PdfPTable RosterTable = new PdfPTable(8);
                RosterTable.SpacingBefore = 10f;
                RosterTable.WidthPercentage = 100;

                //Used for creating bold font
                Font header = FontFactory.GetFont(FontFactory.HELVETICA_BOLD);
                Font bold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8);
                Font norm = FontFactory.GetFont(FontFactory.HELVETICA, 8);

                //Column titles with bold text for stock table
                RosterTable.AddCell(new Paragraph("Id", bold));
                RosterTable.AddCell(new Paragraph("Name", bold));
                RosterTable.AddCell(new Paragraph("Contract", bold));
                RosterTable.AddCell(new Paragraph("Appointed", bold));
                RosterTable.AddCell(new Paragraph("Absence", bold));
                RosterTable.AddCell(new Paragraph("Unsocial", bold));
                RosterTable.AddCell(new Paragraph("Neg", bold));
                RosterTable.AddCell(new Paragraph("CO", bold));

                foreach (Staff s in rosters)
                {
                    RosterTable.AddCell(new Paragraph(s.Id.ToString(), norm));
                    RosterTable.AddCell(new Paragraph(s.Name, norm));
                    RosterTable.AddCell(new Paragraph(s.ContractHours.ToString(), norm));
                    RosterTable.AddCell(new Paragraph(s.AppointedHours.ToString(), norm));
                    RosterTable.AddCell(new Paragraph(s.AbsenceHours.ToString(), norm));
                    RosterTable.AddCell(new Paragraph(s.UnsocialHours.ToString(), norm));
                    double difference = s.ContractHours - (s.AppointedHours + s.AbsenceHours);
                    if(difference > 0)
                    {
                        RosterTable.AddCell(new Paragraph(difference.ToString(), norm));
                        RosterTable.AddCell(new Paragraph("0", norm));
                    }
                    else if (difference < 0)
                    {
                        RosterTable.AddCell(new Paragraph("0", norm));
                        RosterTable.AddCell(new Paragraph(Math.Abs(difference).ToString(), norm));
                    }
                    else
                    {
                        RosterTable.AddCell(new Paragraph("0", norm));
                        RosterTable.AddCell(new Paragraph("0", norm));
                    }
                }

                //Title used with date and time when created
                Paragraph titleParagraph = new Paragraph("Roster Report: Week " + start + " - Week " + end, header);
                titleParagraph.Alignment = Element.ALIGN_CENTER;

                //Creates and adds everything to pdf output
                report.Open();
                report.Add(titleParagraph);
                report.Add(RosterTable);
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
    }
}
