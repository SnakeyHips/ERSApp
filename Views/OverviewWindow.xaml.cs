using System.Collections.ObjectModel;
using System.Windows;
using ERSApp.Model;
using ERSApp.ViewModel;
using MahApps.Metro.Controls;
using System.Windows.Controls;

namespace ERSApp.Views
{
    public partial class OverviewWindow : MetroWindow
    {
        public ObservableCollection<Session> OverviewSessions { get; set; }

        public OverviewWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            lblHeader.Content = CollectionManager.SelectedDate.DayOfWeek.ToString() + " - " +
                CollectionManager.SelectedDate.ToShortDateString();
            OverviewSessions = new ObservableCollection<Session>(SessionViewModel.Sessions);
            //OverviewSessions.Distinct();
            foreach(Session s in OverviewSessions)
            {
                cboFilter.Items.Add(s.Site);
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


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
