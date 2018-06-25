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
    public partial class TeamViewWindow : MetroWindow
    {
        public ObservableCollection<TeamSite> TeamSites { get; set; }
        Team Selected;
        public TeamViewWindow(Team t)
        {
            InitializeComponent();
            this.DataContext = this;
            TeamSites = new ObservableCollection<TeamSite>();
            Selected = t;
            lblHeader.Content = Selected.Name;
            SV1Column.Header = Selected.SV1Name;
            DRI1Column.Header = Selected.DRI1Name;
            DRI2Column.Header = Selected.DRI2Name;
            RN1Column.Header = Selected.RN1Name;
            RN2Column.Header = Selected.RN2Name;
            RN3Column.Header = Selected.RN3Name;
            CCA1Column.Header = Selected.CCA1Name;
            CCA2Column.Header = Selected.CCA2Name;
            CCA3Column.Header = Selected.CCA3Name;
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
                TeamSites.Clear();
                List<string> Dates = new List<string>();
                for (DateTime dt = dateStart.SelectedDate.Value; dt <= dateEnd.SelectedDate.Value; dt = dt.AddDays(1))
                {
                    Dates.Add(dt.ToShortDateString());
                }

                if (Dates.Count > 0)
                {
                    foreach (string date in Dates)
                    {
                        TeamSite temp = new TeamSite()
                        {
                            Date = date,
                            Day = DateTime.Parse(date).DayOfWeek.ToString(),
                            SV1Name = Selected.SV1Name,
                            SV1Site = GetTeamSite(date, Selected.SV1Id),
                            DRI1Name = Selected.DRI1Name,
                            DRI1Site = GetTeamSite(date, Selected.DRI1Id),
                            DRI2Name = Selected.DRI2Name,
                            DRI2Site = GetTeamSite(date, Selected.DRI2Id),
                            RN1Name = Selected.RN1Name,
                            RN1Site = GetTeamSite(date, Selected.RN1Id),
                            RN2Name = Selected.RN2Name,
                            RN2Site = GetTeamSite(date, Selected.RN2Id),
                            RN3Name = Selected.RN3Name,
                            RN3Site = GetTeamSite(date, Selected.RN3Id),
                            CCA1Name = Selected.CCA1Name,
                            CCA1Site = GetTeamSite(date, Selected.CCA1Id),
                            CCA2Name = Selected.CCA2Name,
                            CCA2Site = GetTeamSite(date, Selected.CCA2Id),
                            CCA3Name = Selected.CCA3Name,
                            CCA3Site = GetTeamSite(date, Selected.CCA3Id)
                        };
                        TeamSites.Add(temp);
                    }
                }
            }
        }

        private string GetTeamSite(string date, int id)
        {
            string site = SessionViewModel.GetSessionSite(date, id.ToString());
            if(site == "")
            {
                site = AbsenceViewModel.GetStatus(id, DateTime.Parse(date));
                if (site.Equals("Okay"))
                {
                    site = "";
                }
            }
            return site;
        }

        private async void btnReportStaff_Click(object sender, RoutedEventArgs e)
        {
            if (TeamSites.Count > 0)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Title = "Choose Report Save Location";
                saveDialog.FileName = Selected.Name + " Report";
                saveDialog.Filter = "PDF document (*.pdf)|*.pdf";
                bool? result = saveDialog.ShowDialog();
                if (result == true)
                {
                    await CreateSessionReport(TeamSites, saveDialog.FileName);
                }
            }
            else
            {
                await this.ShowMessageAsync("", "No sessions found for that staff.");
            }
        }

        //Method for creating sessions pdf report
        private async Task CreateSessionReport(ObservableCollection<TeamSite> teamSites, string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);

                //Document is A4 size with margins of 36 each side
                Document report = new Document(PageSize.A4, 10, 10, 10, 10);

                PdfWriter writer = PdfWriter.GetInstance(report, fs);

                //Table for displaying stock quantities with 2 being amount of columns
                PdfPTable teamTable = new PdfPTable(11);
                teamTable.SpacingBefore = 10f;
                teamTable.WidthPercentage = 100;

                //Used for creating bold font
                Font title = FontFactory.GetFont(FontFactory.HELVETICA_BOLD);
                Font bold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8);
                bold.Color = BaseColor.WHITE;
                Font norm = FontFactory.GetFont(FontFactory.HELVETICA, 8);

                //Column titles with bold text for stock table
                string[] headers = { "Day", "Date", Selected.SV1Name, Selected.DRI1Name, Selected.DRI2Name, Selected.RN1Name,
                    Selected.RN2Name, Selected.RN3Name, Selected.CCA1Name, Selected.CCA2Name, Selected.CCA3Name };

                foreach (string h in headers)
                {
                    teamTable.AddCell(new PdfPCell(new Paragraph(h, bold))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        BackgroundColor = BaseColor.GRAY
                    });
                }

                foreach (TeamSite s in teamSites)
                {
                    teamTable.AddCell(new Paragraph(s.Day, norm));
                    teamTable.AddCell(new Paragraph(s.Date, norm));
                    teamTable.AddCell(new Paragraph(s.SV1Site, norm));
                    teamTable.AddCell(new Paragraph(s.DRI1Site, norm));
                    teamTable.AddCell(new Paragraph(s.DRI2Site, norm));
                    teamTable.AddCell(new Paragraph(s.RN1Site, norm));
                    teamTable.AddCell(new Paragraph(s.RN2Site, norm));
                    teamTable.AddCell(new Paragraph(s.RN3Site, norm));
                    teamTable.AddCell(new Paragraph(s.CCA1Site, norm));
                    teamTable.AddCell(new Paragraph(s.CCA2Site, norm));
                    teamTable.AddCell(new Paragraph(s.CCA3Site, norm));
                }

                //Title used with date and time when created
                Paragraph titleParagraph = new Paragraph(Selected.Name + " Report: " + teamSites[0].Date + 
                    " - " + teamSites[teamSites.Count - 1].Date, title);
                titleParagraph.Alignment = Element.ALIGN_CENTER;

                //Creates and adds everything to pdf output
                report.Open();
                report.Add(titleParagraph);
                report.Add(teamTable);
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
