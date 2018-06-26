using System.Collections.ObjectModel;
using System.Windows;
using ERSApp.Models;
using ERSApp.ViewModels;
using MahApps.Metro.Controls;
using System.Windows.Controls;
using System.Collections.Generic;

namespace ERSApp.Views
{
    public partial class OverviewWindow : MetroWindow
    {
        public ObservableCollection<Session> OverviewSessions { get; set; }
        public HashSet<string> Sites { get; set; }

        public OverviewWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            lblHeader.Content = SelectedDate.Date.DayOfWeek.ToString() + " - " +
                SelectedDate.Date.ToShortDateString();
            OverviewSessions = new ObservableCollection<Session>(SessionViewModel.Sessions);
            Sites = new HashSet<string>();
            foreach(Session s in OverviewSessions)
            {
                Sites.Add(s.Site);
            }
        }

        private void cboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OverviewSessions.Clear();
            foreach(Session s in SessionViewModel.Sessions)
            {
                if (s.Site.Equals(cboFilter.SelectedValue))
                {
                    OverviewSessions.Add(s);
                }
            }
        }
        
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            cboFilter.SelectedItem = null;
            OverviewSessions.Clear();
            foreach(Session s in SessionViewModel.Sessions)
            {
                OverviewSessions.Add(s);
            }
        }
        
        //Method for viewing session note
        private void btnNote_Click(object sender, RoutedEventArgs e)
        {
            if(lstOverview.SelectedValue != null)
            {
                NoteDialog noteDialog = new NoteDialog();
                noteDialog.Owner = this;
                noteDialog.txtNote.Text = (string)lstOverview.SelectedValue;
                noteDialog.txtNote.IsEnabled = false;
                noteDialog.btnAccept.Visibility = Visibility.Collapsed;
                noteDialog.ShowDialog();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
